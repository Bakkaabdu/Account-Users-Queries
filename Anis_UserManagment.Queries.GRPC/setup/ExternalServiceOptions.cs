using System.ComponentModel.DataAnnotations;

namespace Anis_UserManagment.Queries.GRPC.setup
{
    public class ExternalServiceOptions
    {
        public const string ExternalServices = "ExternalServices";

        [Required]
        [DataType(DataType.Url)]
        public required string AccountUsersCommandUrl { get; init; }
    }
}
