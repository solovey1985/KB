# Git Advanced

Advanced Git discussions are usually about team process rather than only command syntax.

## Branching strategies

Common strategies include:

- trunk-based development
- Git Flow
- lightweight main-plus-PR workflows such as GitHub Flow

## Trunk-based development

Trunk-based development keeps one main line of integration with short-lived branches.

Benefits:

- smaller merges
- fewer long-running conflicts
- easier continuous delivery

## Git Flow

Git Flow adds more structure with `main`, `develop`, `feature/*`, `release/*`, and `hotfix/*` branches.

It helps with scheduled release management, but it also adds process overhead.

## GitHub Flow

GitHub Flow is lighter:

- branch from `main`
- open a pull request
- review and test
- merge back to `main`

## Strategy trade-offs

No workflow is universally best.

Choose based on:

- team size
- release frequency
- CI/CD maturity
- release management needs

## Interview reminders

- talk about trade-offs, not only diagrams
- mention release cadence and CI/CD maturity
- smaller branches usually reduce integration pain
