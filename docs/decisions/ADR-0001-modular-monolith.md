# ADR-0001: Modular monolith as initial architecture

- **Status**: Accepted
- **Date**: 2026-07-07

## Context

The project is early-stage and aims to ship a usable first release while learning strong engineering practices.  
A full distributed architecture would add operational complexity before product fit is clear.

## Decision

Use a modular monolith: one backend deployable with explicit module boundaries and internal contracts.

## Consequences

### Positive
- Faster delivery and simpler local development.
- Lower operational overhead.
- Clear path to add boundaries and tests before any extraction.

### Negative
- Requires discipline to avoid “big ball of mud” coupling.
- Some modules may need extraction later if scaling demands differ.

## Alternatives considered

- **Microservices from day one**: rejected due to complexity and slower iteration.
- **Single unstructured monolith**: rejected due to long-term maintainability risks.

