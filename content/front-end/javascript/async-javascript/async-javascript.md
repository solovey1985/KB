# Async JavaScript

Asynchronous JavaScript is what allows frontend applications to stay responsive while waiting for timers, network requests, and user interactions.

## Event loop, tasks, and microtasks

The JavaScript runtime uses the call stack together with queues.

Promise handlers run in the microtask queue, which is processed before the next macrotask such as a timer callback.

```javascript
console.log('start');

setTimeout(() => console.log('timeout'), 0);
Promise.resolve().then(() => console.log('promise'));

console.log('end');

// start
// end
// promise
// timeout
```

## Callbacks

Callbacks were the traditional async pattern, but deeply nested callbacks are hard to manage.

```javascript
loadUser(userId, user => {
  loadOrders(user.id, orders => {
    render(user, orders);
  });
});
```

## Promises

Promises model one future result with three states:

- pending
- fulfilled
- rejected

```javascript
fetch('/api/user')
  .then(response => response.json())
  .then(user => console.log(user))
  .catch(error => console.error(error));
```

## async/await

`async`/`await` makes Promise-based code read more sequentially.

```javascript
async function loadUser() {
  try {
    const response = await fetch('/api/user');
    const user = await response.json();
    return user;
  } catch (error) {
    console.error(error);
    throw error;
  }
}
```

## Parallel versus sequential work

Independent async tasks should often run in parallel.

```javascript
async function loadDashboard() {
  const [user, settings] = await Promise.all([
    fetch('/api/user').then(r => r.json()),
    fetch('/api/settings').then(r => r.json())
  ]);

  return { user, settings };
}
```

## Error handling and race conditions

Async code needs explicit failure handling and awareness of ordering problems.

Common issues:

- unhandled promise rejections
- multiple requests finishing in an unexpected order
- stale UI updates from older requests

## Interview reminders

- always mention microtasks when comparing Promises and timers
- mention `Promise.all` for independent tasks
- mention `try/catch` with `async`/`await`
