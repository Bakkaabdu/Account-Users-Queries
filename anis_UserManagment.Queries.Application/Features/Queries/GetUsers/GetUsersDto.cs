using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Application.Features.Queries.GetUsers
{
    public class GetUsersDto
    {
        public Guid AccountId { get; set; }
        public List<Guid> Users { get; set; } = new List<Guid>();
    }
}
