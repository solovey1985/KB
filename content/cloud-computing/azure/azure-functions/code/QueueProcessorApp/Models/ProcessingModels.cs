namespace QueueProcessorApp.Models;

/// <summary>
/// Represents a message received from Azure Queue Storage
/// </summary>
public class QueueMessage
{
    public string MessageId { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    public int RetryCount { get; set; } = 0;
}

/// <summary>
/// Entity representing a processed item in SQL Server
/// </summary>
public class ProcessedItem
{
    public int Id { get; set; }
    public string MessageId { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public ProcessingStatus Status { get; set; }
    public DateTime ProcessedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? ErrorMessage { get; set; }
    public int RetryAttempts { get; set; } = 0;
}

/// <summary>
/// Enumeration for processing status
/// </summary>
public enum ProcessingStatus
{
    Pending = 0,
    Processing = 1,
    Completed = 2,
    Failed = 3,
    Skipped = 4
}

/// <summary>
/// Audit log for tracking function executions
/// </summary>
public class ExecutionLog
{
    public int Id { get; set; }
    public string FunctionName { get; set; } = string.Empty;
    public string MessageId { get; set; } = string.Empty;
    public LogLevel Level { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Exception { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public long DurationMs { get; set; }
}

public enum LogLevel
{
    Trace = 0,
    Debug = 1,
    Information = 2,
    Warning = 3,
    Error = 4,
    Critical = 5,
    None = 6
}
