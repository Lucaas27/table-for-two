# Domain glossary

## Core terms

- **Household**: The shared workspace for exactly two active members in MVP.
- **Member**: A user belonging to a household with permissions to plan and shop.
- **Recipe**: A reusable cooking definition with ingredients, servings, and tags.
- **Ingredient**: A named item in a recipe, with quantity and unit text.
- **Serving**: The baseline portion count used for scaling recipe quantities.
- **Tag**: A label for filtering/grouping recipes (for example, quick, vegetarian).
- **Favourite**: A member-level or household-level preference marker for recipes.

## Planning terms

- **Meal Plan Week**: A household week view with planned meals by day/slot.
- **Meal Slot**: A time bucket (MVP assumption: dinner only).
- **Planned Meal**: A recipe assigned to a day/slot, with target servings.

## Shopping terms

- **Shopping List**: Household list derived from planned meals plus manual edits.
- **Shopping Item**: A single list line with description, quantity text, and state.
- **Shopping Item State**: Pending or completed.
- **Refresh**: Rebuilding list entries when meal plan changes.

## Feedback and automation

- **Meal Feedback**: Rating and note captured after a meal is cooked.
- **Background Job**: Long-running asynchronous task processed outside request flow.
- **Job Attempt**: One execution try for a job.
- **Idempotency**: Safe re-execution without duplicate side effects.
- **AI Suggestion Draft**: Proposed plan content from AI, stored but not auto-applied.
- **Approval**: Explicit user confirmation before a draft changes meal plans.

