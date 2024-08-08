using Application.DTOS;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IAsyncBaseRepository<TModel, TModelDtos, TList>
    where TModel : class
    where TList : class
    {
        Task<TModel> RetrieveRecordAsync(Expression<Func<TModel, bool>> filter, params Expression<Func<TModel, object>>[] includes);

        Task<List<TModel>> RetrieveAllRecordAsync(Expression<Func<TModel, bool>> filter);

        Task<TModel> SaveRecordAsync(TModel record);

        Task DeleteRecordAsync(Expression<Func<TModel, bool>> filter);

        Task SaveChangesAsync();

        Task<List<TList>> SpRetrieveListAsync(string spName, List<SPParameters> parameters = null);

        Task<List<TList>> QueryBuilderRetrieveListtAsync(string spName, ListParameters listParameters, List<SPParameters> parameters = null);
        Task<string> Validate(TModelDtos model);
    }
}
