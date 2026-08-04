# Local Development & Debugging Guide

This guide covers debugging, testing, and running the Azure Functions locally.

## Prerequisites

### Required Tools

```bash
# .NET 8 SDK
# Download from: https://dotnet.microsoft.com/download

# Azure Functions Core Tools
npm install -g azure-functions-core-tools@4 --unsafe-perm true

# SQL Server / LocalDB
# Download from: https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb

# Azure Storage Emulator (Azurite)
npm install -g azurite

# Optional: Azure Storage Explorer
# Download from: https://azure.microsoft.com/features/storage-explorer/
```

### VS Code Extensions

Install these extensions for better development experience:

```
ms-dotnettools.csharp              # C# Language Support
ms-vscode.makefile-tools           # Makefile support
ms-azuretools.vscode-azurefunctions # Azure Functions Tools
ms-mssql.mssql                      # SQL Server support
```

## Starting Local Development

### Step 1: Start Azure Storage Emulator

```bash
# Terminal 1: Start Azurite (local Azure Storage emulator)
azurite --silent --location ./local-storage
```

This creates local Azure Queue Storage and Blob Storage at:
- Queue: `http://127.0.0.1:10001`
- Blob: `http://127.0.0.1:10000`

### Step 2: Create Local Database

```bash
# Terminal 2: Navigate to project
cd QueueProcessorApp

# Install EF Core CLI tools (one-time)
dotnet tool install --global dotnet-ef

# Create migration (if needed)
dotnet ef migrations add Initial

# Create/update database
dotnet ef database update
```

Database is created at: `(localdb)\mssqllocaldb` with name `QueueProcessorDb`

### Step 3: Start Function App

```bash
# Terminal 2: Start Azure Functions
func start
```

Expected output:
```
Azure Functions Core Tools
Core Tools Version:       4.x.x Commit hash: xxx

Functions:

  ProcessQueueMessage: queueTrigger

For detailed output, run func with --verbose flag.
Listening on http://0.0.0.0:7071
Application started. Press Ctrl+C to exit.
```

## Debugging in VS Code

### Configuration

Create `.vscode/launch.json`:

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Attach to Azure Functions",
            "type": "coreclr",
            "request": "attach",
            "processId": "${command:pickProcess}",
            "preLaunchTask": "func: host start",
            "problemMatcher": []
        },
        {
            "name": ".NET: Debug Unit Tests",
            "type": "coreclr",
            "request": "launch",
            "program": "${workspaceFolder}/QueueProcessorApp/bin/Debug/net8.0/QueueProcessorApp.dll",
            "args": [],
            "cwd": "${workspaceFolder}/QueueProcessorApp",
            "stopAtEntry": false,
            "console": "internalConsole"
        }
    ]
}
```

### Debugging Steps

1. Set breakpoints in code (click line number in VS Code)
2. Press `F5` to start debugging
3. Select "Attach to Azure Functions"
4. Trigger messages (see Testing section below)
5. Execution pauses at breakpoints

### Debug Console

View debug output:
- Press `Ctrl+Shift+Y` to open Debug Console
- View logs and variable values
- Execute expressions in debug context

## Testing

### Unit Test Setup

Create `QueueProcessorApp.Tests/ProcessQueueServiceTests.cs`:

```csharp
using Xunit;
using Moq;
using QueueProcessorApp.Services;
using QueueProcessorApp.Data;
using Microsoft.Extensions.Logging;

namespace QueueProcessorApp.Tests;

