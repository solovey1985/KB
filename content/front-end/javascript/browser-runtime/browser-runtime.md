# JavaScript Browser and Runtime

This page connects JavaScript language features to the browser runtime.

## Call stack and host environment

JavaScript execution runs on a call stack.

The browser or runtime host provides timers, DOM events, network APIs, and queues for deferred work.

```javascript
function first() {
  second();
}

function second() {
  console.log('running');
}

first();
```

## Event propagation

DOM events move through capturing, target, and bubbling phases.

```javascript
document.body.addEventListener('click', () => {
  console.log('body');
});

button.addEventListener('click', event => {
  console.log('button');
  event.stopPropagation();
});
```

## Event delegation

Event delegation lets one parent handler manage many child elements efficiently.

```javascript
list.addEventListener('click', event => {
  const item = event.target.closest('[data-id]');
  if (!item) return;

  console.log(item.dataset.id);
});
```

## Timers and cleanup

Timers are simple but easy to leak or misuse.

```javascript
const id = setInterval(() => {
  console.log('polling');
}, 1000);

clearInterval(id);
```

## Storage

Frontend apps often use `localStorage` for small persistent browser state.

```javascript
localStorage.setItem('theme', 'dark');
const theme = localStorage.getItem('theme');
```

## Interview reminders

- distinguish JavaScript the language from the browser host environment
- mention event delegation when discussing scalable UI event handling
- explain bubbling clearly when describing click behavior
