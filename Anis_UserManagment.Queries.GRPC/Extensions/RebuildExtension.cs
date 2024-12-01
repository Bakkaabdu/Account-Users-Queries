using anis_UserManagment.Queries.Domain.Events;
using Anis_UserManagment.Queries.GRPC.Protos.Clients;
using Newtonsoft.Json;

namespace Anis_UserManagment.Queries.GRPC.Extensions
{
    public static class RebuildExtension
    {
        public static Event<T> ToEvent<T>(this EventMessage body)
            => new()
    {
        AggregateId = Guid.Parse(body.AggregateId),
        Sequence = body.Sequence,
        UserId = body.UserId,
        Type = body.Type,
        Data = JsonConvert.DeserializeObject<T>(body.Data) ?? throw new ArgumentNullException("Data is not null here"),
        DateTime = body.DateTime.ToDateTime(),
        Version = body.Version
    };
    }
}
