---
title: JavaScript Interview Prep Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the highest-signal JavaScript questions collected from a broader interview question catalog.

Relevant study pages:

- [Material Notes](javascript-interview-prep.md)
- [Concept Map](javascript-interview-prep.concept.md)

## Fundamentals

```interview-question
What is the difference between `null` and `undefined`?
---
answer:
`undefined` usually means a value is missing implicitly, such as an uninitialized variable or absent property.

`null` usually means an intentional empty value chosen by the developer or returned by an API.
hints:
- One is usually implicit.
- One is usually explicit.
- Both represent absence, but not the same intent.
```

Related concepts: [null Versus undefined](javascript-interview-prep.concept.md#null-vs-undefined)

```interview-question
How does JavaScript handle type coercion?
---
answer:
JavaScript can automatically convert values from one type to another during comparisons or operations.

This is called type coercion, and it is why expressions like `'5' == 5` can evaluate to `true` while `'5' === 5` does not.
hints:
- Think automatic conversion.
- Comparisons are a common place it happens.
- `==` and `===` behave differently because of it.
```

Related concepts: [Type Coercion](javascript-interview-prep.concept.md#type-coercion), [Strict Equality](javascript-interview-prep.concept.md#strict-equality)

```interview-choice
Which expression evaluates to `true`?
---
options:
- `'5' === 5`
- `'5' == 5`
- `null === undefined`
correct: 1
explanation:
`==` allows coercion before comparison, so the string `'5'` is converted before being compared to the number `5`.
```

```interview-question
Explain hoisting in JavaScript.
---
answer:
Hoisting is the behavior where declarations are processed before the surrounding code executes.

`var` bindings are available early as `undefined`, while `let` and `const` are hoisted too but remain uninitialized until their declaration runs.
hints:
- The name exists before execution reaches the line.
- `var` behaves differently from `let` and `const`.
- The key distinction is initialization timing.
```

Related concepts: [Hoisting](javascript-interview-prep.concept.md#hoisting)

```interview-question
Describe closure in JavaScript. Can you give an example?
---
answer:
A closure is a function that keeps access to variables from the lexical scope where it was created.

Example: a counter factory can return an inner function that still reads and updates `count` even after the outer function has finished.
hints:
- Think lexical scope.
- The outer function may already be done.
- The inner function still remembers data.
```

Related concepts: [Closure](javascript-interview-prep.concept.md#closure), [Lexical Scope](javascript-interview-prep.concept.md#lexical-scope)

```interview-question
What is the `this` keyword and how does its context change?
---
answer:
For regular functions, `this` depends on how the function is called.

For arrow functions, `this` comes from the surrounding lexical scope and does not get its own call-site binding.
hints:
- Method call versus standalone call matters.
- Arrow functions are different.
- Do not confuse `this` with lexical scope.
```

Related concepts: [this Binding](javascript-interview-prep.concept.md#this-binding), [Arrow Function this](javascript-interview-prep.concept.md#arrow-function-this)

## Functions and Objects

```interview-question
What is a higher-order function in JavaScript?
---
answer:
A higher-order function is a function that takes another function as an argument, returns a function, or both.

Examples include `map`, `filter`, and function factories.
hints:
- Another function is involved.
- It may be an input or an output.
- Array methods are common examples.
```

```interview-question
What are prototypes in JavaScript?
---
answer:
A prototype is the object another object delegates to during property lookup when the property is not found directly on itself.

This is part of how JavaScript shares behavior between objects.
hints:
- Think delegation, not copying.
- Property lookup moves upward.
- Objects can share behavior through it.
```

Related concepts: [Objects and Prototypes](javascript-interview-prep.concept.md#objects-and-prototypes), [Prototype Chain](javascript-interview-prep.concept.md#prototype-chain)

```interview-question
Explain prototypal inheritance.
---
answer:
Prototypal inheritance means one object can inherit behavior by delegating missing property lookups to another object through the prototype chain.

JavaScript classes are modern syntax over this same underlying model.
hints:
- Inheritance happens through linked objects.
- Missing properties trigger lookup.
- Classes do not replace the prototype model underneath.
```

Related concepts: [Prototypal Inheritance](javascript-interview-prep.concept.md#prototypal-inheritance)

```interview-code
language: js
prompt: Complete the function so it returns a closure that increments private state.
starter:
function createCounter() {
  let count = 0;

  return function () {
    
  };
}
solution:
function createCounter() {
  let count = 0;

  return function () {
    count += 1;
    return count;
  };
}
checks:
- includes: count += 1
- includes: return count
```

## Async JavaScript

```interview-question
What is the event loop in JavaScript?
---
answer:
The event loop is the runtime mechanism that checks whether the call stack is clear and then moves queued work onto it.

It allows JavaScript to handle asynchronous tasks while still executing one stack frame at a time.
hints:
- Think stack plus queued work.
- It coordinates when deferred callbacks can run.
- It matters because JavaScript is single-threaded at the language level.
```

Related concepts: [Event Loop](javascript-interview-prep.concept.md#event-loop), [Async Execution](javascript-interview-prep.concept.md#async-execution)

```interview-question
What are promises and how do they manage asynchronous code?
---
answer:
Promises represent the eventual success or failure of asynchronous work.

They let you chain steps with `.then(...)`, handle failures with `.catch(...)`, and avoid deeply nested callback structures.
hints:
- Think eventual result.
- Success and failure are both modeled.
- Chaining is a major benefit.
```

Related concepts: [Promises](javascript-interview-prep.concept.md#promises)

```interview-question
Explain `async`/`await` in JavaScript and how it differs from Promises.
---
answer:
`async`/`await` is Promise-based syntax for writing asynchronous flows in a more readable sequential style.

It does not replace Promises underneath; it is a clearer way to consume them.
hints:
- It still uses Promises.
- The main difference is syntax and readability.
- Errors are usually handled with `try` and `catch`.
```

Related concepts: [async/await](javascript-interview-prep.concept.md#async-await), [Promises](javascript-interview-prep.concept.md#promises)

```interview-choice
Which queued work typically runs before a `setTimeout(..., 0)` callback?
---
options:
- A resolved Promise handler
- A DOM click handler from a future click
- A new script tag download
correct: 0
explanation:
Resolved Promise reactions are processed as microtasks, which run before the next normal task such as a timer callback.
```

## DOM and Browser

```interview-question
Explain event propagation in the DOM.
---
answer:
Event propagation is how an event moves through the DOM, typically through capturing, target handling, and bubbling phases.

In many practical frontend discussions, the most important part is bubbling because parent elements can observe child interactions.
hints:
- The event does not belong only to the target element.
- Bubbling is important for delegation.
- There are phases, not just one step.
```

```interview-question
What is event delegation and why is it useful?
---
answer:
Event delegation is the pattern of attaching one listener to a parent element and handling matching child interactions through bubbling.

It reduces repeated listeners and works well with dynamic lists or frequently changing child elements.
hints:
- One ancestor listener can handle many children.
- Bubbling makes it possible.
- It helps both maintainability and memory use.
```

Related concepts: [Event Delegation](javascript-interview-prep.concept.md#event-delegation)

```interview-question
What is the difference between `localStorage`, `sessionStorage`, and cookies?
---
answer:
`localStorage` persists across browser sessions, `sessionStorage` lasts for the current tab session, and cookies can also be sent automatically with HTTP requests.

Cookies are often discussed separately because they affect request behavior and security decisions.
hints:
- Persistence is one distinction.
- Request transmission is another.
- Only one is automatically included with requests.
```

Related concepts: [Browser Storage](javascript-interview-prep.concept.md#browser-storage)

```interview-question
Can you explain Cross-Site Scripting and how to prevent it?
---
answer:
Cross-Site Scripting, or XSS, happens when untrusted content is injected into a page and executed in the browser.

Prevention includes output encoding, avoiding unsafe HTML insertion, sanitizing where needed, and using protections such as CSP.
hints:
- The browser executes something it should not trust.
- Unsafe HTML insertion is a common path.
- Prevention is both coding discipline and browser policy.
```

Related concepts: [XSS](javascript-interview-prep.concept.md#xss), [CSP](javascript-interview-prep.concept.md#csp)

```interview-question
What is CORS and how does it work?
---
answer:
CORS is a browser-enforced cross-origin access policy.

The browser checks the server's response headers to decide whether frontend JavaScript is allowed to read the cross-origin response.
hints:
- It is enforced by the browser.
- The server opts in with headers.
- It is about reading cross-origin responses from frontend code.
```

Related concepts: [CORS](javascript-interview-prep.concept.md#cors)

## Tooling and Performance

```interview-question
What tools and techniques do you use for debugging JavaScript code?
---
answer:
Common tools include browser devtools, breakpoints, stack traces, network inspection, and targeted logging.

The practical goal is to reproduce the issue, inspect real runtime state, and narrow the failure path quickly.
hints:
- Browser devtools come first.
- Breakpoints are usually more precise than random logs.
- Runtime inspection matters.
```

Related concepts: [Debugging](javascript-interview-prep.concept.md#debugging)

```interview-question
What techniques can be used to improve JavaScript performance?
---
answer:
Useful techniques include reducing unnecessary work, splitting code, lazy loading, minimizing bundle size, avoiding blocking synchronous tasks, and measuring bottlenecks before changing code.

The strongest answer starts with profiling rather than guessing.
hints:
- Measure first.
- Think runtime work and delivery size.
- Lazy loading is one example, not the whole answer.
```

Related concepts: [Performance and Optimization](javascript-interview-prep.concept.md#performance)

```interview-question
What is Babel and how is it used in JavaScript development?
---
answer:
Babel is a transpiler that converts newer JavaScript syntax into code that older target environments can run.

It is commonly used in build pipelines to improve compatibility across browsers.
hints:
- Think compatibility.
- It changes source syntax into supported output.
- It is part of build tooling, not runtime behavior.
```

Related concepts: [Build Tooling](javascript-interview-prep.concept.md#build-tooling)

```interview-question
What is a source map?
---
answer:
A source map links transformed production code back to the original source files.

It helps developers debug bundled or minified code using the original file and line structure.
hints:
- It connects built code to source code.
- Debugging is the main reason.
- Bundling and minification make it necessary.
```

Related concepts: [Build Tooling](javascript-interview-prep.concept.md#build-tooling)
