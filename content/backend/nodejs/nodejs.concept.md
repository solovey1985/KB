---
title: Node.js Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the main Node.js interview concepts into a single memory map.

Study pages: [Section Index](index.md) | [Material Notes](nodejs.md) | [Interview Practice](nodejs.interview.md)

## Concept Map

```concept-card
id: nodejs-core
term: Node.js
children:
- event-loop-node
- core-modules
- async-node
- modules-node
- error-handling-node
- security-node
- performance-node
- testing-node
- express-node
summary:
Node.js is a server-side JavaScript runtime built on V8 and libuv that uses a non-blocking, event-driven I/O model.
details:
It lets a single process handle many concurrent connections without creating a thread per request, which makes it efficient for I/O-bound workloads.
recall:
- What makes Node.js different from traditional threaded servers?
- Which engine runs JavaScript in Node.js?
- What does libuv provide?
```

```concept-card
id: event-loop-node
term: Event Loop
aliases:
- Node.js event loop
parents:
- nodejs-core
children:
- phases-of-event-loop
- microtask-queue-node
- blocking-event-loop
summary:
The event loop is the core mechanism that processes I/O callbacks, timers, and other deferred work in a specific phase order.
details:
libuv implements the event loop. Each iteration visits phases in order: timers, pending callbacks, idle/prepare, poll, check (setImmediate), close callbacks. Microtasks drain between each phase.
recall:
- What are the main phases of the Node.js event loop?
- When do microtasks run relative to the phases?
- Which phase runs setImmediate callbacks?
```

```concept-card
id: phases-of-event-loop
term: Event Loop Phases
parents:
- event-loop-node
summary:
The event loop processes work in a fixed sequence of phases each iteration.
details:
Phases in order: timers (setTimeout/setInterval), pending callbacks (deferred I/O errors), idle/prepare (internal), poll (I/O callbacks), check (setImmediate), close callbacks. The poll phase blocks when idle waiting for I/O.
recall:
- Which phase runs setTimeout callbacks?
- Which phase runs setImmediate callbacks?
- What happens during the poll phase when there is no work?
```

```concept-card
id: microtask-queue-node
term: Microtask Queue
aliases:
- process.nextTick queue
parents:
- event-loop-node
related:
- async-node
summary:
Microtasks, including process.nextTick callbacks and resolved Promise reactions, run after the current operation and before the next event loop phase.
details:
process.nextTick has higher priority than Promise microtasks. Both run before any I/O callback or timer, even setImmediate. Overusing process.nextTick can starve the event loop.
recall:
- Why does process.nextTick run before Promise handlers?
- What risk comes from overusing process.nextTick?
- Why do Promise callbacks run before setTimeout callbacks?
```

```concept-card
id: blocking-event-loop
term: Blocking the Event Loop
parents:
- event-loop-node
summary:
Synchronous CPU-intensive work on the main thread blocks all other requests until it finishes.
details:
Because Node.js runs JavaScript on a single thread, a long synchronous loop or heavy computation prevents any I/O callbacks from being processed. Offload CPU-bound work to worker_threads or child_process.
recall:
- Why does CPU-heavy code block all requests in Node.js?
- Which APIs help offload CPU work?
```

```concept-card
id: core-modules
term: Core Modules
parents:
- nodejs-core
children:
- fs-module
- http-module
- streams-node
- buffer-node
- events-module
- cluster-module
summary:
Node.js ships with built-in modules that cover files, networking, streams, binary data, and process management.
details:
Core modules are available without installation. The most commonly interviewed are fs, http/https, path, os, events, stream, buffer, crypto, child_process, cluster, url.
recall:
- Which module creates HTTP servers?
- Which module provides the EventEmitter class?
- Which module is used to fork worker processes?
```

