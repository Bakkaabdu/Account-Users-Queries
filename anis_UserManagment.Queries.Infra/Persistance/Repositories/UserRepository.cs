using anis_UserManagment.Queries.Application.Contracts.Repositories;
using anis_UserManagment.Queries.Application.Features.Queries.GetUsers;
using anis_UserManagment.Queries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Infra.Persistance.Repositories
{
    public class UserRepository : AsyncRepository<Account>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async override Task<Account?> FindAsync(Guid id, bool includeRelated = false)
        {
            if (includeRelated)
                return await _appDbContext.Accounts.Include(a => a.Users).FirstOrDefaultAsync(a => a.Id == id);

            return await base.FindAsync(id, includeRelated);
        }

        public async Task<GetUsersDto?> GetAccountUsersAsync(GetUsersQuery query)
        {
            return await _appDbContext.Accounts.Where(a => a.Id == query.AccountId)
                            .Select(a => new GetUsersDto
                            {
                                AccountId = a.Id,
                                Users = a.Users.Select(u => u.Id).ToList()
                            }).FirstOrDefaultAsync();

        }
    }
}
