# Node.js Interview Prep

This page condenses the main question areas from a large Node.js interview catalog into one study-oriented guide.

## Coverage map

The source question set spans these areas:

- Node.js basics and core concepts
- core modules (http, fs, streams, Buffer, EventEmitter)
- asynchronous programming and the event loop
- modules and dependency management
- error handling
- security
- testing
- databases and ORMs
- performance and clustering
- HTTP, networking, and WebSockets
- Express.js
- microservices
- message queues and cloud services
- environment and CI/CD
- DevOps and containerization
- IoT and machine learning integration
- RESTful API design

## Fundamentals to answer clearly

Interviewers expect concise, stable answers for:

- what Node.js is and why it exists
- the event loop and non-blocking I/O
- the difference from traditional threaded web servers
- npm and package management

Useful answer shape:

1. define the concept in one sentence
2. state the main practical consequence
3. give a short example or contrast

## Event loop and async model

A strong Node.js answer connects these pieces:

- V8 JavaScript engine
- libuv and its thread pool
- call stack
- event queue (macrotasks)
- microtask queue
- event loop phases

High-value distinctions:

- `setTimeout` and `setInterval` are macrotasks
- resolved Promise callbacks and `process.nextTick` are microtasks and run before the next macrotask
- blocking the event loop with synchronous CPU work degrades all concurrent requests
- `async`/`await` uses Promises underneath and does not bypass the event loop

```javascript
// non-blocking file read
const fs = require('fs');

fs.readFile('data.txt', 'utf8', (err, data) => {
  if (err) throw err;
  console.log(data);
});

console.log('this runs before the file is ready');
```

## Core modules worth knowing

| Module | Purpose |
|---|---|
| `http` / `https` | create HTTP servers and make requests |
| `fs` | read, write, watch files |
| `path` | join and resolve file paths safely |
| `os` | operating system info, CPU count |
| `events` | EventEmitter base class |
| `stream` | Readable, Writable, Transform streams |
| `buffer` | binary data handling |
| `crypto` | hashing, encryption, signing |
| `child_process` | spawn worker processes |
| `cluster` | fork workers to use multiple CPU cores |
| `url` / `querystring` | URL parsing and query strings |

## Streams

Streams process data in chunks rather than loading everything into memory.

Four stream types:

- `Readable` — data source (file read, HTTP request body)
- `Writable` — data sink (file write, HTTP response)
- `Duplex` — both readable and writable (TCP socket)
- `Transform` — duplex that modifies data in transit (gzip, encryption)

```javascript
const { createReadStream, createWriteStream } = require('fs');
const { createGzip } = require('zlib');

createReadStream('input.txt')
  .pipe(createGzip())
  .pipe(createWriteStream('input.txt.gz'));
```

Interview reminder:

- streams are memory-efficient for large data
- `pipe()` handles backpressure automatically
- Node.js streams are instances of `EventEmitter`

## Modules

Node.js supports two module systems:

- **CommonJS** (`require` / `module.exports`) — the traditional Node.js module system
- **ESM** (`import` / `export`) — the JavaScript standard module system, supported from Node 12+

```javascript
// CommonJS
const path = require('path');
module.exports = { helper };

// ESM
import path from 'path';
export { helper };
```

npm manages third-party dependencies through `package.json` and the `node_modules` directory.

`package-lock.json` pins exact dependency versions for reproducible installs.

## Error handling

Errors in Node.js come from several sources:

- synchronous `throw` inside try/catch
- callback-style errors in the first argument (`err, result` convention)
- rejected Promises
- unhandled `EventEmitter` `error` events
- uncaught exceptions from `process.on('uncaughtException', ...)`

Best practices:

- always handle Promise rejections
- prefer `async`/`await` with `try`/`catch` over mixed callback and Promise patterns
- never swallow errors silently
- use structured logging with the error message, stack, and relevant request context

```javascript
async function fetchUser(id) {
  try {
    const user = await db.findById(id);
    return user;
  } catch (err) {
    logger.error({ err, id }, 'failed to fetch user');
    throw err;
  }
}
```

## Security

Common Node.js security topics:

- **dependency auditing** — `npm audit` and keeping packages up to date
- **input validation** — never trust request bodies, query strings, or headers
- **avoiding injection** — parameterized queries for SQL, escape for HTML output
- **environment variables** — keep secrets in `.env` files or secret managers, not source code
- **HTTPS** — terminate TLS at the edge or inside Node.js with the `https` module
- **rate limiting** — protect APIs from abuse and brute force
- **helmet** — sets secure HTTP headers for Express applications
- **CORS configuration** — allow only expected origins

## Performance and clustering

Node.js runs on a single thread. Strategies for scale:

- **cluster module** — forks multiple worker processes matching the CPU core count
- **worker_threads** — offload CPU-heavy computation to background threads
- **horizontal scaling** — run many Node.js instances behind a load balancer
- **caching** — reduce repeated computation and database load with Redis or in-memory caches
- **connection pooling** — reuse database connections rather than opening one per request

```javascript
const cluster = require('cluster');
const os = require('os');

if (cluster.isPrimary) {
  for (let i = 0; i < os.cpus().length; i++) {
    cluster.fork();
  }
} else {
  require('./server');
}
```

## Testing

Key testing layers for Node.js applications:

- **unit tests** — test individual functions and modules in isolation
- **integration tests** — test module collaboration and database behavior
- **end-to-end tests** — test full HTTP request cycles

Common tools: Jest, Mocha, Chai, Supertest, Sinon.

```javascript
// Jest example
test('adds two numbers', () => {
  expect(add(2, 3)).toBe(5);
});
```

Mock dependencies, avoid real network calls in unit tests, and prefer deterministic test fixtures.

## Express.js basics

Express is a minimal HTTP framework on top of Node.js.

Key concepts:

- `app.use(middleware)` — registers middleware functions
- `app.get / .post / .put / .delete` — route handlers
- middleware runs in order and must call `next()` to continue
- error-handling middleware has four arguments: `(err, req, res, next)`

```javascript
const express = require('express');
const app = express();

app.use(express.json());

app.get('/users/:id', async (req, res, next) => {
  try {
    const user = await getUser(req.params.id);
    res.json(user);
  } catch (err) {
    next(err);
  }
});

app.use((err, req, res, next) => {
  res.status(500).json({ error: err.message });
});
```

## RESTful API design reminders

- use nouns for resource paths, not verbs
- use HTTP methods correctly: GET reads, POST creates, PUT/PATCH updates, DELETE removes
- use HTTP status codes accurately: 200, 201, 400, 401, 403, 404, 409, 500
- version your API (e.g. `/api/v1/users`)
- validate and sanitize all inputs
- never expose stack traces in production responses

## Interview reminders

- say "single-threaded event loop" not "Node.js is single-threaded" — libuv uses threads internally
- say "non-blocking I/O" means the event loop does not wait for the OS operation to complete
- say "npm audit" when asked about dependency security
- say "cluster module or worker_threads" when asked about CPU-bound work
- say "`process.nextTick` runs before I/O callbacks" when asked about microtasks
- say "streams are EventEmitter instances" when asked about stream events
