---
title: Authentication and Security Senior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the senior-level authentication and security scenarios from the Web API interview question set.

Relevant concept maps:

- [Identity and Access Concept Map](authentication-identity-access.concept.md)
- [API Hardening Concept Map](authentication-api-hardening.concept.md)

## JWT Stale Claims

```interview-question
Your JWT access token is valid for 15 minutes. A user's role changes from Admin to User, but the old token still contains the Admin claim. How do you handle this?
---
answer:
This is a stale-claims problem caused by the stateless nature of JWT access tokens.

The normal answer is short-lived access tokens plus refresh tokens, so the next token refresh re-reads current roles from the database. For highly sensitive actions, re-check current authorization state server-side even if the token already contains a role claim.

A token blocklist can work for strict revocation requirements, but it reintroduces state and operational complexity.
hints:
- Stateless tokens do not update themselves.
- Short access-token lifetime narrows the exposure window.
- Sensitive operations can re-check current roles.
```

Related concepts: [JWT Access Tokens](authentication-identity-access.concept.md#jwt-access-tokens), [Refresh Tokens](authentication-identity-access.concept.md#refresh-tokens), [Stale Claims Mitigation](authentication-identity-access.concept.md#stale-claims-mitigation)

```interview-choice
Which approach is the normal production default for reducing stale JWT claim exposure?
---
options:
- Long-lived access tokens with no refresh token support
- Short-lived access tokens plus refresh tokens
- Trust the old claim forever until the token expires naturally, no matter the action
correct: 1
explanation:
Short-lived access tokens reduce the stale-claim window, and refresh token exchange lets the server issue a new token using current identity state.
```

## Over-Posting Protection

```interview-question
How do you prevent mass assignment or over-posting attacks in your API?
---
answer:
Never bind request bodies directly to entity types. Use dedicated request DTOs that only contain fields the client is allowed to set.

Then map explicitly from the request DTO to the entity and set protected fields such as `IsAdmin`, `CreatedAt`, or internal flags on the server side.

This is much safer than trying to exclude dangerous fields one by one with binding attributes.
hints:
- The danger comes from binding too much, not too little.
- The entity should not be the request contract.
- Explicit mapping is a security control here.
```

Related concepts: [Over-Posting Prevention](authentication-api-hardening.concept.md#over-posting-prevention), [Request DTOs](authentication-api-hardening.concept.md#request-dtos)

## Secure API Keys

```interview-question
How do you implement API key authentication that is actually secure, not just a string comparison against configuration?
---
answer:
Generate strong random keys, show the raw key only once, and store only a hash of it in the database.

Validate presented keys with constant-time comparison, support expiration and rotation, log usage, and model per-key scopes so different keys can have different permissions.

A single plaintext config value with `==` comparison is not operationally or cryptographically strong enough for production.
hints:
- Treat API keys more like credentials than config values.
- Store hashes, not raw keys.
- Rotation and scopes are part of real production design.
```

Related concepts: [API Key Authentication](authentication-identity-access.concept.md#api-key-authentication), [API Key Hardening](authentication-api-hardening.concept.md#api-key-hardening)

## Secrets in Responses

```interview-question
How do you ensure sensitive fields such as password hashes, SSNs, or internal IDs never appear in API responses?
---
answer:
Use explicit response DTOs and project directly into them. That makes the response shape a controlled contract instead of an accident of the entity model.

This should be reinforced with tests that verify sensitive fields are absent, especially on user and admin-facing endpoints.

`[JsonIgnore]` can still be useful, but it should not be the only protection layer.
hints:
- The shape of the response should be explicit and intentional.
- Tests help prevent accidental future leaks.
- The safest approach avoids returning entities directly.
```

Related concepts: [Response DTOs](authentication-api-hardening.concept.md#response-dtos), [Data Exposure Control](authentication-api-hardening.concept.md#data-exposure-control)

## Common Web Attacks

```interview-question
How do you protect your API against common attacks like SQL injection, XSS, and CSRF?
---
answer:
For SQL injection, use EF Core or parameterized SQL and never concatenate untrusted input.

For XSS, avoid reflecting unsanitized user input into HTML and remember that JSON APIs are safer than HTML output, but not immune if they generate HTML-based content or unsafe error output.

For CSRF, the key distinction is token transport: bearer tokens in `Authorization` headers are not automatically sent by the browser, while cookie-based auth needs CSRF protections such as anti-forgery tokens, strict `SameSite`, or verified origins.
hints:
- Each attack targets a different layer.
- EF Core helps with one of them, not all of them.
- Cookies and bearer tokens behave differently in browsers.
```

Related concepts: [SQL Injection Defense](authentication-api-hardening.concept.md#sql-injection-defense), [XSS Considerations](authentication-api-hardening.concept.md#xss-considerations), [CSRF Defense](authentication-api-hardening.concept.md#csrf-defense)

```interview-choice
Why is CSRF usually not the main concern for a pure JWT bearer-token API?
---
options:
- Browsers do not automatically attach bearer tokens from the `Authorization` header like they do cookies
- JWTs cannot be stolen
- CSRF only exists in MVC apps, not APIs
correct: 0
explanation:
CSRF relies on the browser automatically sending credentials, which fits cookie auth much more than explicit bearer tokens in headers.
```
