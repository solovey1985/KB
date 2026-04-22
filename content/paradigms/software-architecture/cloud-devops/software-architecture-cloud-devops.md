# Cloud Computing and DevOps

Cloud and DevOps practices do not just change deployment tooling. They influence architecture boundaries, delivery safety, and operational feedback loops.

## Cloud impact on architecture

Cloud platforms encourage architectures that are:

- automatable
- observable
- elastic
- resilient to instance failure

This often pushes systems toward immutable infrastructure, externalized state, and explicit runtime configuration.

## Infrastructure as Code

Infrastructure as Code, or IaC, means describing infrastructure and platform configuration in versioned code rather than manual steps.

Architecturally, IaC matters because reliable systems depend on reproducible environments, not just correct application code.

## CI/CD and architecture

Continuous integration and continuous delivery influence architecture by rewarding:

- smaller deployable changes
- better test isolation
- strong contract boundaries
- safer rollback paths

Architectures that are hard to test, package, or deploy independently often slow delivery regardless of feature quality.

## Deployment strategies

Common release strategies include:

- rolling deployment
- blue-green deployment
- canary deployment
- feature flags

Each strategy trades off speed, cost, risk, and rollback simplicity.

## Containers and orchestration

Containers help standardize packaging and runtime behavior.

Orchestration platforms such as Kubernetes add scaling, scheduling, health checks, and service discovery.

These capabilities can support architecture, but they do not automatically fix weak service boundaries or poor operational design.

## Observability and delivery feedback

Cloud-native systems rely on metrics, logs, traces, and deployment telemetry to detect regressions quickly.

Without observability, automated delivery increases deployment speed without increasing safety.

## Interview reminders

- cloud changes architecture through elasticity and failure assumptions
- IaC supports repeatability and safer environment management
- CI/CD works best when architecture allows isolated testing and deployment
- release strategies are architecture-adjacent because they shape risk and rollback
- containerization is packaging, not architecture by itself
