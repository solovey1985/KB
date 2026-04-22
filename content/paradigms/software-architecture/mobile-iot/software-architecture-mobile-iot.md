# Mobile and IoT Architecture

Mobile and IoT systems add constraints that are less visible in traditional server-centric architecture.

## Mobile architecture concerns

Mobile apps must work well under:

- intermittent connectivity
- battery constraints
- limited CPU and storage
- background execution limits
- device diversity

That usually leads to careful synchronization, offline-first thinking where appropriate, and efficient API usage.

## IoT architecture concerns

IoT systems often involve many small devices, weak networks, constrained hardware, and large volumes of telemetry.

Important concerns include:

- device identity and provisioning
- secure communication
- ingestion at scale
- command and control reliability
- fleet monitoring and updates

## Edge computing

Edge computing moves some processing closer to the device or data source instead of sending everything to a central cloud backend.

This helps reduce latency, bandwidth use, and dependence on always-on connectivity.

## Interview reminders

- mobile architecture must respect device and network constraints
- IoT architecture combines scale with unreliable environments
- edge computing is about where processing happens, not just where devices live
- security and update strategy matter more as device count grows
