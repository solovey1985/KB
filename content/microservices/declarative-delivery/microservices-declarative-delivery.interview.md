---
title: Declarative Delivery For Microservices Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse infrastructure-as-code and GitOps delivery questions in the context of Kubernetes microservices.

Relevant study pages:

- [Material Notes](microservices-declarative-delivery.md)
- [Concept Map](microservices-declarative-delivery.concept.md)

## Delivery Boundaries

```interview-question
In a Kubernetes microservices platform, where should responsibility split between Terraform, Helm, Argo CD, and Kubernetes itself?
---
answer:
Terraform should usually provision infrastructure and platform prerequisites. Helm should package and parameterize Kubernetes application manifests. Argo CD should reconcile Git-defined application state into the cluster. Kubernetes should run and maintain the workload state at runtime.

The point is to avoid overlapping ownership where every tool tries to manage the same thing.
hints:
- Provisioning, packaging, reconciliation, runtime.
- Each tool owns a different layer.
- Avoid one tool doing everything.
```

Related concepts: [Terraform Provisioning Boundary](microservices-declarative-delivery.concept.md#terraform-provisioning-boundary), [Helm Chart](microservices-declarative-delivery.concept.md#helm-chart), [Argo CD Application](microservices-declarative-delivery.concept.md#argo-cd-application)

```interview-question
Why is GitOps useful for operating many microservices, not just deploying them once?
---
answer:
GitOps makes desired state visible, auditable, and continuously reconciled. That matters for many microservices because drift, inconsistent environments, and undocumented manual fixes become much harder to control as service count grows.

GitOps helps operations by turning deployment state into something reviewable and recoverable, not only something executable.
hints:
- Think auditability and drift.
- The value continues after the initial deploy.
- Many services amplify inconsistency risk.
```

```interview-question
What does desired state mean in Kubernetes and Argo CD, and why does that matter during incidents?
---
answer:
Desired state is the intended configuration recorded in declarative definitions, usually in Git. During incidents, it matters because it gives responders a stable reference point for what the system should look like.

Without that reference, teams argue about whether the live state is correct or accidental.
hints:
- Desired state is the declared target.
- It becomes a reference point during failure.
- Git is usually the intended source of truth.
```

Related concepts: [Desired State](microservices-declarative-delivery.concept.md#desired-state), [Reconciliation Loop](microservices-declarative-delivery.concept.md#reconciliation-loop)

## Drift And Sync

```interview-question
What is configuration drift, and how should a GitOps team respond when live state differs from Git?
---
answer:
Configuration drift is the mismatch between live environment state and declared Git state. A GitOps team should first determine whether the drift was an intentional emergency change or an unsafe manual divergence, then either reconcile back to Git or commit the intended change properly.

The important part is to make the discrepancy explicit instead of letting live state silently become the new normal.
hints:
- Drift is mismatch, not just “something changed.”
- Intentional emergency changes still need follow-up.
- Silent divergence is the real risk.
```

Related concepts: [Drift](microservices-declarative-delivery.concept.md#drift)

```interview-question
Why should teams avoid `kubectl` hotfixes in a GitOps-managed production environment?
---
answer:
Because the hotfix changes live state without updating the declared source of truth. That creates drift, confuses future rollbacks, and often gets overwritten later by the reconciler.

If an emergency live change is necessary, it should be followed immediately by a proper Git change or a deliberate exception process.
hints:
- Live changes bypass the declared source of truth.
- Reconciliation may undo the hotfix later.
- Rollback and audit trails get messy.
```

```interview-question
What does Argo CD mean by `Synced`, `OutOfSync`, `Healthy`, and `Degraded`?
---
answer:
`Synced` means the live manifests match the desired Git state. `OutOfSync` means they differ. `Healthy` means the application resources are in a good runtime state. `Degraded` means the deployed resources are unstable or failing.

The critical nuance is that an app can be `Synced` and still `Degraded`.
hints:
- Sync is about desired versus live manifests.
- Health is about runtime condition.
- The two states are related but not identical.
```

Related concepts: [Argo CD Application](microservices-declarative-delivery.concept.md#argo-cd-application), [Health Assessment](microservices-declarative-delivery.concept.md#health-assessment), [Sync Policy](microservices-declarative-delivery.concept.md#sync-policy)

## Helm And Configuration

```interview-question
What problem does Helm solve in microservice delivery that raw manifests alone do not?
---
answer:
Helm makes manifest packaging, reuse, and parameterization easier. It lets teams keep one consistent application shape while varying values across services or environments without duplicating whole manifest sets.

Its main value is controlled reuse, not complexity for its own sake.
hints:
- Think packaging and parameterization.
- One shape, many environments.
- Avoid describing Helm as the runtime itself.
```

Related concepts: [Helm Chart](microservices-declarative-delivery.concept.md#helm-chart)

```interview-question
What is the difference between a Helm chart, values, and a release?
---
answer:
A chart is the packaged template set. Values are the configuration inputs used to render that chart. A release is one concrete installed instance of the chart with a specific rendered result in an environment.

The same chart can produce different releases in staging and production by using different values.
hints:
- Template set, inputs, installed instance.
- One chart can back many releases.
- Environment differences usually come through values.
```

Related concepts: [Helm Chart](microservices-declarative-delivery.concept.md#helm-chart), [Helm Values Layering](microservices-declarative-delivery.concept.md#helm-values-layering), [Helm Release](microservices-declarative-delivery.concept.md#helm-release)

```interview-question
How would you structure environment-specific configuration without duplicating whole manifests for every microservice?
---
answer:
Keep a reusable chart or manifest base, then layer environment-specific values or overlays in a controlled and documented way. Differences should be intentional and easy to inspect.

If every environment has its own mostly duplicated manifests, review and troubleshooting become much harder.
hints:
- Reuse the base shape.
- Layer differences intentionally.
- Documentation and visibility matter.
```

## Promotion And Safety

```interview-question
How should promotion from staging to production work in a GitOps model for many microservices?
---
answer:
Promotion should move a known-good declared state forward in a controlled way, usually by merging or updating the environment-specific source that production reconciles from. The same tested configuration pattern should be promoted, not re-created by hand.

The goal is consistent promotion, not environment-specific improvisation.
hints:
- Promote declared state, not ad hoc cluster changes.
- Preserve what was already validated.
- Keep the path auditable.
```

Related concepts: [Promotion And Rollback](microservices-declarative-delivery.concept.md#promotion-and-rollback)

```interview-question
Why can rollback be hard even when Git makes it easy to revert a commit?
---
answer:
Because data, schema, contracts, and external dependencies may have changed in ways the old version cannot safely handle. Reverting Git restores declared config, but it does not automatically reverse irreversible runtime or data changes.

That is why backward compatibility and rollout sequencing matter so much.
hints:
- Git rollback is not data rollback.
- Schemas and contracts often make it harder.
- Think runtime compatibility, not only source control.
```

Related concepts: [Promotion And Rollback](microservices-declarative-delivery.concept.md#promotion-and-rollback)

```interview-question
How do sync waves or hooks help when one change must happen before another?
---
answer:
They enforce ordering for exceptional cases such as migrations, prerequisites, or dependent resource creation. They help when a rollout must be staged rather than applied as one unordered set of resources.

They should still be used carefully because too much ordering logic makes delivery harder to reason about and recover.
hints:
- Think migrations and prerequisites.
- They solve ordering problems.
- Too much orchestration becomes fragile.
```

Related concepts: [Sync Waves And Hooks](microservices-declarative-delivery.concept.md#sync-waves-and-hooks)

## Troubleshooting

```interview-question
A deployment is `Synced` in Argo CD but users are failing. What do you check next?
---
answer:
Check Kubernetes rollout status, readiness failures, logs, metrics, traces, service routing, and dependency health. `Synced` only tells you that live manifests match Git; it does not prove the application is functioning correctly.

The next step is runtime diagnosis, not Git comparison.
hints:
- Synced is not healthy.
- Move from delivery state to runtime state.
- Check readiness and dependencies quickly.
```

```interview-question
What are the most common failure modes in Helm and Argo CD delivery pipelines?
---
answer:
Common failures include wrong values, bad chart assumptions, accidental drift, overly aggressive auto-prune, failed hooks or migrations, incompatible schema changes, and applications that render correctly but fail readiness or runtime checks.

Many delivery failures are not syntax failures. They are coordination and state-boundary failures.
hints:
- Values can be valid but wrong.
- Drift and prune are operational risks.
- Runtime failure can happen after a correct sync.
```

```interview-code
language: yaml
prompt: Complete the Argo CD application so it deploys a Helm chart from Git using a production values file and automated pruning.
starter:
apiVersion: argoproj.io/v1alpha1
kind: Application
metadata:
  name: orders-prod
spec:
  source:
    repoURL: https://example.com/platform.git
    path: services/orders
    targetRevision: main
  destination:
    namespace: orders-prod
    server: https://kubernetes.default.svc
solution:
apiVersion: argoproj.io/v1alpha1
kind: Application
metadata:
  name: orders-prod
spec:
  source:
    repoURL: https://example.com/platform.git
    path: services/orders
    targetRevision: main
    helm:
      valueFiles:
      - values-prod.yaml
  destination:
    namespace: orders-prod
    server: https://kubernetes.default.svc
  syncPolicy:
    automated:
      prune: true
      selfHeal: true
checks:
- includes: helm:
- includes: valueFiles:
- includes: values-prod.yaml
- includes: syncPolicy:
- includes: prune: true
```
