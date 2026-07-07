# Copilot instructions for Table for Two

## Project intent

Build a practical two-person meal-planning app with a small, usable MVP first.

## Architecture guardrails

1. Start with a modular monolith.
2. Organise features as vertical slices.
3. Use a single PostgreSQL database with clear module ownership.
4. Keep AI optional; never depend on AI for core flows.
5. Background jobs should progress from simple worker to durable DB-backed processing before external queue tooling.

## Delivery priorities

1. MVP flow: recipes → weekly plan → combined shopping list → realtime collaboration → meal feedback.
2. Keep complexity proportional to current needs.
3. Capture major technical decisions as ADRs in `docs/decisions/`.

## Safety and quality

- Treat AI output as untrusted input and require explicit user approval before applying suggestions.
- Do not introduce infrastructure such as Redis/RabbitMQ/Kafka/Kubernetes without a documented need.
- Keep tests and docs aligned with behaviour changes.

## Key references

- `README.md`
- `docs/product/`
- `docs/architecture/`
- `docs/decisions/`
- `docs/backlog/`

