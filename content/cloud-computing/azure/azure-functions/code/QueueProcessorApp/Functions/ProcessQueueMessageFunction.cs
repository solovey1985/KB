using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using QueueProcessorApp.Services;

namespace QueueProcessorApp.Functions;

/// <summary>
/// Azure Function triggered by messages in Azure Queue Storage
/// Demonstrates:
/// - Queue Storage trigger
/// - Dependency Injection
/// - Structured logging
/// - Error handling and retries
/// - SQL Server integration
/// </summary>
public class ProcessQueueMessageFunction
{
    private readonly IQueueProcessorService _processorService;
    private readonly ILogger<ProcessQueueMessageFunction> _logger;

    /// <summary>
    /// Constructor with injected dependencies
    /// </summary>
    public ProcessQueueMessageFunction(
        IQueueProcessorService processorService,
        ILogger<ProcessQueueMessageFunction> logger)
    {
        _processorService = processorService;
        _logger = logger;
    }

    [Function("ProcessQueueMessage")]
    public async Task RunAsync(
        [QueueTrigger(
            "messages",
            Connection = "AzureWebJobsStorage")]
        QueueMessage message,
        FunctionContext context,
        CancellationToken cancellationToken)
    {
        var messageId = context.InvocationId;
        
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            { "MessageId", messageId },
            { "QueueTrigger", "messages" },
            { "Timestamp", DateTime.UtcNow }
        }))
        {
            try
            {
                _logger.LogInformation(
                    "Processing message from queue. MessageId: {MessageId}, Content: {Content}",
                    messageId,
                    message.MessageBody);

                // Process the message
                var result = await _processorService.ProcessMessageAsync(
                    message.MessageBody,
                    messageId,
                    cancellationToken);

                _logger.LogInformation(
                    "Successfully processed message {MessageId}. Status: {Status}",
                    messageId,
                    result.Status);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "Message processing was cancelled for {MessageId}", messageId);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid message format for {MessageId}. Message will be moved to poison queue.", messageId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message {MessageId}", messageId);
                
                // Rethrow to trigger retry policy or move to poison queue
                // The Azure Functions runtime will handle retry logic based on host.json configuration
                throw;
            }
        }
    }
}

/// <summary>
/// Helper class to deserialize queue messages
/// </summary>
public class QueueMessage
{
    public string MessageBody { get; set; } = string.Empty;
}
