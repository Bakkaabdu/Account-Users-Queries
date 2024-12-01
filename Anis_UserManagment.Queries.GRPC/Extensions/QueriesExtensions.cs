using anis_UserManagment.Queries.Application.Features.Queries.GetUsers;
using Anis_UserManagment.Queries.GRPC.Protos;

namespace Anis_UserManagment.Queries.GRPC.Extensions
{
    public static class QueriesExtensions
    {
        public static GetUsersQuery ToQuery(this GetUsersRequest request)
    => new(Guid.Parse(request.AccountId));
    }
}