```concept-card
id: fs-module
term: fs Module
aliases:
- File System module
parents:
- core-modules
summary:
The fs module provides APIs to read, write, watch, and manipulate files and directories.
details:
fs has synchronous variants (e.g. readFileSync) and asynchronous callback variants (readFile). Promise-based fs is available via fs/promises. Prefer async APIs to avoid blocking the event loop.
recall:
- Why should synchronous fs calls be avoided in production code?
- What is the fs/promises API?
```

```concept-card
id: http-module
term: http Module
parents:
- core-modules
related:
- express-node
summary:
The http module lets you create HTTP servers and make outbound HTTP requests without any third-party library.
details:
createServer returns a server that emits a 'request' event for each incoming connection. The request and response objects are streams.
recall:
- What event does an HTTP server emit for each request?
- Why are request and response objects also streams?
```

```concept-card
id: streams-node
term: Streams
parents:
- core-modules
children:
- readable-stream
- writable-stream
- transform-stream
summary:
Streams process data incrementally in chunks, which avoids loading entire files or responses into memory.
details:
There are four types: Readable, Writable, Duplex (both), and Transform (modifies data in transit). Streams are EventEmitter instances. pipe() handles backpressure automatically.
recall:
- What are the four stream types?
- Why do streams use less memory than buffering everything?
- What does backpressure mean in the context of streams?
```

```concept-card
id: readable-stream
term: Readable Stream
parents:
- streams-node
summary:
A Readable stream is a data source that emits data events as chunks become available.
details:
Examples: fs.createReadStream, HTTP request body, process.stdin. It can operate in flowing mode (data events) or paused mode (read() calls).
recall:
- What are the two modes of a Readable stream?
- Give two examples of Readable streams.
```

```concept-card
id: writable-stream
term: Writable Stream
parents:
- streams-node
summary:
A Writable stream is a data sink that accepts write calls and drains data to a destination.
details:
Examples: fs.createWriteStream, HTTP response, process.stdout. write() returns false when the internal buffer is full, signaling backpressure.
recall:
- What does write() returning false mean?
- Give two examples of Writable streams.
```

```concept-card
id: transform-stream
term: Transform Stream
parents:
- streams-node
summary:
A Transform stream is a Duplex stream that modifies data as it passes through.
details:
Examples include zlib.createGzip (compression) and crypto cipher streams. Data enters the writable side, gets transformed, and exits the readable side.
recall:
- How does a Transform stream differ from a plain Duplex stream?
- Give two examples of Transform streams.
```

```concept-card
id: buffer-node
term: Buffer
parents:
- core-modules
summary:
Buffer is a fixed-size allocation of raw binary memory used to handle binary data such as files, network packets, and images.
details:
Buffers exist outside the V8 heap. They are used where JavaScript strings are not appropriate for binary data. Buffer.alloc, Buffer.from, and Buffer.concat are the primary constructors.
recall:
- Why does Node.js need a Buffer class when it already has strings?
- Where does Buffer memory live relative to V8?
```

```concept-card
id: events-module
term: EventEmitter
parents:
- core-modules
related:
- streams-node
summary:
EventEmitter is the base class for all Node.js objects that emit named events and accept listener functions.
details:
on() registers a listener, emit() triggers it, once() registers a one-time listener, removeListener() unregisters. Streams, HTTP servers, and many core objects extend EventEmitter.
recall:
- Which method registers a one-time listener?
- Why must the error event always be handled on an EventEmitter?
```

```concept-card
id: cluster-module
term: Cluster Module
parents:
- core-modules
- performance-node
summary:
The cluster module forks child worker processes that each run a copy of the server, sharing a port to use all CPU cores.
details:
The primary process manages workers. Workers handle requests independently. If a worker crashes, the primary can spawn a replacement. worker_threads is preferred for CPU-bound tasks that share memory.
recall:
- Why does the cluster module help Node.js performance?
- What is the difference between cluster workers and worker_threads?
```

