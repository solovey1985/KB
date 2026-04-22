---
title: Kubernetes For Microservices Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the core Kubernetes concepts most relevant to deploying and running microservices.

Study pages: [Section Index](index.md) | [Material Notes](kubernetes-microservices.md) | [Interview Practice](kubernetes-microservices.interview.md)

## Platform Map

```concept-card
id: kubernetes-for-microservices
term: Kubernetes For Microservices
children:
- desired-state-reconciliation
- control-plane-and-nodes
- pod
- deployment
- service
- namespace
- ingress
- config-and-secrets
- probes
- resource-management
- autoscaling
- network-policy
- statefulset
- jobs-and-cronjobs
summary:
Kubernetes for microservices is the use of declarative cluster primitives to deploy, expose, scale, and operate many services consistently.
details:
Its value comes from standardizing service lifecycle, traffic management, recovery, and policy enforcement across a platform with many moving parts.
example:
A team can deploy APIs, workers, and scheduled jobs with one common runtime model instead of managing each service on separate virtual machines by hand.
mnemonic:
Declare the platform, let controllers keep it true.
recall:
- Why is Kubernetes especially useful once many services need a shared runtime platform?
- Which concerns become standardized when Kubernetes is adopted well?
```

```concept-card
id: desired-state-reconciliation
term: Desired State Reconciliation
parents:
- kubernetes-for-microservices
summary:
Desired state reconciliation is the controller-driven process that keeps actual cluster state moving toward the declared target state.
details:
This is the core Kubernetes mental model: you declare what should exist, and control loops continuously try to make reality match it.
example:
If a Deployment declares three replicas and one Pod dies, the controller creates another Pod to restore the desired count.
mnemonic:
Declare once, reconcile continuously.
recall:
- Why is Kubernetes more than just a container launcher?
- What happens when actual state drifts away from desired state?
```

```concept-card
id: control-plane-and-nodes
term: Control Plane And Nodes
parents:
- kubernetes-for-microservices
summary:
The control plane stores cluster state and runs scheduling and reconciliation, while nodes run the workloads.
details:
Official Kubernetes terminology prefers control plane and nodes over the older master and worker wording.
example:
The API server and scheduler live in the control plane, while application Pods run on cluster nodes.
mnemonic:
Control decides, nodes execute.
recall:
- What is the role difference between the control plane and a node?
- Why is the term control plane more correct than master?
```

```concept-card
id: pod
term: Pod
parents:
- kubernetes-for-microservices
summary:
A Pod is the smallest deployable Kubernetes unit and provides a shared runtime context for one or more tightly related containers.
details:
Microservices usually package one main app container per Pod, sometimes with tightly coupled sidecars when needed.
example:
An API container and a closely related sidecar container can share localhost networking and mounted volumes inside one Pod.
mnemonic:
Smallest deployable unit, shared local world.
recall:
- What does a Pod provide that one bare container does not?
- Why are Pods usually managed through controllers instead of directly?
```

```concept-card
id: deployment
term: Deployment
parents:
- kubernetes-for-microservices
related:
- statefulset
summary:
A Deployment manages stateless replicated workloads through ReplicaSets and rolling updates.
details:
It is the normal default for HTTP services, background consumers, and other disposable microservice workloads that can be scaled horizontally.
example:
An orders API can run in a Deployment with five replicas and a rolling update strategy.
mnemonic:
Stateless service, rolling control.
recall:
- Why is Deployment the default controller for many microservices?
- What kinds of workloads are a poor fit for Deployment?
```

```concept-card
id: service
term: Service
parents:
- kubernetes-for-microservices
children:
- ingress
summary:
A Service provides a stable network endpoint and load-balancing abstraction over matching Pods.
details:
It decouples callers from changing Pod IPs and uses selectors with EndpointSlices to route traffic to healthy backends.
example:
A `ClusterIP` service named `orders` keeps working even as Deployment Pods are replaced during rollouts.
mnemonic:
Stable name, changing Pods underneath.
recall:
- Why do microservices need Services instead of calling Pod IPs directly?
- What role do selectors play in Service routing?
```

```concept-card
id: namespace
term: Namespace
parents:
- kubernetes-for-microservices
related:
- network-policy
summary:
A Namespace is a logical scope for separating resources by team, app, or environment.
details:
Namespaces organize and constrain resources, but they do not automatically create network isolation by themselves.
example:
`payments-prod` and `payments-staging` can live in separate namespaces with separate quotas and RBAC rules.
mnemonic:
Scope resources, not automatic isolation.
recall:
- What does a Namespace isolate well?
- What security assumption should you avoid making about Namespaces?
```

