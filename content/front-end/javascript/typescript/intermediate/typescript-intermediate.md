# TypeScript Intermediate

This level focuses on the parts of TypeScript that shape real application APIs and reusable code.

## Functions

TypeScript can type function parameters, return values, optional parameters, default values, rest parameters, and overloads.

```ts
function greet(name: string, title?: string): string {
  return title ? `Hello, ${title} ${name}` : `Hello, ${name}`;
}

function sumAll(...values: number[]): number {
  return values.reduce((acc, value) => acc + value, 0);
}
```

## Object typing

```ts
interface Product {
  id: number;
  title: string;
  price: number;
}

function printProduct(product: Product) {
  console.log(product.title, product.price);
}
```

## Classes in TypeScript

TypeScript classes extend ES classes with stronger typing and access modifiers.

```ts
class User {
  constructor(
    public id: number,
    private token: string,
    readonly email: string,
  ) {}

  getMaskedToken() {
    return this.token.slice(0, 4) + '...';
  }
}
```

## Inheritance and abstract classes

```ts
abstract class Shape {
  abstract area(): number;
}

class Square extends Shape {
  constructor(private size: number) {
    super();
  }

  area(): number {
    return this.size * this.size;
  }
}
```

## Compilation and configuration

At this level, developers should understand:

- how `tsc` compiles `.ts` into `.js`
- why `tsconfig.json` exists
- why target, module, and strict settings matter

```json
{
  "compilerOptions": {
    "target": "ES2020",
    "module": "ESNext",
    "strict": true,
    "outDir": "dist"
  }
}
```

## Interview reminders

- explain overloads as type-level call signatures with one implementation
- mention access modifiers as design tools, not runtime security barriers
- describe `tsconfig` as the project contract for the compiler
