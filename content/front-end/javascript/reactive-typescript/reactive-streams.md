# Reactive Streams and Observables

Observables are useful when one future value is not enough and the application needs a stream of values over time.

## Promise versus Observable

Promises represent one future value.

Observables represent zero to many values over time.

```javascript
// Promise: one result
fetch('/api/user').then(response => response.json());

// Observable: stream of results over time
searchInput$.subscribe(value => console.log(value));
```

## Laziness and cancellation

Promises start immediately.

Observables are usually lazy and begin producing values on subscription.

They also support `unsubscribe()` for cancellation and cleanup.

## Core RxJS operators

```javascript
from([1, 2, 3, 4]).pipe(
  filter(value => value % 2 === 0),
  map(value => value * 10),
  tap(value => console.log('debug', value))
).subscribe(console.log);
```

## switchMap for live search

`switchMap` is especially important in frontend code because it cancels older inner streams when new values arrive.

```javascript
searchTerms$.pipe(
  debounceTime(300),
  switchMap(term => from(fetch(`/api/search?q=${term}`)))
).subscribe();
```

## Interview reminders

- say "one value versus many values over time"
- mention laziness and cancellation when comparing Promises and Observables
- mention `switchMap` for search-as-you-type scenarios
