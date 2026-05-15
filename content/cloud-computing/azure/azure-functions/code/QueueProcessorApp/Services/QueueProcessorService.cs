using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QueueProcessorApp.Data;
using QueueProcessorApp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace QueueProcessorApp.Services;

/// <summary>
/// Implementation of queue message processing service
/// </summary>
public class QueueProcessorService : IQueueProcessorService
{
    private readonly IDataService _dataService;
    private readonly ILogger<QueueProcessorService> _logger;

    public QueueProcessorService(
        IDataService dataService,
        ILogger<QueueProcessorService> logger)
    {
        _dataService = dataService;
        _logger = logger;
    }

    public async Task<ProcessedItem> ProcessMessageAsync(
        string messageContent,
        string messageId,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            _logger.LogInformation("Processing message {MessageId}", messageId);

            // Validate message
            if (!await ValidateMessageAsync(messageContent))
            {
                _logger.LogWarning("Message {MessageId} failed validation", messageId);
                throw new InvalidOperationException("Message validation failed");
            }

            // Check if already processed (idempotency)
            var existingItem = await _dataService.GetProcessedItemAsync(messageId);
            if (existingItem is not null)
            {
                _logger.LogInformation("Message {MessageId} already processed", messageId);
                return existingItem;
            }

            // Parse and process message
            var data = ParseMessageData(messageContent);

            // Create processed item
            var processedItem = new ProcessedItem
            {
                MessageId = messageId,
                Data = JsonSerializer.Serialize(data),
                Status = ProcessingStatus.Processing,
                ProcessedAt = DateTime.UtcNow,
                RetryAttempts = 0
            };

            // Save to database
            var savedItem = await _dataService.CreateProcessedItemAsync(processedItem);
            
            // Simulate processing logic
            await SimulateProcessingAsync(data, cancellationToken);

            // Update status
            savedItem.Status = ProcessingStatus.Completed;
            savedItem.UpdatedAt = DateTime.UtcNow;
            savedItem = await _dataService.UpdateProcessedItemAsync(savedItem);

            stopwatch.Stop();
            await _dataService.LogExecutionAsync(
                nameof(ProcessMessageAsync),
                messageId,
                "Message processed successfully",
                stopwatch.ElapsedMilliseconds);

            _logger.LogInformation(
                "Message {MessageId} processed successfully in {ElapsedMs}ms",
                messageId,
                stopwatch.ElapsedMilliseconds);

            return savedItem;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Error processing message {MessageId}", messageId);
            await _dataService.LogExecutionAsync(
                nameof(ProcessMessageAsync),
                messageId,
                "Message processing failed",
                stopwatch.ElapsedMilliseconds,
                ex);

            throw;
        }
    }

    public async Task<bool> ValidateMessageAsync(string messageContent)
    {
        if (string.IsNullOrWhiteSpace(messageContent))
        {
            _logger.LogWarning("Message content is empty");
            return false;
        }

        try
        {
            // Try parsing as JSON
            JsonDocument.Parse(messageContent);
            _logger.LogDebug("Message validation passed");
            return true;
        }
        catch (JsonException ex)
        {
            _logger.LogWarning(ex, "Message is not valid JSON");
            return false;
        }
    }

    private static Dictionary<string, object> ParseMessageData(string messageContent)
    {
        using var doc = JsonDocument.Parse(messageContent);
        var result = new Dictionary<string, object>();

        foreach (var property in doc.RootElement.EnumerateObject())
        {
            result[property.Name] = property.Value.GetRawText();
        }

        return result;
    }

    private async Task SimulateProcessingAsync(Dictionary<string, object> data, CancellationToken cancellationToken)
    {
        // Simulate business logic processing
        // This could be calling an external API, performing calculations, etc.
        await Task.Delay(100, cancellationToken);
    }
}

/// <summary>
/// Implementation of data service for database operations
/// </summary>
public class DataService : IDataService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DataService> _logger;

    public DataService(ApplicationDbContext context, ILogger<DataService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProcessedItem?> GetProcessedItemAsync(string messageId)
    {
        try
        {
            return await _context.ProcessedItems
                .FirstOrDefaultAsync(x => x.MessageId == messageId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving processed item for message {MessageId}", messageId);
            throw;
        }
    }

    public async Task<ProcessedItem> CreateProcessedItemAsync(ProcessedItem item)
    {
        try
        {
            _context.ProcessedItems.Add(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created processed item {Id} for message {MessageId}", item.Id, item.MessageId);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating processed item for message {MessageId}", item.MessageId);
            throw;
        }
    }

    public async Task<ProcessedItem> UpdateProcessedItemAsync(ProcessedItem item)
    {
        try
        {
            _context.ProcessedItems.Update(item);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Updated processed item {Id} to status {Status}", item.Id, item.Status);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating processed item {Id}", item.Id);
            throw;
        }
    }

    public async Task LogExecutionAsync(
        string functionName,
        string messageId,
        string message,
        long durationMs,
        Exception? ex = null)
    {
        try
        {
            var logEntry = new ExecutionLog
            {
                FunctionName = functionName,
                MessageId = messageId,
                Message = message,
                Level = ex is not null ? Models.LogLevel.Error : Models.LogLevel.Information,
                Exception = ex?.ToString(),
                DurationMs = durationMs,
                CreatedAt = DateTime.UtcNow
            };

            _context.ExecutionLogs.Add(logEntry);
            await _context.SaveChangesAsync();
        }
        catch (Exception logEx)
        {
            _logger.LogError(logEx, "Error writing execution log");
            // Don't throw - logging should not crash the function
        }
    }
}
