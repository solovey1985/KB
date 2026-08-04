# Azure Functions - Complete Guide

## Table of Contents
1. [Getting Started](#getting-started)
2. [Function Structure](#function-structure)
3. [Triggers & Bindings](#triggers--bindings)
4. [Advanced Patterns](#advanced-patterns)
5. [Performance & Optimization](#performance--optimization)
6. [Deployment](#deployment)
7. [Monitoring & Debugging](#monitoring--debugging)

## Getting Started

### Prerequisites
- Azure subscription
- Azure Functions Core Tools
- .NET SDK (for C#)
- VS Code with Azure Functions extension

### Create Your First Function

```bash
# Create function project
func init MyFunctionApp --dotnet

# Create new function
cd MyFunctionApp
func new --name HttpTriggerExample --template "HTTP trigger"

# Run locally
func start
```

### Project Structure

```
MyFunctionApp/
├── local.settings.json    # Local configuration
├── host.json              # Runtime settings
├── .gitignore
├── HttpTriggerExample/
│   ├── function.json      # Function metadata
│   └── HttpTriggerExample.cs
└── Properties/
    └── launchSettings.json
```

## Function Structure

### Basic HTTP Function

```csharp
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

public class HttpTriggerFunction
{
    private readonly ILogger<HttpTriggerFunction> _logger;

    public HttpTriggerFunction(ILogger<HttpTriggerFunction> logger)
    {
        _logger = logger;
    }

    [Function("HttpTriggerFunction")]
    public HttpResponseData Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("HTTP trigger function processed a request.");

        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString("Welcome to Azure Functions!");

        return response;
    }
}
```

### Function Metadata (function.json)

```json
{
  "generatedBy": "Microsoft.Azure.Functions.Worker.Grpc",
  "configurationSource": "attributes",
  "bindings": [
    {
      "type": "httpTrigger",
      "name": "req",
      "direction": "in",
      "authLevel": "function",
      "methods": ["get", "post"]
    },
    {
      "type": "http",
      "name": "$return",
      "direction": "out"
    }
  ]
}
```

## Triggers & Bindings

### HTTP Trigger

**REST API endpoint for synchronous operations**

```csharp
[Function("CreateUser")]
public async Task<HttpResponseData> CreateUser(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")]
    HttpRequestData req,
    [Cosmos("CosmosDB", "users", CreateIfNotExists = true)]
    IAsyncCollector<User> users)
{
    var body = await req.ReadAsStringAsync();
    var user = JsonConvert.DeserializeObject<User>(body);
    
    await users.AddAsync(user);
    
    var response = req.CreateResponse(HttpStatusCode.Created);
    await response.WriteAsJsonAsync(user);
    return response;
}
```

### Timer Trigger

**Scheduled execution**

```csharp
[Function("ProcessTimerJob")]
public void ProcessTimerJob(
    [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer,
    ILogger log)
{
    log.LogInformation($"Timer trigger function executed at: {DateTime.Now}");
    
    if (myTimer.IsPastDue)
    {
        log.LogWarning("Timer is running behind schedule");
    }
}
```

**CRON Expression Format**: `{second} {minute} {hour} {day} {month} {day-of-week}`

### Queue Storage Trigger

**Process messages from Azure Queue Storage**

```csharp
[Function("ProcessQueueMessage")]
public async Task ProcessQueue(
    [QueueTrigger("myqueue")] string message,
    [Blob("container/{id}")] CloudBlockBlob blob,
    ILogger log)
{
    log.LogInformation($"Processing message: {message}");
    
    // Process message
    var content = $"Processed: {message}";
    await blob.UploadTextAsync(content);
}
```

### Blob Storage Trigger

**React to blob creation or updates**

```csharp
[Function("BlobTriggerFunction")]
public void ProcessBlob(
    [BlobTrigger("container/{name}")] Stream myBlob,
    string name,
    ILogger log)
{
    log.LogInformation($"Processing blob: {name}");
    
    // Process blob
    using var reader = new StreamReader(myBlob);
    var content = reader.ReadToEnd();
}
```

### Service Bus Trigger

**Process messages from Service Bus queue or topic**

```csharp
[Function("ProcessServiceBusMessage")]
public async Task ProcessServiceBus(
    [ServiceBusTrigger("myqueue", Connection = "ServiceBusConnection")]
    ServiceBusReceivedMessage message,
    ServiceBusMessageActions messageActions,
    ILogger log)
{
    log.LogInformation($"Message: {message.Body}");
    
    try
    {
        // Process message
        await messageActions.CompleteMessageAsync(message);
    }
    catch (Exception ex)
    {
        log.LogError($"Error: {ex.Message}");
        await messageActions.AbandonMessageAsync(message);
    }
}
```

### Cosmos DB Trigger

**React to document changes**

```csharp
[Function("CosmosDbTrigger")]
public void ProcessCosmosChanges(
    [CosmosDBTrigger(
        databaseName: "db",
        collectionName: "items",
        ConnectionStringSetting = "CosmosDBConnection",
        LeaseCollectionName = "leases")]
    IReadOnlyList<dynamic> changes,
    ILogger log)
{
    foreach (var item in changes)
    {
        log.LogInformation($"Modified document id: {item.id}");
    }
}
```

### Input Bindings (Read Data)

```csharp
[Function("GetUserByIdWithBinding")]
public async Task<HttpResponseData> GetUser(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")]
    HttpRequestData req,
    [Cosmos("db", "users", Id = "{id}", PartitionKey = "{id}")]
    User? user)
{
    if (user == null)
    {
        return req.CreateResponse(HttpStatusCode.NotFound);
    }
    
    var response = req.CreateResponse(HttpStatusCode.OK);
    await response.WriteAsJsonAsync(user);
    return response;
}
```

## Advanced Patterns

### Dependency Injection

**Startup configuration**

```csharp
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetry();
        services.AddHttpClient<IUserService, UserService>();
        services.AddScoped<IRepository, CosmosRepository>();
    })
    .Build();

host.Run();
```

### Error Handling & Retry

```csharp
[Function("ResilientFunction")]
public async Task ProcessWithRetry(
    [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
    ILogger log)
{
    var policy = Policy
        .Handle<HttpRequestException>()
        .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: attempt => 
                TimeSpan.FromSeconds(Math.Pow(2, attempt)),
            onRetry: (outcome, timespan, retryCount, context) =>
            {
                log.LogWarning($"Retry {retryCount} after {timespan.TotalSeconds}s");
            });
    
    var response = await policy.ExecuteAsync(() => 
        MakeExternalCall());
}
```

### Durable Functions Orchestration

```csharp
[Function("OrderProcessingOrchestrator")]
public static async Task RunOrchestrator(
    [OrchestrationTrigger] TaskOrchestrationContext context)
{
    var order = context.GetInput<Order>();
    
    try
    {
        // Validate order
        await context.CallActivityAsync("ValidateOrder", order);
        
        // Process payment
        var paymentResult = await context.CallActivityAsync<PaymentResult>(
            "ProcessPayment", order.Amount);
        
        // Send confirmation
        await context.CallActivityAsync("SendConfirmation", order);
    }
    catch (Exception ex)
    {
        await context.CallActivityAsync("NotifyError", ex.Message);
        throw;
    }
}

[Function("ValidateOrder")]
public static void ValidateOrder([ActivityTrigger] Order order, ILogger log)
{
    log.LogInformation($"Validating order: {order.Id}");
    // Validation logic
}
```

## Performance & Optimization

### Connection Pooling

```csharp
public class HttpClientService
{
    private static readonly HttpClient _httpClient = new();

    public async Task<string> GetDataAsync(string url)
    {
        return await _httpClient.GetStringAsync(url);
    }
}
```

### Batching Operations

```csharp
[Function("BatchProcessor")]
public async Task ProcessBatch(
    [QueueTrigger("myqueue")] IAsyncEnumerable<string> messages,
    [Cosmos("db", "items", CreateIfNotExists = true)]
    IAsyncCollector<Item> items)
{
    var batch = new List<Item>();
    
    await foreach (var message in messages)
    {
        batch.Add(new Item { Data = message });
        
        if (batch.Count >= 100)
        {
            await ProcessBatch(batch, items);
            batch.Clear();
        }
    }
    
    if (batch.Count > 0)
    {
        await ProcessBatch(batch, items);
    }
}
```

### Caching Strategy

```csharp
[Function("CachedDataFunction")]
public async Task<HttpResponseData> GetData(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
    IDistributedCache cache,
    ILogger log)
{
    const string cacheKey = "mydata";
    var cachedData = await cache.GetStringAsync(cacheKey);
    
    if (cachedData == null)
    {
        cachedData = await FetchData();
        await cache.SetStringAsync(cacheKey, cachedData, 
            new DistributedCacheEntryOptions 
            { 
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) 
            });
    }
    
    var response = req.CreateResponse(HttpStatusCode.OK);
    await response.WriteStringAsync(cachedData);
    return response;
}
```

## Deployment

### Deploy to Azure

```bash
# Publish function app
func azure functionapp publish MyFunctionApp

# Publish with specific settings
func azure functionapp publish MyFunctionApp \
    --build remote \
    --runtime dotnet \
    --runtime-version 8.0
```

### Configuration (local.settings.json)

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "CosmosDBConnection": "AccountEndpoint=https://...;",
    "ServiceBusConnection": "Endpoint=sb://...;"
  }
}
```

## Monitoring & Debugging

### Application Insights

```csharp
[Function("MonitoredFunction")]
public void Run(
    [HttpTrigger] HttpRequestData req,
    FunctionContext context,
    ILogger log)
{
    var logger = context.GetLogger("MyFunction");
    
    using (logger.BeginScope(new Dictionary<string, object>
    {
        { "RequestId", req.HttpContext.TraceIdentifier },
        { "UserId", "user123" }
    }))
    {
        logger.LogInformation("Function execution started");
        logger.LogError("An error occurred");
    }
}
```

### Local Debugging

```bash
# Enable local debugging
func start --debug VSCode
```

Set breakpoints in VS Code and attach debugger.

### Health Checks

```csharp
[Function("HealthCheck")]
public async Task<HttpResponseData> HealthCheck(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")]
    HttpRequestData req)
{
    var isHealthy = await CheckDependencies();
    
    var response = req.CreateResponse(
        isHealthy ? HttpStatusCode.OK : HttpStatusCode.ServiceUnavailable);
    
    await response.WriteAsJsonAsync(new 
    { 
        status = isHealthy ? "healthy" : "unhealthy",
        timestamp = DateTime.UtcNow
    });
    
    return response;
}
```

## Summary

Azure Functions provides a powerful serverless platform for event-driven applications. Key takeaways:

- Start with Consumption plan for cost-effectiveness
- Use triggers and bindings for declarative integration
- Implement proper error handling and retries
- Monitor with Application Insights
- Consider Durable Functions for complex workflows
- Keep functions focused and stateless
