---
title: Architecture Analysis and Evaluation Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise architecture questions about quality assessment and drift detection.

Relevant concept maps:

- [Concept Map](software-architecture-analysis-evaluation.concept.md)

## Evaluation

```interview-question
How do you assess the quality of a software architecture?
---
answer:
Architecture quality is assessed by checking how well it supports the most important quality attributes, business constraints, and operational scenarios.

This usually involves scenario-based reviews, risk analysis, trade-off discussion, and evidence from tests, telemetry, or architecture checks.
hints:
- Think scenarios and quality attributes.
- Evidence matters.
- Trade-offs should appear in the answer.
```

```interview-question
What is ATAM?
---
answer:
ATAM is the Architecture Tradeoff Analysis Method, a structured approach for evaluating architecture against quality attribute scenarios.

It helps identify risks, sensitivity points, and trade-offs between competing quality goals.
hints:
- It is a structured analysis method.
- Quality scenarios are central.
- Trade-offs and risks are key outputs.
```

```interview-question
What are architectural fitness functions?
---
answer:
Architectural fitness functions are repeatable checks that verify whether the system still conforms to desired architectural characteristics or rules.

They help detect architecture drift early, especially when automated in normal delivery pipelines.
hints:
- Repeatable checks.
- Drift detection is important.
- Automation is often useful.
```
