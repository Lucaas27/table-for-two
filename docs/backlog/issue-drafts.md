# Issue drafts (technical lead review)

Backlog reviewed for MVP speed, realistic PR size, tangible learning, and safe progression of complexity.

## Milestone intent

| Milestone | Intent | Learning outcome |
| --- | --- | --- |
| M0 — Foundations | Make the project runnable and testable quickly | Learn project setup discipline and Testcontainers basics |
| M1 — Usable Meal Planner | Deliver a useful single-household planning flow | Learn vertical slices across API + web |
| M2 — Shared and Real-Time Experience | Add collaboration and complete MVP scope | Learn SignalR and two-user E2E verification |
| M3 — Background Jobs and Long-Running Work | Build durable async processing progressively | Learn retries, locking, idempotency, cancellation, outbox |
| M4 — Recipe Imports | Use job system for real user value | Learn resilient long-running import flow |
| M5 — Local AI Suggestions | Add optional, safe AI assistance | Learn schema validation + approval-first AI UX |
| M6 — Quality, Observability and Polish | Raise reliability before wider release | Learn practical operability and test hardening |

> **MVP release target:** complete through **M2-03**.  
> M2-04 is strongly recommended before wider testing, but not required to start user trials.

---

## M0 — Foundations

### M0-01
- **Title:** Scaffold solution structure for API, Web, Worker, and Tests
- **Milestone:** M0 — Foundations
- **Area:** Infrastructure
- **Type:** Chore
- **Suggested size:** M
- **Dependencies:** None
- **Outcome:** A clear project skeleton exists for vertical-slice delivery.
- **Scope:** Create/wire `src/api`, `src/web`, `src/worker`, and `tests`; keep structure aligned with module boundaries docs.
- **Acceptance criteria:**
  1. Solution includes API, web, worker, and test projects.
  2. Basic run/build commands are documented in README placeholders.
  3. No business features are implemented.
- **Test expectations:** Solution build succeeds.
- **Out of scope:** Feature endpoints and UI workflows.
- **Suggested branch name:** `chore/m0-scaffold-solution-layout`

### M0-02
- **Title:** Add local infrastructure profile and environment baseline
- **Milestone:** M0 — Foundations
- **Area:** Infrastructure
- **Type:** Task
- **Suggested size:** M
- **Dependencies:** M0-01
- **Outcome:** Local development is reproducible across machines.
- **Scope:** Wire PostgreSQL, Mailpit, and optional MinIO config patterns; document env variable conventions for dev/test.
- **Acceptance criteria:**
  1. Local infra can be started/stopped consistently.
  2. API and worker can read required local configuration.
  3. Development/test environment variable naming is documented.
- **Test expectations:** Local smoke run confirms app-to-database connectivity.
- **Out of scope:** Production deployment setup.
- **Suggested branch name:** `task/m0-local-infra-env-baseline`

### M0-03
- **Title:** CI quality baseline with first Testcontainers integration path
- **Milestone:** M0 — Foundations
- **Area:** Tests
- **Type:** Task
- **Suggested size:** M
- **Dependencies:** M0-02
- **Outcome:** Quality gates exist early and integration testing is proven.
- **Scope:** Add baseline CI workflow for build + tests and one reusable Testcontainers + PostgreSQL integration test harness.
- **Acceptance criteria:**
  1. CI runs build and test commands on pull requests.
  2. First integration test passes reliably with Testcontainers.
  3. Test harness is reusable by later vertical slices.
- **Test expectations:** Repeated local and CI runs of the same integration test are stable.
- **Out of scope:** Full regression coverage.
- **Suggested branch name:** `test/m0-ci-and-testcontainers-baseline`

### M0-04
- **Title:** Baseline health checks, structured logging, and API error contract
- **Milestone:** M0 — Foundations
- **Area:** API
- **Type:** Chore
- **Suggested size:** S
- **Dependencies:** M0-02
- **Outcome:** Core operational and user-facing error behaviour is consistent from day one.
- **Scope:** Add health endpoints, request correlation fields, and a standard error response format for validation/not-found/conflict/server errors.
- **Acceptance criteria:**
  1. Health endpoint reports API and database readiness.
  2. Logs include request correlation identifiers.
  3. API returns a documented, consistent error envelope.
