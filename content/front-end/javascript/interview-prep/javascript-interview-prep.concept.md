---
title: JavaScript Interview Prep Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the main JavaScript interview concepts into a single memory map.

Study pages: [Section Index](index.md) | [Material Notes](javascript-interview-prep.md) | [Interview Practice](javascript-interview-prep.interview.md)

## Interview Map

```concept-card
id: javascript-core
term: JavaScript Core
children:
- values-and-equality
- scope-and-closures
- objects-and-prototypes
- async-execution
- dom-and-events
- platform-and-tooling
summary:
JavaScript interview preparation works best when the language is grouped into a few stable mental models instead of many disconnected facts.
details:
Most questions reduce to language fundamentals, object behavior, async execution, browser interaction, and delivery tooling.
recall:
- Which major concept groups cover most JavaScript interviews?
- Why is grouped understanding better than memorizing isolated answers?
```

```concept-card
id: values-and-equality
term: Values and Equality
parents:
- javascript-core
children:
- null-vs-undefined
- type-coercion
- strict-equality
summary:
JavaScript values include primitives and objects, and equality behavior depends on whether coercion is allowed.
details:
This topic explains why `'5' == 5` differs from `'5' === 5`, and why objects behave differently from primitive values.
recall:
- Why is strict equality usually safer?
- How do primitive and reference values differ?
```

```concept-card
id: null-vs-undefined
term: null Versus undefined
parents:
- values-and-equality
summary:
`undefined` usually means a value is missing implicitly, while `null` usually means empty by intention.
details:
Both represent absence, but they communicate different intent in code and APIs.
recall:
- Which one is usually implicit?
- Why might an API return `null` intentionally?
```

```concept-card
id: type-coercion
term: Type Coercion
parents:
- values-and-equality
related:
- strict-equality
summary:
Type coercion is JavaScript's automatic conversion between types during operations or comparisons.
details:
It can make code concise, but it also creates interview traps and production bugs when the conversion rules are unclear.
recall:
- Why does `==` create surprises?
- Which operations commonly trigger coercion?
```

```concept-card
id: strict-equality
term: Strict Equality
parents:
- values-and-equality
summary:
Strict equality compares both value and type without coercion.
details:
It is the safer default because it avoids hidden conversions and makes intent clearer.
recall:
- Why is `===` the default comparison recommendation?
- What does it prevent that `==` allows?
```

```concept-card
id: scope-and-closures
term: Scope and Closures
parents:
- javascript-core
children:
- hoisting
- lexical-scope
- closure
- this-binding
summary:
Scope controls visibility, and closures preserve access to variables from lexical scope.
details:
This cluster explains `var` versus `let` and `const`, hoisting behavior, closure-based state, and context-sensitive `this` rules.
recall:
- Why is lexical scope central to closures?
- What interview mistakes happen when `this` and scope are mixed together?
```

```concept-card
id: hoisting
term: Hoisting
parents:
- scope-and-closures
summary:
Hoisting means declarations are processed before normal execution reaches them.
details:
`var` bindings are initialized to `undefined`, while `let` and `const` remain uninitialized until their declaration runs.
recall:
- How does hoisting differ for `var` versus `let`?
- Why does the temporal dead zone matter even if the term is not named in the question?
```

```concept-card
id: lexical-scope
term: Lexical Scope
parents:
- scope-and-closures
summary:
Lexical scope means variable access depends on where code is written.
details:
It makes closures predictable because inner functions retain access to the outer variables in their declaration environment.
recall:
- What decides variable visibility in lexical scope?
- Why is lexical scope important to interview closure examples?
```

```concept-card
id: closure
term: Closure
parents:
- scope-and-closures
summary:
A closure is a function that remembers and can still access variables from its lexical scope.
details:
Closures are commonly used for factories, private state, callbacks, and module-like encapsulation.
recall:
- What does a closure retain?
- Which real coding patterns rely on closures?
```

```concept-card
id: this-binding
term: this Binding
parents:
- scope-and-closures
children:
- arrow-function-this
summary:
For regular functions, `this` depends on how the function is called.
details:
Method calls, detached function calls, constructors, and explicit binding all change regular function context.
recall:
- What determines `this` in a regular function?
- Why do detached methods often break?
```

