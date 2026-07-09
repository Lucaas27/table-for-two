# Table for Two

Table for Two is a shared meal-planning app for two people in one household.  
The core flow is: save recipes → plan the week → generate one shared shopping list → update it together in real time → leave feedback to improve future choices.

## Current status

The repository now has initial project scaffolding for:
- API (`src/api/TableForTwo.API`)
- Worker (`src/worker/TableForTwo.Worker`)
- Web (`src/web`)
- Tests (`tests/TableForTwo.API.Tests`)

Feature implementation has not started yet.

## Product and architecture docs

- Product vision: `docs/product/vision.md`
- MVP boundary: `docs/product/mvp-scope.md`
- Domain terms: `docs/product/domain-glossary.md`
- Architecture overview: `docs/architecture/overview.md`
- Module boundaries: `docs/architecture/module-boundaries.md`
- Background jobs: `docs/architecture/background-jobs.md`
- AI integration: `docs/architecture/ai-integration.md`
- Local development: `docs/architecture/local-development.md`
- Testing strategy: `docs/architecture/testing-strategy.md`
- ADRs: `docs/decisions/`
- Release plan and issue template: `docs/backlog/`

## Principles

1. Start with a modular monolith and vertical slices.
2. Deliver a small, usable MVP before advanced automation.
3. Keep AI optional and safe (untrusted output, validated, user-approved).
4. Prefer clear trade-offs over unnecessary complexity.

## Baseline commands

- Build solution: `dotnet build TableForTwo.sln`
- Run API: `dotnet run --project src/api/TableForTwo.API/TableForTwo.API.csproj`
- Run worker: `dotnet run --project src/worker/TableForTwo.Worker/TableForTwo.Worker.csproj`
- Run tests: `dotnet test tests/TableForTwo.API.Tests/TableForTwo.API.Tests.csproj`
- Start web app:
  1. `cd src/web`
  2. `npm install`
  3. `npm run dev`
