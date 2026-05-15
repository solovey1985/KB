# Azure Queue Processor Function App - Boilerplate Example

A complete, production-ready Azure Functions example demonstrating best practices for building serverless applications with .NET 8.

## Features

✅ **Azure Queue Storage Integration** - Trigger functions from queue messages  
✅ **SQL Server Integration** - Entity Framework Core with SQL Server database  
✅ **Dependency Injection** - Full DI/IoC container support  
✅ **Structured Logging** - Application Insights integration and console logging  
✅ **Environment Configuration** - Development, Staging, and Production configs  
✅ **CI/CD Pipeline** - GitHub Actions workflows for automated deployment  
✅ **Error Handling** - Comprehensive error handling and retry logic  
✅ **Local Development** - Azure Storage Emulator and SQL LocalDB support  

## Project Structure

```
QueueProcessorApp/
├── Functions/
│   └── ProcessQueueMessageFunction.cs      # Queue trigger function
├── Models/
│   └── ProcessingModels.cs                 # Data models and enums
├── Data/
│   └── ApplicationDbContext.cs             # Entity Framework context
├── Services/
│   ├── IServices.cs                        # Service interfaces
│   └── QueueProcessorService.cs            # Business logic
├── Configuration/
│   └── SettingsConfiguration.cs            # Settings classes
├── Program.cs                              # Startup & DI configuration
├── host.json                               # Azure Functions runtime config
├── local.settings.json                     # Local secrets (not in git)
├── appsettings.json                        # Base configuration
├── appsettings.Development.json            # Dev environment config
├── appsettings.Staging.json                # Staging environment config
└── appsettings.Production.json             # Production environment config
```

## Prerequisites

- .NET 8 SDK or later
- Azure Storage Emulator (Azurite) or Azure Storage Account
- SQL Server or SQL LocalDB
- Azure Functions Core Tools
- Azure Subscription (for deployment)

## Local Setup

### 1. Clone and Restore

```bash
cd QueueProcessorApp
dotnet restore
```

### 2. Configure Local Development

Create/update `local.settings.json`:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "APPINSIGHTS_INSTRUMENTATIONKEY": ""
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=QueueProcessorDb;Trusted_Connection=true;"
  },
  "FunctionSettings": {
    "MaxRetries": 3,
    "ProcessingTimeout": 30000
  }
}
```

### 3. Create Database

```bash
# Install Entity Framework CLI tools
dotnet tool install --global dotnet-ef

# Create initial migration
dotnet ef migrations add Initial

# Create database
dotnet ef database update
```

### 4. Run Azure Storage Emulator

```bash
# Using Azurite (recommended)
npm install -g azurite
azurite --silent --location ./data

# Or use Azure Storage Emulator
# Download from: https://docs.microsoft.com/azure/storage/common/storage-use-emulator
```

### 5. Start the Function App

```bash
func start
```

The functions will be available at:
- HTTP: `http://localhost:7071/`
- Queue trigger: Listens to `messages` queue in local storage

## Testing Queue Messages

### Using Azure Storage Explorer

1. Download Azure Storage Explorer
2. Connect to local storage emulator
3. Create a queue named `messages`
4. Add a test message with JSON content

### Using CLI

```bash
# Using Azure CLI
az storage message put --queue-name messages --message-text '{"orderId":"123","amount":99.99}' --connection-string "UseDevelopmentStorage=true"
```

### Using PowerShell

```powershell
# Create a queue and add message
$context = New-AzureStorageContext -Local
$queue = Get-AzureStorageQueue -Name messages -Context $context
$msg = New-Object Microsoft.Azure.Storage.Queue.CloudQueueMessage '{"orderId":"123","amount":99.99}'
$queue.CloudQueue.AddMessageAsync($msg)
```

## Debugging

See [DEBUGGING.md](./DEBUGGING.md) for detailed debugging instructions.

### Quick Debug in VS Code

1. Open the project in VS Code
2. Press `F5` to start debugging
3. Set breakpoints in your code
4. Trigger messages to hit breakpoints

## Configuration Management

### Environment-Specific Settings

The application automatically loads configuration based on the environment:

```
appsettings.json           # Base settings (all environments)
appsettings.Development.json   # Development overrides
appsettings.Staging.json       # Staging overrides  
appsettings.Production.json    # Production overrides
```

**Key differences:**

| Setting | Dev | Staging | Production |
|---------|-----|---------|------------|
| Log Level | Debug | Information | Warning |
| DB Timeout | 600s | 300s | 300s |
| Max Retries | 5 | 3 | 5 |
| Batch Size | 5 | 10 | 100 |

## Deployment

### Prerequisites for Deployment

1. Azure subscription and resource group
2. Azure Function App created
3. Azure SQL Database configured
4. GitHub repository secrets configured

### GitHub Secrets Required

