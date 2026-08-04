# Environment Configuration Guide

This document explains how to configure the application for different environments (Development, Staging, Production).

## Configuration Structure

The application uses a hierarchical configuration system:

```
appsettings.json                  # Base settings (applied to all environments)
├─ appsettings.Development.json   # Overrides for Development
├─ appsettings.Staging.json       # Overrides for Staging
└─ appsettings.Production.json    # Overrides for Production
```

## Environment Detection

The active environment is determined by `ASPNETCORE_ENVIRONMENT` variable:

```powershell
# Windows
$env:ASPNETCORE_ENVIRONMENT = "Development"

# Linux/Mac
export ASPNETCORE_ENVIRONMENT=Development
```

If not set, defaults to `Production`.

## Configuration Settings

### FunctionSettings

Controls Azure Function behavior:

```json
{
  "FunctionSettings": {
    "MaxRetries": 3,              // Max retry attempts on failure
    "ProcessingTimeout": 30000,   // Timeout in milliseconds
    "BatchSize": 10               // Messages per batch
  }
}
```

| Environment | MaxRetries | Timeout (ms) | BatchSize |
|-------------|-----------|------------|-----------|
| Development | 5         | 60000      | 5         |
| Staging     | 3         | 30000      | 10        |
| Production  | 5         | 60000      | 100       |

### DatabaseSettings

Configures Entity Framework Core behavior:

```json
{
  "DatabaseSettings": {
    "CommandTimeout": 300,        // SQL command timeout in seconds
    "EnableDetailedErrors": false // Detailed EF error messages
  }
}
```

### Connection Strings

**Development (Local):**
```
Server=(localdb)\mssqllocaldb;Database=QueueProcessorDb;Trusted_Connection=true;TrustServerCertificate=true;
```

**Staging (Azure SQL):**
```
Server=staging-db.database.windows.net;Database=QueueProcessorDb_Staging;User Id=sa;Password=...;Encrypt=true;
```

**Production (Azure SQL):**
```
Server=production-db.database.windows.net;Database=QueueProcessorDb_Prod;User Id=sa;Password=...;Encrypt=true;
```

## Logging Configuration

### Log Levels

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",           // Default level
      "Microsoft": "Warning",             // Microsoft libraries
      "Microsoft.EntityFrameworkCore": "Debug"  // Specific namespace
    }
  }
}
```

**Levels** (from least to most verbose):
1. Critical - Fatal errors
2. Error - Recoverable errors
3. Warning - Potential issues
4. Information - General info
5. Debug - Diagnostic info
6. Trace - Detailed tracing

**Recommended by Environment:**
- Development: Debug
- Staging: Information
- Production: Warning or Error

### Application Insights

```json
{
  "ApplicationInsights": {
    "InstrumentationKey": "${APPINSIGHTS_INSTRUMENTATION_KEY}"
  }
}
```

Variable interpolation supports environment variables with `${VAR_NAME}` syntax.

## Setting Up Environments

### Development Environment

**File: `appsettings.Development.json`**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=QueueProcessorDb_Dev;..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information",
      "Microsoft.EntityFrameworkCore": "Debug"
    }
  },
  "FunctionSettings": {
    "MaxRetries": 5,
    "ProcessingTimeout": 60000,
    "BatchSize": 5
  }
}
```

**Usage:**
```bash
# Locally
func start

# Or explicitly
ASPNETCORE_ENVIRONMENT=Development func start
```

### Staging Environment

**File: `appsettings.Staging.json`**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=staging-db.database.windows.net;User Id=sa;Password=${DB_PASSWORD};..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  },
  "ApplicationInsights": {
    "InstrumentationKey": "${APPINSIGHTS_KEY_STAGING}"
  }
}
```

**Setup in Azure:**
1. Create Function App: `queue-processor-staging`
2. Create App Service Plan: `Standard` tier
3. Set environment variable:
   ```
   ASPNETCORE_ENVIRONMENT = Staging
   ```
4. Configure application settings in Function App

### Production Environment

**File: `appsettings.Production.json`**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-db.database.windows.net;User Id=sa;Password=${DB_PASSWORD};..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Error"
    }
  },
  "FunctionSettings": {
    "MaxRetries": 5,
    "ProcessingTimeout": 60000,
    "BatchSize": 100
  }
}
```

**Setup in Azure:**
1. Create Function App: `queue-processor-prod`
2. Create App Service Plan: `Premium` tier (better performance)
3. Set environment variable:
   ```
   ASPNETCORE_ENVIRONMENT = Production
   ```
4. Configure secrets in Key Vault

## Secrets Management

### Local Development

**File: `.env` (git-ignored)**

```
DB_PASSWORD=LocalPassword123
APPINSIGHTS_INSTRUMENTATION_KEY=
```

Load in `local.settings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "...;Password=${DB_PASSWORD};"
  }
}
```

### Azure Key Vault

**Recommended for cloud environments**

1. Create Key Vault resource in Azure
2. Add secrets:
   ```bash
   az keyvault secret set --vault-name myKeyVault --name DbPassword --value "SecurePassword123"
   az keyvault secret set --vault-name myKeyVault --name AppInsightsKey --value "..."
   ```

