---
title: Collaboration and Team Dynamics Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page focuses on the people and communication side of architecture work.

Study pages: [Section Index](index.md) | [Material Notes](software-architecture-collaboration-team-dynamics.md) | [Interview Practice](software-architecture-collaboration-team-dynamics.interview.md)

## Collaboration Map

```concept-card
id: architecture-collaboration
term: Architecture Collaboration
children:
- agile-architect-role
- architecture-decision-record
summary:
Architecture collaboration is the practice of aligning technical decisions with the people who must understand, build, and operate them.
details:
Strong architecture work includes communication, negotiation, and shared ownership, not only technical correctness.
example:
An architecture change succeeds more often when engineering, product, and operations all understand its trade-offs.
mnemonic:
Good architecture is shared understanding.
recall:
- Why is communication part of architecture quality?
- What happens when architecture is correct but poorly shared?
```

```concept-card
id: agile-architect-role
term: Agile Architect Role
parents:
- architecture-collaboration
summary:
In agile teams, the architect role helps teams make coherent decisions without blocking delivery through distant centralized control.
details:
This role often involves guiding constraints, clarifying trade-offs, and staying close enough to implementation for feedback to matter.
example:
An architect participates in incremental design decisions and reviews trade-offs as delivery unfolds.
mnemonic:
Guide the team, do not disappear from it.
recall:
- Why can architecture fail in agile teams if it becomes too detached?
- What is the difference between guidance and gatekeeping?
```

```concept-card
id: architecture-decision-record
term: Architecture Decision Record
aliases:
- ADR
parents:
- architecture-collaboration
summary:
An Architecture Decision Record captures a significant decision, its context, the chosen option, and the consequences.
details:
ADRs preserve reasoning and make future change discussions easier because teams can see why a path was chosen.
example:
An ADR might explain why the team chose asynchronous integration instead of synchronous service calls.
mnemonic:
Write down the why.
recall:
- What information should an ADR include?
- Why are ADRs valuable months after the decision was made?
```
