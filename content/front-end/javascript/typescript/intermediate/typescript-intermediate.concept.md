---
title: TypeScript Intermediate Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes practical TypeScript features used for real application design.

Study pages: [Section Index](index.md) | [Material Notes](typescript-intermediate.md) | [Interview Practice](typescript-intermediate.interview.md)

## Intermediate Map

```concept-card
id: typed-functions
term: Typed Functions
children:
- function-overloads
summary:
Typed functions use parameter and return types to make behavior explicit and checkable.
details:
TypeScript supports optional parameters, default values, rest parameters, and richer function signatures for reusable APIs.
example:
`function greet(name: string, title?: string): string { ... }`
mnemonic:
Functions should say what they accept and return.
recall:
- What parts of a function can TypeScript type?
- Why does that improve maintainability?
```

```concept-card
id: function-overloads
term: Function Overloads
parents:
- typed-functions
summary:
Function overloads let one implementation expose multiple type-level call signatures.
details:
They are useful when the same logical operation supports multiple valid input shapes.
example:
One greeting function may accept just a name or a title and a name.
mnemonic:
Many call shapes, one implementation body.
recall:
- What problem do overloads solve?
- Why is there still only one runtime implementation?
```

```concept-card
id: typescript-classes
term: TypeScript Classes
children:
- access-modifiers
- abstract-class
summary:
TypeScript classes extend JavaScript classes with stronger type-system features and member declarations.
details:
They add compile-time structure such as typed fields, abstract members, readonly members, and parameter properties.
example:
`class User { constructor(public id: number) {} }`
mnemonic:
JavaScript classes with stronger design signals.
recall:
- What does TypeScript add to classes beyond ES syntax?
- Why do these additions matter in larger codebases?
```

```concept-card
id: access-modifiers
term: Access Modifiers
parents:
- typescript-classes
summary:
Access modifiers control intended visibility of class members.
details:
`public`, `private`, and `protected` help model encapsulation and API boundaries in class design.
example:
`private token: string` should not be consumed by outside callers directly.
mnemonic:
Good class APIs expose less, not more.
recall:
- What does each access modifier communicate?
- Why are access modifiers useful even though runtime JavaScript remains flexible?
```

```concept-card
id: abstract-class
term: Abstract Class
parents:
- typescript-classes
summary:
An abstract class defines a partially implemented base type that cannot be instantiated directly.
details:
It is useful when related subclasses should share behavior and must also implement specific contracts.
example:
`abstract class Shape { abstract area(): number; }`
mnemonic:
Shared base behavior without direct instances.
recall:
- Why use an abstract class instead of a normal class?
- What must subclasses provide?
```

```concept-card
id: typescript-compiler
term: TypeScript Compiler
children:
- tsconfig
summary:
The TypeScript compiler checks types and emits JavaScript output.
details:
It is usually invoked through `tsc` or higher-level build tooling that still relies on the same compiler model.
example:
`tsc --noEmitOnError` prevents JavaScript output if compilation errors remain.
mnemonic:
Check types, then emit JavaScript.
recall:
- What jobs does the compiler perform?
- Why does TypeScript still need a compile step?
```

```concept-card
id: tsconfig
term: tsconfig
parents:
- typescript-compiler
summary:
`tsconfig.json` is the configuration file that defines TypeScript compiler behavior for a project.
details:
It controls target output, module format, strictness, included files, and many other project-wide behaviors.
example:
`{ "compilerOptions": { "strict": true, "target": "ES2020" } }`
mnemonic:
One file tells the compiler how the project should behave.
recall:
- Why is `tsconfig.json` important in real projects?
- Which compiler settings are especially impactful?
```