```concept-card
id: arrow-function-this
term: Arrow Function this
parents:
- this-binding
summary:
Arrow functions do not create their own `this` and instead use the surrounding lexical value.
details:
This makes them useful in callbacks, but a poor fit for object methods that expect dynamic receiver context.
recall:
- Why can arrow functions simplify callbacks?
- Why are they often wrong for object methods?
```

```concept-card
id: objects-and-prototypes
term: Objects and Prototypes
parents:
- javascript-core
children:
- prototype-chain
- prototypal-inheritance
- object-creation-patterns
summary:
JavaScript objects inherit behavior through prototypes rather than classical class-based inheritance alone.
details:
Classes are modern syntax, but property lookup still follows the prototype chain underneath.
recall:
- What is a prototype in practical terms?
- Why are JavaScript classes still prototype-based?
```

```concept-card
id: prototype-chain
term: Prototype Chain
parents:
- objects-and-prototypes
summary:
The prototype chain is the lookup path JavaScript follows when a property is not found directly on an object.
details:
Delegation continues upward until the property is found or the chain ends.
recall:
- What happens when a property is missing on the current object?
- Why is delegation a better word than copying here?
```

```concept-card
id: prototypal-inheritance
term: Prototypal Inheritance
parents:
- objects-and-prototypes
related:
- prototype-chain
summary:
Prototypal inheritance lets one object delegate behavior to another through linked prototypes.
details:
It is the underlying inheritance model of JavaScript, even when using class syntax.
recall:
- How is prototypal inheritance different from class copying?
- Which APIs expose this model directly?
```

```concept-card
id: object-creation-patterns
term: Object Creation Patterns
parents:
- objects-and-prototypes
summary:
Objects can be created with literals, factories, constructors, classes, or `Object.create`.
details:
Each approach has different ergonomics, but they all still produce objects that participate in prototype-based behavior.
recall:
- Which object creation styles should you be ready to name?
- Which one shows prototype linkage most explicitly?
```

```concept-card
id: async-execution
term: Async Execution
parents:
- javascript-core
children:
- event-loop
- promises
- async-await
- microtask-queue
summary:
JavaScript handles asynchronous work by combining the call stack, host APIs, queues, and the event loop.
details:
This area explains why callbacks, Promises, and `async`/`await` behave as they do in browsers and runtimes.
recall:
- Which runtime pieces work together to schedule async code?
- Why is `async`/`await` not a separate async system?
```

```concept-card
id: event-loop
term: Event Loop
parents:
- async-execution
summary:
The event loop coordinates when queued work can move onto the call stack after current work finishes.
details:
It connects host APIs, task queues, and microtasks so JavaScript can remain single-threaded while handling asynchronous activity.
recall:
- What does the event loop do once the call stack is empty?
- Why can long synchronous work still block the UI?
```

```concept-card
id: promises
term: Promises
parents:
- async-execution
related:
- async-await
- microtask-queue
summary:
Promises represent the eventual completion or failure of asynchronous work.
details:
They support chaining, centralized error handling, and a cleaner model than deeply nested callbacks.
recall:
- What problem do Promises solve over callbacks?
- How do they relate to microtasks?
```

```concept-card
id: async-await
term: async/await
parents:
- async-execution
related:
- promises
summary:
`async`/`await` is Promise-based syntax that makes asynchronous code read more like sequential code.
details:
It improves readability while preserving the same underlying Promise semantics for resolution and rejection.
recall:
- What does `await` operate on underneath?
- Why is `async`/`await` easier to read in many flows?
```

```concept-card
id: microtask-queue
term: Microtask Queue
parents:
- async-execution
related:
- promises
- event-loop
summary:
The microtask queue holds high-priority queued work such as resolved Promise reactions.
details:
Microtasks run before the next normal task, which is why Promise callbacks often run sooner than timer callbacks.
recall:
- Why can Promise handlers run before `setTimeout` callbacks?
- What kinds of work usually enter the microtask queue?
```

```concept-card
id: dom-and-events
term: DOM and Events
parents:
- javascript-core
children:
- event-delegation
- browser-storage
- web-security
summary:
Frontend JavaScript must interact with the DOM, user events, client storage, and browser security rules.
details:
This cluster covers selecting elements, responding to events, managing storage, and understanding browser-enforced protections.
recall:
- Which topics sit beside DOM manipulation in real frontend interviews?
- Why do events and storage belong to browser knowledge, not just language knowledge?
```

