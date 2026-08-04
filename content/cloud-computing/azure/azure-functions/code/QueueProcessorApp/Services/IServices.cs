using QueueProcessorApp.Models;

namespace QueueProcessorApp.Services;

/// <summary>
/// Service interface for processing queue messages
/// </summary>
public interface IQueueProcessorService
{
    Task<ProcessedItem> ProcessMessageAsync(string messageContent, string messageId, CancellationToken cancellationToken);
    Task<bool> ValidateMessageAsync(string messageContent);
}

/// <summary>
/// Service interface for data operations
/// </summary>
public interface IDataService
{
    Task<ProcessedItem?> GetProcessedItemAsync(string messageId);
    Task<ProcessedItem> CreateProcessedItemAsync(ProcessedItem item);
    Task<ProcessedItem> UpdateProcessedItemAsync(ProcessedItem item);
    Task LogExecutionAsync(string functionName, string messageId, string message, long durationMs, Exception? ex = null);
}
