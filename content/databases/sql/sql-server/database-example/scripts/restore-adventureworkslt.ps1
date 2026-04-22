[CmdletBinding()]
param(
    [string]$ComposeFilePath = "",
    [string]$EnvFilePath = "",
    [string]$ServiceName = "sqlserver",
    [string]$DatabaseName = "AdventureWorksLT2019",
    [string]$BackupFilePathInContainer = "/var/opt/mssql/backup/AdventureWorksLT2019.bak",
    [string]$DataDirectoryInContainer = "/var/opt/mssql/data",
    [int]$MaxAttempts = 60,
    [int]$DelaySeconds = 2
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$scriptRoot = Split-Path -Parent $PSCommandPath
if (-not $ComposeFilePath) {
    $ComposeFilePath = Join-Path $scriptRoot "..\compose.yaml"
}

if (-not $EnvFilePath) {
    $EnvFilePath = Join-Path $scriptRoot "..\.env"
}

function Get-DotEnvValue {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path,
        [Parameter(Mandatory = $true)]
        [string]$Key
    )

    if (-not (Test-Path -LiteralPath $Path)) {
        throw "Missing environment file: $Path"
    }

    $line = Get-Content -LiteralPath $Path |
        Where-Object { $_ -match "^$([regex]::Escape($Key))=" } |
        Select-Object -First 1

    if (-not $line) {
        throw "Missing key '$Key' in $Path"
    }

    return $line.Substring($Key.Length + 1).Trim()
}

function Invoke-ComposeSqlcmd {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Query,
        [string[]]$ExtraSqlcmdArgs = @()
    )

    $arguments = @(
        "compose",
        "-f", $ComposeFilePath,
        "exec",
        "-T",
        $ServiceName,
        "/opt/mssql-tools18/bin/sqlcmd",
        "-S", "localhost",
        "-U", "sa",
        "-P", $script:SaPassword,
        "-No"
    ) + $ExtraSqlcmdArgs + @(
        "-Q", $Query
    )

    $output = & docker @arguments 2>&1
    if ($LASTEXITCODE -ne 0) {
        throw ($output -join [Environment]::NewLine)
    }

    return $output
}

function Wait-ForSqlServer {
    for ($attempt = 1; $attempt -le $MaxAttempts; $attempt++) {
        try {
            Invoke-ComposeSqlcmd -Query "SET NOCOUNT ON; SELECT 1;" -ExtraSqlcmdArgs @("-h", "-1", "-W") | Out-Null
            return
        }
        catch {
            if ($attempt -eq $MaxAttempts) {
                throw "SQL Server did not become ready after $MaxAttempts attempts. Last error: $($_.Exception.Message)"
            }

            Start-Sleep -Seconds $DelaySeconds
        }
    }
}

function Get-DatabaseExists {
    $result = Invoke-ComposeSqlcmd `
        -Query "SET NOCOUNT ON; SELECT COUNT(*) FROM sys.databases WHERE name = N'$DatabaseName';" `
        -ExtraSqlcmdArgs @("-h", "-1", "-W")

    return (($result | Out-String).Trim() -eq "1")
}

function Get-BackupFiles {
    $result = Invoke-ComposeSqlcmd `
        -Query "RESTORE FILELISTONLY FROM DISK = N'$BackupFilePathInContainer';" `
        -ExtraSqlcmdArgs @("-h", "-1", "-W", "-s", "|")

    $lines = ($result | ForEach-Object { $_.Trim() }) | Where-Object { $_ }
    $files = @()

    foreach ($line in $lines) {
        $parts = $line.Split("|")
        if ($parts.Length -lt 3) {
            continue
        }

        $files += [pscustomobject]@{
            LogicalName = $parts[0].Trim()
            Type        = $parts[2].Trim()
        }
    }

    if (-not $files) {
        throw "RESTORE FILELISTONLY returned no file metadata."
    }

    return $files
}

$SaPassword = Get-DotEnvValue -Path $EnvFilePath -Key "MSSQL_SA_PASSWORD"

Write-Host "Waiting for SQL Server to become ready..."
Wait-ForSqlServer

if (Get-DatabaseExists) {
    Write-Host "$DatabaseName already exists. Nothing to restore."
    exit 0
}

Write-Host "Inspecting backup file metadata..."
$backupFiles = Get-BackupFiles

$moveClauses = @()
$dataIndex = 0
$logIndex = 0

foreach ($file in $backupFiles) {
    $logicalName = $file.LogicalName.Replace("'", "''")

    if ($file.Type -eq "L") {
        $logIndex++
        $targetFile = if ($logIndex -eq 1) {
            "$DatabaseName`_log.ldf"
        }
        else {
            "$DatabaseName`_log$logIndex.ldf"
        }
    }
    else {
        $dataIndex++
        $targetFile = if ($dataIndex -eq 1) {
            "$DatabaseName.mdf"
        }
        else {
            "$DatabaseName`_$dataIndex.ndf"
        }
    }

    $targetPath = "$DataDirectoryInContainer/$targetFile"
    $moveClauses += "MOVE N'$logicalName' TO N'$targetPath'"
}

$restoreQuery = @"
RESTORE DATABASE [$DatabaseName]
FROM DISK = N'$BackupFilePathInContainer'
WITH $(($moveClauses -join ",`n     ")),
     RECOVERY,
     REPLACE,
     STATS = 5;
"@

Write-Host "Restoring $DatabaseName from $BackupFilePathInContainer..."
Invoke-ComposeSqlcmd -Query $restoreQuery | Out-Null

$verification = Invoke-ComposeSqlcmd `
    -Query "SET NOCOUNT ON; SELECT name FROM sys.databases WHERE name = N'$DatabaseName';" `
    -ExtraSqlcmdArgs @("-h", "-1", "-W")

if ((($verification | Out-String).Trim()) -ne $DatabaseName) {
    throw "Restore completed, but database verification failed."
}

Write-Host "Restore completed successfully."
