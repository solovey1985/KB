---
title: Declarative Delivery For Microservices Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the concepts behind declarative microservice delivery on Kubernetes.

Study pages: [Section Index](index.md) | [Material Notes](microservices-declarative-delivery.md) | [Interview Practice](microservices-declarative-delivery.interview.md)

## Delivery Map

```concept-card
id: declarative-delivery-for-microservices
term: Declarative Delivery For Microservices
children:
- desired-state
- reconciliation-loop
- gitops
- drift
- terraform-provisioning-boundary
- helm-chart
- helm-values-layering
- helm-release
- argo-cd-application
- sync-policy
- health-assessment
- sync-waves-and-hooks
- promotion-and-rollback
summary:
Declarative delivery for microservices is the practice of defining intended deployment state in version-controlled files and reconciling that state into Kubernetes consistently.
details:
It brings predictability and auditability to multi-service delivery by separating provisioning, packaging, sync, and runtime enforcement responsibilities.
example:
A team provisions platform resources with Terraform, packages services with Helm, syncs them with Argo CD, and lets Kubernetes run the workloads.
mnemonic:
Declare in Git, reconcile to cluster, verify in runtime.
recall:
- Why is declarative delivery especially useful when many services share one platform?
- Which layers of the delivery chain should stay clearly separated?
```

```concept-card
id: desired-state
term: Desired State
parents:
- declarative-delivery-for-microservices
related:
- drift
summary:
Desired state is the intended system configuration recorded in source-controlled definitions.
details:
It is the reference point against which live environment state is compared during deployment and incident investigation.
example:
The Git repo says a service should run image `orders:v42` with 4 replicas and a specific ingress hostname.
mnemonic:
What should exist, not what happens to exist.
recall:
- Why is desired state the anchor for GitOps delivery?
- Why should live cluster state not become the source of truth?
```

```concept-card
id: reconciliation-loop
term: Reconciliation Loop
parents:
- declarative-delivery-for-microservices
related:
- gitops
summary:
A reconciliation loop continuously compares actual state to desired state and drives the system back toward the declared target.
details:
This is the operating model behind both Kubernetes controllers and GitOps delivery controllers such as Argo CD.
example:
If a live resource is changed manually, the reconciler can detect the mismatch and restore the Git-defined version.
mnemonic:
Compare, detect, converge.
recall:
- Why is reconciliation stronger than one-time deployment automation?
- What kinds of operational mistakes does reconciliation expose well?
```

```concept-card
id: gitops
term: GitOps
parents:
- declarative-delivery-for-microservices
related:
- drift
summary:
GitOps is the operating model where Git holds desired application state and controllers continuously reconcile that state into the target environment.
details:
It improves auditability, rollback clarity, and drift visibility, but it also requires teams to stop treating direct cluster edits as a normal workflow.
example:
Promotion to production happens by merging the desired version in Git, not by hand-editing a Deployment in the cluster.
mnemonic:
Git defines, controllers apply.
recall:
- What makes GitOps more than just deploying from Git?
- Why are manual cluster changes risky in a GitOps system?
```

```concept-card
id: drift
term: Drift
parents:
- declarative-delivery-for-microservices
related:
- desired-state
summary:
Drift is the mismatch between live environment state and the intended state stored in source control.
details:
Drift can come from emergency fixes, manual edits, hidden environment overrides, or resource generation differences. Detecting it early keeps delivery predictable.
example:
A production Service was manually patched to a different target port, so live traffic no longer matches what Git declares.
mnemonic:
Live moved away from declared truth.
recall:
- What are common causes of drift in microservice platforms?
- Why can drift make rollback harder?
```

```concept-card
id: terraform-provisioning-boundary
term: Terraform Provisioning Boundary
parents:
- declarative-delivery-for-microservices
summary:
Terraform provisioning boundary is the limit where Terraform should manage infrastructure and platform prerequisites rather than daily application rollout state.
details:
Terraform is strong at provisioning cloud and shared platform resources, but it is usually not the best primary tool for fine-grained day-to-day microservice release state inside Kubernetes.
example:
Use Terraform for DNS, databases, IAM, and cluster add-ons, but let Argo CD manage the service Deployment state inside the cluster.
mnemonic:
Provision the platform, do not micromanage app rollouts.
recall:
- What kinds of resources fit Terraform well in a microservice platform?
- Why is Terraform usually the wrong primary owner of live app rollout state?
```

