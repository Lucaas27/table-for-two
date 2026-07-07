# Table for Two

Table for Two is a shared meal-planning app for two people in one household.  
The core flow is: save recipes → plan the week → generate one shared shopping list → update it together in real time → leave feedback to improve future choices.

## Current status

This repository currently contains planning and architecture documentation only.  
Application implementation will start after assumptions and scope are confirmed.

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

## Expected commands (placeholders)

Exact commands will be confirmed once the projects are scaffolded.

- Build: `<build-command>`
- Unit tests: `<unit-test-command>`
- Integration tests: `<integration-test-command>`
- Lint/format: `<lint-or-format-command>`

