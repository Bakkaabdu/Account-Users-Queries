using anis_UserManagment.Queries.Application.Contracts.Repositories;
using anis_UserManagment.Queries.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Application.Features.Queries.GetUsers
{
    public class GetUsersHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUsersQuery, GetUsersDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<GetUsersDto> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.Accounts.GetAccountUsersAsync(query);

            return account == null ? throw new AppException(ExceptionStatusCode.NotFound, "Account Not Found") : account;
        }
    }
}