Configure these in Settings → Secrets and Variables → Actions:

```
AZURE_CREDENTIALS_DEV       # Azure service principal JSON
AZURE_CREDENTIALS_PROD      # Azure service principal JSON
AZURE_FUNCTIONAPP_PUBLISH_PROFILE_DEV
AZURE_FUNCTIONAPP_PUBLISH_PROFILE_PROD
SLACK_WEBHOOK              # Optional, for notifications
```

### Create Azure Service Principal

```bash
az ad sp create-for-rbac --name "GithubDeployment" --role contributor --scopes /subscriptions/{SUBSCRIPTION_ID}
```

### Deploy to Azure

**Automatic (via Git):**
```bash
git push origin develop  # Triggers dev deployment
git push origin main     # Triggers prod deployment
```

**Manual (Azure CLI):**
```bash
az functionapp deployment source config-zip \
  --resource-group myResourceGroup \
  --name queue-processor-prod \
  --src-path deploy.zip
```

## Monitoring

### Application Insights

View logs and metrics in Azure Portal:
1. Go to Application Insights resource
2. View Live Metrics Stream
3. Query logs with KQL

### Example Queries

```kusto
// Get recent errors
traces
| where severityLevel == 3
| order by timestamp desc
| limit 100

// Function execution times
customMetrics
| where name == "ProcessMessageDuration"
| summarize AvgDuration=avg(value), MaxDuration=max(value) by bin(timestamp, 1m)

// Failed message processing
customEvents
| where name == "MessageProcessingFailed"
| summarize FailureCount=count() by tostring(customDimensions.Status)
```

### Local Logs

Check the console output or Azure Storage Emulator logs for local debugging.

## Key Implementation Details

### Dependency Injection

```csharp
// Services are registered in Program.cs
services.AddScoped<IQueueProcessorService, QueueProcessorService>();
services.AddScoped<IDataService, DataService>();
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
```

### Queue Trigger

```csharp
[Function("ProcessQueueMessage")]
public async Task RunAsync(
    [QueueTrigger("messages")] QueueMessage message,
    FunctionContext context,
    CancellationToken cancellationToken)
{
    // Message automatically deserialized
    // Function context provides invocation metadata
}
```

### SQL Server Integration

```csharp
// EF Core provides type-safe data access
var item = new ProcessedItem 
{ 
    MessageId = messageId,
    Data = jsonContent,
    Status = ProcessingStatus.Processing
};
await _dataService.CreateProcessedItemAsync(item);
```

### Structured Logging

```csharp
using (_logger.BeginScope(new Dictionary<string, object>
{
    { "MessageId", messageId },
    { "Timestamp", DateTime.UtcNow }
}))
{
    _logger.LogInformation("Processing message {MessageId}", messageId);
}
```

## Error Handling & Retries

Queue triggers automatically retry failed messages based on `host.json`:
- Retry on exception: Yes (configurable)
- Max attempts: Configurable via queue metadata
- Poison queue: Failed messages after max retries

```json
{
  "extensions": {
    "queues": {
      "maxPollingInterval": "00:00:02",
      "visibilityTimeout": "00:00:30",
      "batchSize": 16,
      "maxDequeueCount": 3,
      "newBatchThreshold": 8
    }
  }
}
```

## Performance Optimization

### Batch Processing

Tune batch size per environment in appsettings:
- **Development**: 5 messages
- **Staging**: 10 messages  
- **Production**: 100 messages

### Connection Pooling

EF Core connection pooling is enabled by default in .NET 8.

### Timeout Settings

- Function timeout: 5 minutes (configurable)
- DB command timeout: 300 seconds (production)
- Processing timeout: 30 seconds (configurable)

## Troubleshooting

### Queue Messages Not Processing

1. Verify queue name matches trigger configuration
2. Check storage connection string in `local.settings.json`
3. Ensure Azure Storage Emulator is running
4. Check function logs for exceptions

### Database Connection Issues

1. Verify connection string format
2. Ensure SQL Server/LocalDB is running
3. Check firewall rules (for cloud deployment)
4. Verify authentication credentials

### Deployment Failures

1. Check GitHub Actions logs
2. Verify Azure credentials are valid
3. Ensure Function App has required permissions
4. Check for resource quota limits

## Next Steps

- Add unit tests (xUnit/NUnit)
- Implement Durable Functions for long-running tasks
- Add Event Hub triggers for higher throughput
- Integrate with Azure Service Bus
- Add Azure App Configuration for centralized settings
- Implement API versioning
- Add custom telemetry
- Set up alerts and auto-remediation

## References

- [Azure Functions Documentation](https://docs.microsoft.com/azure/azure-functions/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [GitHub Actions](https://github.com/features/actions)
- [Application Insights](https://docs.microsoft.com/azure/azure-monitor/app/app-insights-overview)

## License

MIT
