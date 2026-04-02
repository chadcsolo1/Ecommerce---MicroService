namespace Order.Entities
{
    public class OutboxMessage : EntityBase
    {
        public string Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string CorrelationId { get; set; } = string.Empty;
        public DateTime OccurredOn { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public bool isProcessed => ProcessedOn.HasValue;
        public string? Error { get; set; }
    }
}