- **Test expectations:** Integration checks for health endpoint and representative error responses.
- **Out of scope:** Metrics dashboards or alerting platform.
- **Suggested branch name:** `chore/m0-health-logging-error-contract`

---

## M1 — Usable Meal Planner

### M1-01
- **Title:** Household creation and membership API slice
- **Milestone:** M1 — Usable Meal Planner
- **Area:** API
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M0-04
- **Outcome:** Household context exists for all core MVP features.
- **Scope:** Create/get household endpoints, membership rules (max two active members in MVP), and validation.
- **Acceptance criteria:**
  1. Household can be created and retrieved.
  2. Membership limits are enforced.
  3. Household scoping rules are applied consistently.
- **Test expectations:** Unit tests for membership rules + integration tests for create/read flows.
- **Out of scope:** Advanced roles/permissions model.
- **Suggested branch name:** `feat/m1-household-api-slice`

### M1-02
- **Title:** Household onboarding web flow
- **Milestone:** M1 — Usable Meal Planner
- **Area:** Web
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M1-01
- **Outcome:** Users can establish household context from the UI.
- **Scope:** Onboarding route with create/join flow and route guard for users without household context.
- **Acceptance criteria:**
  1. User can create or join a household from the web app.
  2. Validation and API errors are visible and understandable.
  3. Users without household context are redirected to onboarding.
- **Test expectations:** Component tests for form validation and route guard behaviour.
- **Out of scope:** Email invitation delivery.
- **Suggested branch name:** `feat/m1-household-onboarding-web`

### M1-03
- **Title:** Recipe create and list API slice
- **Milestone:** M1 — Usable Meal Planner
- **Area:** API
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M1-01
- **Outcome:** Household members can add recipes and see a library.
- **Scope:** Create recipe endpoint and household-scoped list query with ingredients, servings, and tags.
- **Acceptance criteria:**
  1. Recipe creation validates required fields.
  2. List endpoint returns only household recipes.
  3. Response shape supports web list/detail needs.
- **Test expectations:** Unit validation tests + integration tests for create/list.
- **Out of scope:** Recipe import from URL.
- **Suggested branch name:** `feat/m1-recipe-create-list-api`

### M1-04
- **Title:** Recipe maintenance API slice (update, delete, tags, favourites)
- **Milestone:** M1 — Usable Meal Planner
- **Area:** API
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M1-03
- **Outcome:** Recipes are fully maintainable through API without manual database edits.
- **Scope:** Update/delete endpoints, tag updates, favourite toggle, and list filtering by tags/favourites.
- **Acceptance criteria:**
  1. Update/delete/favourite actions persist correctly.
  2. Tag and favourite filters return expected subsets.
  3. Concurrency and household-scoping rules are enforced.
- **Test expectations:** Integration tests covering update/delete/toggle/filter combinations.
- **Out of scope:** Recipe history/versioning.
- **Suggested branch name:** `feat/m1-recipe-maintenance-and-favourites-api`

### M1-05
- **Title:** Recipe management web screens
- **Milestone:** M1 — Usable Meal Planner
- **Area:** Web
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M1-03, M1-04
- **Outcome:** Users can manage recipes end-to-end in the UI.
- **Scope:** Recipe list/detail/edit views with tag and favourite controls.
- **Acceptance criteria:**
  1. Users can create, edit, delete, and favourite recipes.
  2. Tag/favourite filters work in UI.
  3. Loading, empty, and error states are handled.
- **Test expectations:** Component tests for forms, filters, and API error display.
- **Out of scope:** Drag-and-drop recipe organisation.
- **Suggested branch name:** `feat/m1-recipe-management-web`

