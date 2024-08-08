using Application.Contracts.Persistence;
using Application.DTOS;
using Common;
using Infrastructure.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EventFeatures.UserAccountEvents.Queries
{
    public class UserAccountListQueryRequest : IRequest<BaseResponse<List<UserAccountListDtos>>>
    {
        public ListParameters ListParameters { get; set; }
        public List<SPParameters> SPParameters { get; set; }
    }

    public class UserAccountListQueryRequestHandler : IRequestHandler<UserAccountListQueryRequest, BaseResponse<List<UserAccountListDtos>>>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        public UserAccountListQueryRequestHandler(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        public async Task<BaseResponse<List<UserAccountListDtos>>> Handle(UserAccountListQueryRequest request, CancellationToken cancellationToken)
        {
         
            var response = new BaseResponse<List<UserAccountListDtos>>();

            try
            {
                var list = await _userAccountRepository.QueryBuilderRetrieveListtAsync("security/UserAccountList",request.ListParameters,request.SPParameters);
                response.Data = list;
                response.TotalCount = list.FirstOrDefault().TotalCount;

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
