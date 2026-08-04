namespace QueueProcessorApp.Configuration;

/// <summary>
/// Configuration settings for Azure Function
/// </summary>
public class FunctionSettings
{
    public int MaxRetries { get; set; } = 3;
    public int ProcessingTimeout { get; set; } = 30000; // milliseconds
    public int BatchSize { get; set; } = 10;
}

/// <summary>
/// Configuration settings for database operations
/// </summary>
public class DatabaseSettings
{
    public int CommandTimeout { get; set; } = 300; // seconds
    public bool EnableDetailedErrors { get; set; } = false;
}
