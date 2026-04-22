---
title: Architecture Analysis and Evaluation Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers the vocabulary for assessing architectural quality and drift.

Study pages: [Section Index](index.md) | [Material Notes](software-architecture-analysis-evaluation.md) | [Interview Practice](software-architecture-analysis-evaluation.interview.md)

## Evaluation Map

```concept-card
id: architecture-evaluation
term: Architecture Evaluation
children:
- atam
- architectural-fitness-function
summary:
Architecture evaluation is the process of judging whether the system structure and decisions support the required quality attributes.
details:
It uses scenarios, trade-off analysis, and evidence instead of relying only on intuition or aesthetics.
example:
Reviewing whether an event-driven design actually satisfies throughput and diagnosability requirements is architecture evaluation.
mnemonic:
Judge by scenarios, not slogans.
recall:
- Why is architecture evaluation scenario-driven?
- What risks appear when architecture is never reevaluated?
```

```concept-card
id: atam
term: ATAM
parents:
- architecture-evaluation
summary:
ATAM is the Architecture Tradeoff Analysis Method, a structured approach for evaluating architecture against quality scenarios.
details:
It helps teams surface trade-offs, sensitivity points, and risks in a more explicit and repeatable way.
example:
A team uses ATAM to compare how two architectures handle throughput, reliability, and changeability scenarios.
mnemonic:
Make trade-offs explicit.
recall:
- What kinds of outputs does ATAM produce?
- Why is ATAM useful when multiple quality attributes conflict?
```

```concept-card
id: architectural-fitness-function
term: Architectural Fitness Function
parents:
- architecture-evaluation
summary:
An architectural fitness function is a repeatable check that verifies whether the system still matches desired architectural characteristics.
details:
Fitness functions can be automated or manual, but they are most useful when they run continuously and detect drift early.
example:
A test that fails if the domain layer imports infrastructure code is a fitness function.
mnemonic:
Keep checking the intended shape.
recall:
- Why are fitness functions helpful after the initial design phase?
- Which architectural rules are good candidates for automation?
```
