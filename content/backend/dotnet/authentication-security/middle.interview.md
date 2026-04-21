---
title: Authentication and Security Middle Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the mid-level authentication and security trade-offs from the Web API interview question set.

Relevant concept maps:

- [Identity and Access Concept Map](authentication-identity-access.concept.md)
- [API Hardening Concept Map](authentication-api-hardening.concept.md)

## Policy-Based Authorization

```interview-question
How do you implement authorization policies in ASP.NET Core beyond simple role checks?
---
answer:
Use policy-based authorization to combine multiple requirements such as roles, claims, and custom rules.

For more advanced cases, create custom `IAuthorizationRequirement` and `IAuthorizationHandler` implementations, and use resource-based authorization when the decision depends on the specific entity being accessed.

This scales much better than scattering `User.IsInRole(...)` checks throughout endpoints.
hints:
- Roles are only one kind of requirement.
- Policies can combine multiple conditions.
- Some access decisions depend on the resource itself.
```

Related concepts: [Policy-Based Authorization](authentication-identity-access.concept.md#policy-based-authorization), [Resource-Based Authorization](authentication-identity-access.concept.md#resource-based-authorization)

```interview-choice
Which authorization style is best when access depends on whether the current user owns the specific resource instance?
---
options:
- Simple role checks only
- Resource-based authorization
- Disable authorization for that endpoint
correct: 1
explanation:
Resource-based authorization evaluates the authenticated user together with the specific resource being accessed.
```

## CORS Security

```interview-question
You are building an API for a React SPA on another domain. What CORS configuration do you need, and what are the security implications?
---
answer:
Configure a named CORS policy with explicit origins, methods, and headers, and place `UseCors(...)` before authentication in the pipeline.

Do not use `AllowAnyOrigin()` with `AllowCredentials()`, and do not use `AllowAnyOrigin()` in production unless the API is truly public and unauthenticated.

CORS must be tested from a real browser because tools like Postman do not enforce browser CORS rules.
hints:
- Be specific about origins.
- Credentials plus wildcard origins is a major red flag.
- Middleware order matters here too.
```

Related concepts: [CORS Security](authentication-api-hardening.concept.md#cors-security)

## Secrets Management

```interview-question
How do you securely store connection strings, API keys, and other secrets in a .NET application across environments?
---
answer:
Use layered secret management by environment: user secrets for local development, environment variables or a secret manager in production, and non-sensitive defaults in `appsettings.json`.

Never commit secrets to source control and never rely on `appsettings.json` plus `.gitignore` as the security boundary.

In cloud environments, services such as Azure Key Vault or AWS Secrets Manager are the usual production solution.
hints:
- Development and production should not use the same secret source.
- Source control is never the place for secrets.
- Secret managers exist for a reason.
```

Related concepts: [Secret Management](authentication-api-hardening.concept.md#secret-management)

## Multiple Authentication Schemes

```interview-question
Your API needs to support both JWT bearer tokens and API key authentication. How do you configure multiple authentication schemes?
---
answer:
Register both schemes with the authentication builder and configure authorization so endpoints can accept either the bearer scheme or the API key scheme.

A custom API key handler should return `AuthenticateResult.NoResult()` when the key header is missing so other schemes still have a chance to authenticate the request.

This keeps authentication inside the framework pipeline instead of re-implementing it in custom middleware.
hints:
- Both schemes belong in the authentication system.
- Missing API key should not automatically fail the whole request.
- The handler result matters.
```

Related concepts: [Multiple Authentication Schemes](authentication-identity-access.concept.md#multiple-authentication-schemes), [API Key Authentication](authentication-identity-access.concept.md#api-key-authentication)

```interview-choice
What should a custom API key handler usually return when the API key header is absent and another auth scheme may still succeed?
---
options:
- `AuthenticateResult.Fail(...)`
- `AuthenticateResult.NoResult()`
- Throw an exception immediately
correct: 1
explanation:
`NoResult()` allows the next configured scheme, such as JWT bearer authentication, to try authenticating the request.
```

## Sensitive Data in Responses

```interview-question
Your API returns user data. How do you ensure sensitive fields like password hashes, SSNs, or internal notes never appear in API responses?
---
answer:
Use response DTOs and project directly into them instead of returning entities.

This makes the allowed response contract explicit and avoids accidental leakage if entity shape changes. Relying only on `[JsonIgnore]` on entity properties is more fragile because it depends on serialization behavior and developer discipline.
hints:
- The safest solution controls the response shape explicitly.
- The entity is not the response contract.
- Projection helps avoid even loading sensitive columns when they are not needed.
```

Related concepts: [Response DTOs](authentication-api-hardening.concept.md#response-dtos), [Data Exposure Control](authentication-api-hardening.concept.md#data-exposure-control)

## Security Headers

```interview-question
What are the most common security headers your API should return, and how do you configure them?
---
answer:
Common API-relevant headers include `X-Content-Type-Options: nosniff`, HSTS through `UseHsts()`, and often `X-Frame-Options`, `Referrer-Policy`, and `Permissions-Policy` depending on what the API serves.

They are usually added in middleware, and Kestrel's `Server` header should also be removed when possible to avoid leaking infrastructure details.

If the API serves any HTML tooling, CSP also becomes relevant.
hints:
- APIs still benefit from HTTP security headers.
- Some headers are added directly, one comes from HSTS middleware.
- Reducing exposed server details is also useful.
```

Related concepts: [Security Headers](authentication-api-hardening.concept.md#security-headers)
