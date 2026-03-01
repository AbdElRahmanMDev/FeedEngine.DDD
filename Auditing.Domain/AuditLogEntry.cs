namespace Auditing.Domain
{
    public class AuditLogEntry
    {
        public Guid Id { get; set; }
        public string Action { get; set; } = default!;
        public string Data { get; set; } = default!;
        public DateTime CreatedAtUtc { get; set; }
    }
}
