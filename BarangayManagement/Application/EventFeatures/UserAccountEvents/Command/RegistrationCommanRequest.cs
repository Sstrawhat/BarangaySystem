using Application.Contracts.Persistence;
using Application.DTOS;
using Common;
using Domain.Models;
using Infrastructure.Security;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;

namespace Application.EventFeatures.UserAccountEvents.Command
{
    public class RegistrationCommanRequest : IRequest<BaseResponse<UserAccountDtos>>
    {
        public UserAccountDtos userAccountDtos { get; set; }
    }

    public class RegistrationCommandRequestHandler : IRequestHandler<RegistrationCommanRequest, BaseResponse<UserAccountDtos>>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        public RegistrationCommandRequestHandler(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        public async Task<BaseResponse<UserAccountDtos>> Handle(RegistrationCommanRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<UserAccountDtos>();

            try
            {
              //Prevent insert when one is not executed properly
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var useraccount = request.userAccountDtos;

                    var validateUserAccount = await _userAccountRepository.Validate(useraccount);

                    if (validateUserAccount != "")
                    {
                        response.Messages.Add(validateUserAccount);
                        response.IsSuccess = false;
                    }
                    else
                    {
                        var generatedPassword = new CryptographicSignature().SignData(useraccount.Username, useraccount.Password);

                        useraccount.Password = generatedPassword;
                        useraccount.IsActive = false;

                        var mappeduserAccount = Mapper.Map<UserAccountDtos, UserAccount>(useraccount);

                        await _userAccountRepository.SaveRecordAsync(mappeduserAccount);
                        await _userAccountRepository.SaveChangesAsync();

                        response.Messages.Add("Account has been register but not verified. please contact your administrator.");
                        response.IsSuccess = true;

                        scope.Complete();
                    }

                }

            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
            }

            return response;
        }
    }
}


