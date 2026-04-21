---
title: Authentication API Hardening Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page covers the API hardening and data-protection concepts from the authentication and security interview topic.

Study pages: [Section Index](index.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Hardening Map

```concept-card
id: api-hardening
term: API Hardening
children:
- cors-security
- secret-management
- request-dtos
- response-dtos
- over-posting-prevention
- data-exposure-control
- security-headers
- api-key-hardening
- sql-injection-defense
- xss-considerations
- csrf-defense
summary:
API hardening is the set of design decisions that reduce attack surface, credential exposure, and unsafe default behaviors.
details:
It covers safe input contracts, safe output contracts, secure configuration, browser-facing protections, and defenses against common web attacks.
mnemonic:
Harden input, output, config, and transport.
recall:
- Which concerns belong to API hardening rather than identity itself?
- Why does hardening require both design-time and runtime decisions?
```

```concept-card
id: cors-security
term: CORS Security
parents:
- api-hardening
summary:
CORS security is the careful configuration of which browser origins may call the API and under what credential rules.
details:
Explicit origins, methods, and headers are safer than wildcards. Wildcard origins with credentials are especially dangerous and are blocked by browsers for good reason.
example:
Allow `https://app.example.com`, not `*`, when the SPA uses cookies or auth headers.
mnemonic:
Specific origin, specific method, specific trust.
recall:
- Why is `AllowAnyOrigin()` risky in production?
- Why must CORS often be tested from a real browser?
```

```concept-card
id: secret-management
term: Secret Management
parents:
- api-hardening
summary:
Secret management stores sensitive values such as connection strings and API keys outside source-controlled configuration files.
details:
Development often uses user secrets, while production uses environment variables or dedicated secret stores such as Azure Key Vault.
example:
Use `dotnet user-secrets` locally and Azure Key Vault or environment variables in production.
mnemonic:
Secrets live outside the repo.
recall:
- Why is `.gitignore` not a real secret-management strategy?
- Which secret sources are common in development and production?
```

```concept-card
id: request-dtos
term: Request DTOs
parents:
- api-hardening
related:
- over-posting-prevention
summary:
Request DTOs define exactly which fields a client is allowed to send into the API.
details:
They prevent entity binding from becoming an accidental write surface and make validation and contract evolution easier to control.
example:
`CreateUserRequest` should contain `Name` and `Email`, not `IsAdmin` or `PasswordHash`.
mnemonic:
Only accept what the client should control.
recall:
- Why should request DTOs be separate from entity types?
- How do request DTOs improve security and maintainability together?
```

```concept-card
id: response-dtos
term: Response DTOs
parents:
- api-hardening
related:
- data-exposure-control
summary:
Response DTOs define exactly which fields the API is allowed to expose to callers.
details:
They protect against accidental leakage and make the response contract independent from the internal persistence model.
example:
`UserResponse` can expose `Id`, `Name`, and `Email` while leaving out `PasswordHash` and internal notes.
mnemonic:
Expose the contract, not the entity.
recall:
- Why are response DTOs safer than returning entities directly?
- How do response DTOs help when entity models change over time?
```

```concept-card
id: over-posting-prevention
term: Over-Posting Prevention
parents:
- api-hardening
related:
- request-dtos
summary:
Over-posting prevention stops clients from setting fields they should not control, such as admin flags or internal timestamps.
details:
The normal defense is a request DTO plus explicit server-side mapping, not a blacklist of fields to ignore.
mnemonic:
Accept less, trust less, map explicitly.
recall:
- What kind of attack does over-posting enable?
- Why is explicit mapping a stronger defense than excluding fields one by one?
```

```concept-card
id: data-exposure-control
term: Data Exposure Control
parents:
- api-hardening
related:
- response-dtos
summary:
Data exposure control prevents sensitive or internal fields from leaking through API responses.
details:
The safest pattern is projection directly into response DTOs plus tests that assert sensitive fields are not present.
mnemonic:
Project only what may be seen.
recall:
- Why is projection useful for data exposure control?
- Why should tests reinforce DTO-based response safety?
```

```concept-card
id: security-headers
term: Security Headers
parents:
- api-hardening
summary:
Security headers are HTTP response headers that reduce browser-side attack surface and information leakage.
details:
Examples include `X-Content-Type-Options`, HSTS, `Referrer-Policy`, and `Permissions-Policy`. APIs benefit too, especially when they serve any browser-consumed content or tooling.
example:
`X-Content-Type-Options: nosniff` and `Strict-Transport-Security` are common low-cost defaults.
mnemonic:
Small headers, big guardrails.
recall:
- Why do APIs still benefit from security headers?
- Which common headers are easy wins for most APIs?
```

```concept-card
id: api-key-hardening
term: API Key Hardening
parents:
- api-hardening
related:
- api-key-authentication
summary:
API key hardening adds hashing, constant-time comparison, scoping, expiration, and rotation to API key systems.
details:
It turns a naive secret string into a manageable credential system with better auditability and lower compromise impact.
mnemonic:
Hash it, scope it, rotate it.
recall:
- Which extra controls make API key auth production-ready?
- Why is constant-time comparison relevant for API key validation?
```

```concept-card
id: sql-injection-defense
term: SQL Injection Defense
parents:
- api-hardening
summary:
SQL injection defense prevents user input from being interpreted as executable SQL.
details:
Parameterized queries and EF Core LINQ translation are the normal defenses. Unsafe string concatenation is the main anti-pattern.
mnemonic:
Data stays data, never code.
recall:
- What is the main unsafe pattern that leads to SQL injection?
- Why does parameterization solve the core problem?
```

```concept-card
id: xss-considerations
term: XSS Considerations
parents:
- api-hardening
summary:
XSS considerations focus on preventing untrusted content from being rendered as executable script in browser contexts.
details:
Pure JSON APIs are safer than HTML responses, but XSS still matters when the API returns HTML, echoes unsafe content, or feeds data into browser-rendered views.
mnemonic:
Untrusted input must never become executable output.
recall:
- Why are JSON APIs safer than HTML responses for XSS?
- In what cases can an API still contribute to XSS risk?
```

```concept-card
id: csrf-defense
term: CSRF Defense
parents:
- api-hardening
summary:
CSRF defense prevents a browser from being tricked into sending authenticated requests the user did not intend.
details:
It matters mainly when authentication is cookie-based because browsers attach cookies automatically. Bearer tokens in authorization headers change the threat model because the browser does not auto-send them in the same way.
example:
Cookie-authenticated admin panels need anti-forgery protection, while bearer-token APIs mainly need to protect token storage and transport.
mnemonic:
Auto-sent credentials need anti-forgery controls.
recall:
- Why is CSRF tied closely to cookie-based authentication?
- Why is the threat model different for bearer-token APIs?
```