### M1-06
- **Title:** Weekly meal-plan API slice
- **Milestone:** M1 — Usable Meal Planner
- **Area:** API
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M1-03
- **Outcome:** Recipes can be scheduled by week/day/slot.
- **Scope:** Get/update week meal plan (MVP: dinner slot only) with serving adjustments.
- **Acceptance criteria:**
  1. Planned meals can be added, changed, and removed.
  2. Recipe ownership and serving rules are validated.
  3. Plan updates emit a clear internal signal for shopping refresh.
- **Test expectations:** Integration tests for add/change/remove scenarios.
- **Out of scope:** Multiple meal slots per day.
- **Suggested branch name:** `feat/m1-weekly-meal-plan-api`

### M1-07
- **Title:** Shopping-list refresh and manual item API slice
- **Milestone:** M1 — Usable Meal Planner
- **Area:** API
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M1-06
- **Outcome:** Shopping list stays aligned with meal-plan changes while allowing manual edits.
- **Scope:** Consolidation logic, refresh trigger from meal-plan updates, and manual item add/toggle/remove operations.
- **Acceptance criteria:**
  1. Meal-plan changes trigger deterministic list refresh.
  2. Manual item persistence rule is documented and enforced.
  3. Item completion toggles are reliable.
- **Test expectations:** Unit tests for consolidation + integration tests for refresh/manual-item interactions.
- **Out of scope:** Real-time updates.
- **Suggested branch name:** `feat/m1-shopping-list-refresh-api`

### M1-08
- **Title:** Planner and shopping list web flow (non-realtime)
- **Milestone:** M1 — Usable Meal Planner
- **Area:** Web
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M1-06, M1-07, M1-05
- **Outcome:** Users can complete the core planning/shopping flow without realtime collaboration yet.
- **Scope:** Weekly planner UI + shopping list UI with manual item controls and explicit loading/empty/error states.
- **Acceptance criteria:**
  1. Users can assign meals to days.
  2. Shopping list updates after plan changes.
  3. Manual item create/toggle/delete works.
  4. User-facing error messages exist for failed plan or list actions.
- **Test expectations:** Component tests for planner/list interactions and visible error states.
- **Out of scope:** SignalR collaboration.
- **Suggested branch name:** `feat/m1-planner-shopping-web`

---

## M2 — Shared and Real-Time Experience

### M2-01
- **Title:** SignalR shopping-list hub and event contract
- **Milestone:** M2 — Shared and Real-Time Experience
- **Area:** API
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M1-07
- **Outcome:** Backend can publish household-scoped shopping-list changes in real time.
- **Scope:** Hub setup, event payload contract, and household authorisation rules for subscriptions.
- **Acceptance criteria:**
  1. Relevant list changes emit realtime events.
  2. Clients can subscribe only to authorised household channels.
  3. Event contract is documented.
- **Test expectations:** Integration tests for event publication and subscription authorisation.
- **Out of scope:** Presence/chat features.
- **Suggested branch name:** `feat/m2-signalr-shopping-hub`

### M2-02
- **Title:** Web realtime shopping-list synchronisation and reconnect handling
- **Milestone:** M2 — Shared and Real-Time Experience
- **Area:** Web
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M2-01, M1-08
- **Outcome:** Two household members see list changes without refresh.
- **Scope:** SignalR client integration, cache updates, reconnect state rehydration, and stale event handling.
- **Acceptance criteria:**
  1. Remote list changes appear automatically.
  2. Reconnect path restores current server state.
  3. Stale or out-of-order events do not corrupt UI state.
- **Test expectations:** Component/integration tests for realtime state updates and reconnect path.
- **Out of scope:** Offline-first sync.
- **Suggested branch name:** `feat/m2-web-realtime-shopping-sync`

### M2-03
- **Title:** Meal feedback vertical slice (API + web)
- **Milestone:** M2 — Shared and Real-Time Experience
- **Area:** Web
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M1-06, M1-08
- **Outcome:** Users can rate meals and store notes after cooking.
- **Scope:** Feedback create/list API plus web UI for rating and note capture tied to planned meals.
- **Acceptance criteria:**
  1. Rating and notes can be created and viewed per meal/recipe.
  2. Invalid rating input returns clear validation errors.
  3. Feedback is household-scoped and queryable by date/recipe.
