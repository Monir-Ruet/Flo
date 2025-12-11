using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Flo.Core.Shared.Enums.Events;

namespace Flo.Core.Shared.Entities.Events;

public class Event
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string EventType { get; set; }

    [Required]
    public string Payload { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; }

    public int? RetryCount { get; set; }

    public string Status { get; set; } = nameof(EventStatus.Pending);

    public string? Error { get; set; }

    public void MarkAsProcessed()
    {
        Status = nameof(EventStatus.Processed);
        ProcessedAt = DateTime.UtcNow;
        Error = null;
    }

    public void MarkAsFailed(string error)
    {
        Status = nameof(EventStatus.Failed);
        Error = error;
        RetryCount++;
        ProcessedAt = DateTime.UtcNow;
    }

    public void MarkForRetry()
    {
        Status = nameof(EventStatus.Pending);
        Error = null;
        RetryCount ??= 0;
        RetryCount++;
    }
}