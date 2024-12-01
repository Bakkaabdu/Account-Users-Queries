using Anis_UserManagment.Queries.GRPC.Protos;
using MediatR;

namespace Anis_UserManagment.Queries.GRPC.Services
{
    public class UsersQueriesService : ManageUsersQueries.ManageUsersQueriesBase
    {
        private readonly IMediator _mediator;

        public UsersQueriesService(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
