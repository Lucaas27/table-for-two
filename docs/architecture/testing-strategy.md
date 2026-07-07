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

## Quality gates (placeholders)

- Build: `<build-command>`
- Unit tests: `<unit-test-command>`
- Integration tests: `<integration-test-command>`
- Lint/format/type checks: `<quality-command>`

## Trade-offs

- More integration tests increase confidence but cost runtime; focus on critical paths first.
- Full E2E coverage is valuable later, but not needed to start delivering MVP slices.

