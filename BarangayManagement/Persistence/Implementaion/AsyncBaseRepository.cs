using Common;
using Persistence.AppContext;
using EntityFrameworkCore.RawSQLExtensions.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Application.Contracts.Persistence;
using Application.DTOS;

namespace Persistence.Implementaion
{
    public class AsyncBaseRepository<TModel, TModelDtos, TList> : IAsyncBaseRepository<TModel, TModelDtos, TList>
     where TModel : class
     where TModelDtos : class
     where TList : class
    {
        private readonly ApplicationDBContext _context;

        public AsyncBaseRepository(ApplicationDBContext context)
        {
            this._context = context;
        }

        public async Task<TModel> RetrieveRecordAsync(Expression<Func<TModel, bool>> filter, params Expression<Func<TModel, object>>[] includes)
        {
            var query = _context.Set<TModel>().AsQueryable().AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var result = await query.FirstOrDefaultAsync(filter);

            return result;
        }

        public async Task<TModel> SaveRecordAsync(TModel record)
        {
            _context.Set<TModel>().Update(record);
            return record;
        }

        public async Task DeleteRecordAsync(Expression<Func<TModel, bool>> filter)
        {
            var result = await this.RetrieveRecordAsync(filter);
            _context.Set<TModel>().Remove(result);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

        }

        public async Task<List<TList>> SpRetrieveListAsync(string spName, List<SPParameters> parameters = null)
        {
            var arrayParameters = new List<string>();
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    arrayParameters.Add($"@{parameter.Parameter}='{parameter.Value}'");
                }
            }

            var list = await _context.Database.SqlQuery<TList>($"EXEC {spName} {((arrayParameters.Count() > 0) ? string.Join(",", arrayParameters) : "")}").ToListAsync();

            return (List<TList>)list;
        }

        public async Task<List<TList>> QueryBuilderRetrieveListtAsync(string spName, ListParameters listParameters, List<SPParameters> parameters = null)
        {
            var sqlQuery = "";
            var arrayParameters = new List<SqlParameter>();
            if(parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    sqlQuery += $" @{parameter.Parameter}='{parameter.Value}',";
                    arrayParameters.Add(new SqlParameter(parameter.Parameter, parameter.Value));
                }
            }

            var queryFile = File.ReadAllText("../UI.BarangayPortal/Queries/" + spName + ".txt");

            sqlQuery = $"""
                            DECLARE @PageNumber INT = {listParameters.PageNumber};
                            DECLARE @PageSize INT = {listParameters.RowCount};
                            DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
                            DECLARE @FetchCount INT = @PageSize;

                            {queryFile}

                            ORDER BY {listParameters.SortField} {listParameters.SortType}
                            OFFSET @Offset ROWS
                            FETCH NEXT @FetchCount ROWS ONLY

                        """;




            var list = await _context.Database.SqlQuery<TList>(sqlQuery, arrayParameters.ToArray()).ToListAsync();

            return (List<TList>)list;
        }

        public async Task<List<TModel>> RetrieveAllRecordAsync(Expression<Func<TModel, bool>> filter)
        {
            return await _context.Set<TModel>().AsNoTracking().Where(filter).ToListAsync();
        }

        public async Task<string> Validate(TModelDtos model)
        {
            var errorMessages = new List<string>();

            Type modelClass = typeof(TModelDtos);

            var properties = modelClass.GetProperties();

            foreach (var property in properties)
            {
                // Example: Check if a property has a required attribute
                var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
                var stringLengthAttribute = property.GetCustomAttribute<StringLengthAttribute>();
                var duplicateAttribute = property.GetCustomAttribute<CustomAttributeDuplicate>();

                if (requiredAttribute != null)
                {
                    // Example: Perform validation based on the attribute's presence
                    var propertyValue = property.GetValue(model);

                    if (propertyValue == null || string.IsNullOrWhiteSpace(propertyValue.ToString()))
                    {
                        // Add an error message if validation fails
                        errorMessages.Add($"{property.Name} is required.");
                    }
                }

                if (stringLengthAttribute != null)
                {
                    var propertyValue = (string)property.GetValue(model);

                    if (propertyValue != null && propertyValue.Length > stringLengthAttribute.MaximumLength)
                    {

                        errorMessages.Add($"{property.Name} must be at most {stringLengthAttribute.MaximumLength} characters long.");
                    }
                }

                if (duplicateAttribute != null)


                {
                    var propertyValue = (string)property.GetValue(model);

                    var type = model.GetType().GetProperty(duplicateAttribute.Key);
                    var keyValue = type.GetValue(model);

                    if (propertyValue != null && !string.IsNullOrWhiteSpace(propertyValue.ToString()))
                    {
                        var sqlQuery = $" SELECT {duplicateAttribute.Field} FROM {duplicateAttribute.Table} WHERE {duplicateAttribute.Field} = '{propertyValue}' AND {duplicateAttribute.Key} != '{keyValue}'";
                        var list = await _context.Database.SqlQuery<string>(sqlQuery).ToListAsync();
                        if (list.Count > 0)
                            errorMessages.Add($"{propertyValue} is already taken.");

                    }
                }


            }


            return (errorMessages.Count() > 0) ? string.Join(",", errorMessages) : "";
        }

    }
}
