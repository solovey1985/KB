# TypeScript Basic

TypeScript is a statically typed superset of JavaScript.

That means:

- valid JavaScript is valid TypeScript
- TypeScript adds a type system and stronger tooling
- TypeScript code is compiled to JavaScript before running in the browser

## Why TypeScript helps

TypeScript improves:

- editor support
- autocomplete and navigation
- refactoring safety
- early error detection
- API clarity in larger codebases

## Basic types

```ts
let name: string = 'Ana';
let age: number = 32;
let active: boolean = true;
let tags: string[] = ['frontend', 'typescript'];
let status: 'idle' | 'loading' | 'success' | 'error' = 'idle';
```

Common early-use types include:

- `string`
- `number`
- `boolean`
- arrays
- tuples
- unions
- `null` / `undefined`
- `void`
- `never`

## Type inference

You do not always need explicit annotations.

```ts
let count = 10;       // inferred as number
let message = 'Hi';   // inferred as string
```

Inference is useful, but explicit types are still valuable when they improve readability or API design.

## Variables and declarations

Prefer `const` by default, then `let` when reassignment is necessary.

Avoid `var` in modern code.

```ts
const apiBase = '/api';
let retries = 0;
```

## Interfaces and type aliases

```ts
interface User {
  id: number;
  name: string;
}

type Status = 'idle' | 'loading' | 'success' | 'error';
```

Use interfaces naturally for object-like contracts.

Use type aliases when naming unions, intersections, tuples, or broader type expressions.

## Enums

Enums provide named constants, though many teams prefer union literals for simpler frontend cases.

```ts
enum RequestState {
  Idle,
  Loading,
  Success,
  Error,
}
```

## Compilation basics

TypeScript is compiled with `tsc` or integrated build tooling.

Key ideas:

- `tsconfig.json` controls compilation
- strictness options shape safety level
- TypeScript types are erased from runtime JavaScript output

## Interview reminders

- explain TypeScript as JavaScript plus static analysis and tooling
- mention inference, not only annotations
- distinguish `interface` and `type` by use case, not dogma
- mention strictness as a practical project choice
