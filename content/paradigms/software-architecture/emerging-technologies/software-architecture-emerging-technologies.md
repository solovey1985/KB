# Emerging Technologies and Future Trends

Emerging technologies change architecture when they introduce new workload shapes, latency expectations, trust models, or deployment constraints.

## AI in architecture

AI can affect architecture through:

- inference services
- model lifecycle management
- data pipelines
- observability for drift and accuracy
- human-in-the-loop workflows

It adds concerns beyond normal software delivery, especially around evaluation, cost, and model behavior changes over time.

## Blockchain

Blockchain changes architecture when distributed trust, immutability, or decentralized coordination matter.

It is not a general replacement for databases. It is only useful when its trust model matches the problem strongly enough to justify its cost and complexity.

## AR, VR, and immersive systems

AR and VR increase pressure on:

- latency
- synchronization
- media throughput
- device capability awareness

Architectures for immersive systems often need edge support, realtime communication, and careful performance tuning.

## 5G and network change

5G expands what is feasible for mobile, edge, and realtime workloads, but it does not remove the need for graceful degradation or good offline-aware design.

## Interview reminders

- new technology matters architecturally only when it changes constraints or trade-offs
- AI introduces data, evaluation, and lifecycle concerns
- blockchain is a specialized trust architecture, not a default persistence choice
- immersive systems are sensitive to latency and throughput
- better networks reduce some constraints but do not eliminate distributed-system realities
