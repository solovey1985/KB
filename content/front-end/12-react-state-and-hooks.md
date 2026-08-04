# React State, Context, Reducers, and Custom Hooks

> **Code context:** `features/auth/AuthContext.tsx` and `features/courses/CourseContext.tsx`.

## 1. Local versus shared state

Use local `useState` for state owned by one component, such as the login form values. Use Context when multiple distant components need a stable application concern such as the authenticated student or courses and enrollment.

This app has two domain providers rather than one global store:

- `AuthProvider` owns the current student and login lifecycle.
- `CourseProvider` owns courses, enrollment, loading/error state, and enrollment actions.

Angular's close equivalent is a root-provided service; Vue's is a Pinia store. React Context is transport for a value, not a state-management architecture by itself.

## 2. Reducers model transitions

The course domain has related values and multiple async transitions, so it uses `useReducer`:

```tsx
case 'enrolled':
  return { ...state, enrollments: [...state.enrollments, action.enrollment] }
```

Reducers are pure functions: `(previousState, action) => nextState`. They make valid state changes easy to locate and test. Use `useState` when an update is simple; use a reducer when actions describe meaningful transitions.

## 3. Custom hooks form the public API

```tsx
export function useCourses() {
  const context = useContext(CourseContext)
  if (!context) throw new Error('useCourses must be used within CourseProvider.')
  return context
}
```

Components depend on `useCourses()`, not the raw context. This is similar to injecting an Angular service: callers get a compact domain API, and the provider implementation can evolve internally. The same pattern applies to `useAuth()`.

## 4. Async boundary

`services/fakeApi.ts` represents the backend boundary. It simulates latency and is the only layer allowed to mutate its mock database. Context actions call this API, then dispatch the response to local UI state.

This separation is intentional: pages do not contain raw data access, and the future replacement with `fetch` or a client library will be contained in `services/`.

## 5. Derived values

`totalCredits` is derived from active enrollments and courses. It is not independently stored, avoiding synchronization bugs. Angular computed Signals and Vue computed properties serve the same purpose. Use `useMemo` only if calculating a derived value becomes expensive.
