# ADR-0002: Feature-first vertical slices

- **Status**: Accepted
- **Date**: 2026-07-07

## Context

The project goal includes learning practical .NET architecture without unnecessary layering overhead.

## Decision

Organise backend features as vertical slices by module/use case, where each slice contains endpoint, validation, application logic, and persistence integration.

## Consequences

### Positive
- Changes stay local to a feature.
- Easier reasoning about behaviour end-to-end.
- Strong fit with modular-monolith boundaries.

### Negative
- Some code duplication may appear initially.
- Requires clear conventions to avoid inconsistent slice quality.

## Alternatives considered

- **Layer-first structure (controllers/services/repositories)**: rejected due to tendency towards wide, coupled changes.
- **Pure clean architecture with heavy abstraction everywhere**: deferred; selective abstraction will be used where complexity justifies it.

