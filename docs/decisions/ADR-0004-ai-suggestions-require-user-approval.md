# ADR-0004: AI suggestions require explicit user approval

- **Status**: Accepted
- **Date**: 2026-07-07

## Context

AI outputs may be inconsistent, malformed, or contextually inappropriate.  
The product must remain trustworthy and predictable for household planning.

## Decision

AI-generated content is stored as a draft and can only affect meal plans after explicit user approval.

## Consequences

### Positive
- Protects users from silent, low-quality plan changes.
- Keeps a clear review and audit path.
- Supports safer experimentation with local models.

### Negative
- Adds an extra user step.
- Slows fully automated planning workflows.

## Alternatives considered

- **Auto-apply AI output**: rejected due to trust and data-quality risk.
- **No AI support at all**: deferred; optional module remains valuable for later exploration.