```concept-card
id: helm-chart
term: Helm Chart
parents:
- declarative-delivery-for-microservices
children:
- helm-values-layering
- helm-release
summary:
A Helm chart is a packaged, templatized collection of Kubernetes resource definitions for deploying an application.
details:
Charts let teams standardize manifest structure while still varying environment-specific or service-specific values safely.
example:
One service chart can define Deployment, Service, Ingress, ConfigMap, and Job templates driven by values files.
mnemonic:
Package the shape, vary the values.
recall:
- What problem does Helm solve better than copy-pasting raw manifests?
- Why should charts stay understandable instead of overly clever?
```

```concept-card
id: helm-values-layering
term: Helm Values Layering
parents:
- helm-chart
summary:
Helm values layering is the practice of combining default and environment-specific values to render the final manifests.
details:
It keeps one chart reusable across environments, but poor layering can hide important differences and make troubleshooting much harder.
example:
Use `values.yaml` for defaults and `values-prod.yaml` for production-specific replica, ingress, and resource overrides.
mnemonic:
One chart, layered intent.
recall:
- Why is values layering powerful for microservice delivery?
- What makes values layering become dangerous or opaque?
```

```concept-card
id: helm-release
term: Helm Release
parents:
- helm-chart
summary:
A Helm release is one installed instance of a chart with a concrete set of rendered values and resulting resources.
details:
Release identity matters operationally because the same chart can be installed many times across different environments or tenants.
example:
`orders-prod` and `orders-staging` can both come from the same chart but differ as separate releases with different values.
mnemonic:
Same chart, different installed reality.
recall:
- What is the difference between a Helm chart and a Helm release?
- Why does release identity matter across environments?
```

```concept-card
id: argo-cd-application
term: Argo CD Application
parents:
- declarative-delivery-for-microservices
children:
- sync-policy
- health-assessment
summary:
An Argo CD Application is the Git-tracked deployment unit that maps source manifests or charts to a target cluster and namespace.
details:
It is the main object Argo CD uses to compare desired state with live state, report differences, and apply synchronization.
example:
One Argo CD Application points to the `orders` Helm chart path in Git and targets the production namespace in a specific cluster.
mnemonic:
One declared app, one reconciled target.
recall:
- What does an Argo CD Application connect together?
- Why is it the main unit of GitOps delivery in Argo CD?
```

```concept-card
id: sync-policy
term: Sync Policy
parents:
- argo-cd-application
summary:
Sync policy defines whether Argo CD applies changes manually or automatically, and whether it prunes and self-heals drifted resources.
details:
Automation increases consistency and speed, but it also increases the need for repo discipline because mistakes can be applied rapidly and widely.
example:
Auto-sync with prune and self-heal can quickly restore drift, but it can also remove a manually created emergency resource if that resource is not declared in Git.
mnemonic:
More automation, more repo discipline required.
recall:
- Why can auto-prune and self-heal be powerful and risky at the same time?
- When might manual sync still be the safer default?
```

```concept-card
id: health-assessment
term: Health Assessment
parents:
- argo-cd-application
summary:
Health assessment is the GitOps controller view of whether deployed resources are healthy, degraded, or otherwise unstable.
details:
An application can be fully synced from Git yet still unhealthy at runtime, so sync status and health status must be interpreted separately.
example:
An app shows `Synced` in Argo CD because manifests match Git, but it is `Degraded` because the Deployment never passes readiness.
mnemonic:
Synced is not the same as healthy.
recall:
- Why must operators distinguish sync state from health state?
- What kind of failures appear when an app is synced but degraded?
```

```concept-card
id: sync-waves-and-hooks
term: Sync Waves And Hooks
parents:
- declarative-delivery-for-microservices
summary:
Sync waves and hooks control ordering for resources and tasks that must happen before or after the main application rollout.
details:
They are useful for migrations and dependency ordering, but overusing them can make delivery flows harder to reason about and harder to recover.
example:
Run a migration Job in an earlier sync wave before deploying the new application Pods that depend on the schema.
mnemonic:
Order the exceptions, not everything.
recall:
- When are sync waves or hooks justified?
- Why should they stay limited and well understood?
```

```concept-card
id: promotion-and-rollback
term: Promotion And Rollback
parents:
- declarative-delivery-for-microservices
summary:
Promotion and rollback are the controlled movement between environment states and the return to a known-good version when a release fails.
details:
Rollback is only truly easy when contracts, schema changes, and environment differences remain backward compatible enough for the older state to run safely.
example:
Reverting a Git commit may restore the old manifests, but it will not automatically undo an incompatible schema change already applied in production.
mnemonic:
Git can move back fast, data may not.
recall:
- Why is rollback sometimes harder than reverting Git?
- What makes promotion trustworthy across environments?
```
