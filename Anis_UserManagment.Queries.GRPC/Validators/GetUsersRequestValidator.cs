using Anis_UserManagment.Queries.GRPC.Protos;
using FluentValidation;

namespace Anis_UserManagment.Queries.GRPC.Validators
{
    public class GetUsersRequestValidator : AbstractValidator<GetUsersRequest>
    {
        public GetUsersRequestValidator()
        {
            RuleFor(r => r.AccountId)
               .Must(accountId => Guid.TryParse(accountId, out _))
                    .NotEqual(Guid.Empty.ToString());
        }
    }
}
