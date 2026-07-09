# Local development

## Environment assumptions

- macOS development environment.
- Rider for backend workflow.
- OrbStack with Docker Compose for local infrastructure.

## Planned local services

1. PostgreSQL (primary application database).
2. Mailpit (email preview/testing).
3. MinIO (optional object storage for later photo/file use cases).

## Workflow (current scaffold baseline)

Use `README.md` as the source of truth for day-to-day run commands.  
Current baseline:

1. Scaffold build check: `dotnet build TableForTwo.sln && npm --prefix src/web run lint && npm --prefix src/web run build`
2. Run API: `dotnet run --project src/api/TableForTwo.API/TableForTwo.API.csproj`
3. Run worker: `dotnet run --project src/worker/TableForTwo.Worker/TableForTwo.Worker.csproj`
4. Run tests: `dotnet test tests/TableForTwo.API.Tests/TableForTwo.API.Tests.csproj`
5. Start web app:
   - `cd src/web`
   - `npm install`
   - `npm run dev`

Infrastructure service bootstrap commands will be added in M0 once PostgreSQL, Mailpit, and MinIO are wired for local use.

## Configuration notes

- Keep environment variables in local `.env` files (not committed).
- Use separate development/test database names.
- AI endpoint configuration should be optional and disabled by default.

## Health expectations

- API health endpoint should verify API process health and database connectivity.
- Worker health should report liveness and queue-processing capability when enabled.