```concept-card
id: async-node
term: Async Programming
parents:
- nodejs-core
children:
- callbacks-node
- promises-node
- async-await-node
summary:
Node.js async programming evolved from callbacks to Promises to async/await, all backed by the event loop.
details:
Callbacks are the lowest-level pattern. Promises model eventual completion. async/await provides sequential-looking syntax over Promises. All patterns rely on the event loop to resume deferred work.
recall:
- What problem does the Promise pattern solve over callbacks?
- What does async/await use underneath?
```

```concept-card
id: callbacks-node
term: Callbacks
parents:
- async-node
summary:
Callbacks are functions passed as arguments that Node.js calls when an asynchronous operation completes.
details:
Node.js follows the error-first callback convention: the first argument is an error (null if none), subsequent arguments are results. Deep nesting creates callback hell.
recall:
- What is the error-first callback convention?
- What problem does callback hell describe?
```

```concept-card
id: promises-node
term: Promises
parents:
- async-node
related:
- async-await-node
summary:
Promises represent eventual completion or failure and support chaining and centralized error handling with .catch().
details:
A Promise is pending, fulfilled, or rejected. Promise.all runs many in parallel. Promise.allSettled waits for all regardless of outcome. Unhandled rejections should always be caught.
recall:
- What are the three Promise states?
- When would you use Promise.all versus Promise.allSettled?
```

```concept-card
id: async-await-node
term: async/await
parents:
- async-node
related:
- promises-node
summary:
async/await is Promise-based syntax that makes asynchronous flows read as sequential code.
details:
An async function always returns a Promise. await suspends the function until the awaited Promise settles. Errors are caught with try/catch. It does not bypass the event loop.
recall:
- What does an async function always return?
- How do you handle errors in an async/await flow?
```

```concept-card
id: modules-node
term: Module Systems
parents:
- nodejs-core
children:
- commonjs-node
- esm-node
- npm-node
summary:
Node.js supports both CommonJS (require/exports) and ESM (import/export) module formats.
details:
CommonJS is the original Node.js module system and loads synchronously. ESM is the JavaScript standard and is supported natively from Node 12+. package.json "type" field controls the default interpretation.
recall:
- How does CommonJS differ from ESM loading behavior?
- Which package.json field switches a package to ESM?
```

```concept-card
id: commonjs-node
term: CommonJS
aliases:
- require/module.exports
parents:
- modules-node
summary:
CommonJS loads modules synchronously using require() and exports values through module.exports.
details:
Modules are cached after first load. Circular dependencies are possible but may produce partial results. Most npm packages historically use CommonJS.
recall:
- What happens to a module after it is required the first time?
- Why can circular requires produce unexpected results?
```

```concept-card
id: esm-node
term: ESM
aliases:
- ES Modules
parents:
- modules-node
summary:
ESM uses import/export syntax, loads asynchronously, and supports static analysis of dependencies.
details:
Top-level await is allowed in ESM. Files must use .mjs extension or set "type": "module" in package.json. Named and default exports work differently from CommonJS.
recall:
- What file extension signals ESM to Node.js?
- What does top-level await enable?
```

```concept-card
id: npm-node
term: npm
parents:
- modules-node
summary:
npm is the default Node.js package manager used to install, update, audit, and script project dependencies.
details:
package.json declares dependencies and scripts. package-lock.json pins exact resolved versions. npm audit checks for known vulnerabilities. npx runs packages without installing them globally.
recall:
- What is the purpose of package-lock.json?
- What does npm audit do?
```

```concept-card
id: error-handling-node
term: Error Handling
parents:
- nodejs-core
summary:
Node.js errors can be synchronous, callback-based, Promise rejections, or EventEmitter error events.
details:
Best practice: always catch Promise rejections, use try/catch with async/await, handle EventEmitter error events, and use process.on('uncaughtException') only as a last resort for logging before exit. Never swallow errors silently.
recall:
- What happens if an EventEmitter emits an error event with no listener?
- Why is process.on('uncaughtException') dangerous for application recovery?
```

