namespace BuildingBlocks.Infrastructure.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; set; }
        public DateTime OccurredOnUtc { get; set; }

        // Assembly-qualified name of the event type (so we can deserialize)
        public string Type { get; set; } = default!;

        // JSON payload of the event
        public string Content { get; set; } = default!;

        public DateTime? ProcessedOnUtc { get; set; }
        public string? Error { get; set; }
    }
}
