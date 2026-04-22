# AdventureWorksLT2019 in Docker

This folder gives you a local SQL Server 2019 container with the bundled `AdventureWorksLT2019.bak` restored for query practice.

## Files

- `compose.yaml`: SQL Server 2019 container definition
- `.env`: local SA password used by Docker Compose
- `.env.example`: example env file if you want to recreate `.env`
- `scripts/restore-adventureworkslt.ps1`: idempotent restore helper
- `AdventureWorksLT2019.bak`: sample database backup

## Start SQL Server

From this directory:

```powershell
docker compose up -d
```

The container listens on `localhost,14333`.

## Restore The Database

Run the restore script after the container is up:

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\restore-adventureworkslt.ps1
```

The script is safe to rerun. If `AdventureWorksLT2019` already exists, it exits without restoring again.

## Connect

Use these values in SSMS, Azure Data Studio, VS Code SQL tools, or `sqlcmd`:

- Server: `localhost,14333`
- Login: `sa`
- Password: read it from `.env`
- Database: `AdventureWorksLT2019`

Example with `sqlcmd` on the host:

```powershell
sqlcmd -S localhost,14333 -U sa -P "<password from .env>" -No -d AdventureWorksLT2019
```

## First Practice Query

```sql
SELECT TOP 10 ProductID, Name, ListPrice
FROM SalesLT.Product
ORDER BY ProductID;
```

## Useful Commands

Stop the container:

```powershell
docker compose down
```

Start it again later:

```powershell
docker compose up -d
```

Show logs:

```powershell
docker compose logs -f sqlserver
```

## Reset Everything

This deletes the container and the persisted database files:

```powershell
docker compose down
docker volume rm sqlserver-adventureworkslt-data
```

Then start the container again and rerun the restore script.

## Troubleshooting

If the container exits immediately:

- make sure the SA password in `.env` meets SQL Server password rules

If port `14333` is already in use:

- change the host port in `compose.yaml`

If restore fails:

- confirm the container is healthy with `docker compose ps`
- check logs with `docker compose logs sqlserver`
- rerun the restore script after the server is ready
