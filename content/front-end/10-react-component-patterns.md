# React Component Patterns

> **Code context:** `react-student-dashboard/src/features`.

## 1. Props flow down; callbacks flow up

`CourseCard` receives data and an event callback from its parent:

```tsx
interface Props {
  course: Course
  status: EnrollmentStatus | null
  busy: boolean
  onEnroll(): void
}
```

The card does not know where enrollment state lives. It only invokes `onEnroll`. This keeps it reusable and testable.

Angular: this corresponds to `input()` fields and an `output()` emitter. React has no special emit API—functions are normal values passed as props. Vue's `defineProps` and `defineEmits` make the same relationship explicit.

## 2. Feature ownership

The React project is arranged by business capability:

```text
features/
  auth/       AuthContext and LoginPage
  courses/    CourseContext, CourseCard, dashboard and detail pages
  enrollment/ EnrollmentPage
shared/       generic components, types, utilities
services/     async API boundary
app/          providers and routing
```

Keep code close to the feature that owns it. A component becomes `shared` only when more than one feature needs it and its API is genuinely general.

This differs from the Angular sample's framework-role folders (`services`, `guards`, `components`). Both approaches are valid, but feature ownership scales well when a React app grows.

## 3. Composition over inheritance

React favors assembling components. `AppShell` owns navigation and places `<Outlet />` where a route child renders. `ProtectedRoute` composes an authentication decision around protected route children.

```tsx
return isAuthenticated
  ? <Outlet />
  : <Navigate to="/login" replace state={{ from: location.pathname }} />
```

This is analogous to Angular nesting routes below a route with `canActivate`, but it is visible in the rendered component tree.

## 4. Avoid premature memoization

`useMemo` and `useCallback` are used in the contexts where stable values/functions help provider consumers avoid unnecessary updates. They are not required for every function or calculated value. First write correct, clear components; profile before adding broad memoization.

Angular computed Signals and Vue computed refs have built-in dependency tracking. React's `useMemo` is instead a performance cache and does not make a value reactive by itself.
