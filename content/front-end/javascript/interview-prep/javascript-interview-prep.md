# JavaScript Interview Prep

This page condenses the main question areas from a large JavaScript interview catalog into one study-oriented guide.

## Coverage map

The source question set spans these areas:

- fundamentals
- functions and higher-order functions
- objects and prototypes
- asynchronous JavaScript
- DOM manipulation and browser APIs
- modern JavaScript features
- event handling
- storage and web security
- debugging and performance
- testing and networking
- patterns, tooling, and web platform knowledge
- framework awareness and mobile development context

## Fundamentals to answer clearly

Interviewers usually expect concise, stable answers for:

- primitive versus reference values
- `null` versus `undefined`
- coercion and equality
- hoisting and scope
- closures
- `this`
- arrow functions

Useful answer shape:

1. define the concept in one sentence
2. state the main practical consequence
3. give a short example or contrast

## Objects and prototypes

JavaScript objects can be created with object literals, factory functions, constructor functions, classes, or `Object.create`.

Prototype-based inheritance means objects can delegate property lookup through the prototype chain.

```javascript
const animal = {
  speak() {
    return 'sound';
  }
};

const dog = Object.create(animal);
dog.name = 'Rex';

console.log(dog.speak()); // 'sound'
```

Interview reminder:

- say that classes are syntax on top of prototypes, not a separate inheritance model

## Async model

A strong JavaScript answer usually connects these pieces correctly:

- call stack
- Web APIs or host APIs
- callback queue
- microtask queue
- event loop
- Promises and `async`/`await`

High-value distinctions:

- callbacks are a lower-level async pattern
- Promises model eventual completion or failure
- `async`/`await` improves readability but still uses Promises underneath
- microtasks such as resolved Promise callbacks run before the next macrotask

## DOM and browser APIs

Frontend JavaScript interviews often shift from language questions into browser behavior.

Know how to explain:

- DOM selection with `querySelector` and related APIs
- creating, appending, and removing elements
- event bubbling and event delegation
- `preventDefault()` versus `stopPropagation()`
- `localStorage`, `sessionStorage`, and cookies
- browser history manipulation

```javascript
document.querySelector('#save').addEventListener('click', event => {
  event.preventDefault();
});
```

## Modern syntax worth knowing cold

Be comfortable using and explaining:

- `let` and `const`
- destructuring
- template literals
- rest and spread
- default parameters
- modules
- dynamic imports
- `Object.entries()` and `Object.values()`
- `BigInt`

## Security and platform awareness

For browser-facing roles, basic security knowledge matters.

Focus on:

- XSS prevention by treating untrusted input as unsafe HTML
- CORS as a browser enforcement rule around cross-origin requests
- CSP as a policy layer that restricts resource execution

Good interview behavior:

- explain what the browser is protecting
- explain what the server must still validate

## Debugging, performance, and tooling

Common practical topics include:

- browser devtools
- breakpoints and call stacks
- exception handling
- lazy loading
- bundling and minification
- source maps
- ESLint
- npm and build systems like Webpack

Useful framing:

- debugging is about reproducing and narrowing the issue
- performance is about measuring bottlenecks before optimizing
- tooling exists to improve correctness, compatibility, and delivery speed

## Framework and ecosystem awareness

Even a JavaScript interview page may include ecosystem questions.

Be ready to distinguish:

- library versus framework
- React Virtual DOM at a high level
- Angular data binding at a high level
- Vue's approachable component model
- React Native versus browser-based web apps

These answers should stay conceptual unless the role is framework-specific.

## A practical preparation strategy

Use the original question list as a checklist, but group your preparation by mental model instead of memorizing 100 isolated answers.

Suggested order:

1. foundations and functions
2. objects, prototypes, and modern syntax
3. async execution model
4. DOM, events, and storage
5. security, networking, and performance
6. tooling, frameworks, and mobile context

## Interview reminders

- prefer precise language over long language
- use `===` unless you can justify coercion
- say "lexical scope" when explaining closures
- say "call site decides `this` for regular functions"
- say "`async`/`await` is Promise-based syntax"
- say "event delegation reduces many child listeners to one parent listener"