```concept-card
id: event-delegation
term: Event Delegation
parents:
- dom-and-events
summary:
Event delegation handles many child interactions with one listener on a common ancestor.
details:
It relies on event bubbling and is more memory-efficient than attaching many individual listeners.
recall:
- Why is event delegation efficient?
- What browser behavior makes it possible?
```

```concept-card
id: browser-storage
term: Browser Storage
parents:
- dom-and-events
summary:
Browser storage includes `localStorage`, `sessionStorage`, and cookies, each with different scope and lifetime trade-offs.
details:
These APIs help keep client state, but they differ in persistence, transmission behavior, and security considerations.
recall:
- Which storage survives browser restarts?
- Which mechanism is automatically sent with requests?
```

```concept-card
id: web-security
term: Web Security Basics
parents:
- dom-and-events
children:
- xss
- cors
- csp
summary:
JavaScript interviews often expect awareness of browser security constraints and common attack surfaces.
details:
XSS, CORS, and CSP are frequent topics because they directly affect browser-based applications.
recall:
- Which security topics show up most often in frontend interviews?
- Why are these topics tied to browser behavior?
```

```concept-card
id: xss
term: XSS
aliases:
- Cross-Site Scripting
parents:
- web-security
summary:
XSS is the injection of untrusted script or markup into a page where it can execute in the user's browser.
details:
Prevention focuses on output encoding, safe DOM APIs, sanitization where needed, and avoiding unsafe HTML insertion.
recall:
- Why is `innerHTML` risky with untrusted input?
- What broad strategies reduce XSS risk?
```

```concept-card
id: cors
term: CORS
aliases:
- Cross-Origin Resource Sharing
parents:
- web-security
summary:
CORS is a browser-enforced mechanism that controls whether a web page can access resources from another origin.
details:
The server opts in with response headers, but enforcement happens in the browser.
recall:
- Where is CORS enforced?
- What role does the server play in cross-origin access?
```

```concept-card
id: csp
term: CSP
aliases:
- Content Security Policy
parents:
- web-security
summary:
CSP is a policy mechanism that restricts which resources and script sources a page is allowed to load or execute.
details:
CSP reduces exploitability of injection attacks by limiting what injected code can do.
recall:
- How does CSP help reduce XSS impact?
- What kind of restrictions can a CSP define?
```

```concept-card
id: platform-and-tooling
term: Platform and Tooling
parents:
- javascript-core
children:
- debugging
- performance
- build-tooling
summary:
Strong interview answers often include how JavaScript code is debugged, optimized, and delivered.
details:
Modern frontend work depends on devtools, bundlers, linters, source maps, package managers, and performance practices.
recall:
- Which tooling topics appear in practical JavaScript interviews?
- Why is delivery tooling part of JavaScript knowledge now?
```

```concept-card
id: debugging
term: Debugging
parents:
- platform-and-tooling
summary:
Debugging is the process of reproducing, inspecting, and narrowing a problem using tools such as breakpoints, stack traces, and logs.
details:
Browser devtools are the main environment for inspecting runtime state, network activity, and DOM behavior.
recall:
- Which browser tools are most useful for JavaScript debugging?
- Why are breakpoints more useful than adding many random logs?
```

```concept-card
id: performance
term: Performance and Optimization
parents:
- platform-and-tooling
summary:
Performance work focuses on measuring bottlenecks and improving load time, runtime responsiveness, and bundle size.
details:
Common topics include lazy loading, minification, bundling, render-blocking work, and avoiding unnecessary synchronous cost.
recall:
- Why should performance work start with measurement?
- Which JavaScript delivery choices affect load time most?
```

```concept-card
id: build-tooling
term: Build Tooling
parents:
- platform-and-tooling
summary:
Build tooling includes npm, bundlers, linters, source maps, and transpilation tools such as Babel.
details:
These tools support package management, compatibility, code quality, debugging, and production builds.
recall:
- What problems do Babel and source maps solve?
- Why is ESLint part of maintainability rather than runtime behavior?
```
