using Application.Contracts.Persistence;
using Application.DTOS;
using Common;
using Domain.Models;
using Infrastructure.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EventFeatures.UserAccountEvents.Queries
{
    public class LoginAccountQueryRequest : IRequest<BaseResponse<UserAccountDtos>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginAccountQueryRequestHandler : IRequestHandler<LoginAccountQueryRequest, BaseResponse<UserAccountDtos>>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        public LoginAccountQueryRequestHandler(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        public async Task<BaseResponse<UserAccountDtos>> Handle(LoginAccountQueryRequest request, CancellationToken cancellationToken)
        {
         
            var response = new BaseResponse<UserAccountDtos>();
            try
            {
                var useraccount = await _userAccountRepository.RetrieveRecordAsync(p => p.Username == request.Username);
                if(useraccount == null)
                {
                    response.Messages.Add("No account registered.");
                    response.IsSuccess = false;
                    return response;
                }
                if(useraccount.IsActive == false)
                {
                    response.Messages.Add("Account is inactive.");
                    response.IsSuccess = false;
                    return response;
                }

                var validate = new CryptographicSignature().VerifySignature(request.Username, request.Password, useraccount.Password);

                if (!validate)
                {
                    response.Messages.Add("Invalid username or password.");
                    response.IsSuccess = false;
                    await _userAccountRepository.LoginAudit(new AuditLoginDtos { Username = useraccount.Username, IsLoginSuccess = false });
                    return response;
                }

                await _userAccountRepository.LoginAudit(new AuditLoginDtos { Username = useraccount.Username, IsLoginSuccess = true });
                response.Data = Mapper.Map<UserAccount,UserAccountDtos>(useraccount);
                response.Data.UserTypeText = await _userAccountRepository.GetUserType((int)response.Data.UserType);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.IsSuccess = false;
            }

            return response;
        }
    }
}
