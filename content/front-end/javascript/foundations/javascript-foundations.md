# JavaScript Foundations

Use this page to build a stable mental model for values, scope, equality, closures, and execution context.

## Why this topic matters

Almost every JavaScript interview question becomes easier when you understand:

- how values are represented
- where variables are visible
- how coercion and equality work
- what closures capture
- how `this` is chosen

## Primitive and reference values

JavaScript has primitive values such as `string`, `number`, `boolean`, `null`, `undefined`, `bigint`, and `symbol`.

Objects, arrays, and functions are reference values.

```javascript
const language = 'JavaScript';
const version = 2026;
const tags = ['frontend', 'runtime'];
const settings = { strict: true };
```

Practical distinction:

- primitives are copied by value
- objects and arrays are copied by reference

```javascript
const a = { done: false };
const b = a;

b.done = true;

console.log(a.done); // true
```

## Scope and declarations

`var` is function-scoped and hoisted differently from `let` and `const`.

`let` and `const` are block-scoped and live in the temporal dead zone until initialization.

```javascript
function demoScope() {
  if (true) {
    var oldStyle = 'function scope';
    const modern = 'block scope';
  }

  console.log(oldStyle); // function scope
  // console.log(modern); // ReferenceError
}
```

## Equality and coercion

`==` performs type coercion before comparing values.

`===` compares both type and value, so it is the safer default.

```javascript
console.log('5' == 5);  // true
console.log('5' === 5); // false
console.log(null == undefined);  // true
console.log(null === undefined); // false
```

Rule of thumb:

- prefer `===`
- use `==` only when you intentionally want coercion and can explain it

## Closures

A closure is a function that keeps access to variables from the lexical scope where it was created.

```javascript
function createCounter() {
  let count = 0;

  return function increment() {
    count += 1;
    return count;
  };
}

const counter = createCounter();

console.log(counter()); // 1
console.log(counter()); // 2
```

Closures are useful for:

- data hiding
- callbacks and event handlers
- factory functions

## The `this` keyword

For regular functions, `this` depends on how the function is called.

Arrow functions do not create their own `this`; they use the surrounding lexical value.

```javascript
const user = {
  name: 'Mira',
  regular() {
    return this.name;
  },
  arrow: () => this
};

console.log(user.regular()); // 'Mira'
console.log(user.arrow());   // lexical this, not user
```

## Interview reminders

- say "lexical scope" when discussing closures
- say "invocation determines `this`" for regular functions
- say "strict equality avoids coercion surprises" when comparing `==` and `===`
