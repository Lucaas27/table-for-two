# Local development

## Environment assumptions

- macOS development environment.
- Rider for backend workflow.
- OrbStack with Docker Compose for local infrastructure.

## Planned local services

1. PostgreSQL (primary application database).
2. Mailpit (email preview/testing).
3. MinIO (optional object storage for later photo/file use cases).

## Workflow

Use `README.md` as the source of truth for day-to-day run commands.  
Current baseline:

1. Scaffold build check: `dotnet build TableForTwo.sln && npm --prefix src/web run lint && npm --prefix src/web run build`
2. Start local infrastructure: `docker compose up -d postgres mailpit`
3. Start optional MinIO profile: `docker compose --profile optional-storage up -d minio`
4. Run API: `dotnet run --project src/api/TableForTwo.API/TableForTwo.API.csproj`
5. Run worker: `dotnet run --project src/worker/TableForTwo.Worker/TableForTwo.Worker.csproj`
6. Run tests: `dotnet test tests/TableForTwo.API.Tests/TableForTwo.API.Tests.csproj`
7. Start web app:
   - `cd src/web`
   - `npm install`
   - `npm run dev`
8. Stop local infrastructure: `docker compose down`

The API performs a startup PostgreSQL connection check as a local smoke signal for app-to-database connectivity.

## Configuration notes

- Copy `.env.example` to `.env` and keep local `.env` files out of source control.
- Use separate development/test database names (`table_for_two_dev` and `table_for_two_test` by default).
- Configuration is bound via `IOptions` from these sections:
  - `Infrastructure:Postgres`
  - `Infrastructure:Mailpit`
  - `Infrastructure:Minio`
- Environment variables should use the .NET double-underscore convention:
  - `Infrastructure__Postgres__ConnectionString`
  - `Infrastructure__Mailpit__Host`
  - `Infrastructure__Mailpit__Port`
  - `Infrastructure__Mailpit__SenderEmail`
  - `Infrastructure__Minio__Enabled`
  - `Infrastructure__Minio__Endpoint`
  - `Infrastructure__Minio__AccessKey`
  - `Infrastructure__Minio__SecretKey`
  - `Infrastructure__Minio__Bucket`
  - `Infrastructure__Minio__UseSsl`
- AI endpoint configuration should be optional and disabled by default.

## Health expectations

- API health endpoint should verify API process health and database connectivity.
- Worker health should report liveness and queue-processing capability when enabled.
