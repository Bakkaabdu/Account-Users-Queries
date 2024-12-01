using Anis_UserManagment.Queries.GRPC.Protos.Clients;
using Anis_UserManagment.Queries.GRPC.Protos;
using anis_UserManagment.Queries.Infra.Constants;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using anis_UserManagment.Queries.Domain.Events.DataTypes;
using Anis_UserManagment.Queries.GRPC.Extensions;

namespace Anis_UserManagment.Queries.GRPC.Services
{
    public class RebuildService : AccountUsersRebuilder.AccountUsersRebuilderBase
    {
        private readonly UsersEventsHistory.UsersEventsHistoryClient _eventHistoryClient;
        private readonly IMediator _mediator;
        private readonly ILogger<RebuildService> _logger;

        public RebuildService(UsersEventsHistory.UsersEventsHistoryClient eventHistoryClient, IMediator mediator, ILogger<RebuildService> logger)
        {
            _eventHistoryClient = eventHistoryClient;
            _mediator = mediator;
            _logger = logger;
        }

        public async override Task<Empty> BuildAccountUsers(BuildRequest request, ServerCallContext context)
        {
            for (int i = request.Page; i > 0; i++)
            {
                var getEvent = new GetEventsRequest()
                {
                    CurrentPage = i,
                    PageSize = request.Size,
                };

                _logger.LogWarning("Start Handling page {page}", i);

                var response = await _eventHistoryClient.GetEventsAsync(getEvent);

                if (response.Events.Count > 0)
                {
                    await HandleResponseAsync(response);

                    _logger.LogWarning("End Handling page {page}", i);

                }
                else
                {
                    _logger.LogWarning("End page {page}", i);
                    break;
                }
            }

            return new Empty();
        }

        private async Task HandleResponseAsync(Response response)
        {
            foreach (var @event in response.Events)
            {
                switch (@event.Type)
                {
                    case EventType.UserAssignedToAccount:
                        await _mediator.Send(@event.ToEvent<UserAssignedData>());
                        break;

                    case EventType.UserDeletedFromAccount:
                        await _mediator.Send(@event.ToEvent<UserUnAssignedData>());
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(@event.Type, "Event Type out of range in rebuild service");
                }
            }
        }
    }
}