- **Test expectations:** Unit tests for rating rules, integration tests for lifecycle, and component tests for form errors.
- **Out of scope:** Auto-generated recommendations.
- **Suggested branch name:** `feat/m2-meal-feedback-vertical-slice`

### M2-04
- **Title:** Learning slice: Playwright two-user collaboration journey
- **Milestone:** M2 — Shared and Real-Time Experience
- **Area:** Tests
- **Type:** Task
- **Suggested size:** M
- **Dependencies:** M2-02
- **Outcome:** End-to-end confidence for the key shared shopping-list scenario.
- **Scope:** Add one Playwright flow with two browser contexts validating realtime collaboration and basic error recovery.
- **Acceptance criteria:**
  1. Two-user scenario runs locally and in CI.
  2. Test covers one reconnect/error state path.
  3. Fixture setup is reusable for future E2E tests.
- **Test expectations:** Stable Playwright smoke test with documented retry policy.
- **Out of scope:** Full E2E coverage across every feature.
- **Suggested branch name:** `test/m2-playwright-two-user-flow`

---

## M3 — Background Jobs and Long-Running Work

### M3-01
- **Title:** Learning slice: simple hosted worker lifecycle
- **Milestone:** M3 — Background Jobs and Long-Running Work
- **Area:** Worker
- **Type:** Task
- **Suggested size:** S
- **Dependencies:** M0-02
- **Outcome:** Team understands worker startup/shutdown cadence and cancellation behaviour.
- **Scope:** Add minimal `BackgroundService` loop with structured lifecycle logs.
- **Acceptance criteria:**
  1. Worker starts and stops cleanly.
  2. Cancellation token is respected.
  3. Loop cadence is visible in logs.
- **Test expectations:** Worker lifecycle smoke test.
- **Out of scope:** Persisted jobs.
- **Suggested branch name:** `task/m3-hosted-worker-lifecycle`

### M3-02
- **Title:** Persisted job model with enqueue and status APIs
- **Milestone:** M3 — Background Jobs and Long-Running Work
- **Area:** API
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M3-01
- **Outcome:** Long-running work has durable, queryable state.
- **Scope:** Job schema, enqueue endpoint, and status endpoint for queued/running/succeeded/failed/cancelled states.
- **Acceptance criteria:**
  1. Jobs persist across restarts.
  2. API can enqueue and retrieve status history.
  3. Invalid status transitions are prevented.
- **Test expectations:** Integration tests for enqueue and transition rules.
- **Out of scope:** Retry and locking logic.
- **Suggested branch name:** `feat/m3-persisted-jobs-api`

### M3-03
- **Title:** Worker job claiming with lease-based locking
- **Milestone:** M3 — Background Jobs and Long-Running Work
- **Area:** Worker
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M3-02
- **Outcome:** Jobs are safely claimed by one worker at a time.
- **Scope:** Poll/claim flow with lease ownership and lease timeout recovery.
- **Acceptance criteria:**
  1. Concurrent workers do not process the same job simultaneously.
  2. Expired leases can be recovered safely.
  3. Claiming behaviour is observable in status/history.
- **Test expectations:** Integration tests for contention and lease-expiry recovery.
- **Out of scope:** Retry policy tuning.
- **Suggested branch name:** `feat/m3-worker-lease-locking`

### M3-04
- **Title:** Retry policy and attempt history for failed jobs
- **Milestone:** M3 — Background Jobs and Long-Running Work
- **Area:** Worker
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M3-03
- **Outcome:** Transient failures are retried predictably with clear diagnostics.
- **Scope:** Bounded retry count, backoff strategy, and attempt/error history persistence.
- **Acceptance criteria:**
  1. Retryable failures reschedule correctly.
  2. Permanent failures transition to terminal failed state.
  3. Attempt history captures error context per run.
- **Test expectations:** Integration tests for retry scheduling and terminal failure behaviour.
- **Out of scope:** Idempotency semantics.
- **Suggested branch name:** `feat/m3-job-retries-and-history`

