using MediatR;


namespace anis_UserManagment.Queries.Domain.Events
{
    public class Event<T> : IRequest<bool>
    {
        public Guid AggregateId { get; set; }
        public int Sequence { get; set; }
        public string? UserId { get; set; }
        public required string Type { get; set; }
        public required T Data { get; set; }
        public DateTime DateTime { get; set; }
        public int Version { get; set; }
    }
}
