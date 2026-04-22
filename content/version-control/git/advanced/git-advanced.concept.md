---
title: Git Advanced Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes Git workflow strategy concepts around team process and release structure.

Study pages: [Section Index](index.md) | [Material Notes](git-advanced.md) | [Interview Practice](git-advanced.interview.md)

## Advanced Map

```concept-card
id: branching-strategy
term: Branching Strategy
children:
- trunk-based-development
- git-flow
- github-flow
- short-lived-branches
summary:
A branching strategy is the workflow a team uses to organize integration, review, releases, and hotfixes.
details:
The best strategy depends on release cadence, team coordination, and CI/CD maturity rather than on universal preference.
example:
One team may ship from `main` many times a day, while another may require staged release branches and hotfix lines.
mnemonic:
Choose the workflow that fits the release model.
recall:
- Why is workflow choice context-dependent?
- Which team factors matter most when choosing a strategy?
```

```concept-card
id: trunk-based-development
term: Trunk-Based Development
parents:
- branching-strategy
related:
- short-lived-branches
summary:
Trunk-based development centers on one main integration branch and short-lived feature work.
details:
It reduces long-running divergence and supports frequent integration and delivery.
example:
Developers branch briefly from `main`, merge back quickly, and rely on strong automated testing.
mnemonic:
Integrate often, branch briefly.
recall:
- Why does trunk-based development reduce merge pain?
- What team capability does it depend on heavily?
```

```concept-card
id: git-flow
term: Git Flow
parents:
- branching-strategy
summary:
Git Flow is a structured branching model with dedicated branches for development, releases, features, and hotfixes.
details:
It is helpful when release management is formal, but it adds more process and branch coordination overhead.
example:
`main`, `develop`, `feature/*`, `release/*`, and `hotfix/*`
mnemonic:
More branch structure, more release ceremony.
recall:
- What kinds of projects fit Git Flow well?
- What overhead does Git Flow introduce?
```

```concept-card
id: github-flow
term: GitHub Flow
parents:
- branching-strategy
summary:
GitHub Flow is a lightweight workflow built around `main`, short-lived branches, pull requests, and fast integration.
details:
It suits teams that deploy frequently and want simple collaboration rules.
example:
Create a branch, open a PR, review and test it, then merge into `main`.
mnemonic:
Branch, review, merge, ship.
recall:
- Why is GitHub Flow considered lightweight?
- Which team habits make it work well?
```

```concept-card
id: short-lived-branches
term: Short-Lived Branches
parents:
- branching-strategy
related:
- trunk-based-development
summary:
Short-lived branches reduce drift and make integration easier by limiting how long work diverges from the main branch.
details:
They are a practical way to reduce merge conflict frequency and integration surprise.
example:
A feature branch that lives for a few hours or one day is usually easier to integrate than one that lives for weeks.
mnemonic:
The shorter the branch lives, the less history fights back.
recall:
- Why do short-lived branches reduce integration pain?
- What risks grow when branches live too long?
```
