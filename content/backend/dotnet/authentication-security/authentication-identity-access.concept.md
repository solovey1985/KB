---
title: Authentication Identity and Access Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the identity and access-control concepts behind the junior, middle, and part of the senior interview questions.

Study pages: [Section Index](index.md) | [Junior Questions](junior.interview.md) | [Middle Questions](middle.interview.md) | [Senior Questions](senior.interview.md)

## Identity Map

```concept-card
id: authentication-and-security
term: Authentication and Security
children:
- authentication
- authorization
- policy-based-authorization
- resource-based-authorization
- jwt-access-tokens
- refresh-tokens
- stale-claims-mitigation
- multiple-authentication-schemes
- api-key-authentication
summary:
Authentication and security in Web APIs cover identity proof, permission checks, credential handling, and safe access patterns.
details:
An API must know who the caller is, what that caller can do, and how to protect the system when credentials, tokens, and authorization decisions change over time.
example:
`POST /api/login` authenticates the user, while `GET /api/admin/users` authorizes whether that authenticated identity may view admin data.
mnemonic:
Know the caller, check the right, protect the boundary.
recall:
- What major areas sit under API authentication and security?
- Why are identity proof and permission checks separate concerns?
```

```concept-card
id: authentication
term: Authentication
parents:
- authentication-and-security
related:
- authorization
summary:
Authentication is the process of proving who the caller is.
details:
The API validates credentials or tokens and builds an identity for the request. Without successful authentication, authorization decisions cannot proceed meaningfully.
example:
A valid bearer token can authenticate the request and produce a `ClaimsPrincipal` for the rest of the pipeline.
mnemonic:
First prove who is calling.
recall:
- What question does authentication answer?
- Why must authentication happen before authorization?
```

```concept-card
id: authorization
term: Authorization
parents:
- authentication-and-security
related:
- authentication
summary:
Authorization is the process of deciding what an authenticated caller is allowed to do.
details:
It uses roles, claims, policies, or resource-specific rules to determine whether access should be granted.
example:
An authenticated `Editor` may still receive `403 Forbidden` on an admin-only endpoint.
mnemonic:
Then decide what that identity may do.
recall:
- What question does authorization answer?
- Why can a request be authenticated but still denied?
```

```concept-card
id: policy-based-authorization
term: Policy-Based Authorization
parents:
- authorization
related:
- resource-based-authorization
summary:
Policy-based authorization combines roles, claims, and custom requirements into reusable named access rules.
details:
It is more scalable than ad hoc role checks because access logic is centralized and can express richer combinations of conditions.
example:
`CanEditProduct` can require both the `ProductManager` role and an `inventory` department claim.
mnemonic:
Name the rule, reuse the rule.
recall:
- Why do policies scale better than scattered role checks?
- What kinds of requirements can a policy combine?
```

```concept-card
id: resource-based-authorization
term: Resource-Based Authorization
parents:
- authorization
related:
- policy-based-authorization
summary:
Resource-based authorization evaluates access against the specific resource instance being acted on.
details:
It is used when access depends on facts such as resource ownership, tenant membership, or record state rather than just static roles.
example:
A user may edit an order only when `order.CreatedByUserId == currentUserId`.
mnemonic:
Check the user against this resource.
recall:
- When is resource-based authorization necessary?
- Why are role checks alone often insufficient for ownership rules?
```

```concept-card
id: jwt-access-tokens
term: JWT Access Tokens
parents:
- authentication-and-security
children:
- refresh-tokens
- stale-claims-mitigation
summary:
JWT access tokens are self-contained tokens that let APIs authenticate callers without re-querying identity data on every request.
details:
They are efficient and stateless, but their claims become stale until the token expires or the API performs additional server-side checks.
example:
A token issued with the `Admin` role stays `Admin` until expiry even if the database role changes sooner.
mnemonic:
Fast to verify, slow to forget old claims.
recall:
- What operational strength makes JWT access tokens popular?
- What security trade-off appears when claims change after token issuance?
```

```concept-card
id: refresh-tokens
term: Refresh Tokens
parents:
- jwt-access-tokens
summary:
Refresh tokens let a client obtain new access tokens without forcing the user to log in again.
details:
They are the usual way to keep access tokens short-lived while still providing a practical login experience. The refresh flow is where the server can re-check current roles and account state.
example:
An expired 5-minute access token can be replaced through a refresh endpoint that re-checks the user's current permissions.
mnemonic:
Short token, longer session.
recall:
- Why do refresh tokens pair naturally with short-lived access tokens?
- What security decision can happen during refresh that cannot happen inside an old access token?
```

```concept-card
id: stale-claims-mitigation
term: Stale Claims Mitigation
parents:
- jwt-access-tokens
summary:
Stale claims mitigation reduces the risk of old permissions remaining valid inside already issued tokens.
details:
Common approaches include short token lifetimes, refresh token exchange, re-checking permissions for sensitive actions, and blocklists when hard revocation is required.
mnemonic:
Old token, smaller window, stronger checks.
recall:
- What is the stale-claims problem?
- Which mitigations are practical defaults versus heavy revocation strategies?
```

```concept-card
id: multiple-authentication-schemes
term: Multiple Authentication Schemes
parents:
- authentication-and-security
related:
- api-key-authentication
summary:
Multiple authentication schemes let an API accept different credential types such as bearer tokens and API keys.
details:
They should be configured through ASP.NET Core authentication so each scheme can authenticate cleanly without bypassing the framework pipeline.
example:
One endpoint can accept either `Authorization: Bearer ...` or `X-API-Key: ...` through two configured schemes.
mnemonic:
Many credentials, one auth pipeline.
recall:
- Why should multiple credential types stay inside the auth framework rather than custom middleware?
- What framework behavior makes scheme composition possible?
```

```concept-card
id: api-key-authentication
term: API Key Authentication
parents:
- authentication-and-security
related:
- multiple-authentication-schemes
summary:
API key authentication uses a client-held secret to authenticate machine-to-machine or partner requests.
details:
Production-quality API key systems need hashed storage, expiration, rotation, scoping, and logging rather than one shared plaintext string in configuration.
example:
Store `SHA-256(key)` plus scopes and expiration date, then show the raw key only once at creation time.
mnemonic:
Treat keys like passwords, not app settings.
recall:
- Why is a plaintext config key comparison not enough for production?
- What operational features make API keys safer to manage?
```
