# Local development

## Environment assumptions

- macOS development environment.
- Rider for backend workflow.
- OrbStack with Docker Compose for local infrastructure.

## Planned local services

1. PostgreSQL (primary application database).
2. Mailpit (email preview/testing).
3. MinIO (optional object storage for later photo/file use cases).

## Workflow (placeholders)

Exact commands will be confirmed once API and web projects are scaffolded.

1. Start infrastructure: `<compose-up-command>`
2. Run backend API: `<run-api-command>`
3. Run web app: `<run-web-command>`
4. Stop infrastructure: `<compose-down-command>`

## Configuration notes

- Keep environment variables in local `.env` files (not committed).
- Use separate development/test database names.
- AI endpoint configuration should be optional and disabled by default.

## Health expectations

- API health endpoint should verify API process health and database connectivity.
- Worker health should report liveness and queue-processing capability when enabled.

