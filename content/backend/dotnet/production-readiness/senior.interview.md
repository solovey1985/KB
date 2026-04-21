---
title: Production Readiness Senior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the senior-level production readiness scenarios from the Web API interview question set.

Relevant concept maps:

- [Operations and Deployment Concept Map](production-operations-deployment.concept.md)
- [Observability and Runtime Concept Map](production-observability-runtime.concept.md)

## Structured Logging

```interview-question
How do you implement structured logging in a .NET API? Why is it better than string-based logging?
---
answer:
Use structured logging with message templates so important values become searchable log properties instead of only text inside a log line.

This makes log querying, correlation, and alerting much easier in production.

String interpolation may be readable locally, but it is much weaker for observability tooling.
hints:
- Think searchable properties, not just printed text.
- Queryability is the key advantage.
- Production analysis is easier when fields are structured.
```

Related concepts: [Structured Logging](production-observability-runtime.concept.md#structured-logging)

## Graceful Shutdown

```interview-question
Your API runs in Kubernetes with multiple replicas. How do you handle graceful shutdown when a pod is terminated?
---
answer:
The app should stop taking new work, finish in-flight requests where possible, and shut down background processing cleanly before the pod is killed.

This requires coordination between ASP.NET Core shutdown handling and Kubernetes termination settings such as grace periods and pre-stop behavior.

Without graceful shutdown, in-flight requests can fail unnecessarily during deploys or scaling events.
hints:
- SIGTERM is part of the story.
- The app needs time to finish cleanly.
- Infrastructure and app settings must align.
```

Related concepts: [Graceful Shutdown](production-operations-deployment.concept.md#graceful-shutdown)

## Production Monitoring

```interview-question
How do you monitor your API in production? What metrics and alerts do you set up?
---
answer:
Use the three pillars of observability: logs, metrics, and traces.

At minimum, track latency, error rate, request rate, memory usage, CPU, and downstream dependency health. Alerts should be tied to user-impacting thresholds rather than just raw noise.

The goal is to know about production problems before users report them.
hints:
- Metrics alone are not enough.
- Alerts should reflect service health and user impact.
- Observability should be proactive, not reactive.
```

Related concepts: [Observability Pillars](production-observability-runtime.concept.md#observability-pillars), [Alerting Strategy](production-observability-runtime.concept.md#alerting-strategy)

## Rollback

```interview-question
You deploy a new version and error rates spike. How do you handle rollback?
---
answer:
First confirm the spike correlates with the deployment, assess blast radius, and roll back quickly if the impact is significant.

Rollback is easier when deployments, images, and migrations are designed for reversibility or forward compatibility.

Teams should prefer blameless rollback discipline over trying to debug a serious incident live on broken production traffic.
hints:
- Confirm the timeline.
- Know when to stop debugging and undo the change.
- Database changes make rollback strategy more complex.
```

Related concepts: [Rollback Strategy](production-operations-deployment.concept.md#rollback-strategy)
