# React Core Fundamentals

> **Audience:** Angular developers learning React. Vue comparisons appear where they clarify a difference.
> **Code context:** `react-student-dashboard`, the student course and enrollment application.

## Navigation

| # | Guide | Focus |
|---|---|---|
| **1** | **Core fundamentals** | JSX, rendering, state, effects |
| 2 | [Component patterns](./10-react-component-patterns.md) | Props, callbacks, composition |
| 3 | [Routing and protection](./11-react-routing-and-protection.md) | React Router and protected routes |
| 4 | [State and hooks](./12-react-state-and-hooks.md) | Context, reducers, custom hooks |

See [Angular, React, and Vue comparison](./ANGULAR_REACT_VUE_COMPARISON.md).

## 1. Bootstrap

`src/main.tsx` creates a React root and renders the app into `#root`:

```tsx
createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <AppProviders><AppRouter /></AppProviders>
  </StrictMode>,
)
```

Angular bootstraps a root component through `bootstrapApplication()` and providers. React instead calls `createRoot()` directly; application-wide providers are ordinary components that wrap the router. Vue's `createApp().use(...).mount(...)` is the closest secondary comparison.

`StrictMode` is development-only help: it intentionally re-runs selected logic to expose unsafe side effects. Effects must therefore be repeatable and include cleanup when they subscribe to external resources.

## 2. Components return JSX

A React component is a function that receives props and returns JSX:

```tsx
export function CourseCard({ course, status, busy, onEnroll }: Props) {
  return <article className="card">
    <h2><Link to={`/courses/${course.id}`}>{course.name}</Link></h2>
    <button disabled={busy} onClick={onEnroll}>Enroll</button>
  </article>
}
```

JSX looks like HTML but is JavaScript syntax. Expressions use `{...}`, attributes use JavaScript names such as `className`, and event handlers receive functions rather than template event strings.

| Idea | Angular | React | Vue |
|---|---|---|---|
| Component | decorated class | function | SFC |
| Markup | external/inline template | JSX return value | `<template>` |
| Conditional UI | `@if` | `condition && <View />` | `v-if` |
| Lists | `@for` | `items.map(...)` | `v-for` |

## 3. State causes rendering

In React, a component is rendered from its current props and state. Calling a state setter schedules a new render; React calculates the next UI from scratch and updates only the needed DOM parts.

```tsx
const [email, setEmail] = useState('alice@university.edu')
<input value={email} onChange={(event) => setEmail(event.target.value)} />
```

This is a **controlled input**: the value lives in React state and the change handler updates it. Angular's closest equivalent is a reactive form control; Vue's is `v-model`.

Do not mutate state objects or arrays in place. Return a new array/object instead. The course reducer demonstrates this with `map()` and array spreading.

## 4. Effects synchronize with external systems

Rendering should be pure: it describes UI and should not fetch, subscribe, or write outside React. `useEffect` performs that synchronization after React commits a render.

```tsx
useEffect(() => {
  if (student) void load(student.id)
}, [student, load])
```

This Dashboard effect loads data whenever its declared inputs change. The dependency array is not an optimization hint—it documents which values the effect reads.

Angular developers can think of this as a focused replacement for parts of `ngOnInit` and `ngOnChanges`, but it runs after rendering. Vue's closest API is `watchEffect()` or `onMounted()` plus `watch()`.

## 5. Mental model

The important React shift is: **UI = function of state**. There are no decorators, no template compiler syntax to learn, and no framework DI container. Composition happens through imports, props, and provider components.
