# Testing strategy

## Objectives

1. Protect core meal-planning and shopping behaviour.
2. Catch module-boundary regressions early.
3. Keep feedback fast enough for daily development.

## Test layers

| Layer | Purpose | Typical scope |
| --- | --- | --- |
| Unit tests | Fast logic verification | Domain rules, validators, mappers, consolidation logic |
| Integration tests | Verify real wiring | API + PostgreSQL + module interactions |
| End-to-end smoke (later) | High-level confidence | Critical user flow checks across web + API |

## Priority scenarios

- Recipe creation/editing with ingredient and serving validation.
- Meal-plan updates triggering shopping-list refresh.
- Realtime shopping-item updates visible to both users.
- Background job state transitions (queued/running/retry/failed/succeeded) once introduced.
- AI draft lifecycle (generate, validate, approve/reject) once introduced.

## Tooling direction

- xUnit + FluentAssertions for backend tests.
- Testcontainers for PostgreSQL-backed integration tests.
- Frontend test stack to be selected during web scaffolding.

## Quality gates

- Build: `dotnet build TableForTwo.sln`
- Backend tests: `dotnet test tests/TableForTwo.API.Tests/TableForTwo.API.Tests.csproj`
- Frontend lint/type/build: `npm --prefix src/web run lint && npm --prefix src/web run build`

## Current CI baseline

- Pull requests run backend restore/build/test in GitHub Actions.
- Backend integration tests use Testcontainers with PostgreSQL rather than a shared CI database service.
- Pull requests also run web dependency install, lint, and production build checks.

## Trade-offs

- More integration tests increase confidence but cost runtime; focus on critical paths first.
- Full E2E coverage is valuable later, but not needed to start delivering MVP slices.
