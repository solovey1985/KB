# React Routing and Route Protection

> **Code context:** `react-student-dashboard/src/app/router.tsx`.

## 1. Router configuration

The app creates one browser router outside the React tree and gives it to `RouterProvider`:

```tsx
const router = createBrowserRouter([
  { element: <AppShell />, children: [/* routes */] },
])

export function AppRouter() {
  return <RouterProvider router={router} />
}
```

This is closest to Angular's route array plus `provideRouter(routes)`. Vue similarly creates a router once with `createRouter()` and registers it with the app.

## 2. Nested layouts and protection

`AppShell` contains the navigation layout and `<Outlet />`. The protected routes are nested below `ProtectedRoute`, which either renders its outlet or redirects to login. The redirect retains the requested path in route state, so Login can return the user after authentication.

React does not include Angular-style `CanActivate` functions. In a client-rendered app, a wrapper component is a straightforward pattern. For server-backed applications, React Router loaders can instead enforce access before a route component renders.

## 3. Parameters and navigation

`/courses/:courseId` is read with `useParams()`:

```tsx
const { courseId } = useParams()
const course = courses.find((item) => item.id === courseId)
```

Use `<Link>` and `<NavLink>` for navigation rather than `<a href>`, because they keep navigation inside the client router. `useNavigate()` is the imperative alternative used after a successful login.

## 4. Code splitting

Pages are loaded only when needed:

```tsx
const EnrollmentPage = lazy(() => import('../features/enrollment/EnrollmentPage')
  .then((module) => ({ default: module.EnrollmentPage })))
```

`Suspense` supplies the loading UI while that JavaScript chunk downloads. This maps directly to Angular `loadComponent: () => import(...)` and Vue route components using `() => import(...)`.
