using Application.Contracts.Persistence;
using Application.DTOS;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IUserAccountRepository : IAsyncBaseRepository<UserAccount, UserAccountDtos, UserAccountListDtos>
    {
        Task<string> GetUserType(int value);
        Task LoginAudit(AuditLoginDtos model);
    }
}
