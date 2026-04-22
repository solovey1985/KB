---
title: Cloud Computing and DevOps Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page focuses on delivery and operational concepts that shape architecture in cloud environments.

Study pages: [Section Index](index.md) | [Material Notes](software-architecture-cloud-devops.md) | [Interview Practice](software-architecture-cloud-devops.interview.md)

## Cloud Map

```concept-card
id: cloud-native-architecture
term: Cloud-Native Architecture
children:
- infrastructure-as-code
- cicd
- blue-green-deployment
summary:
Cloud-native architecture assumes automation, elasticity, observability, and failure-tolerant runtime behavior.
details:
It usually prefers disposable instances, externalized state, and repeatable platform setup over hand-managed environments.
example:
A service deployed through pipelines to containerized infrastructure with health checks and autoscaling reflects cloud-native architecture.
mnemonic:
Automate, observe, and expect failure.
recall:
- How does cloud hosting change system assumptions?
- Why is disposability useful in cloud environments?
```

```concept-card
id: infrastructure-as-code
term: Infrastructure as Code
parents:
- cloud-native-architecture
summary:
Infrastructure as Code represents platform resources and configuration in versioned code.
details:
It improves repeatability, reviewability, and recovery because environments can be recreated rather than manually rebuilt.
example:
Terraform definitions for networks, databases, and Kubernetes clusters are IaC.
mnemonic:
Version the platform, not just the app.
recall:
- Why does IaC matter for architecture, not only operations?
- What risks come from hand-configured environments?
```

```concept-card
id: cicd
term: CI/CD
parents:
- cloud-native-architecture
summary:
CI/CD is the practice of continuously integrating changes and delivering them through automated pipelines.
details:
Architectures that support good CI/CD are easier to test, package, and release safely in small increments.
example:
A pipeline that runs tests, builds containers, and deploys only changed services supports CI/CD.
mnemonic:
Small changes, automated path.
recall:
- How can architecture block effective CI/CD?
- Why do smaller deployable units help release safety?
```

```concept-card
id: blue-green-deployment
term: Blue-Green Deployment
parents:
- cloud-native-architecture
summary:
Blue-green deployment runs old and new environments side by side so traffic can switch with low downtime.
details:
It simplifies rollback because the previous version remains intact, but it can increase cost and require careful data compatibility planning.
example:
Traffic is switched from the blue environment to the green one after health verification.
mnemonic:
Two environments, one clean switch.
recall:
- Why is blue-green useful for rollback?
- What data migration risks still remain?
```
