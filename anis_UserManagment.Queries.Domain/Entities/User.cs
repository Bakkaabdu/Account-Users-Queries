using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Domain.Entities
{
    public class User
    {
        public User(Guid id, Guid accountId)
        {
            Id = id;
            AccountId = accountId;
        }
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}
