---
title: Microservices Production Operations Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse production operations and troubleshooting scenarios for microservices.

Relevant study pages:

- [Material Notes](microservices-production-operations.md)
- [Concept Map](microservices-production-operations.concept.md)

## Observability

```interview-question
What changes when microservices move from development into production operations?
---
answer:
The main challenge shifts from building features to operating a distributed system safely. Teams now need observability, deployment discipline, rollback plans, health probes, and repeatable incident response.

Production success depends on how quickly the team can detect issues, narrow blast radius, and stabilize the system under real traffic.
hints:
- Think beyond coding.
- The problem becomes operating many boundaries.
- Detection and recovery matter as much as implementation.
```

```interview-question
How do you monitor health and performance in a microservices environment?
---
answer:
Use a combination of metrics, structured logs, and distributed traces. Metrics show rates, errors, latency, saturation, queue depth, and lag. Logs provide local details. Traces reveal the slow or failing hop across services.

The important point is that no single telemetry type is enough in a distributed system.
hints:
- Use three telemetry types together.
- Mention error rate and latency explicitly.
- Traces matter because requests cross boundaries.
```

Related concepts: [Observability](microservices-production-operations.concept.md#observability), [Metrics and Alerting](microservices-production-operations.concept.md#metrics-and-alerting), [Distributed Tracing](microservices-production-operations.concept.md#distributed-tracing)

```interview-question
Why are correlation IDs and distributed tracing important in production troubleshooting?
---
answer:
They let you follow one request or workflow across multiple services. Without correlation, logs from different services are disconnected and incident diagnosis becomes much slower.

Distributed tracing is especially useful for locating which downstream hop added latency or failed the request.
hints:
- One user request becomes many service calls.
- Correlation turns separate logs into one story.
- Tracing helps localize the failing hop.
```

## Health And Releases

```interview-question
What is the difference between liveness, readiness, and startup probes?
---
answer:
Liveness answers whether the process should be restarted. Readiness answers whether the instance should receive traffic right now. Startup answers whether the application is still booting and should not yet be judged by normal health timing.

Using them correctly prevents restart loops and reduces failed requests during warm-up and deployments.
hints:
- One is about restarts.
- One is about traffic.
- One protects slow boot sequences.
```

Related concepts: [Health Probes](microservices-production-operations.concept.md#health-probes)

```interview-choice
Which probe should usually stop traffic during a temporary downstream dependency issue without forcing constant process restarts?
---
options:
- Liveness probe
- Readiness probe
- Startup probe only
correct: 1
explanation:
Readiness is usually the right signal for temporarily removing an instance from traffic. Liveness should be used more carefully because failing it can trigger restart loops.
```

```interview-question
How do canary and blue-green deployments differ?
---
answer:
Canary releases expose a small percentage of live traffic to the new version first, then expand if metrics stay healthy. Blue-green deployments keep two full environments and switch traffic between them.

Canary gives gradual real-traffic feedback. Blue-green gives faster rollback but usually costs more infrastructure.
hints:
- One is gradual traffic shift.
- One is a full environment switch.
- Rollback speed is a key distinction.
```

Related concepts: [Canary Release](microservices-production-operations.concept.md#canary-release), [Blue-Green Deployment](microservices-production-operations.concept.md#blue-green-deployment)

```interview-question
What makes rollback harder in microservices than simply restoring an older image?
---
answer:
Rollback can be blocked or complicated by schema changes, contract mismatches, configuration changes, message format changes, and workflows already in flight.

That is why safe releases depend on backward compatibility and disciplined deployment sequencing, not only on container versioning.
hints:
- Think beyond the application binary.
- Data and contracts are often the hard part.
- In-flight work also matters.
```

Related concepts: [Rollback Strategy](microservices-production-operations.concept.md#rollback-strategy)

## Troubleshooting

```interview-question
How would you troubleshoot a sudden latency spike across one user journey, such as checkout?
---
answer:
First define the blast radius and confirm when the latency started. Then inspect request metrics, follow one representative trace through the path, and identify the slow hop. After that, inspect structured logs and recent deploy or config changes for the affected services.

The goal is to narrow the problem from the whole workflow to one dependency, one rollout, or one capacity bottleneck.
hints:
- Start with scope and timing.
- Use traces to find the slow hop.
- Then compare with recent change history.
```

Related concepts: [Troubleshooting Workflow](microservices-production-operations.concept.md#troubleshooting-workflow)

```interview-question
A service looks healthy locally, but users still see failures. What would you check next?
---
answer:
Check upstream routing, downstream dependencies, readiness state, recent deploys, and end-to-end traces. A service can be alive and even locally healthy while still failing the full user flow because a dependency or contract path is broken.

End-user success should be validated through the full path, not from one local health endpoint alone.
hints:
- Local health is not the same as end-to-end success.
- Think upstream and downstream.
- Traces are useful here.
```

```interview-question
How would you investigate growing consumer lag in an event-driven microservice?
---
answer:
Check whether traffic volume increased, whether handler latency changed, whether a downstream dependency is slowing consumers, whether poison messages are being retried repeatedly, and whether scaling is limited by partitions or concurrency settings.

Consumer lag is often a symptom of throughput mismatch rather than a broker problem alone.
hints:
- Think producer rate versus consumer throughput.
- Slow handlers and retries are common causes.
- Partition and concurrency limits matter.
```

```interview-question
What should a good production runbook contain for a microservice?
---
answer:
A good runbook should include service purpose, ownership, dashboards, alerts, dependencies, deploy and rollback steps, common failure modes, first mitigation steps, and escalation paths.

Its job is to reduce guesswork during incidents and help responders make safe decisions under pressure.
hints:
- Owners and dashboards are basic requirements.
- Mitigation steps matter, not just description.
- Escalation should be explicit.
```

Related concepts: [Runbook](microservices-production-operations.concept.md#runbook)
