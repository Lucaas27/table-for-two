# ADR-0003: Database-backed background jobs before external queues

- **Status**: Accepted
- **Date**: 2026-07-07

## Context

Recipe URL import and related tasks are long-running and failure-prone.  
The project explicitly aims to learn durable job execution mechanics before introducing job libraries or brokers.

## Decision

Adopt a staged approach:
1. Simple hosted worker.
2. Persisted jobs in PostgreSQL.
3. Retries, locking, idempotency, cancellation.
4. Outbox pattern for reliable message hand-off.
5. Re-evaluate Hangfire/queue later if needed.

## Consequences

### Positive
- Deep understanding of job semantics and failure handling.
- Durable status and diagnostics from early stages.
- Avoids early dependency sprawl.

### Negative
- More in-house implementation effort.
- Potential rework if high throughput demands emerge quickly.

## Alternatives considered

- **Hangfire immediately**: deferred; valuable option once requirements justify abstraction.
- **Queue broker immediately**: rejected for now; adds infra complexity not yet proven necessary.