### M3-05
- **Title:** Idempotency and cancellation support for long-running jobs
- **Milestone:** M3 — Background Jobs and Long-Running Work
- **Area:** Worker
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M3-04
- **Outcome:** Replays and user cancellations are safe and predictable.
- **Scope:** Idempotency keys/guards, cancellation request API, and cooperative cancellation checks in worker handlers.
- **Acceptance criteria:**
  1. Re-processing the same job does not duplicate side effects.
  2. Cancellation requests move jobs to cancelled state cleanly.
  3. Cancellation and idempotency outcomes are visible in job history.
- **Test expectations:** Integration tests for duplicate execution and cancellation paths.
- **Out of scope:** Distributed queue integration.
- **Suggested branch name:** `feat/m3-job-idempotency-cancellation`

### M3-06
- **Title:** Outbox pattern foundation (non-blocking for MVP and imports)
- **Milestone:** M3 — Background Jobs and Long-Running Work
- **Area:** Worker
- **Type:** Task
- **Suggested size:** M
- **Dependencies:** M3-04
- **Outcome:** Reliable event hand-off pattern is introduced without forcing early infrastructure changes.
- **Scope:** Outbox table, atomic write in transaction, dispatcher loop, and delivery state tracking.
- **Acceptance criteria:**
  1. Domain write and outbox write commit atomically.
  2. Dispatcher retries undelivered outbox records.
  3. Duplicate dispatches are safely handled.
- **Test expectations:** Integration tests for atomic commit and eventual dispatch.
- **Out of scope:** Kafka/RabbitMQ adoption.
- **Suggested branch name:** `task/m3-outbox-foundation`

---

## M4 — Recipe Imports

### M4-01
- **Title:** Recipe import request API and enqueue flow
- **Milestone:** M4 — Recipe Imports
- **Area:** API
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M3-02
- **Outcome:** Users can request asynchronous recipe imports by URL.
- **Scope:** Import request endpoint, URL validation, and job enqueue response with status reference.
- **Acceptance criteria:**
  1. Valid import requests enqueue jobs.
  2. Invalid requests return clear validation errors.
  3. API returns job identifier and status endpoint reference.
- **Test expectations:** Integration tests for enqueue success and validation failures.
- **Out of scope:** Fetching/parsing recipe source content.
- **Suggested branch name:** `feat/m4-recipe-import-request-api`

### M4-02
- **Title:** Import worker handler (minimal parser) with draft persistence
- **Milestone:** M4 — Recipe Imports
- **Area:** Worker
- **Type:** Feature
- **Suggested size:** L
- **Dependencies:** M4-01, M3-05
- **Outcome:** Import jobs produce draft recipes with robust failure handling.
- **Scope:** Fetch content, parse a constrained first format set, map into draft recipe model, and persist progress/errors.
- **Acceptance criteria:**
  1. Successful imports create household-scoped draft recipes.
  2. Transient and permanent failures are distinguished.
  3. Handler remains idempotent across retries.
- **Test expectations:** Integration tests for success, transient failure, permanent parse failure, and duplicate execution.
- **Out of scope:** Broad multi-site scraping support.
- **Suggested branch name:** `feat/m4-import-worker-minimal-parser`

### M4-03
- **Title:** Import status and draft review web flow
- **Milestone:** M4 — Recipe Imports
- **Area:** Web
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M4-01, M4-02
- **Outcome:** Users can track imports and safely decide what to keep.
- **Scope:** Job status UI, failed/cancelled/retry states, and draft review/edit/accept interactions.
- **Acceptance criteria:**
  1. Status progression is visible (queued/running/succeeded/failed/cancelled).
  2. Failed imports show actionable error information.
  3. Draft can be reviewed before becoming a normal recipe.
- **Test expectations:** Component tests for status/error state rendering and draft review actions.
- **Out of scope:** Bulk import tooling.
- **Suggested branch name:** `feat/m4-import-status-draft-review-web`

---

## M5 — Local AI Suggestions

