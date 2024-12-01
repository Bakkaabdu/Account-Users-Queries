using anis_UserManagment.Queries.Application.Contracts.Repositories;
using anis_UserManagment.Queries.Domain.Events;
using anis_UserManagment.Queries.Domain.Events.DataTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Application.Features.Events
{
    public class UserUnAssignedHandler : IRequestHandler<Event<UserUnAssignedData>, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserUnAssignedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(Event<UserUnAssignedData> request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.Accounts.FindAsync(request.AggregateId, true);

            if (account == null)
                return false;

            if (account.Sequence == request.Sequence - 1)
            {
                account.InCreamentSequence();

                var user = await _unitOfWork.Users.FindAsync(request.Data.UserId);

                if (user != null)
                    await _unitOfWork.Users.RemoveAsync(user);

                await _unitOfWork.SaveChangesAsync();
            }

            return account.Sequence >= request.Sequence;
        }
    }
}
