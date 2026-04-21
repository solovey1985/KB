# TypeScript Basics for JavaScript Developers

TypeScript adds static type information on top of JavaScript.

## Why it helps

It improves:

- editor support
- refactoring safety
- API design clarity
- early feedback before runtime

## Basic types

```ts
let name: string = 'Ana';
let age: number = 32;
let active: boolean = true;
```

## Function typing

```ts
function greet(name: string): string {
  return `Hello, ${name}`;
}
```

## Interfaces and type aliases

```ts
interface User {
  id: number;
  name: string;
}

type Status = 'idle' | 'loading' | 'success' | 'error';
```

## Generics

```ts
function first<T>(items: T[]): T | undefined {
  return items[0];
}
```

## Interview reminders

- explain TypeScript as a tooling and safety layer on top of JavaScript
- mention interfaces, unions, and generics as common interview anchors
- keep examples small and practical
