using anis_UserManagment.Queries.Application.Features.Queries.GetUsers;
using Anis_UserManagment.Queries.GRPC.Protos;

namespace Anis_UserManagment.Queries.GRPC.Extensions
{
    public static class ResponseExtension
    {
        public static GetUsersResponse ToResponse(this GetUsersDto dto)
             => new()
            {
                AccountId = dto.AccountId.ToString(),
                Users =
                {
                    dto.Users.Select(s=>s.ToString())
                }
            };
    }
}
