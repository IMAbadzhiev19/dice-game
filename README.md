# Dice Service

Simple ASP.NET Core Web API for rolling dice and viewing roll history. The application uses PostgreSQL for storage and applies EF Core migrations automatically on startup.

## Requirements

- .NET 10 SDK
- Docker Desktop or Docker Engine with Compose support

## Setup

1. Start PostgreSQL:

```bash
docker compose up -d
```

2. Restore the solution:

```bash
dotnet restore DiceService/DiceService.sln
```

3. Build the solution:

```bash
dotnet build DiceService/DiceService.sln
```

## Run

Start the API from the presentation project:

```bash
dotnet run --project DiceService/DiceService.Presentation/DiceService.Presentation.csproj
```

By default the app runs on:

- `http://localhost:5199`
- `https://localhost:7155`

In Development, Swagger is available at:

- `http://localhost:5199/swagger`

## Configuration

The default connection string is defined in `DiceService/DiceService.Presentation/appsettings.json` and points to the local PostgreSQL container:

- Host: `localhost`
- Port: `5432`
- Database: `dice_service_db`
- Username: `postgres`
- Password: `postgres`

If you need to change the database settings, update the `ConnectionStrings:DefaultConnection` value in `appsettings.json` or override it with environment variables.

## API Endpoints

- `POST /api/dice/roll` - create a new dice roll
- `GET /api/dice/rolls` - list dice rolls with query parameters

## Notes

- Database migrations run automatically when the app starts.
- If PostgreSQL is not running, start the container before launching the API.
