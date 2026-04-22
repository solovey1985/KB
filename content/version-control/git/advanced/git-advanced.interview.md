---
title: Git Advanced Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise Git workflow and branching-strategy questions.

Relevant concept maps:

- [Concept Map](git-advanced.concept.md)

## Branching Strategies

```interview-question
What is trunk-based development and when is it useful?
---
answer:
Trunk-based development centers on one main integration branch with short-lived feature work and frequent integration.

It is useful for teams with strong CI/CD practices that want small merges and frequent delivery.
hints:
- One long-lived main branch.
- Short-lived branches.
- CI/CD maturity matters.
```

Related concepts: [Trunk-Based Development](git-advanced.concept.md#trunk-based-development), [Short-Lived Branches](git-advanced.concept.md#short-lived-branches)

```interview-question
When would a team choose Git Flow over GitHub Flow?
---
answer:
Git Flow is more suitable when the team has formal release management, separate development and production readiness stages, and possibly multiple supported release lines.

GitHub Flow is lighter and fits faster continuous-delivery environments better.
hints:
- Scheduled releases are a clue.
- Extra branch structure is part of Git Flow.
- Simplicity is the clue for GitHub Flow.
```

Related concepts: [Git Flow](git-advanced.concept.md#git-flow), [GitHub Flow](git-advanced.concept.md#github-flow)

```interview-choice
Which workflow is usually the lightest and most optimized for frequent deployment from `main`?
---
options:
- Git Flow
- GitHub Flow
- Long-lived release-only branching with no PRs
correct: 1
explanation:
GitHub Flow is the lightweight main-plus-PR model commonly used for continuous deployment oriented teams.
```
