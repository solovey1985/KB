# Async JavaScript Patterns for Front-End Interviews — Complete Guide

> All code samples are from the **angular-student-dashboard** application.
> Each section links to the exact source file so you can read the full context.
> The app simulates a REST API with network latency (200-800ms) to make async
> behaviour observable in the UI — loading spinners, disabled buttons, error messages.

---

## Table of Contents

1. [The Event Loop — How Async Actually Works](#1-the-event-loop)
2. [Callbacks — The Original Pattern](#2-callbacks)
3. [Promises — Flat Chains and Centralized Errors](#3-promises)
4. [async/await — Synchronous-Looking Async](#4-asyncawait)
5. [Promise Combinators — all, allSettled, race, any](#5-promise-combinators)
6. [Practical Patterns — Retry, Timeout, Concurrency](#6-practical-patterns)
7. [Async in Angular — Services, Components, Guards](#7-async-in-angular)
8. [Common Pitfalls](#8-common-pitfalls)
9. [Interview Questions & Answers](#9-interview-questions--answers)

---

## 1. The Event Loop

> **Source:** `src/app/utils/async-patterns.ts` (event loop section)

JavaScript is **single-threaded**. It can only execute one piece of code at a time. Yet it handles thousands of concurrent operations (network requests, timers, user events) without blocking. This is possible because of the **event loop**.

### Architecture

```
┌───────────────────────────────────────────────────────────┐
│                    CALL STACK                             │
│  (synchronous code executes here, one frame at a time)   │
└────────────────────────┬──────────────────────────────────┘
                         │ when empty, event loop checks:
                         ▼
┌───────────────────────────────────────────────────────────┐
│              MICROTASK QUEUE (higher priority)            │
│  - Promise callbacks (.then, .catch, .finally)           │
│  - queueMicrotask()                                      │
│  - MutationObserver callbacks                            │
│  Drained completely before any macrotask runs.           │
└────────────────────────┬──────────────────────────────────┘
                         │ when microtask queue is empty:
                         ▼
┌───────────────────────────────────────────────────────────┐
│              MACROTASK QUEUE (lower priority)             │
│  - setTimeout / setInterval callbacks                    │
│  - DOM events (click, input, etc.)                       │
│  - I/O callbacks (fetch response, file read, etc.)       │
│  - requestAnimationFrame (before repaint)                │
│  One macrotask is processed, then microtasks drain again.│
└───────────────────────────────────────────────────────────┘
```

### Execution order per iteration

1. Execute all synchronous code on the call stack
2. **Drain the entire microtask queue** (all pending microtasks)
3. Execute **one** macrotask from the macrotask queue
4. Drain the microtask queue again
5. Render/repaint if needed
6. Go to step 3

### Classic interview question: predict the output

```typescript
// src/app/utils/async-patterns.ts — eventLoopDemo()
function eventLoopDemo(): void {
  console.log('1 — synchronous');                       // 1st

  setTimeout(() => {
    console.log('2 — macrotask (setTimeout)');           // 5th
  }, 0);

  Promise.resolve().then(() => {
    console.log('3 — microtask (Promise.then)');         // 3rd
  });

  queueMicrotask(() => {
    console.log('4 — microtask (queueMicrotask)');       // 4th
  });

  console.log('5 — synchronous');                       // 2nd
}

// Output order: 1, 5, 3, 4, 2
```

**Why?**
- `1` and `5` are synchronous — they execute immediately on the call stack.
- `3` and `4` are microtasks — they drain before any macrotask.
- `2` is a macrotask (`setTimeout` with 0ms is still a macrotask) — runs last.

### Microtask starvation

Microtasks that schedule more microtasks are drained in the same cycle. This can starve the macrotask queue:

```typescript
// src/app/utils/async-patterns.ts — microtaskStarvation()
function microtaskStarvation(): void {
  let count = 0;

  function scheduleMicrotask(): void {
    if (count < 5) {
      count++;
      console.log(`Microtask ${count}`);
      queueMicrotask(scheduleMicrotask);  // schedules another microtask
    }
  }

  setTimeout(() => console.log('Macrotask'), 0);
  queueMicrotask(scheduleMicrotask);

  // Output: Microtask 1, 2, 3, 4, 5, then Macrotask
}
```

### How `await` interacts with the event loop

Each `await` wraps the continuation in a microtask. Control returns to the event loop, allowing other microtasks to run:

```typescript
async function asyncMicrotaskDemo(): Promise<void> {
  console.log('A — before await');              // synchronous

  await Promise.resolve();
  // Everything after `await` is a microtask continuation
  console.log('B — after first await');

  await Promise.resolve();
  console.log('C — after second await');
}
```

---

## 2. Callbacks

> **Source:** `src/app/api/fake-api.ts:162-332`

The oldest async pattern in JavaScript. A function accepts another function (the **callback**) that is invoked when the async operation completes.

### Error-first convention (Node.js style)

```typescript
// src/app/api/fake-api.ts:172
export type ApiCallback<T> = (error: ApiError | null, result?: ApiResponse<T>) => void;
```

- First argument is `error` (null on success)
- Second argument is `result` (undefined on failure)

### Basic callback API

```typescript
// src/app/api/fake-api.ts:174-186
export function fetchCoursesCallback(callback: ApiCallback<Course[]>): void {
  const delay = simulateLatency();
  console.log(`[CallbackAPI] fetchCourses — will respond in ${delay}ms`);

  setTimeout(() => {
    callback(null, {
      data: cloneData(dbCourses),
      status: 200,
      message: 'OK',
      timestamp: Date.now(),
    });
  }, delay);
}
```

The `setTimeout` simulates network latency. The callback is invoked inside the `setTimeout`, which means it runs as a **macrotask** — it won't execute until the call stack is empty and all microtasks have drained.

### Callback Hell (Pyramid of Doom)

When callbacks depend on each other, nesting gets deep and unmanageable:

```typescript
// src/app/api/fake-api.ts:242-283
export function enrollWithPrereqCheckCallback(
  courseId: string,
  studentId: string,
  callback: ApiCallback<Enrollment>,
): void {
  // Level 1: fetch the course
  fetchCourseByIdCallback(courseId, (err1, courseRes) => {
    if (err1) { callback(err1); return; }

    const course = courseRes!.data;

    if (course.prerequisiteIds.length === 0) {
      enrollCallback(courseId, studentId, callback);   // Level 2
      return;
    }

    // Level 2: fetch prerequisite
    fetchCourseByIdCallback(course.prerequisiteIds[0], (err2, _prereqRes) => {
      if (err2) { callback(err2); return; }

      // Level 3: enroll
      enrollCallback(courseId, studentId, (err3, enrollRes) => {
        if (err3) { callback(err3); return; }

        // Level 4: we're 4 levels deep
        console.log('[CallbackHell] Finally enrolled');
        callback(null, enrollRes);
      });
    });
  });
}
```

**Problems with callbacks:**
- Deep nesting makes code hard to read and maintain
- Error handling is scattered across every level
- No standardized way to compose multiple async operations
- No built-in cancellation or timeout

---

## 3. Promises

> **Source:** `src/app/api/fake-api.ts:334-579`

A **Promise** represents a value that may not be available yet. It's a container for the eventual result of an async operation.

### Promise states

```
                 ┌─── fulfilled (resolved) with a value
pending ─────┤
                 └─── rejected with a reason (error)
```

A Promise **settles at most once** — once fulfilled or rejected, it never changes state.

### Creating a Promise

```typescript
// src/app/api/fake-api.ts:340-354
export function fetchCourses(): Promise<ApiResponse<Course[]>> {
  return new Promise((resolve) => {
    const delay = simulateLatency();

    setTimeout(() => {
      resolve({
        data: cloneData(dbCourses),
        status: 200,
        message: 'OK',
        timestamp: Date.now(),
      });
    }, delay);
  });
}
```

The `new Promise` constructor takes an **executor** function with `resolve` and `reject` parameters. Call `resolve(value)` on success, `reject(error)` on failure.

### Promise with rejection

```typescript
// src/app/api/fake-api.ts:356-378
export function fetchCourseById(id: string): Promise<ApiResponse<Course>> {
  return new Promise((resolve, reject) => {
    const delay = simulateLatency();

    setTimeout(() => {
      const course = dbCourses.find((c) => c.id === id);
      if (course) {
        resolve({
          data: cloneData(course),
          status: 200,
          message: 'OK',
          timestamp: Date.now(),
        });
      } else {
        reject({
          status: 404,
          message: `Course ${id} not found`,
          code: 'COURSE_NOT_FOUND',
        } satisfies ApiError);
      }
    }, delay);
  });
}
```

### Promise chaining — solving callback hell

The same enroll-with-prereq-check logic, now flat and readable:

```typescript
// src/app/api/fake-api.ts:563-579
export function enrollWithPrereqCheckPromise(
  courseId: string,
  studentId: string,
): Promise<ApiResponse<Enrollment>> {
  return fetchCourseById(courseId)
    .then((courseRes) => {
      const course = courseRes.data;

      if (course.prerequisiteIds.length === 0) {
        return enrollCourse(courseId, studentId);
      }

      return fetchCourseById(course.prerequisiteIds[0])
        .then(() => enrollCourse(courseId, studentId));
    });
}
```

Each `.then()` receives the resolved value of the previous Promise and can return a new Promise (which the chain automatically awaits).

### .catch() and .finally()

```typescript
getUserOrderTotalPromise(userId)
  .then((total) => console.log(`Total: $${total}`))
  .catch((error) => console.error('Failed:', error.message))
  .finally(() => console.log('Cleanup — runs regardless'));
```

- `.catch()` handles rejections from any step in the chain
- `.finally()` runs whether the Promise fulfilled or rejected (like `try/finally`)

### Promisifying a callback function

A common migration pattern — wrap a callback API in a Promise:

```typescript
// src/app/utils/async-patterns.ts
function fetchUserPromisified(userId: string): Promise<User> {
  return new Promise((resolve, reject) => {
    fetchUserCallback(userId, (err, user) => {
      if (err) reject(err);
      else resolve(user!);
    });
  });
}
```

---

## 4. async/await

> **Source:** `src/app/api/fake-api.ts:581-618`, `src/app/services/course.service.ts`, `src/app/services/auth.service.ts`

Syntactic sugar over Promises that makes async code read like synchronous code. An `async` function **always returns a Promise**. `await` **pauses execution** until the Promise settles.

### The same logic — three ways

**Callback (4 levels deep):**
```typescript
fetchCourseByIdCallback(courseId, (err1, courseRes) => {
  fetchCourseByIdCallback(prereqId, (err2, prereqRes) => {
    enrollCallback(courseId, studentId, (err3, enrollRes) => {
      callback(null, enrollRes);
    });
  });
});
```

**Promise chain (flat):**
```typescript
fetchCourseById(courseId)
  .then(courseRes => fetchCourseById(prereqId))
  .then(() => enrollCourse(courseId, studentId));
```

**async/await (reads like sync):**
```typescript
// src/app/api/fake-api.ts:588-603
export async function enrollWithPrereqCheckAsync(
  courseId: string,
  studentId: string,
): Promise<ApiResponse<Enrollment>> {
  const courseRes = await fetchCourseById(courseId);
  const course = courseRes.data;

  if (course.prerequisiteIds.length > 0) {
    await fetchCourseById(course.prerequisiteIds[0]);
  }

  return enrollCourse(courseId, studentId);
}
```

### Error handling with try/catch

```typescript
// src/app/services/course.service.ts:90-101
async loadCourses(): Promise<void> {
  this._loadingCourses.set(true);
  this._error.set(null);

  try {
    const response = await fetchCourses();
    this.coursesSignal.set(response.data);
  } catch (err) {
    const apiErr = err as ApiError;
    this._error.set(apiErr.message ?? 'Failed to load courses');
  } finally {
    this._loadingCourses.set(false);
  }
}
```

The `try/catch/finally` pattern maps directly to Promise's `.then()/.catch()/.finally()`:
- `try` block = the "happy path"
- `catch` block = rejection handler
- `finally` block = cleanup that runs regardless

### Return values

An `async` function always wraps its return value in a Promise:

```typescript
async function getNumber(): Promise<number> {
  return 42;  // equivalent to Promise.resolve(42)
}
```

If you `throw` inside an async function, it becomes a rejected Promise:

```typescript
async function fail(): Promise<never> {
  throw new Error('oops');  // equivalent to Promise.reject(new Error('oops'))
}
```

---

## 5. Promise Combinators

> **Source:** `src/app/api/fake-api.ts:605-688`, `src/app/utils/async-patterns.ts`

### Promise.all — parallel execution, fail-fast

Runs all promises in parallel. Resolves when **ALL** resolve. Rejects immediately if **ANY** reject.

```typescript
// src/app/api/fake-api.ts:608-619
export async function fetchMultipleCourses(
  ids: string[],
): Promise<Course[]> {
  const promises = ids.map((id) =>
    fetchCourseById(id)
      .then((res) => res.data)
      .catch(() => null)            // Swallow 404s for missing courses
  );

  const results = await Promise.all(promises);
  return results.filter((c): c is Course => c !== null);
}
```

**Used in practice** — loading dashboard data in parallel:

```typescript
// src/app/pages/dashboard/dashboard.component.ts:55-59
await Promise.all([
  this.courseService.loadCourses(),
  this.courseService.loadEnrollments(student.id),
]);
```

Total time = max(coursesFetch, enrollmentsFetch), **not** the sum.

### Promise.allSettled — wait for everything

Never rejects. Returns the outcome (fulfilled or rejected) for every promise.

```typescript
// src/app/api/fake-api.ts:624-642
export async function fetchCoursesSettled(
  ids: string[],
): Promise<{ successes: Course[]; failures: ApiError[] }> {
  const promises = ids.map((id) => fetchCourseById(id).then((res) => res.data));
  const results = await Promise.allSettled(promises);

  const successes: Course[] = [];
  const failures: ApiError[] = [];

  for (const result of results) {
    if (result.status === 'fulfilled') {
      successes.push(result.value);
    } else {
      failures.push(result.reason as ApiError);
    }
  }

  return { successes, failures };
}
```

Use when you want **partial results** even if some fail.

### Promise.race — first to settle

Resolves or rejects with the first promise that settles. Classic use case: **timeout wrapper**.

```typescript
// src/app/api/fake-api.ts:647-662
export function fetchWithTimeout<T>(
  promise: Promise<T>,
  timeoutMs: number,
): Promise<T> {
  const timeoutPromise = new Promise<never>((_, reject) => {
    setTimeout(() => {
      reject({
        status: 408,
        message: `Request timed out after ${timeoutMs}ms`,
        code: 'TIMEOUT',
      } satisfies ApiError);
    }, timeoutMs);
  });

  return Promise.race([promise, timeoutPromise]);
}
```

### Promise.any (ES2021) — first to succeed

Resolves with the first promise that **fulfills** (ignores rejections). Only rejects if ALL promises reject (`AggregateError`).

```typescript
// src/app/utils/async-patterns.ts
async function fetchFromFastestMirror(userId: string): Promise<User> {
  return Promise.any([
    fetchUserPromise(userId),   // mirror 1
    fetchUserPromise(userId),   // mirror 2
    fetchUserPromise(userId),   // mirror 3
  ]);
}
```

### Comparison table

| Combinator | Resolves when | Rejects when | Use case |
|---|---|---|---|
| `Promise.all` | All fulfill | Any rejects | Parallel fetch, need all results |
| `Promise.allSettled` | All settle | Never | Partial results, error reporting |
| `Promise.race` | First settles | First settles | Timeout, fastest response |
| `Promise.any` | First fulfills | All reject | Redundant sources, fastest success |

---

## 6. Practical Patterns

> **Source:** `src/app/api/fake-api.ts:666-688`, `src/app/utils/async-patterns.ts`

### Retry with exponential backoff

```typescript
// src/app/api/fake-api.ts:666-688
export async function fetchWithRetry<T>(
  fn: () => Promise<T>,
  maxRetries = 3,
  baseDelay = 500,
): Promise<T> {
  for (let attempt = 0; attempt <= maxRetries; attempt++) {
    try {
      return await fn();
    } catch (error) {
      if (attempt === maxRetries) {
        throw error;    // final attempt — propagate
      }

      const delay = baseDelay * Math.pow(2, attempt);   // 500, 1000, 2000
      console.log(`[Retry] Attempt ${attempt + 1} failed, retrying in ${delay}ms...`);
      await new Promise((resolve) => setTimeout(resolve, delay));
    }
  }

  throw new Error('Unreachable');
}
```

**Key concepts:**
- Exponential backoff: `delay = baseDelay * 2^attempt` (500ms, 1000ms, 2000ms)
- Rethrows on final attempt so the caller can handle the error
- Uses `await new Promise(resolve => setTimeout(resolve, delay))` to pause

### Sequential vs. Parallel execution

```typescript
// src/app/utils/async-patterns.ts

// SEQUENTIAL — total time ≈ sum of all times
async function fetchSequential(userId: string): Promise<[User, Order[]]> {
  const user = await fetchUserPromise(userId);     // waits ~500ms
  const orders = await fetchOrdersPromise(userId); // waits another ~500ms
  return [user, orders];                           // total ≈ 1000ms
}

// PARALLEL — total time ≈ slowest time
async function fetchParallel(userId: string): Promise<[User, Order[]]> {
  const userPromise = fetchUserPromise(userId);       // starts immediately
  const ordersPromise = fetchOrdersPromise(userId);   // starts immediately
  const user = await userPromise;                     // waits for user
  const orders = await ordersPromise;                 // likely already done
  return [user, orders];                              // total ≈ 500ms
}
```

**Rule of thumb:** If two operations don't depend on each other, start both before awaiting either.

### Concurrency limiter

Process at most N items at a time — avoids overwhelming the server:

```typescript
// src/app/utils/async-patterns.ts
async function processWithConcurrency<T, R>(
  items: T[],
  processor: (item: T) => Promise<R>,
  concurrency: number = 3,
): Promise<R[]> {
  const results: R[] = new Array(items.length);
  let index = 0;

  async function worker(): Promise<void> {
    while (index < items.length) {
      const currentIndex = index++;
      results[currentIndex] = await processor(items[currentIndex]);
    }
  }

  const workers = Array.from(
    { length: Math.min(concurrency, items.length) },
    () => worker(),
  );
  await Promise.all(workers);
  return results;
}
```

### Non-blocking iteration

Processing large arrays synchronously blocks the UI. Yielding between chunks keeps the UI responsive:

```typescript
// src/app/utils/async-patterns.ts
async function processInChunks<T>(
  items: T[],
  processor: (item: T) => void,
  chunkSize: number = 100,
): Promise<void> {
  for (let i = 0; i < items.length; i += chunkSize) {
    const chunk = items.slice(i, i + chunkSize);
    chunk.forEach(processor);

    // Yield to the event loop — allows UI updates and other callbacks
    await new Promise((resolve) => setTimeout(resolve, 0));
  }
}
```

---

## 7. Async in Angular

> **Source:** `src/app/services/`, `src/app/pages/`, `src/app/guards/`

### Pattern: Signals + async/await

Angular signals are **synchronous** — you call `.set()` or `.update()` to change them. But the data that populates them can come from **async** operations. The pattern:

1. Component renders with initial state (empty arrays, loading = true)
2. `ngOnInit()` kicks off async data fetching
3. When the API responds, signals are updated
4. Angular's change detection re-renders the template automatically

### Service: loading/error state with signals

```typescript
// src/app/services/course.service.ts
@Injectable({ providedIn: 'root' })
export class CourseService {
  // Core state
  private readonly coursesSignal = signal<Course[]>([]);

  // Loading/error state — exposed as readonly
  private readonly _loadingCourses = signal(false);
  private readonly _error = signal<string | null>(null);

  readonly loadingCourses = this._loadingCourses.asReadonly();
  readonly error = this._error.asReadonly();

  // Computed signals react when the source signals change
  readonly enrolledCourses = computed(() => {
    const activeEnrollments = this.enrollmentsSignal().filter(
      (e): e is ActiveEnrollment => e.kind === 'active'
    );
    return this.coursesSignal().filter((c) =>
      activeEnrollments.some((e) => e.courseId === c.id)
    );
  });

  async loadCourses(): Promise<void> {
    this._loadingCourses.set(true);
    this._error.set(null);

    try {
      const response = await fetchCourses();
      this.coursesSignal.set(response.data);
    } catch (err) {
      const apiErr = err as ApiError;
      this._error.set(apiErr.message ?? 'Failed to load courses');
    } finally {
      this._loadingCourses.set(false);
    }
  }
}
```

### Service: async login

```typescript
// src/app/services/auth.service.ts
async login(email: string, password: string): Promise<boolean> {
  this._loading.set(true);
  this._error.set(null);

  try {
    const response = await loginApi(email, password);
    const studentData = response.data;
    this.currentStudent.set(studentData);

    const user = new StudentUser(
      studentData.id, studentData.name, studentData.email,
      studentData.gpa, studentData.enrollmentYear, studentData.major,
    );
    user.recordLogin();
    this.currentStudentUser.set(user);
    return true;
  } catch (err) {
    const apiErr = err as ApiError;
    this._error.set(apiErr.message ?? 'Authentication failed');
    return false;
  } finally {
    this._loading.set(false);
  }
}
```

### Component: async ngOnInit

Angular calls `ngOnInit()` synchronously. If it returns a Promise, **Angular does NOT await it**. The component renders immediately with the initial state, and updates reactively when signals change.

```typescript
// src/app/pages/dashboard/dashboard.component.ts
async ngOnInit(): Promise<void> {
  console.log('[Dashboard] ngOnInit — starting async data load');

  const student = this.student();
  if (!student) return;

  // Parallel fetch with Promise.all
  await Promise.all([
    this.courseService.loadCourses(),
    this.courseService.loadEnrollments(student.id),
  ]);

  console.log('[Dashboard] Data loaded');
}
```

### Component: async event handler

Angular's event system handles async handlers transparently — the returned Promise is fire-and-forget:

```typescript
// src/app/pages/login/login.component.ts
async onSubmit(event: Event): Promise<void> {
  event.preventDefault();

  if (!this.email() || !this.password()) {
    this.errorMessage.set('Please enter email and password.');
    return;
  }

  this.errorMessage.set('');

  // await pauses until the API responds (300-1000ms)
  const success = await this.authService.login(this.email(), this.password());

  if (success) {
    this.router.navigate(['/dashboard']);
  } else {
    this.errorMessage.set(
      this.authService.authError() ?? 'Invalid credentials.'
    );
  }
}
```

### Template: conditional rendering based on async state

```html
<!-- src/app/pages/dashboard/dashboard.component.html -->
@if (loadingCourses()) {
  <div class="loading-container">
    <div class="spinner"></div>
    <p>Loading your courses...</p>
  </div>
} @else if (enrolledCourses().length === 0) {
  <p class="empty-message">
    You haven't enrolled in any courses yet.
  </p>
} @else {
  <div class="course-grid">
    @for (course of enrolledCourses(); track course.id) {
      <app-course-card [course]="course" ... />
    }
  </div>
}
```

### Guard: async canActivate

Angular route guards support returning `Promise<boolean | UrlTree>`. The guard below ensures enrollment data is loaded before checking the credit limit:

```typescript
// src/app/guards/enrollment.guard.ts
export const enrollmentGuard: CanActivateFn = async () => {
  const courseService = inject(CourseService);
  const authService = inject(AuthService);
  const router = inject(Router);

  const MAX_CREDITS = 18;

  // Ensure data is loaded before checking
  const student = authService.student();
  if (student) {
    await courseService.loadEnrollments(student.id);
  }

  if (courseService.totalCredits() < MAX_CREDITS) {
    return true;
  }

  return router.createUrlTree(['/dashboard'], {
    queryParams: { maxCredits: 'true' },
  });
};
```

### Sequential async in course detail

The course-detail page needs the course data first (to get `prerequisiteIds`), then fetches prerequisites in parallel:

```typescript
// src/app/pages/course-detail/course-detail.component.ts
async ngOnInit(): Promise<void> {
  const id = this.route.snapshot.paramMap.get('id');
  if (!id) { this.loading.set(false); return; }

  try {
    // Step 1: Sequential — need the course to get prereq IDs
    const courseRes = await fetchCourseById(id);
    this.course.set(courseRes.data);

    // Step 2: Parallel — fetch all prerequisites at once
    if (courseRes.data.prerequisiteIds.length > 0) {
      const prereqs = await fetchMultipleCourses(courseRes.data.prerequisiteIds);
      this.prerequisites.set(prereqs);
    }

    // Step 3: Check enrollment status
    const student = this.authService.student();
    if (student) {
      await this.courseService.loadEnrollments(student.id);
      this.enrollmentStatus.set(
        this.courseService.getEnrollmentStatus(id, student.id)
      );
    }
  } catch {
    this.error.set('Failed to load course details');
  } finally {
    this.loading.set(false);
  }
}
```

### Decorator interaction with async methods

The `@Log` decorator works with async methods because the `async` keyword makes the method return a Promise, and the decorator wraps the call:

```typescript
// src/app/services/course.service.ts
@Log
async enroll(courseId: string, studentId: string): Promise<boolean> {
  // ...
}
```

The `@Log` decorator logs `called with: [courseId, studentId]` and `returned: Promise { <pending> }`. The Promise itself resolves later — the decorator sees the Promise object, not the resolved value.

The `@Memoize` decorator was **removed** from the async version because caching a Promise that may resolve to different data on each call (e.g., after another student enrolls) would produce stale results.

---

## 8. Common Pitfalls

> **Source:** `src/app/utils/async-patterns.ts` (pitfalls section)

### Pitfall 1: `forEach` does NOT await

```typescript
// BUG: all fetches start simultaneously!
ids.forEach(async (id) => {
  const user = await fetchUserPromise(id);
  console.log(user.name);
});
console.log('Done'); // Logs BEFORE any user name!
```

`forEach` calls the callback synchronously for each item. Each `async` callback returns a Promise, but `forEach` ignores those Promises.

**Fix: use `for...of`**

```typescript
for (const id of ids) {
  const user = await fetchUserPromise(id);
  console.log(user.name);
}
console.log('Done'); // Logs AFTER all user names
```

### Pitfall 2: Forgetting to return/await in .then()

```typescript
// BUG: forgot to return the inner Promise
fetchUserPromise('user-1')
  .then((user) => {
    fetchOrdersPromise(user.id);  // not returned!
    return user.name;  // returns immediately
  });
```

The inner `fetchOrdersPromise` fires but the chain doesn't wait for it.

**Fix: always return Promises in `.then()`**

```typescript
fetchUserPromise('user-1')
  .then((user) => {
    return fetchOrdersPromise(user.id);  // chain waits
  });
```

### Pitfall 3: Unhandled rejections

```typescript
// BAD: no .catch() and no try/catch
fetchUserPromise('bad');  // unhandled rejection!

// GOOD: always handle rejections
fetchUserPromise('bad').catch((err) => {
  console.error('Handled:', err.message);
});
```

### Pitfall 4: async ngOnInit doesn't delay rendering

```typescript
// Angular does NOT await ngOnInit's return value
async ngOnInit(): Promise<void> {
  await this.loadData();  // component already rendered with empty state!
}
```

This is **expected behavior**, not a bug. Use loading signals to show spinners while data loads.

### Pitfall 5: Race conditions in components

If a user navigates quickly between pages, an older request might resolve after a newer one:

```typescript
// Component 1 starts loading course A (slow request)
// User navigates to Component 2 (course B loads fast)
// Component 1's response arrives and updates shared state — stale data!
```

**Fix:** Check if the component is still alive or if the route param still matches before updating state.

---

## 9. Interview Questions & Answers

### Q1: What is the event loop?

**A:** The event loop is JavaScript's concurrency model. JS is single-threaded — it executes one thing at a time on the call stack. When the stack is empty, the event loop checks two queues: the **microtask queue** (Promise callbacks, `queueMicrotask`) is drained completely first, then **one** macrotask (setTimeout, DOM events, I/O) is processed. After each macrotask, microtasks are drained again. This cycle repeats indefinitely.

### Q2: What is the difference between microtasks and macrotasks?

**A:**
- **Microtasks** (Promise `.then`/`.catch`/`.finally`, `queueMicrotask`, `MutationObserver`): Higher priority. The entire microtask queue drains before any macrotask runs. Microtasks that schedule more microtasks run in the same cycle.
- **Macrotasks** (`setTimeout`, `setInterval`, DOM events, I/O): Lower priority. Only one macrotask per event loop iteration.

### Q3: What is a Promise?

**A:** A Promise is an object representing the eventual completion or failure of an async operation. It has three states: **pending** (initial), **fulfilled** (resolved with a value), or **rejected** (failed with a reason). Once settled, a Promise is immutable — its state and value never change. Promises can be chained with `.then()`, errors caught with `.catch()`, and cleanup done with `.finally()`.

### Q4: What is the difference between `.then()` and `async/await`?

**A:** They are functionally equivalent — `async/await` is syntactic sugar over Promises. `await expression` is roughly equivalent to `expression.then(result => ...)`. The advantages of `async/await`: reads like synchronous code, try/catch error handling, easier debugging (stack traces, breakpoints). The advantage of `.then()`: more explicit composition and easier to build dynamic chains.

### Q5: What happens if you don't `await` a Promise?

**A:** The Promise still executes — it's fire-and-forget. The async operation runs in the background, but:
- You can't use its result
- If it rejects, it may cause an unhandled rejection warning/error
- Subsequent code runs immediately without waiting

### Q6: How do you run Promises in parallel?

**A:** Use `Promise.all([p1, p2, p3])`. All promises start executing immediately (they are created before being passed to `Promise.all`). The result resolves when all fulfill, or rejects immediately if any reject. For partial results, use `Promise.allSettled()`.

### Q7: What is `Promise.race` used for?

**A:** It resolves/rejects with the first Promise to settle. Common use cases:
- **Timeout:** Race a request against a timer — whichever finishes first wins
- **Fastest source:** Race multiple mirrors/caches for the fastest response

### Q8: How does `async/await` interact with the event loop?

**A:** `await` wraps the remainder of the function (after the `await`) in a microtask. When you `await`, execution pauses, control returns to the event loop, and the continuation is scheduled as a microtask when the awaited Promise settles. This means other microtasks (and even macrotasks if the Promise takes time) can run between `await` points.

### Q9: Can Angular's `ngOnInit` be async?

**A:** Yes, you can declare `async ngOnInit(): Promise<void>`. However, Angular calls `ngOnInit` synchronously and **does not await the returned Promise**. The component renders immediately with the initial state. You should use loading signals/flags to show a loading state while data fetches, and update signals when the data arrives — Angular's change detection handles the re-render automatically.

### Q10: What is callback hell and how do you fix it?

**A:** Callback hell (Pyramid of Doom) is deeply nested callbacks where each operation depends on the previous one. Each level adds indentation and error handling, making code hard to read and maintain. Solutions:
1. **Named functions** — flatten nesting by extracting callbacks (partial fix)
2. **Promises** — chain `.then()` calls instead of nesting
3. **async/await** — most readable, synchronous-looking code

### Q11: How do you handle errors in async/await?

**A:** Several patterns:
1. **try/catch** (most common): wrap `await` in try/catch, handle in catch block
2. **Inline .catch()**: `const result = await promise.catch(() => defaultValue)`
3. **Go-style tuple**: return `[error, null]` or `[null, result]` from helper functions

The `finally` block (or `.finally()`) runs regardless of success/failure — ideal for cleanup like clearing loading states.

### Q12: What is the difference between `Promise.all` and `Promise.allSettled`?

**A:**
- `Promise.all`: Rejects immediately if ANY promise rejects (fail-fast). Use when you need ALL results and want to abort on first failure.
- `Promise.allSettled`: Never rejects. Waits for ALL promises and returns an array of `{status: 'fulfilled', value}` or `{status: 'rejected', reason}` objects. Use when you want partial results.

### Q13: Why doesn't `forEach` work with `await`?

**A:** `Array.prototype.forEach` calls its callback synchronously for each element and ignores the return value. Even if the callback is `async`, `forEach` doesn't await the returned Promises. Use `for...of` for sequential async iteration, or `Promise.all(arr.map(async item => ...))` for parallel execution.

### Q14: What is the difference between returning and awaiting a Promise in an async function?

**A:**
```typescript
async function foo() { return fetchData(); }     // returns the Promise directly
async function bar() { return await fetchData(); } // awaits, then wraps in new Promise
```
Functionally equivalent for the caller. However, `return await` is useful inside a `try/catch` — without `await`, the rejection happens after the function returns, bypassing the catch block.

### Q15: How do you implement a timeout for a Promise?

**A:** Use `Promise.race` with a timer:
```typescript
function withTimeout<T>(promise: Promise<T>, ms: number): Promise<T> {
  const timeout = new Promise<never>((_, reject) =>
    setTimeout(() => reject(new Error(`Timeout after ${ms}ms`)), ms)
  );
  return Promise.race([promise, timeout]);
}
```

### Q16: What is the difference between `Promise.resolve()` and `new Promise(resolve => resolve())`?

**A:** `Promise.resolve(value)` creates an already-fulfilled Promise. It's synchronous and more efficient. `new Promise(resolve => resolve(value))` creates a pending Promise and resolves it in the executor — the `.then()` callback runs as a microtask in either case. `Promise.resolve` is preferred for wrapping known values.

### Q17: How do signals + async/await work together in Angular?

**A:** Signals are synchronous containers. Async/await is used to fetch data from APIs. The pattern:
1. Set loading signal to `true`
2. `await` the API call
3. Set the data signal with the response
4. Set loading signal to `false` (in `finally`)

Computed signals that derive from the data signal automatically recompute when the data signal changes, and Angular's template bindings re-render automatically. No manual subscription management needed.

---

## Files Modified in This Chapter

| File | Change |
|---|---|
| `src/app/api/fake-api.ts` | **New** — Complete fake REST API with callback, Promise, and async/await patterns |
| `src/app/utils/async-patterns.ts` | **New** — Educational reference: callbacks→Promises→async/await evolution, event loop demos, pitfalls |
| `src/app/services/course.service.ts` | Converted from synchronous mock data to async API calls with loading/error signals |
| `src/app/services/auth.service.ts` | Converted login to async with loading/error signals |
| `src/app/pages/login/login.component.ts` | Async `onSubmit`, disabled inputs during loading |
| `src/app/pages/dashboard/dashboard.component.ts` | Async `ngOnInit` with `Promise.all` for parallel loading |
| `src/app/pages/dashboard/dashboard.component.html` | Loading spinner, error alert with dismiss |
| `src/app/pages/enrollment/enrollment.component.ts` | Async `ngOnInit` and `onEnroll` |
| `src/app/pages/enrollment/enrollment.component.html` | Loading spinner, error alert |
| `src/app/pages/course-detail/course-detail.component.ts` | Async course fetch + sequential→parallel prerequisite resolution |
| `src/app/pages/course-detail/course-detail.component.html` | Loading, error, and action-loading states |
| `src/app/guards/enrollment.guard.ts` | Converted to async guard — ensures data loaded before checking credits |
