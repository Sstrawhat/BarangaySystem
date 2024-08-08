using Application.Contracts.Persistence;
using Common;
using Persistence.AppContext;
using EntityFrameworkCore.RawSQLExtensions.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Application.DTOS;

namespace Persistence.Implementaion
{
    public class UserAccountRepository : AsyncBaseRepository<UserAccount,UserAccountDtos, UserAccountListDtos>, IUserAccountRepository
    {
        private ApplicationDBContext _context;
        public UserAccountRepository(ApplicationDBContext ApplicationDBContext) : base(ApplicationDBContext)
        {
            _context = ApplicationDBContext;
            
        }

        public async Task<string> GetUserType(int value)
        {
            var usertype = await _context.Database.SqlQuery<string>($"SELECT DisplayField FROM system.EnumValues WHERE SourceName = 'UserType' AND Value = {value}").FirstOrDefaultAsync();

            return usertype;
        }

        public async Task LoginAudit(AuditLoginDtos model)
        {
            _context.Set<AuditLogin>().Add(new AuditLogin { Username = model.Username, IsLoginSuccess = model.IsLoginSuccess, AccessDate = DateTime.Now});
            await _context.SaveChangesAsync();
        }
    }
}
