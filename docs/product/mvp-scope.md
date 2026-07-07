# MVP scope

## Scope intent

Ship a small but genuinely useful first release for a two-person household before advanced platform features.

## In MVP

1. Household creation and two-member membership.
2. Recipe management: ingredients, servings, tags, favourites.
3. Weekly meal plan (initially dinner-focused).
4. Combined shopping list generated from the meal plan.
5. Real-time shared shopping list updates.
6. Recipe ratings and meal notes.

## Later (post-MVP)

1. Recipe URL import as long-running background jobs.
2. Pantry tracking.
3. Weekly reminders.
4. AI-generated meal-plan suggestions (draft + approval flow).
5. Job progress UI with retries and cancellation controls.
6. Optional recipe photos/file storage.

## Explicitly out of scope for now

- Microservices decomposition.
- Redis/RabbitMQ/Kafka by default.
- Kubernetes and advanced platform orchestration.
- Event sourcing.
- Vector databases or search clusters without a proven need.

## MVP user journeys

1. Create household and invite partner.
2. Add recipes and mark favourites.
3. Build weekly plan.
4. Auto-refresh shared shopping list from plan changes.
5. Both users tick off list items live.
6. Leave rating and note after cooking.