public class QueueProcessorServiceTests
{
    [Fact]
    public async Task ProcessMessageAsync_WithValidMessage_ReturnsProcessedItem()
    {
        // Arrange
        var mockDataService = new Mock<IDataService>();
        var mockLogger = new Mock<ILogger<QueueProcessorService>>();
        var service = new QueueProcessorService(mockDataService.Object, mockLogger.Object);

        var messageContent = "{\"orderId\":\"123\",\"amount\":99.99}";
        var messageId = "msg-001";

        // Act
        var result = await service.ProcessMessageAsync(messageContent, messageId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("msg-001", result.MessageId);
    }
}
```

Run tests:

```bash
cd QueueProcessorApp
dotnet test
```

### Integration Testing

Test with actual local storage:

```csharp
[Fact]
public async Task QueueTrigger_WithMessage_ProcessesSuccessfully()
{
    // Setup
    var context = new FunctionsHostBuilder()
        .WithConfiguration(GetTestConfiguration())
        .Build();

    // Act
    await context.CallFunctionAsync("ProcessQueueMessage", 
        new QueueMessage { MessageBody = testJson });

    // Assert
    // Verify database entry created
}
```

### Manual Testing with Azure Storage Explorer

1. Open Azure Storage Explorer
2. Connect to Local emulator (Azurite)
3. Create queue: `messages`
4. Add message with content:
   ```json
   {"orderId":"123","amount":99.99}
   ```
5. Check function logs for processing

### Testing with PowerShell

```powershell
# Install Azure Storage module
Install-Module -Name Az.Storage -Force

# Create storage context for local emulator
$context = New-AzureStorageContext -Local

# Create queue
New-AzureStorageQueue -Name "messages" -Context $context

# Create and add message
$queue = Get-AzureStorageQueue -Name "messages" -Context $context
$msg = New-Object Microsoft.Azure.Storage.Queue.CloudQueueMessage '{"test":"data"}'
$queue.CloudQueue.AddMessageAsync($msg).Wait()

# View queue messages
Get-AzureStorageQueueMessage -Queue $queue.CloudQueue
```

### Testing with Azure CLI

```bash
# Create storage context
az storage queue create --name messages --connection-string "UseDevelopmentStorage=true"

# Add message
az storage message put \
  --queue-name messages \
  --message-text '{"orderId":"123","amount":99.99}' \
  --connection-string "UseDevelopmentStorage=true"

# View messages
az storage message peek \
  --queue-name messages \
  --connection-string "UseDevelopmentStorage=true"
```

## Logging & Troubleshooting

### View Logs

**VS Code Integrated Terminal:**
```bash
# Run with verbose output
func start --verbose
```

**Application Insights (Local):**
```bash
# View Application Insights emulator
# Logs appear in console output
```

### Common Issues

#### Queue Not Processing Messages

```bash
# Check if queue exists
az storage queue list --connection-string "UseDevelopmentStorage=true"

# Verify storage emulator is running
lsof -i :10000  # Port for blob storage
lsof -i :10001  # Port for queue storage
```

#### Database Connection Failed

```bash
# Check if LocalDB is running
sqllocaldb info mssqllocaldb

# Restart LocalDB
sqllocaldb stop mssqllocaldb
sqllocaldb start mssqllocaldb

# Verify connection string
sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT @@VERSION"
```

#### Function Timeout

Increase timeout in `host.json`:
```json
{
  "functionTimeout": "00:10:00"
}
```

#### Dependency Injection Issues

Verify registrations in `Program.cs`:
```csharp
# Add these lines to debug:
services.AddLogging(config => config.AddConsole());
```

### Debug SQL Queries

Enable EF Core SQL logging in development:

```csharp
// In Program.cs
services.AddDbContext<ApplicationDbContext>(options =>
    options
        .UseSqlServer(connectionString)
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
);
```

## Performance Testing

### Load Testing with k6

Create `load-test.js`:

```javascript
import http from 'k6/http';
import { check } from 'k6';

export let options = {
  stages: [
    { duration: '30s', target: 20 },
    { duration: '1m', target: 50 },
    { duration: '20s', target: 0 },
  ],
};

export default function() {
  let url = 'http://localhost:7071/api/ProcessQueueMessage';
  let payload = JSON.stringify({
    orderId: 'order-' + __VU + '-' + __ITER,
    amount: Math.random() * 1000,
  });

  let params = {
    headers: { 'Content-Type': 'application/json' },
  };

  let res = http.post(url, payload, params);
  check(res, {
    'status is 200': (r) => r.status === 200,
  });
}
```

Run load test:
```bash
k6 run load-test.js
```

## Database Migrations

### Create New Migration

```bash
dotnet ef migrations add AddNewColumn
```

### Update Database

```bash
dotnet ef database update
```

### Revert Migration

```bash
# Revert to specific migration
dotnet ef database update PreviousMigration

# Remove migration (local development only)
dotnet ef migrations remove
```

## Environment Variables

### Set Local Environment Variables

**.env file** (create in project root):
```
AZURE_STORAGE_CONNECTION_STRING=UseDevelopmentStorage=true
DATABASE_CONNECTION_STRING=Server=(localdb)\mssqllocaldb;Database=QueueProcessorDb;
```

**PowerShell:**
```powershell
$env:AZURE_STORAGE_CONNECTION_STRING = "UseDevelopmentStorage=true"
$env:DATABASE_CONNECTION_STRING = "Server=(localdb)\mssqllocaldb;Database=QueueProcessorDb;"
```

**Linux/Mac:**
```bash
export AZURE_STORAGE_CONNECTION_STRING="UseDevelopmentStorage=true"
export DATABASE_CONNECTION_STRING="Server=(localdb)\mssqllocaldb;Database=QueueProcessorDb;"
```

## Continuous Local Testing

### Auto-reload on Changes

```bash
# Install dotnet-watch
dotnet tool install --global dotnet-watch

# Start with auto-reload
dotnet watch --project QueueProcessorApp run
```

## Next Steps

- Set up pre-commit hooks for code quality
- Configure code coverage reporting
- Implement contract testing with Pact
- Set up distributed tracing with OpenTelemetry
