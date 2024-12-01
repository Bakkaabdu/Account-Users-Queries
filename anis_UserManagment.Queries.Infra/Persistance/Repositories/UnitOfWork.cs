using anis_UserManagment.Queries.Application.Contracts.Repositories;
using anis_UserManagment.Queries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Infra.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        private IUserRepository? _accounts;

        public IUserRepository Accounts
        {
            get
            {
                if (_accounts != null)
                    return _accounts;

                return _accounts = new UserRepository(_appDbContext);
            }
        }

        private IAsyncRepository<User>? _users;

        public IAsyncRepository<User> Users
        {
            get
            {
                if (_users != null)
                    return _users;

                return _users = new AsyncRepository<User>(_appDbContext);
            }
        }


        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
