using anis_UserManagment.Queries.Application.Features.Queries.GetUsers;
using anis_UserManagment.Queries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Application.Contracts.Repositories
{
    public interface IUserRepository : IAsyncRepository<Account>
    {
        Task<GetUsersDto?> GetAccountUsersAsync(GetUsersQuery query);
    }
}
