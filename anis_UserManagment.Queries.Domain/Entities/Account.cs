using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Domain.Entities
{
    public class Account
    {
        private Account(Guid id, int sequence)
        {

        }

        public Guid Id { get; set; }
        public int Sequence { get; set; }
        public List<User> Users { get; set; }

        public static Account Create(Guid id, int sequence)
        {
            var account = new Account(id, sequence);
            return account;
        }

        public void InCreamentSequence() 
            => Sequence++;
    }
}