3. Configure Function App:
   ```bash
   az functionapp config appsettings set \
     --resource-group myResourceGroup \
     --name queue-processor-prod \
     --settings "DB_PASSWORD=@Microsoft.KeyVault(SecretUri=https://myKeyVault.vault.azure.net/secrets/DbPassword/)"
   ```

4. Reference in settings:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "...;Password=${DB_PASSWORD};"
     }
   }
   ```

## Environment Variables

### Required for All Environments

```
FUNCTIONS_WORKER_RUNTIME = dotnet-isolated
ASPNETCORE_ENVIRONMENT = [Development|Staging|Production]
```

### Azure-Specific

```
AZURE_SUBSCRIPTION_ID       # For deployment
AZURE_RESOURCE_GROUP        # For deployment
AZURE_FUNCTIONAPP_NAME      # For deployment
APPINSIGHTS_INSTRUMENTATION_KEY
AzureWebJobsStorage         # Storage connection
```

### Database-Specific

```
DB_CONNECTION_STRING
DB_PASSWORD (if using Key Vault)
DB_COMMAND_TIMEOUT
```

## Local vs Cloud Configuration

| Aspect | Local | Cloud |
|--------|-------|-------|
| **Database** | LocalDB | Azure SQL |
| **Storage** | Emulator (Azurite) | Azure Storage |
| **Logging** | Console | Application Insights |
| **Secrets** | .env file | Key Vault |
| **Config** | appsettings.json | App Settings |

## Configuration in Code

### Access Settings

```csharp
// In Program.cs
var configuration = context.Configuration;

// Get value
var dbTimeout = configuration.GetValue<int>("DatabaseSettings:CommandTimeout");

// Bind to class
services.Configure<FunctionSettings>(
    configuration.GetSection("FunctionSettings"));

// Use in service
public class MyService
{
    private readonly IOptions<FunctionSettings> _settings;
    
    public MyService(IOptions<FunctionSettings> settings)
    {
        _settings = settings;
    }
    
    public void DoWork()
    {
        var maxRetries = _settings.Value.MaxRetries;
    }
}
```

### Environment-Based Logic

```csharp
var env = context.HostingEnvironment;

if (env.IsEnvironment("Development"))
{
    // Dev-specific code
}
else if (env.IsEnvironment("Production"))
{
    // Prod-specific code
}
```

## Validation & Defaults

Settings are validated in `Program.cs`:

```csharp
var connectionString = configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not configured.");

var settings = new FunctionSettings();
configuration.GetSection("FunctionSettings").Bind(settings);

if (settings.MaxRetries < 1 || settings.MaxRetries > 10)
    throw new InvalidOperationException(
        "MaxRetries must be between 1 and 10");
```

## Deployment Configuration

### GitHub Actions Secrets

Configure in repository Settings → Secrets:

```
AZURE_CREDENTIALS_DEV
AZURE_CREDENTIALS_PROD
APPINSIGHTS_KEY_DEV
APPINSIGHTS_KEY_PROD
DB_PASSWORD_DEV
DB_PASSWORD_PROD
```

### Deployment Script

```powershell
# deploy.ps1
param(
    [string]$Environment = "Development",
    [string]$Version = "1.0.0"
)

$resourceGroup = "rg-queue-processor-$Environment"
$functionAppName = "queue-processor-$Environment"

# Deploy based on environment
if ($Environment -eq "Production") {
    Write-Host "Deploying to Production..."
    # Production-specific steps
} else {
    Write-Host "Deploying to $Environment..."
    # Dev/Staging steps
}

az functionapp deployment source config-zip `
    --resource-group $resourceGroup `
    --name $functionAppName `
    --src-path deploy.zip
```

## Monitoring Configuration

### Development Alerts

- Minimum severity: Warning
- Destinations: Console only

### Production Alerts

- Minimum severity: Error
- Destinations: Email, Slack, PagerDuty
- Escalation: After 5 minutes

Configure in Application Insights:
```
Action Groups → Alert Rules → Setup notifications
```

## Best Practices

✅ **DO:**
- Use hierarchy: Base config + environment overrides
- Validate settings on startup
- Use Key Vault for secrets
- Document all configuration options
- Keep sensitive data out of git
- Use different resources per environment

❌ **DON'T:**
- Hardcode configuration values
- Store secrets in code
- Mix environment-specific logic in business code
- Use same database for dev and prod
- Commit local.settings.json

## Troubleshooting

### Configuration Not Loading

```bash
# Check environment variable
echo $ASPNETCORE_ENVIRONMENT  # Linux/Mac
echo %ASPNETCORE_ENVIRONMENT%  # Windows

# Restart function app
func stop
func start
```

### Wrong Settings File Used

```bash
# Verify which file is loaded (add to Program.cs)
Console.WriteLine($"Environment: {env.EnvironmentName}");
```

### Environment Variables Not Resolved

```json
// Correct syntax
"ConnectionString": "...;Password=${DB_PASSWORD};"

// Incorrect
"ConnectionString": "...;Password=${DB_PASSWORD};"  // Won't work without integration
```
