---
title: Node.js Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise the highest-signal Node.js questions collected from a broader interview question catalog.

Relevant study pages:

- [Material Notes](nodejs.md)
- [Concept Map](nodejs.concept.md)

## Basics and Core Concepts

```interview-question
What is Node.js and why is it used?
---
answer:
Node.js is a server-side JavaScript runtime built on the V8 engine and the libuv library.

It is used because its non-blocking, event-driven I/O model lets a single process handle many concurrent connections efficiently, making it well suited for I/O-bound workloads like APIs and real-time services.
hints:
- Name the underlying engine and library.
- The key word is non-blocking.
- Compare to threading models.
```

Related concepts: [Node.js](nodejs.concept.md#nodejs-core)

```interview-question
What is the event loop in Node.js?
---
answer:
The event loop is the mechanism that processes I/O callbacks, timers, and deferred work in a fixed sequence of phases each iteration.

libuv implements it. Phases include: timers, pending callbacks, poll (I/O), check (setImmediate), and close callbacks. Microtasks drain between each phase transition.
hints:
- Name at least three phases.
- Microtasks are not a phase but drain between them.
- libuv implements it.
```

Related concepts: [Event Loop](nodejs.concept.md#event-loop-node), [Event Loop Phases](nodejs.concept.md#phases-of-event-loop)

```interview-choice
What correctly describes Node.js's threading model?
---
options:
- JavaScript runs on a single thread; libuv uses a thread pool for I/O
- All I/O and JavaScript execution happen on the same thread
- Node.js creates one OS thread per request
correct: 0
explanation:
JavaScript executes on one thread. libuv's thread pool handles asynchronous I/O operations in the background, allowing the event loop to remain unblocked.
```

```interview-question
Explain what "non-blocking" means in Node.js.
---
answer:
Non-blocking means that when Node.js initiates an I/O operation, it does not wait for that operation to finish before running other code.

Instead, it registers a callback and the event loop continues processing other work until the OS signals completion, at which point the callback is queued.
hints:
- Think about what the thread does while waiting for disk or network.
- A callback is involved.
- The event loop continues.
```

```interview-question
What is a package.json file?
---
answer:
package.json is the manifest for a Node.js project. It declares the project name, version, scripts, and its runtime and development dependencies.

npm reads it when installing packages and running scripts.
hints:
- Name at least three things it declares.
- npm uses it.
```

## Core Modules

```interview-question
Describe some of the core modules of Node.js.
---
answer:
Important built-in modules include:

- `http` / `https` — create servers and make requests
- `fs` — read and write files
- `path` — join and resolve paths portably
- `events` — the EventEmitter base class
- `stream` — Readable, Writable, Transform
- `crypto` — hashing and encryption
- `cluster` — fork worker processes
- `os` — CPU count and system info
hints:
- Name at least four modules.
- Group them by purpose.
- Do not confuse built-ins with popular npm packages.
```

Related concepts: [Core Modules](nodejs.concept.md#core-modules)

```interview-question
What are streams in Node.js and what types are available?
---
answer:
Streams process data incrementally in chunks instead of loading everything into memory at once.

There are four types: Readable (data source), Writable (data sink), Duplex (both), and Transform (modifies data in transit). All streams extend EventEmitter.
hints:
- There are exactly four types.
- One type modifies data.
- They inherit from EventEmitter.
```

Related concepts: [Streams](nodejs.concept.md#streams-node)

```interview-choice
Which stream type compresses data as it passes through?
---
options:
- Transform
- Duplex
- Readable
correct: 0
explanation:
A Transform stream is a Duplex stream that modifies data as it flows through. zlib.createGzip is a typical example.
```

```interview-question
What is the Buffer class in Node.js?
---
answer:
Buffer is a fixed-size raw binary memory allocation used to handle binary data such as file contents, network packets, and images.

Buffers live outside the V8 heap and are needed when JavaScript strings are not appropriate for binary data.
hints:
- Think binary data.
- It lives outside the V8 heap.
- Strings are not always suitable for binary.
```

Related concepts: [Buffer](nodejs.concept.md#buffer-node)

```interview-question
How do you use the EventEmitter in Node.js?
---
answer:
Require the `events` module and call `new EventEmitter()`, or extend it in a class.

Use `on(event, listener)` to register a handler, `emit(event, ...args)` to fire it, and `once(event, listener)` for a one-time listener.
hints:
- Name the module.
- Three key methods.
- on, emit, once.
```

Related concepts: [EventEmitter](nodejs.concept.md#events-module)

```interview-code
language: js
prompt: Complete the EventEmitter setup so that 'greet' logs the name once and then stops listening.
starter:
const EventEmitter = require('events');
const emitter = new EventEmitter();

emitter.

emitter.emit('greet', 'Alice');
emitter.emit('greet', 'Bob');
solution:
const EventEmitter = require('events');
const emitter = new EventEmitter();

emitter.once('greet', (name) => {
  console.log(`Hello, ${name}`);
});

emitter.emit('greet', 'Alice');
emitter.emit('greet', 'Bob');
checks:
- includes: once
- includes: greet
```

## Async Programming

```interview-question
Describe the event-driven programming in Node.js.
---
answer:
Event-driven programming means the flow of the program is determined by events such as I/O completions, timers, or user-defined signals.

In Node.js, the event loop listens for these events and calls the associated callback or listener function when they occur.
hints:
- Events drive the execution order.
- Callbacks are registered in advance.
- The event loop coordinates.
```

```interview-question
How do you handle asynchronous operations in Node.js?
---
answer:
Three main patterns:

1. Callbacks — error-first convention, passed to async functions
2. Promises — chainable with `.then()` and `.catch()`
3. `async`/`await` — sequential syntax over Promises with `try`/`catch`

Modern code prefers async/await for readability and error handling.
hints:
- Name all three patterns.
- Say which is modern.
- Mention error handling in each.
```

Related concepts: [Async Programming](nodejs.concept.md#async-node)

```interview-choice
Which callback runs first given this code: setTimeout(() => {}, 0) and Promise.resolve().then(() => {})?
---
options:
- The Promise .then() callback
- The setTimeout callback
- They run in the same tick
correct: 0
explanation:
Resolved Promise callbacks are microtasks and drain after the current operation and before the next event loop phase, so they run before the setTimeout callback which is a macrotask.
```

```interview-question
What is process.nextTick and how does it differ from setImmediate?
---
answer:
`process.nextTick` queues a callback to run after the current operation completes, before the event loop moves to the next phase — even before Promise microtasks.

`setImmediate` queues a callback to run in the check phase of the next event loop iteration, after I/O callbacks.
hints:
- nextTick runs before I/O callbacks.
- setImmediate runs after I/O in the same iteration.
- nextTick has higher priority than Promise handlers.
```

Related concepts: [Microtask Queue](nodejs.concept.md#microtask-queue-node)

```interview-code
language: js
prompt: Rewrite the callback-based readFile call using async/await.
starter:
const fs = require('fs');

function readConfig(path, callback) {
  fs.readFile(path, 'utf8', (err, data) => {
    if (err) return callback(err);
    callback(null, data);
  });
}
solution:
const fs = require('fs/promises');

async function readConfig(path) {
  const data = await fs.readFile(path, 'utf8');
  return data;
}
checks:
- includes: async
- includes: await
- includes: readFile
```

## Error Handling

```interview-question
How do you handle errors in Node.js?
---
answer:
Node.js errors arise in four ways: synchronous throw (caught with try/catch), callback errors (error-first convention), rejected Promises (caught with .catch or try/catch in async/await), and EventEmitter error events (must have a listener or the process crashes).

Always handle Promise rejections. Never swallow errors silently.
hints:
- Name all four sources.
- What happens to an unhandled EventEmitter error event?
- What happens to an unhandled Promise rejection?
```

Related concepts: [Error Handling](nodejs.concept.md#error-handling-node)

```interview-choice
What happens if an EventEmitter emits an 'error' event and there is no listener?
---
options:
- Node.js throws the error and may crash the process
- The error is silently ignored
- The error is caught by the event loop
correct: 0
explanation:
If no 'error' listener is registered, Node.js treats it as an uncaught exception, which by default prints a stack trace and exits the process.
```

## Modules

```interview-question
What is the difference between CommonJS and ESM in Node.js?
---
answer:
CommonJS uses `require()` and `module.exports` and loads synchronously. It is the original Node.js module system.

ESM uses `import`/`export`, loads asynchronously, supports static analysis, and is the JavaScript standard. It requires `.mjs` extension or `"type": "module"` in package.json.
hints:
- Compare syntax.
- Compare loading behavior.
- Which is the JS standard?
```

Related concepts: [Module Systems](nodejs.concept.md#modules-node), [CommonJS](nodejs.concept.md#commonjs-node), [ESM](nodejs.concept.md#esm-node)

## Security

```interview-question
What are some security best practices for Node.js applications?
---
answer:
Key practices:

- validate and sanitize all inputs
- use parameterized queries to prevent injection
- store secrets in environment variables, not source code
- run `npm audit` regularly
- use `helmet` in Express for secure HTTP headers
- apply rate limiting
- use HTTPS
- keep dependencies updated
hints:
- Input first.
- Then dependencies.
- Then transport.
- Then headers.
```

Related concepts: [Security](nodejs.concept.md#security-node)

## Performance and Clustering

```interview-question
How does the cluster module work in Node.js?
---
answer:
The cluster module forks multiple child worker processes, each running the same server script and sharing the same port.

A primary process manages the workers. This allows Node.js to use all available CPU cores since each worker is an independent process with its own event loop.
hints:
- Multiple processes, one port.
- Primary manages workers.
- Each worker has its own event loop.
```

Related concepts: [Cluster Module](nodejs.concept.md#cluster-module)

```interview-question
What is the difference between the cluster module and worker_threads?
---
answer:
The cluster module creates separate processes with isolated memory and their own event loops. It is suitable for scaling network servers across CPU cores.

`worker_threads` creates threads that share memory with the parent thread. It is suitable for CPU-bound computation that should not block the main event loop.
hints:
- Processes versus threads.
- Memory isolation differs.
- Different use cases.
```

```interview-choice
Which approach is best for offloading a CPU-intensive image compression task in a Node.js server?
---
options:
- worker_threads
- cluster module
- setTimeout with a large delay
correct: 0
explanation:
worker_threads run in separate threads sharing memory with the main process and are designed to handle CPU-bound work without blocking the event loop.
```

## Express.js

```interview-question
What is Express middleware and how does it work?
---
answer:
Middleware functions have the signature `(req, res, next)` and are called in the order they are registered.

Each middleware either ends the request by sending a response, or calls `next()` to pass control to the following middleware or route handler. Error-handling middleware has four parameters: `(err, req, res, next)`.
hints:
- Three-parameter signature.
- next() passes control.
- Error handlers have four parameters.
```

Related concepts: [Middleware](nodejs.concept.md#middleware-express)

```interview-code
language: js
prompt: Add a simple request logger middleware that prints the HTTP method and URL, then passes control to the next handler.
starter:
const express = require('express');
const app = express();

app.use(

);

app.get('/', (req, res) => res.send('ok'));
solution:
const express = require('express');
const app = express();

app.use((req, res, next) => {
  console.log(`${req.method} ${req.url}`);
  next();
});

app.get('/', (req, res) => res.send('ok'));
checks:
- includes: next()
- includes: req.method
- includes: req.url
```

## Testing

```interview-question
What testing layers are important in a Node.js application?
---
answer:
Three main layers:

1. **Unit tests** — test individual functions in isolation with mocked dependencies
2. **Integration tests** — test module collaboration and real database interaction
3. **End-to-end tests** — test full HTTP request/response cycles using tools like Supertest

Common tools: Jest, Mocha, Chai, Supertest, Sinon.
hints:
- Three layers.
- Name at least two tools.
- What is mocked in unit tests?
```

Related concepts: [Testing](nodejs.concept.md#testing-node)

## Scaling and DevOps

```interview-question
What are some strategies for scaling Node.js applications?
---
answer:
Main strategies:

- **cluster module** — use all CPU cores in one machine
- **horizontal scaling** — run multiple instances behind a load balancer
- **worker_threads** — offload CPU-heavy tasks from the event loop
- **caching** — reduce redundant computation and DB load with Redis
- **connection pooling** — reuse database connections
- **async I/O** — never block the event loop with synchronous work
hints:
- At least four strategies.
- Distinguish vertical from horizontal.
- Mention caching.
```

```interview-question
What is serverless architecture and how does it relate to Node.js?
---
answer:
Serverless means running functions on cloud infrastructure that automatically provisions and scales without managing servers.

Node.js is a common runtime in serverless platforms such as AWS Lambda, Azure Functions, and Google Cloud Functions because of its fast startup and lightweight footprint.
hints:
- No server management.
- Name one or two platforms.
- Why is Node.js a good fit?
```

## API Design

```interview-question
What are best practices for designing RESTful APIs in Node.js?
---
answer:
Key practices:

- use nouns for resource paths, not verbs
- use HTTP methods correctly: GET reads, POST creates, PUT/PATCH updates, DELETE removes
- return accurate HTTP status codes: 200, 201, 400, 401, 403, 404, 500
- version the API (e.g. `/api/v1/users`)
- validate and sanitize all input
- never expose internal stack traces in error responses
- use JSON consistently for request and response bodies
hints:
- Nouns not verbs.
- HTTP methods map to CRUD.
- Status codes matter.
- Versioning.
```
