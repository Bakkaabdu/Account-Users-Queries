using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Application.Features.Queries.GetUsers
{
    public record GetUsersQuery(Guid AccountId) : IRequest<GetUsersDto>;
}
