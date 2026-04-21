---
title: Authentication and Security Junior Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the junior-level authentication and security distinctions from the Web API interview question set.

Relevant concept maps:

- [Identity and Access Concept Map](authentication-identity-access.concept.md)
- [API Hardening Concept Map](authentication-api-hardening.concept.md)

## Authentication Versus Authorization

```interview-question
What is the difference between authentication and authorization? Give a real API example.
---
answer:
Authentication answers who the caller is. Authorization answers what that authenticated caller is allowed to do.

In an API, a login request authenticates the user and issues a token. A later request with that token is authenticated first, and then the API checks whether the user's role or policy allows access to the requested endpoint.

`401` usually means the user is not authenticated, while `403` means the user is authenticated but not allowed.
hints:
- One is identity, the other is permission.
- Tokens prove identity first.
- Status codes help distinguish the two failures.
```

Related concepts: [Authentication](authentication-identity-access.concept.md#authentication), [Authorization](authentication-identity-access.concept.md#authorization)

```interview-choice
Which status code usually means the caller is authenticated but lacks permission?
---
options:
- `401 Unauthorized`
- `403 Forbidden`
- `200 OK`
correct: 1
explanation:
`403 Forbidden` means the identity is known, but the caller does not have the required permission for the resource.
```