```concept-card
id: ingress
term: Ingress
parents:
- service
summary:
Ingress defines HTTP and HTTPS routing into cluster services.
details:
It is only the routing resource; it still requires an Ingress Controller to enforce those rules. Gateway API is a newer and more expressive traffic-management model worth knowing as well.
example:
Route `api.example.com/orders` to the orders service through an NGINX Ingress Controller.
mnemonic:
Rules need a controller to become traffic.
recall:
- Why is an Ingress resource alone not enough?
- What newer traffic-management API should you be aware of besides Ingress?
```

```concept-card
id: config-and-secrets
term: Config And Secrets
parents:
- kubernetes-for-microservices
summary:
ConfigMaps hold non-sensitive configuration, while Secrets hold sensitive values such as credentials and tokens.
details:
Separating config from image keeps services more portable and easier to promote across environments without rebuilding containers.
example:
Store feature flags in a ConfigMap and database credentials in a Secret mounted into the same Deployment.
mnemonic:
Config outside image, secrets outside plain text.
recall:
- Why is externalized configuration important for Kubernetes workloads?
- What is the operational difference between ConfigMaps and Secrets?
```

```concept-card
id: probes
term: Probes
parents:
- kubernetes-for-microservices
summary:
Probes are health checks that control restart behavior and traffic eligibility.
details:
Liveness answers whether the process should be restarted, readiness controls whether traffic should reach the Pod, and startup protects slow boots from being killed too early.
example:
A service can be alive but not ready while warming caches, so readiness should fail while liveness still passes.
mnemonic:
Alive, ready, or still starting.
recall:
- Which probe protects traffic rather than restart policy?
- Why is startup probe useful for slow boot services?
```

```concept-card
id: resource-management
term: Resource Management
parents:
- kubernetes-for-microservices
related:
- autoscaling
summary:
Resource management uses requests and limits to guide scheduling and bound workload consumption.
details:
Good requests and limits reduce noisy-neighbor problems, help the scheduler pack workloads more safely, and make autoscaling more stable.
example:
An API with realistic CPU requests is less likely to be packed onto an overloaded node than one with no declared requirements.
mnemonic:
Ask honestly, limit safely.
recall:
- Why do requests matter to the scheduler?
- What instability appears when requests and limits are missing or unrealistic?
```

```concept-card
id: autoscaling
term: Autoscaling
parents:
- kubernetes-for-microservices
related:
- resource-management
summary:
Autoscaling adjusts workload or cluster capacity based on demand and policy.
details:
Horizontal Pod Autoscaler changes replica count, Vertical Pod Autoscaler adjusts requested resources, and Cluster Autoscaler adds or removes nodes.
example:
An HPA scales a checkout API from 4 to 12 replicas during traffic spikes while Cluster Autoscaler adds nodes to fit the extra Pods.
mnemonic:
Scale Pods, tune size, grow nodes.
recall:
- What is the difference between HPA and Cluster Autoscaler?
- Why does autoscaling depend on decent resource signals?
```

```concept-card
id: network-policy
term: Network Policy
parents:
- kubernetes-for-microservices
related:
- namespace
summary:
Network Policies define which Pod-to-Pod or namespace-level traffic is allowed.
details:
They are important in microservices platforms because namespaces alone do not prevent unwanted east-west traffic.
example:
Only allow the API namespace to call the payments service namespace on one specific TCP port.
mnemonic:
Allowed paths must be declared, not assumed.
recall:
- Why are Network Policies important even when Namespaces already exist?
- What kind of exposure do they reduce?
```

```concept-card
id: statefulset
term: StatefulSet
parents:
- kubernetes-for-microservices
related:
- deployment
summary:
A StatefulSet manages stateful workloads that need stable identity or persistent storage.
details:
It is the right choice for databases, brokers, and other workloads where ordered identity and durable storage matter more than disposable replicas.
example:
A Kafka broker or PostgreSQL instance usually fits StatefulSet better than Deployment.
mnemonic:
Stable name, stable storage, stable order.
recall:
- When should you choose StatefulSet over Deployment?
- Why are stable identities important for some workloads?
```

```concept-card
id: jobs-and-cronjobs
term: Jobs And CronJobs
parents:
- kubernetes-for-microservices
summary:
Jobs and CronJobs run short-lived or scheduled work to completion.
details:
They are useful for migrations, scheduled cleanup, backups, and batch tasks that should not live inside always-on microservice processes.
example:
Run a migration Job before promotion, then use a nightly CronJob for cleanup or backup work.
mnemonic:
Finish the task, then exit.
recall:
- Why are Jobs and CronJobs often better than embedding batch work into long-running services?
- Which microservice operations commonly fit these controllers?
```
