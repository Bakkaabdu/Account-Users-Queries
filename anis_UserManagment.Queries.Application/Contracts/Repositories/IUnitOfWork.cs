using anis_UserManagment.Queries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Application.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<User> Users { get; }

        IUserRepository Accounts { get; }
        Task SaveChangesAsync();
    }
}
