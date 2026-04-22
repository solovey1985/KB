---
title: Kubernetes For Microservices Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse Kubernetes questions in the context of deploying and running microservices in production.

Relevant study pages:

- [Material Notes](kubernetes-microservices.md)
- [Concept Map](kubernetes-microservices.concept.md)

## Platform Basics

```interview-question
What is Kubernetes, and why is it used for microservices deployment?
---
answer:
Kubernetes is a declarative platform that schedules, runs, scales, and reconciles containerized workloads toward a desired state. It is useful for microservices because it standardizes deployment, service discovery, self-healing, rollouts, scaling, and policy enforcement across many services.

The main value is operational consistency, not just container execution.
hints:
- Think desired state and controllers.
- Think platform consistency across many services.
- It is more than a container runtime.
```

```interview-question
Describe the roles of the control plane and nodes in Kubernetes.
---
answer:
The control plane stores cluster state and runs components such as the API server, scheduler, and controllers. Nodes run the actual Pods.

Official Kubernetes terminology prefers control plane and nodes rather than the older master and worker wording.
hints:
- One side decides and reconciles.
- The other side executes workloads.
- Use modern terminology.
```

Related concepts: [Desired State Reconciliation](kubernetes-microservices.concept.md#desired-state-reconciliation), [Control Plane And Nodes](kubernetes-microservices.concept.md#control-plane-and-nodes)

```interview-question
How do Pods facilitate container management in Kubernetes, and why do teams usually avoid managing Pods directly in production?
---
answer:
Pods provide the shared execution context for one or more tightly related containers, including shared networking and volumes. Teams usually avoid managing Pods directly because controllers like Deployments and StatefulSets provide replica management, rollout control, and self-healing.

Directly managed Pods are too low-level for most production microservice workloads.
hints:
- Pods are the smallest deployable unit.
- Controllers provide the production safety features.
- Shared local context is part of the answer.
```

```interview-question
Differentiate between Deployments, StatefulSets, DaemonSets, and Jobs in Kubernetes.
---
answer:
Deployments are the default for stateless replicated microservices. StatefulSets are for workloads that need stable identity or persistent storage. DaemonSets run one Pod per node for node-level agents. Jobs and CronJobs run work to completion for migrations, cleanup, or scheduled tasks.

The right choice depends on workload behavior, not on personal preference.
hints:
- Stateless service, stateful service, node agent, batch task.
- Each controller exists for a different runtime pattern.
- Think microservice platform use cases.
```

Related concepts: [Deployment](kubernetes-microservices.concept.md#deployment), [StatefulSet](kubernetes-microservices.concept.md#statefulset), [Jobs And CronJobs](kubernetes-microservices.concept.md#jobs-and-cronjobs)

## Networking And Exposure

```interview-question
What types of Services exist in Kubernetes, and how do they help microservice communication?
---
answer:
Common service types are `ClusterIP`, `NodePort`, `LoadBalancer`, and `ExternalName`. Services provide stable discovery and routing so callers do not depend on changing Pod IPs.

For microservices, `ClusterIP` is the most common internal communication model, while external exposure usually builds on Ingress or cloud load balancers.
hints:
- Stable endpoint over changing Pods.
- Internal traffic usually starts with `ClusterIP`.
- External exposure is a separate concern.
```

Related concepts: [Service](kubernetes-microservices.concept.md#service)

```interview-question
What is Ingress, and why is an Ingress Controller required?
---
answer:
Ingress is the HTTP and HTTPS routing resource that defines how external traffic should reach internal services. An Ingress Controller is required because the resource alone is only configuration; the controller actually applies and enforces the routing behavior.

It is also worth knowing that Gateway API is the newer, more expressive traffic-management model.
hints:
- Ingress is the rule, not the engine.
- A controller must interpret the rules.
- Mention Gateway API as a modern follow-up.
```

Related concepts: [Ingress](kubernetes-microservices.concept.md#ingress)

```interview-question
How do labels and selectors work together in Kubernetes?
---
answer:
Labels annotate resources with identifying key-value pairs, and selectors use those labels to choose target resources. Services and controllers both depend on selectors to connect to the right Pods.

If labels and selectors do not match, traffic routing or controller behavior breaks even when the Pods themselves are healthy.
hints:
- Labels identify.
- Selectors choose.
- Mismatches cause silent platform problems.
```

```interview-choice
Which statement about Namespaces is correct?
---
options:
- Namespaces automatically isolate all network traffic between workloads.
- Namespaces scope resources and policies, but NetworkPolicies are still needed for traffic isolation.
- Namespaces replace RBAC for access control.
correct: 1
explanation:
Namespaces are logical scopes, not automatic network barriers. Traffic isolation normally requires NetworkPolicies or equivalent enforcement.
```

## Configuration And Safety

```interview-question
How do ConfigMaps, Secrets, ServiceAccounts, and RBAC differ in Kubernetes?
---
answer:
ConfigMaps store non-sensitive configuration, Secrets store sensitive values, ServiceAccounts provide workload identity, and RBAC controls what that identity can do.

They solve different platform concerns and should not be treated as interchangeable configuration features.
hints:
- Config and secret are data.
- ServiceAccount is identity.
- RBAC is authorization.
```

Related concepts: [Config And Secrets](kubernetes-microservices.concept.md#config-and-secrets)

```interview-question
What is the difference between liveness, readiness, and startup probes?
---
answer:
Liveness determines whether the container should be restarted. Readiness determines whether the Pod should receive traffic. Startup protects slow-booting applications from being killed before they finish initialization.

For production microservices, readiness is especially important because it controls traffic safety during startup and rollout.
hints:
- Restart, traffic, boot.
- Readiness protects callers.
- Startup probe protects slow initialization.
```

Related concepts: [Probes](kubernetes-microservices.concept.md#probes)

```interview-question
What would you consider when performing a rolling update in Kubernetes?
---
answer:
Consider readiness behavior, `maxUnavailable`, `maxSurge`, dependency compatibility, safe database migration sequencing, and rollback ability. A rollout is only safe if new Pods become ready under real conditions and the older version can still coexist long enough for transition.

Changing the image alone is not the whole deployment design.
hints:
- Readiness is central.
- Compatibility matters across versions.
- Rollback must stay practical.
```

## Scaling And Policies

```interview-question
How do resource requests and limits affect microservice stability in Kubernetes?
---
answer:
Requests influence scheduling decisions, and limits cap maximum resource use. Good values improve placement, reduce noisy-neighbor issues, and make autoscaling more reliable.

Missing or unrealistic values often lead to unstable performance, failed scheduling, or incorrect scaling behavior.
hints:
- Requests matter before the Pod even starts.
- Limits cap usage after it starts.
- Scaling depends on believable resource signals.
```

Related concepts: [Resource Management](kubernetes-microservices.concept.md#resource-management)

```interview-question
How do HPA and Cluster Autoscaler differ?
---
answer:
Horizontal Pod Autoscaler changes the number of Pod replicas for a workload. Cluster Autoscaler changes the number of nodes in the cluster.

They solve different layers of scaling and are often used together for stateless microservices.
hints:
- One scales workloads.
- One scales infrastructure.
- They can cooperate during traffic growth.
```

Related concepts: [Autoscaling](kubernetes-microservices.concept.md#autoscaling)

```interview-question
What benefits do NetworkPolicies provide in a microservices platform?
---
answer:
NetworkPolicies restrict which Pods and namespaces may communicate. They reduce accidental exposure, tighten east-west traffic, and make service boundaries more enforceable.

They are especially important because Namespaces alone do not automatically isolate network traffic.
hints:
- Think east-west traffic control.
- Namespaces are not enough.
- Policy reduces accidental reachability.
```

Related concepts: [Network Policy](kubernetes-microservices.concept.md#network-policy)

## Production Troubleshooting

```interview-question
How would you troubleshoot a Service that exists but is not routing traffic to the application?
---
answer:
Check the Service selector, matching Pod labels, readiness status, EndpointSlices, target port mapping, and whether the Pods are actually healthy and ready. A Service can exist correctly as an object while still having no usable backends.

The fastest path is usually object matching first, then endpoint readiness, then container health.
hints:
- Start with selector and labels.
- Then check endpoints.
- Then check readiness and port mapping.
```

```interview-question
What are common reasons a Pod stays in `Pending` or enters `CrashLoopBackOff`?
---
answer:
`Pending` usually means scheduling or dependency issues such as insufficient capacity, unsatisfied constraints, or missing volumes. `CrashLoopBackOff` usually means the container starts and fails repeatedly due to bad commands, missing config, failed dependencies, or an overly aggressive liveness probe.

The two states point to different parts of the platform lifecycle, so they should be investigated differently.
hints:
- `Pending` is often before the workload really starts.
- `CrashLoopBackOff` is repeated failure after start.
- Probe configuration can cause false instability.
```

```interview-code
language: yaml
prompt: Complete the deployment so the container only receives traffic when `/ready` succeeds and has a CPU request.
starter:
apiVersion: apps/v1
kind: Deployment
metadata:
  name: orders-api
spec:
  replicas: 3
  selector:
    matchLabels:
      app: orders-api
  template:
    metadata:
      labels:
        app: orders-api
    spec:
      containers:
      - name: orders-api
        image: orders:v1
        ports:
        - containerPort: 8080
solution:
apiVersion: apps/v1
kind: Deployment
metadata:
  name: orders-api
spec:
  replicas: 3
  selector:
    matchLabels:
      app: orders-api
  template:
    metadata:
      labels:
        app: orders-api
    spec:
      containers:
      - name: orders-api
        image: orders:v1
        ports:
        - containerPort: 8080
        readinessProbe:
          httpGet:
            path: /ready
            port: 8080
        resources:
          requests:
            cpu: "100m"
checks:
- includes: readinessProbe
- includes: /ready
- includes: requests:
- includes: cpu:
```
