# TypeScript Advanced

Advanced TypeScript is about designing expressive reusable types, not just adding more annotations.

## Generics

Generics let code work across many value types while preserving useful relationships between inputs and outputs.

```ts
function first<T>(items: T[]): T | undefined {
  return items[0];
}
```

## Generic constraints

Constraints narrow what a generic type is allowed to be.

```ts
interface Lengthwise {
  length: number;
}

function logLength<T extends Lengthwise>(value: T): T {
  console.log(value.length);
  return value;
}
```

## Utility types

Utility types help transform and reuse types without repeating structure manually.

Common ones include:

- `Partial<T>`
- `Required<T>`
- `Readonly<T>`
- `Pick<T, K>`
- `Omit<T, K>`
- `Record<K, V>`

```ts
interface User {
  id: number;
  name: string;
  email: string;
}

type UserPreview = Pick<User, 'id' | 'name'>;
```

## Advanced type operators

Advanced TypeScript often uses:

- `keyof`
- indexed access types
- conditional types
- mapped types
- template literal types

```ts
type Keys = keyof User; // 'id' | 'name' | 'email'
```

## Practical advice

Advanced types should improve clarity and safety.

If the type system becomes harder to understand than the problem itself, the design may be too clever.

## Interview reminders

- explain generics as reusable type-safe relationships
- mention constraints when discussing practical generic design
- treat utility types as everyday advanced tools
- mention readability trade-offs when discussing complex mapped or conditional types
