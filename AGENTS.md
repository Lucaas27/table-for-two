# AGENTS

This repository is documentation-first at the moment.  
Before writing code, follow the product and architecture guidance below.

## Read first

- `docs/product/mvp-scope.md`
- `docs/architecture/overview.md`
- `docs/architecture/module-boundaries.md`
- `docs/architecture/background-jobs.md`
- `docs/architecture/ai-integration.md`
- `docs/architecture/testing-strategy.md`
- `docs/decisions/`

## Working expectations

1. Prioritise MVP delivery; avoid adding non-essential infrastructure.
2. Keep backend work modular-monolith + feature-first vertical slices.
3. Treat AI as optional and untrusted; drafts require user approval.
4. Avoid claiming tools/patterns are mandatory unless a concrete problem requires them.

## Build, test, and quality (placeholders)

- Build: `<build-command>`
- Unit tests: `<unit-test-command>`
- Integration tests: `<integration-test-command>`
- Quality checks (lint/format/type): `<quality-command>`

Any change should update relevant documentation and ADRs when decisions shift.
    
## Core principles

- Build one working vertical slice at a time.
- Prefer simple solutions over new infrastructure.
- Keep business rules out of endpoints and React components.
- Never return EF Core entities directly from API endpoints.
- Validate external input at the boundary.
- Add or update tests for behavioural changes.
- Do not introduce dependencies without explaining the trade-off.
- Background jobs must be observable, retryable, idempotent and cancellable.
- AI output is untrusted input and requires validation plus user approval.
- Update ADRs only when an architectural decision changes.

## Project structure

- `src/api` contains the ASP.NET Core API.
- `src/worker` contains long-running background work.
- `src/web` contains the React application.
- `docs/architecture` explains system design.
- `docs/decisions` contains ADRs.
- `tests` contains unit, integration and end-to-end tests.

## Before completing work

- Run relevant backend tests.
- Run frontend type checking and linting.
- Add tests for changed behaviour.
- Update documentation only where the change affects architecture or behaviour.
