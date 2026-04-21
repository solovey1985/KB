# JavaScript Functions and Patterns

This page introduces the function and object patterns that appear constantly in frontend codebases.

## Function declarations and expressions

JavaScript supports function declarations, function expressions, and arrow functions.

```javascript
function greet(name) {
  return `Hello, ${name}`;
}

const greetUser = function (name) {
  return `Hello, ${name}`;
};

const greetArrow = name => `Hello, ${name}`;
```

Use whichever form communicates intent clearly.

## Higher-order functions

A higher-order function accepts another function or returns one.

```javascript
const multiplier = factor => value => value * factor;

const double = multiplier(2);
console.log(double(5)); // 10
```

This pattern matters because array methods, callbacks, and reusable utilities are built on it.

## Array transformation methods

Frontend code often relies on `map`, `filter`, and `reduce`.

```javascript
const users = [
  { name: 'Ana', active: true },
  { name: 'Mira', active: false }
];

const activeNames = users
  .filter(user => user.active)
  .map(user => user.name);
```

## Destructuring and rest/spread

These patterns make object and array handling more expressive.

```javascript
const user = { id: 1, name: 'Ana', role: 'admin' };
const { name, ...rest } = user;

const copy = { ...user, role: 'editor' };
```

## Modules

ES modules let code be split into focused files with explicit dependencies.

```javascript
// math.js
export function sum(a, b) {
  return a + b;
}

// app.js
import { sum } from './math.js';
console.log(sum(2, 3));
```

## IIFEs and private state

IIFEs are less central in modern code because modules solve many of the same problems, but they still matter in interviews and older code.

```javascript
const counterModule = (() => {
  let count = 0;

  return {
    next() {
      count += 1;
      return count;
    }
  };
})();
```

## Interview reminders

- say "higher-order" when a function accepts or returns another function
- say "modules replace much of the old IIFE need" in modern codebases
- use `map`, `filter`, and `reduce` examples naturally in answers
