# ADR-0005: Single PostgreSQL database for all modules

- **Status**: Accepted
- **Date**: 2026-07-07

## Context

The system starts as a modular monolith with closely related data and transactional workflows (meal plan ↔ shopping list ↔ feedback).

## Decision

Use one PostgreSQL database, with module-owned schemas/tables and explicit ownership rules.

## Consequences

### Positive
- Simpler operations and local development.
- Easier transactional consistency for cross-module flows.
- Works well with Testcontainers integration tests.

### Negative
- Shared database can encourage accidental coupling if boundaries are ignored.
- Future extraction of modules may require data separation work.

## Alternatives considered

- **Multiple databases per module now**: rejected due to operational overhead and limited current value.
- **Non-relational primary store**: rejected; relational constraints and querying suit MVP domain well.

