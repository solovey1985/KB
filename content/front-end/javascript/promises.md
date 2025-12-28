 Promises represent a significant shift in asynchronous error handling and function chaining in JavaScript. Let's delve into the main concepts, supplemented with examples:

- [1. **Promise States:**](#1-promise-states)
- [2. **Promise Construction:**](#2-promise-construction)
- [3. **Then \& Catch:**](#3-then--catch)
- [4. **Chaining:**](#4-chaining)
- [5. **Static Methods:**](#5-static-methods)
- [Pitfalls:](#pitfalls)

### 1. **Promise States:**
A Promise has one of three states:

- **Pending:** The initial state; the promise is neither fulfilled nor rejected.
- **Fulfilled (Resolved):** The operation completed successfully, and the promise has a resulting value.
- **Rejected:** The operation failed, and the promise has a reason for the failure.

### 2. **Promise Construction:**
A Promise is created using the `Promise` constructor.

```javascript
const myPromise = new Promise((resolve, reject) => {
  if (/* some condition */) {
    resolve('Success!');
  } else {
    reject('Failure!');
  }
});
```

### 3. **Then & Catch:**

- `.then()`: Attach callback for when the promise is resolved.
- `.catch()`: Attach callback for when the promise is rejected.

```javascript
myPromise
  .then(result => {
    console.log(result); // If resolved, logs 'Success!'
  })
  .catch(error => {
    console.log(error); // If rejected, logs 'Failure!'
  });
```

### 4. **Chaining:**
Promises can be chained. A callback in `.then()` can return another promise, allowing you to chain `.then()` calls.

```javascript
const fetchData = () => {
  return new Promise((resolve) => {
    setTimeout(() => resolve('Data fetched!'), 1000);
  });
};

fetchData()
  .then(result => {
    console.log(result); // Logs 'Data fetched!' after 1 second
    return 'Processing data...';
  })
  .then(data => {
    console.log(data); // Logs 'Processing data...'
  });
```

### 5. **Static Methods:**
- **Promise.resolve(value)**: Returns a promise that is resolved with the given value.
- **Promise.reject(reason)**: Returns a promise that is rejected with the given reason.
- **Promise.all(iterable)**: Returns a promise that resolves when all of the promises in the iterable have resolved, or rejects with the reason of the first passed promise that rejects.
- **Promise.race(iterable)**: Returns a promise that resolves or rejects as soon as one of the promises in the iterable resolves or rejects.

```javascript
const promise1 = Promise.resolve('First');
const promise2 = new Promise((res) => setTimeout(res, 100, 'Second'));

Promise.all([promise1, promise2]).then(values => {
  console.log(values); // ['First', 'Second']
});
```

### Pitfalls:

1. **Unhandled Rejections:** If a promise is rejected and there isn't a corresponding `.catch()` to handle the error, it might lead to unhandled promise rejections. Always have a `.catch()` at the end of your promise chain.

2. **Error Swallowing:** Promises can sometimes "swallow" exceptions, meaning if you have an error in a `.then()` callback and you don't have a subsequent `.catch()`, that error might not be evident.

3. **Overcomplication:** For simple, one-off asynchronous operations, callbacks might be simpler than wrapping them in a promise.

4. **Memory Overhead:** Each promise has a memory overhead. If creating a large number of promises, it can eat up memory.

5. **Error Propagation:** If not careful, an error in one part of a promise chain can propagate down and be caught in a later `.catch()`, making it unclear where the error originated.

6. **State Immutability:** Once a promise is settled (either resolved or rejected), its state cannot change. This is by design but can be surprising if you come from a background of using other async patterns.

To make the most of promises, it's essential to understand these intricacies and potential pitfalls. Overall, promises offer a powerful way to work with asynchronous operations in a more structured manner than traditional callbacks.