# Azure Functions Boilerplate - Complete Solution Index

Complete, production-ready Azure Functions example with all enterprise requirements integrated.

## 📁 Project Structure

```
code/
├── QueueProcessorApp/                  # Main function app project
│   ├── .gitignore                      # Git ignore rules
│   ├── Program.cs                      # Startup & Dependency Injection
│   ├── host.json                       # Azure Functions runtime config
│   ├── QueueProcessorApp.csproj        # Project file with all dependencies
│   │
│   ├── Functions/
│   │   └── ProcessQueueMessageFunction.cs    # Queue trigger function
│   │
│   ├── Models/
│   │   └── ProcessingModels.cs              # Data models & enums
│   │       ├── QueueMessage
│   │       ├── ProcessedItem
│   │       ├── ExecutionLog
│   │       └── ProcessingStatus enum
│   │
│   ├── Data/
│   │   └── ApplicationDbContext.cs           # Entity Framework Core context
│   │       ├── DbSet<ProcessedItem>
│   │       └── DbSet<ExecutionLog>
│   │
│   ├── Services/
│   │   ├── IServices.cs                     # Service interfaces
│   │   │   ├── IQueueProcessorService
│   │   │   └── IDataService
│   │   └── QueueProcessorService.cs         # Service implementations
│   │       ├── ProcessMessageAsync
│   │       ├── ValidateMessageAsync
│   │       └── DataService (DB operations)
│   │
│   ├── Configuration/
│   │   └── SettingsConfiguration.cs         # Configuration classes
│   │       ├── FunctionSettings
│   │       └── DatabaseSettings
│   │
│   ├── Properties/
│   │   └── launchSettings.json              # Launch profiles
│   │
│   ├── Configuration Files
│   │   ├── local.settings.json              # Local development secrets
│   │   ├── appsettings.json                 # Base settings
│   │   ├── appsettings.Development.json     # Dev environment
│   │   ├── appsettings.Staging.json         # Staging environment
│   │   └── appsettings.Production.json      # Production environment
│   │
│   └── Database/
│       └── Migrations/                      # EF Core migrations (generated)
│
├── .github/
│   └── workflows/
│       ├── ci-build.yml                     # CI pipeline
│       ├── deploy-to-dev.yml                # Dev deployment
│       └── deploy-to-prod.yml               # Prod deployment
│
├── Documentation/
│   ├── README.md                            # Getting started guide
│   ├── DEBUGGING.md                         # Local debugging guide
│   ├── CONFIGURATION.md                     # Environment configuration
│   └── INDEX.md                             # This file
│
└── .env.example                             # Environment variables template
```

## ✨ Key Features Implemented

### 1. **Queue Storage Trigger** ✅
- Processes messages from Azure Queue Storage
- Automatic retry and poison queue handling
- Configurable max retry attempts
- Message deduplication

**File:** [ProcessQueueMessageFunction.cs](QueueProcessorApp/Functions/ProcessQueueMessageFunction.cs)

### 2. **SQL Server Integration** ✅
- Entity Framework Core with SQL Server
- Automatic migrations support
- Connection pooling
- Configurable timeout per environment

**Files:**
- [ApplicationDbContext.cs](QueueProcessorApp/Data/ApplicationDbContext.cs)
- [ProcessingModels.cs](QueueProcessorApp/Models/ProcessingModels.cs)

### 3. **Structured Logging** ✅
- Console logging for local development
- Application Insights integration
- Structured logging with correlation IDs
- Environment-based log levels
- Execution audit trails

**Features:**
- LogInformation, LogWarning, LogError support
- Scope tracking with message IDs
- Duration tracking for performance monitoring
- Exception logging with stack traces

### 4. **Dependency Injection** ✅
- Full DI/IoC container in Program.cs
- Service registration per environment
- Scoped database contexts
- HttpClient factory
- Configuration binding

**Configuration:** [Program.cs](QueueProcessorApp/Program.cs)

**Usage:**
```csharp
services.AddScoped<IQueueProcessorService, QueueProcessorService>();
services.AddScoped<IDataService, DataService>();
services.AddDbContext<ApplicationDbContext>();
services.Configure<FunctionSettings>(configuration.GetSection("FunctionSettings"));
```

### 5. **Environment Configuration** ✅
Three complete environment profiles:

| Aspect | Development | Staging | Production |
|--------|-------------|---------|------------|
| **Connection** | LocalDB | Azure SQL | Azure SQL |
| **Log Level** | Debug | Information | Warning |
| **Max Retries** | 5 | 3 | 5 |
| **DB Timeout** | 600s | 300s | 300s |
| **Batch Size** | 5 | 10 | 100 |

**Files:**
- [appsettings.json](QueueProcessorApp/appsettings.json) - Base
- [appsettings.Development.json](QueueProcessorApp/appsettings.Development.json)
- [appsettings.Staging.json](QueueProcessorApp/appsettings.Staging.json)
- [appsettings.Production.json](QueueProcessorApp/appsettings.Production.json)

### 6. **CI/CD Deployment Pipeline** ✅
Three GitHub Actions workflows:

**CI Pipeline:** [ci-build.yml](.github/workflows/ci-build.yml)
- Run on every PR
- Build, test, code analysis
- Artifact upload

**Dev Deployment:** [deploy-to-dev.yml](.github/workflows/deploy-to-dev.yml)
- Trigger: Push to `develop` branch
- Build → Test → Deploy to Dev
- Health check post-deployment

