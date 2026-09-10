# Angular, React, and Vue: Student Dashboard Comparison

> Main comparison: Angular to React. Vue is included as a useful secondary reference.

| Concern | Angular | React | Vue |
|---|---|---|---|
| App bootstrap | `bootstrapApplication` + providers | `createRoot` + provider components | `createApp` + plugins |
| Component | class/decorator or standalone component | function returning JSX | SFC, often `<script setup>` |
| UI syntax | HTML template | JSX | HTML template/directives |
| Local state | Signals | `useState` / `useReducer` | `ref` / `reactive` |
| Derived state | `computed()` | render-time calculation / `useMemo` if costly | `computed()` |
| Lifecycle/side effect | hooks such as `ngOnInit` | `useEffect` | `onMounted`, `watch` |
| Parent to child | `input()` | props | props |
| Child to parent | `output()` | callback prop | emitted event |
| Shared state | service + Signals | Context + reducer/custom hook | Pinia store |
| Dependency access | `inject()` / DI tree | imports and hooks/providers | imports, composables, Pinia |
| Route protection | `canActivate` | protected route wrapper or loader | navigation guard |
| Display transform | pipe | function/helper in JSX | composable/helper |
| Lazy page | `loadComponent` | `lazy` + `Suspense` | dynamic `import()` |

## Angular developer’s React translation

1. A React function component replaces both an Angular class and its template. Its body may run many times; do not use it like `ngOnInit`.
2. Props replace inputs. A callback prop replaces an output event.
3. `useEffect` is for synchronization outside rendering—HTTP calls, subscriptions, browser APIs—not for all derived state.
4. Context provides a value to descendants. Pair it with a reducer or domain-specific hooks for coherent application state.
5. React has no built-in DI system. Dependencies are typically imported, passed as props, or made available through a provider.

## Where Vue helps

Vue's `ref`/`computed` model may feel more immediately familiar to Angular Signal users. React separates state (`useState`) from derived calculations (ordinary JavaScript during render), which is why React code often looks more like standard TypeScript.

In all three apps, the durable concepts are identical: one-way data flow, domain ownership, route protection, asynchronous boundaries, and UI derived from state.
