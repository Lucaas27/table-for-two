# Release plan

## Milestones

| Milestone | Outcome | In scope | Explicitly out |
| --- | --- | --- | --- |
| M0 Foundation | Ready-to-build baseline | Repo conventions, local infra plan, architecture decisions, testing plan | Feature implementation |
| M1 Household access | Two-person workspace setup | Auth, household creation, membership boundaries | Pantry, AI, import jobs |
| M2 Recipes | Usable recipe library | Recipe CRUD, ingredients, servings, tags, favourites | URL import pipeline |
| M3 Planning + shopping (MVP release) | End-to-end core value | Weekly plan, consolidated shopping list, realtime updates | AI suggestions, file uploads |
| M4 Feedback loop | Better repeat decisions | Ratings and notes linked to cooked meals | Automated recommendation engine |
| M5 Background jobs | Durable async processing | URL import jobs, persisted status, retries/cancellation | Queue broker dependency |
| M6 AI suggestions (optional) | Assisted planning with control | Draft suggestions, validation, explicit approval | Auto-apply without review |
| M7 Hardening | Release confidence | Observability polish, reliability, UX refinements | Unnecessary platform expansion |

## Release guardrails

1. MVP is complete at M3; later milestones must not block initial release.
2. Operational complexity must be justified by a concrete problem.
3. New architecture decisions should be captured as ADRs.