### M5-01
- **Title:** Optional LM Studio integration with feature toggle
- **Milestone:** M5 — Local AI Suggestions
- **Area:** API
- **Type:** Task
- **Suggested size:** M
- **Dependencies:** M1-08, M2-03
- **Outcome:** AI can be enabled deliberately without affecting core product flows.
- **Scope:** LM Studio client adapter, timeout handling, configuration validation, and disabled-by-default feature toggle.
- **Acceptance criteria:**
  1. Core app behaviour is unchanged when AI is disabled.
  2. AI connectivity/config errors are surfaced clearly.
  3. Integration follows local OpenAI-compatible API contract.
- **Test expectations:** Integration tests for enabled/disabled/error paths.
- **Out of scope:** Auto-triggering AI suggestions.
- **Suggested branch name:** `task/m5-lmstudio-optional-adapter`

### M5-02
- **Title:** Generate AI meal-plan suggestion drafts with strict schema validation
- **Milestone:** M5 — Local AI Suggestions
- **Area:** API
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M5-01
- **Outcome:** AI output becomes safe draft data, not direct meal-plan mutations.
- **Scope:** Prompt contract, schema validation, output normalisation, and draft persistence with audit metadata.
- **Acceptance criteria:**
  1. Invalid AI output is rejected and logged safely.
  2. Valid output is stored as pending-approval draft.
  3. Audit metadata captures model and generation context.
- **Test expectations:** Unit tests for validation + integration tests for draft persistence.
- **Out of scope:** Automatic meal-plan updates.
- **Suggested branch name:** `feat/m5-ai-draft-generation-api`

### M5-03
- **Title:** AI draft review, approve, and reject web flow
- **Milestone:** M5 — Local AI Suggestions
- **Area:** Web
- **Type:** Feature
- **Suggested size:** M
- **Dependencies:** M5-02, M1-08
- **Outcome:** User approval remains the gate before any AI change is applied.
- **Scope:** Draft review UI, diff-like suggestion presentation, approve/reject actions, and clear failure messaging.
- **Acceptance criteria:**
  1. Draft suggestions are visible and understandable.
  2. Approve applies changes; reject keeps current plan intact.
  3. User-facing errors are shown for validation/apply failures.
- **Test expectations:** Component tests for approve/reject/error states.
- **Out of scope:** Conversational AI assistant UI.
- **Suggested branch name:** `feat/m5-ai-draft-approval-web`

---

## M6 — Quality, Observability and Polish

### M6-01
- **Title:** Observability hardening for API, worker, imports, and AI flows
- **Milestone:** M6 — Quality, Observability and Polish
- **Area:** Infrastructure
- **Type:** Chore
- **Suggested size:** M
- **Dependencies:** M4-03, M5-03
- **Outcome:** Runtime behaviour and failures are diagnosable end-to-end.
- **Scope:** Improve structured log fields, correlation across request/job flows, and readiness/liveness checks for worker-enabled deployments.
- **Acceptance criteria:**
  1. Request-to-job correlation is traceable in logs.
  2. Health checks cover API, DB, and worker readiness.
  3. Import and AI failure modes emit consistent operational signals.
- **Test expectations:** Integration checks for health endpoints and representative failure logging paths.
- **Out of scope:** Full observability stack rollout.
- **Suggested branch name:** `chore/m6-observability-hardening`

### M6-02
- **Title:** Quality hardening: targeted regressions, accessibility baseline, and CI stability
- **Milestone:** M6 — Quality, Observability and Polish
- **Area:** Tests
- **Type:** Task
- **Suggested size:** M
- **Dependencies:** M2-04, M6-01
- **Outcome:** Release confidence improves without chasing exhaustive coverage.
- **Scope:** Add targeted regression tests for core journeys, accessibility checks for planner/shopping/recipe screens, and CI flaky-test troubleshooting runbook.
- **Acceptance criteria:**
  1. Core user journeys have stable automated regression coverage.
  2. Accessibility checks pass for key MVP screens.
  3. CI flake handling guidance is documented.
- **Test expectations:** Targeted integration + Playwright smoke run in CI.
- **Out of scope:** 100% coverage goals.
- **Suggested branch name:** `test/m6-quality-regression-a11y-ci`