```concept-card
id: security-node
term: Security
parents:
- nodejs-core
children:
- input-validation-node
- dependency-security-node
summary:
Node.js applications face injection, dependency vulnerabilities, secret leakage, and inadequate access control.
details:
Use helmet for HTTP headers in Express, validate all inputs, audit dependencies regularly, keep secrets in environment variables, use HTTPS, and apply rate limiting.
recall:
- What does the helmet package do?
- Which command checks for known dependency vulnerabilities?
```

```concept-card
id: input-validation-node
term: Input Validation
parents:
- security-node
summary:
All incoming data from request bodies, query strings, and headers must be validated and sanitized.
details:
Use a validation library such as Joi or Zod for declarative schema validation. Avoid eval or dynamic construction from user input. Use parameterized queries for databases.
recall:
- Why is parameterized query usage a security requirement?
- Name two Node.js validation libraries.
```

```concept-card
id: dependency-security-node
term: Dependency Security
parents:
- security-node
summary:
Third-party packages are a common attack surface and must be audited and kept up to date.
details:
npm audit reports known vulnerabilities. Lock files reduce supply-chain risk. Prefer packages with active maintenance and minimal transitive dependencies.
recall:
- What command scans for known vulnerabilities in npm packages?
- Why is a lock file helpful for security?
```

```concept-card
id: performance-node
term: Performance
parents:
- nodejs-core
children:
- cluster-module
- caching-node
summary:
Node.js performance depends on avoiding event loop blocking, scaling across cores, and reducing redundant work.
details:
Profile with --prof or clinic.js before optimizing. Use the cluster module or multiple instances behind a load balancer. Offload CPU work to worker_threads. Cache hot data in memory or Redis.
recall:
- What profiling tools are available for Node.js?
- Why should optimization start with measurement rather than guessing?
```

```concept-card
id: caching-node
term: Caching
parents:
- performance-node
summary:
Caching stores computed results so repeated requests avoid redundant computation or database queries.
details:
In-memory caching is fastest but local to one process. Redis provides a shared cache for multi-instance deployments. Cache invalidation and TTL design require care.
recall:
- When is in-memory caching insufficient in a clustered environment?
- What does TTL stand for and why does it matter in caching?
```

```concept-card
id: testing-node
term: Testing
parents:
- nodejs-core
summary:
Node.js applications are typically tested at unit, integration, and end-to-end levels.
details:
Common tools: Jest (assertions, mocking, coverage), Mocha+Chai, Supertest (HTTP integration), Sinon (spies, stubs). Mock external dependencies in unit tests. Use real connections in integration tests.
recall:
- Which library is commonly used to test HTTP endpoints in Node.js?
- What is the difference between a stub and a spy in testing?
```

```concept-card
id: express-node
term: Express.js
parents:
- nodejs-core
children:
- middleware-express
- routing-express
summary:
Express is a minimal web framework for Node.js that adds routing, middleware composition, and request/response helpers.
details:
Middleware functions have the signature (req, res, next). Error-handling middleware has four arguments (err, req, res, next). Request processing is a middleware pipeline in declared order.
recall:
- What is the signature of an Express error-handling middleware?
- What happens if middleware does not call next()?
```

```concept-card
id: middleware-express
term: Middleware
parents:
- express-node
summary:
Middleware functions intercept requests in a pipeline, performing tasks like parsing, authentication, and logging before a route handler responds.
details:
Built-in middleware includes express.json() and express.urlencoded(). Third-party middleware includes cors, helmet, morgan. Custom middleware runs in the order it is registered with app.use().
recall:
- In what order does Express execute middleware?
- What three parameters does a standard middleware function receive?
```

```concept-card
id: routing-express
term: Routing
parents:
- express-node
summary:
Express routes map HTTP method and path combinations to handler functions.
details:
app.get, app.post, app.put, app.delete define method-specific routes. Router objects group related routes. Route parameters are accessed via req.params, query strings via req.query.
recall:
- How do you access a URL parameter in an Express route?
- What is an Express Router used for?
```
