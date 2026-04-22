# Architecture Analysis and Evaluation

Good architecture is not only explained. It is evaluated against explicit goals, scenarios, and trade-offs.

## Assessing architecture quality

Architecture quality is usually assessed through:

- alignment with important quality attributes
- risk identification
- trade-off analysis
- operability and maintainability evidence
- scenario-based review

## ATAM

The Architecture Tradeoff Analysis Method, or ATAM, is a structured way to evaluate an architecture against scenarios and competing quality attributes.

It helps reveal:

- sensitivity points
- trade-offs
- risks
- non-risks

## Fitness functions

Architectural fitness functions are automated or repeatable checks that verify whether a system still matches desired architectural characteristics.

Examples:

- no forbidden dependency directions
- service startup time limits
- API latency budgets
- test coverage on critical contracts

## Why evaluation matters

Architectures drift over time.

Evaluation helps teams detect when the real system no longer matches the intended structure or quality goals.

## Interview reminders

- architecture quality should be evaluated with scenarios, not only opinion
- ATAM makes trade-offs explicit
- fitness functions help keep architecture honest over time
- evaluation is useful because architecture tends to drift under delivery pressure
