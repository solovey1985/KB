---
title: Production Readiness Middle Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the mid-level production readiness trade-offs from the Web API interview question set.

Relevant concept maps:

- [Operations and Deployment Concept Map](production-operations-deployment.concept.md)
- [Observability and Runtime Concept Map](production-observability-runtime.concept.md)

## Health Checks

```interview-question
How do you implement health checks in a .NET API? What do you check?
---
answer:
Use ASP.NET Core health checks and separate liveness from readiness.

Liveness answers whether the process is alive. Readiness answers whether the app can currently serve traffic, which often includes database, cache, or downstream dependency checks.

Health checks should stay lightweight and operational, not become full business tests.
hints:
- Liveness and readiness are different.
- Check dependencies that affect serving traffic.
- Keep the checks cheap.
```

Related concepts: [Health Checks](production-observability-runtime.concept.md#health-checks), [Liveness Versus Readiness](production-observability-runtime.concept.md#liveness-versus-readiness)

## Docker

```interview-question
How do you containerize a .NET API with Docker? Walk through your Dockerfile.
---
answer:
Use a multi-stage Dockerfile: build and publish in the SDK image, then copy the output into a smaller runtime image.

The main goals are small runtime images, good layer caching, and not shipping the SDK into production.

Production container images should also avoid running as root when possible.
hints:
- Build stage and runtime stage are separate.
- The runtime image should be smaller.
- Security and caching both matter.
```

Related concepts: [Containerization](production-operations-deployment.concept.md#containerization)

## CI/CD

```interview-question
How do you configure CI/CD for a .NET API using GitHub Actions?
---
answer:
The pipeline should restore, build, test, and then publish artifacts or container images.

Integration tests should run against real dependencies when needed, and deployments should be separate from basic PR validation.

A good pipeline makes builds reproducible, traceable, and safe to promote between environments.
hints:
- Build and test come before deploy.
- Real dependencies can be part of CI.
- Traceability matters for releases.
```

Related concepts: [CI/CD Pipeline](production-operations-deployment.concept.md#cicd-pipeline)

## Logging Abstractions

```interview-question
What is the difference between `ILogger<T>` and `ILoggerFactory`? When do you use each?
---
answer:
`ILogger<T>` is the normal typed logger used by a specific service or class.

`ILoggerFactory` is used when log categories must be created dynamically rather than being tied to a compile-time type.

Most services should use `ILogger<T>`. `ILoggerFactory` is the exception, not the default.
hints:
- One is the common typed case.
- The other is for dynamic categories.
- Category names are part of the difference.
```

Related concepts: [Logging Categories](production-observability-runtime.concept.md#logging-categories)

## Environment Configuration

```interview-question
How do you handle configuration for different environments such as dev, staging, and production?
---
answer:
Use layered configuration: base settings, environment-specific overrides, environment variables, and secret sources where appropriate.

Use options binding and validation so missing critical config fails fast.

Avoid shipping different appsettings files manually as a deployment strategy.
hints:
- Configuration is layered.
- Secrets should not live in normal appsettings files.
- Startup validation matters.
```

Related concepts: [Configuration Layering](production-operations-deployment.concept.md#configuration-layering)