**Prod Deployment:** [deploy-to-prod.yml](.github/workflows/deploy-to-prod.yml)
- Trigger: Push to `main` branch
- Manual approval
- Build → Test → Deploy to Prod
- Slack notifications

### 7. **Local Development & Debugging** ✅
Complete setup guide with:
- Azure Storage Emulator (Azurite) integration
- SQL LocalDB setup
- VS Code debugging configuration
- Unit test examples
- Integration test patterns

**Guide:** [DEBUGGING.md](DEBUGGING.md)

### 8. **Error Handling & Retry Logic** ✅
- Exception handling in functions
- Automatic retry policies
- Poison queue support
- Idempotency checks
- Graceful degradation

## 🚀 Quick Start

### Local Setup (5 minutes)

```bash
# 1. Clone and restore
cd QueueProcessorApp
dotnet restore

# 2. Create database
dotnet ef database update

# 3. Start storage emulator (in separate terminal)
azurite --silent --location ./data

# 4. Start function app
func start
```

### Testing Messages

```bash
# Using Azure CLI
az storage message put \
  --queue-name messages \
  --message-text '{"orderId":"123","amount":99.99}' \
  --connection-string "UseDevelopmentStorage=true"
```

### Deployment

```bash
# Automatic (via git)
git push origin develop  # → Deploy to Dev
git push origin main     # → Deploy to Prod

# Manual
func azure functionapp publish MyFunctionApp
```

## 📚 Documentation

| Document | Purpose |
|----------|---------|
| [README.md](README.md) | Getting started, project overview |
| [DEBUGGING.md](DEBUGGING.md) | Local development, testing, troubleshooting |
| [CONFIGURATION.md](CONFIGURATION.md) | Environment setup, secrets, settings |

## 🔧 Technologies & Packages

### Core
- **.NET 8** - Latest LTS framework
- **Azure Functions v4** - Serverless runtime
- **Entity Framework Core 8.0** - ORM
- **SQL Server** - Database

### NuGet Packages
```
Microsoft.Azure.Functions.Worker
Microsoft.Azure.Functions.Worker.Sdk
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.Extensions.Configuration.Json
Microsoft.ApplicationInsights
Azure.Storage.Queues (via Storage extensions)
```

### Development Tools
- Azure Functions Core Tools
- Azure Storage Emulator (Azurite)
- SQL LocalDB
- VS Code / Visual Studio

## 📋 Configuration Highlights

### Program.cs (Dependency Injection)
- Service registration
- Database context configuration
- Logging setup
- Configuration binding

### host.json
- Function timeout: 5 minutes
- Extension bundle
- Logging settings

### appsettings.*.json
- Connection strings (per environment)
- Log levels (per environment)
- Function settings
- Feature flags

## 🔒 Security Best Practices

✅ Implemented:
- Connection strings in configuration (not code)
- Azure Key Vault support for production secrets
- .env file for local development (git-ignored)
- No hardcoded credentials
- Structured exception handling

## 📊 Monitoring & Observability

✅ Implemented:
- Application Insights integration
- Structured logging with correlation IDs
- Execution audit logs in database
- Duration tracking
- Error tracking

### Example KQL Queries
```kusto
// Get recent errors
traces | where severityLevel == 3 | order by timestamp desc

// Function performance
customMetrics | where name == "ProcessMessageDuration"
| summarize Avg=avg(value) by bin(timestamp, 1m)
```

## 🧪 Testing Support

✅ Included:
- Unit test setup with Xunit
- Mock interfaces for testing
- Integration test patterns
- Local storage emulator for testing
- Load testing with k6

## 🚨 Common Issues & Solutions

| Issue | Solution |
|-------|----------|
| Queue not processing | Check Azurite is running on port 10001 |
| DB connection failed | Verify LocalDB is running: `sqllocaldb start mssqllocaldb` |
| Config not loading | Set `ASPNETCORE_ENVIRONMENT=Development` |
| Timeout errors | Increase timeout in host.json |

## 📈 Scalability Considerations

### Development
- Consumption plan (Pay per use)
- Single instance
- 5-10 messages/batch

### Staging
- Premium plan
- 2-3 instances
- 10-20 messages/batch

### Production
- Premium or Dedicated plan
- Auto-scale 3-10 instances
- 50-100 messages/batch

## 🔄 Next Steps

1. ✅ Clone/modify for your use case
2. ✅ Update connection strings for your environment
3. ✅ Customize business logic in QueueProcessorService
4. ✅ Add unit tests in Tests project
5. ✅ Configure GitHub Actions secrets
6. ✅ Deploy to Azure

## 📝 Key Code Examples

### Processing Queue Message
```csharp
var result = await _processorService.ProcessMessageAsync(
    message.MessageBody,
    messageId,
    cancellationToken);
```

### Database Operations
```csharp
var item = new ProcessedItem { MessageId = id, Data = json };
await _dataService.CreateProcessedItemAsync(item);
```

### Structured Logging
```csharp
using (_logger.BeginScope(new { MessageId = id }))
{
    _logger.LogInformation("Processing started");
}
```

## 📞 Support Resources

- [Azure Functions Documentation](https://docs.microsoft.com/azure/azure-functions/)
- [Entity Framework Core Docs](https://docs.microsoft.com/ef/core/)
- [GitHub Actions](https://docs.github.com/actions)
- [Application Insights](https://docs.microsoft.com/azure/azure-monitor/app/app-insights-overview)

---

**Version:** 1.0.0  
**Last Updated:** 2024  
**Status:** ✅ Production Ready
