# Vue.js Routing & Navigation Guards

> **Series**: Vue.js Comprehensive Guide (3 of 5)
> **Audience**: Developers with Angular experience transitioning to Vue.js
> **Code context**: Student Dashboard + Class Enrollment application

### Document Navigation

| # | Document | Focus |
|---|----------|-------|
| 1 | [Core Fundamentals](./01-vue-core-fundamentals.md) | Reactivity, templates, single-file components |
| 2 | [Component Patterns](./02-vue-component-patterns.md) | Props, emits, lifecycle hooks, component design |
| **3** | **Routing & Navigation Guards** (this document) | vue-router, lazy loading, route guards |
| 4 | [State Management with Pinia](./04-vue-state-management.md) | Stores, reactive state, actions, getters |
| 5 | [Data Transforms & Composables](./05-vue-composables-and-transforms.md) | Composables, formatting, reusable logic |

See also: [Angular vs Vue.js Side-by-Side Comparison](./COMPARISON.md)

---

## Table of Contents

1. [Router Setup](#1-router-setup)
2. [Route Configuration](#2-route-configuration)
3. [Lazy Loading](#3-lazy-loading)
4. [Route Parameters & Query Params](#4-route-parameters--query-params)
5. [Programmatic Navigation](#5-programmatic-navigation)
6. [Navigation Guards](#6-navigation-guards)
7. [Router View & Router Link](#7-router-view--router-link)
8. [Angular Comparison](#8-angular-comparison)

---

## 1. Router Setup

Vue Router is installed as a plugin in the application bootstrap:

**Source**: `vue-student-dashboard/src/main.ts`

```typescript
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

const app = createApp(App)
app.use(createPinia())
app.use(router)          // Install vue-router plugin
app.mount('#app')
```

The router instance is created in a dedicated file:

**Source**: `vue-student-dashboard/src/router/index.ts`

```typescript
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [ /* ... */ ],
})

export default router
```

### `createRouter()` options

| Option | Purpose |
|--------|---------|
| `history` | History mode — `createWebHistory()` for clean URLs (`/dashboard`), `createWebHashHistory()` for hash-based (`/#/dashboard`) |
| `routes` | Array of route definitions |
| `scrollBehavior` | Optional function to control scroll position on navigation |
| `linkActiveClass` | CSS class for active `<RouterLink>` elements (default: `router-link-active`) |

**References**:
- [Vue Router Getting Started](https://router.vuejs.org/guide/)
- [createRouter API](https://router.vuejs.org/api/#createrouter)
- [History Modes](https://router.vuejs.org/guide/essentials/history-mode.html)

---

## 2. Route Configuration

**Source**: `vue-student-dashboard/src/router/index.ts`

```typescript
import { authGuard } from '@/guards/auth.guard'
import { enrollmentGuard } from '@/guards/enrollment.guard'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    // Redirect
    {
      path: '/',
      redirect: '/dashboard',
    },

    // Public route (no guard)
    {
      path: '/login',
      name: 'login',
      component: () => import('@/views/LoginView.vue'),
    },

    // Protected route (single guard)
    {
      path: '/dashboard',
      name: 'dashboard',
      component: () => import('@/views/DashboardView.vue'),
      beforeEnter: [authGuard],
    },

    // Protected route (multiple guards, run sequentially)
    {
      path: '/enrollment',
      name: 'enrollment',
      component: () => import('@/views/EnrollmentView.vue'),
      beforeEnter: [authGuard, enrollmentGuard],
    },

    // Route with dynamic parameter
    {
      path: '/course/:id',
      name: 'courseDetail',
      component: () => import('@/views/CourseDetailView.vue'),
      beforeEnter: [authGuard],
    },

    // Catch-all (404)
    {
      path: '/:pathMatch(.*)*',
      name: 'notFound',
      component: () => import('@/views/NotFoundView.vue'),
    },
  ],
})
```

### Route definition properties

| Property | Purpose | Example |
|----------|---------|---------|
| `path` | URL pattern | `'/course/:id'` |
| `name` | Route identifier for programmatic navigation | `'courseDetail'` |
| `component` | Component to render (can be lazy) | `() => import(...)` |
| `redirect` | Redirect to another path | `'/dashboard'` |
| `beforeEnter` | Per-route navigation guard(s) | `[authGuard]` |
| `children` | Nested route definitions | *(not used in this app)* |
| `meta` | Custom metadata accessible in guards | `{ requiresAuth: true }` |
| `props` | Pass route params as component props | `true` or function |

### Named routes

Every route has an optional `name` property. Named routes are the preferred way to reference routes in Vue because they're decoupled from the URL path:

```typescript
// Using name (recommended — survives path refactoring)
router.push({ name: 'courseDetail', params: { id: 'cs-101' } })

// Using path (fragile — breaks if path changes)
router.push('/course/cs-101')
```

### Catch-all / 404 route

Vue Router uses regex-based path matching. The catch-all pattern is:

```typescript
{
  path: '/:pathMatch(.*)*',
  name: 'notFound',
  component: () => import('@/views/NotFoundView.vue'),
}
```

The `(.*)` captures any remaining URL segments. The outer `*` makes it repeatable (matching `/a/b/c`).

**References**:
- [Route Configuration](https://router.vuejs.org/guide/essentials/route-matching-syntax.html)
- [Named Routes](https://router.vuejs.org/guide/essentials/named-routes.html)
- [Dynamic Route Matching](https://router.vuejs.org/guide/essentials/dynamic-matching.html)

---

## 3. Lazy Loading

Every route in our application uses dynamic `import()` for code-splitting:

```typescript
{
  path: '/dashboard',
  name: 'dashboard',
  component: () => import('@/views/DashboardView.vue'),
}
```

This tells the bundler (Vite) to create a separate chunk for each view. The component is only fetched from the server when the route is first navigated to.

### Build output showing lazy chunks

```
dist/assets/LoginView-DzMGXpby.js         1.43 kB
dist/assets/DashboardView-C-j3h2PE.js     2.90 kB
dist/assets/EnrollmentView-Cz7rtm4j.js    2.05 kB
dist/assets/CourseDetailView-DoEkK9I5.js   2.67 kB
dist/assets/NotFoundView-cMuUDTks.js       0.48 kB
```

Each view is a separate file, downloaded on demand.

### Eager loading (alternative)

For routes that are always visited, you can import eagerly:

```typescript
import LoginView from '@/views/LoginView.vue'

{
  path: '/login',
  component: LoginView,    // Included in the main bundle
}
```

**References**:
- [Vue Router Lazy Loading](https://router.vuejs.org/guide/advanced/lazy-loading.html)
- [Vite Code Splitting](https://vite.dev/guide/build.html#chunking-strategy)

---

## 4. Route Parameters & Query Params

### Route parameters (`:id`)

Defined in the route path:
```typescript
{ path: '/course/:id', name: 'courseDetail', ... }
```

Read in the component via `useRoute()`:

**Source**: `vue-student-dashboard/src/views/CourseDetailView.vue`

```typescript
import { useRoute } from 'vue-router'

const route = useRoute()
const courseId = route.params.id as string

const course = computed(() => courseStore.getCourseById(courseId))
```

`route.params` is a reactive object — if the route changes while the component is still mounted (e.g., navigating from `/course/cs-101` to `/course/math-201`), the params update and any computed values depending on them re-evaluate. To respond to param changes with side effects, use `watch()`:

```typescript
watch(() => route.params.id, (newId) => {
  // Fetch new data when the route param changes
  fetchCourseData(newId as string)
})
```

### Query parameters

Defined when navigating:
```typescript
router.push({ name: 'dashboard', query: { maxCredits: 'true' } })
// URL: /dashboard?maxCredits=true
```

Read in the component:

**Source**: `vue-student-dashboard/src/views/DashboardView.vue`

```typescript
const route = useRoute()
const maxCreditsWarning = ref(route.query.maxCredits === 'true')
```

### The `useRoute()` object

| Property | Type | Description |
|----------|------|-------------|
| `route.params` | `Record<string, string>` | Dynamic route parameters |
| `route.query` | `Record<string, string>` | URL query parameters |
| `route.name` | `string` | Current route name |
| `route.path` | `string` | Current URL path |
| `route.fullPath` | `string` | Full URL including query and hash |
| `route.meta` | `Record<string, any>` | Route meta fields |
| `route.matched` | `RouteRecordNormalized[]` | All matched route records |

**References**:
- [Dynamic Route Matching](https://router.vuejs.org/guide/essentials/dynamic-matching.html)
- [Route Object Properties](https://router.vuejs.org/api/#routelocationnormalized)

---

## 5. Programmatic Navigation

Navigation from script code uses the `useRouter()` composable:

**Source**: `vue-student-dashboard/src/views/DashboardView.vue`

```typescript
import { useRouter } from 'vue-router'

const router = useRouter()

// Navigate by name with params
function onViewCourseDetails(courseId: string): void {
  router.push({ name: 'courseDetail', params: { id: courseId } })
}

// Navigate by name with query params
function navigateToEnrollment(): void {
  router.push({ name: 'enrollment' })
}

// Navigate by path (less preferred)
router.push('/login')
```

### Navigation methods

| Method | Purpose |
|--------|---------|
| `router.push(location)` | Navigate and add entry to history |
| `router.replace(location)` | Navigate without adding history entry (replaces current) |
| `router.go(n)` | Move forward/backward in history (`router.go(-1)` = back) |
| `router.back()` | Shorthand for `router.go(-1)` |
| `router.forward()` | Shorthand for `router.go(1)` |

### Location object format

```typescript
// By name (recommended)
router.push({ name: 'courseDetail', params: { id: 'cs-101' } })

// By path
router.push({ path: '/course/cs-101' })

// With query params
router.push({ name: 'dashboard', query: { maxCredits: 'true' } })

// Replace instead of push
router.replace({ name: 'login' })
```

**References**:
- [Programmatic Navigation](https://router.vuejs.org/guide/essentials/navigation.html)

---

## 6. Navigation Guards

Guards are functions that run before a route transition completes. They can allow the navigation, redirect to a different route, or cancel it.

### Per-route guards (`beforeEnter`)

Attached directly to route definitions. This is the equivalent of Angular's `canActivate`.

**Source**: `vue-student-dashboard/src/guards/auth.guard.ts`

```typescript
import type { NavigationGuardWithThis } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

export const authGuard: NavigationGuardWithThis<undefined> = (to, from) => {
  const authStore = useAuthStore()

  if (authStore.isAuthenticated) {
    return true                        // Allow navigation
  }

  return { name: 'login' }             // Redirect to login
}
```

**Source**: `vue-student-dashboard/src/guards/enrollment.guard.ts`

```typescript
import type { NavigationGuardWithThis } from 'vue-router'
import { useCourseStore } from '@/stores/course'

export const enrollmentGuard: NavigationGuardWithThis<undefined> = (to, from) => {
  const courseStore = useCourseStore()
  const MAX_CREDITS = 18

  if (courseStore.totalCredits < MAX_CREDITS) {
    return true                        // Allow navigation
  }

  return { name: 'dashboard', query: { maxCredits: 'true' } }  // Redirect with data
}
```

### Attaching guards to routes

```typescript
{
  path: '/enrollment',
  name: 'enrollment',
  component: () => import('@/views/EnrollmentView.vue'),
  beforeEnter: [authGuard, enrollmentGuard],     // Array — run sequentially
}
```

When multiple guards are provided, they execute in order. If any guard returns a redirect or `false`, subsequent guards are skipped.

### Guard return values

| Return | Effect |
|--------|--------|
| `true` or `undefined` | Allow navigation |
| `false` | Cancel navigation, stay on current route |
| `{ name: 'route' }` | Redirect to named route |
| `{ path: '/path' }` | Redirect to path |
| `{ name: 'route', query: { ... } }` | Redirect with query params |

### Guard function signature

```typescript
(to: RouteLocationNormalized, from: RouteLocationNormalized) => NavigationGuardReturn
```

| Parameter | Description |
|-----------|-------------|
| `to` | The route being navigated **to** |
| `from` | The route being navigated **from** |

Both objects have `params`, `query`, `name`, `path`, `meta`, and `matched` properties.

### Global guards

For cross-cutting concerns (like auth checks on every route), register guards on the router instance:

```typescript
// Global before guard — runs before EVERY navigation
router.beforeEach((to, from) => {
  const authStore = useAuthStore()
  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    return { name: 'login' }
  }
})

// Global after hook — runs AFTER navigation completes (no redirect possible)
router.afterEach((to, from) => {
  document.title = `${to.meta.title || 'Student Portal'}`
})

// Global resolve guard — runs after per-route guards and async components
router.beforeResolve((to) => {
  // Final check before navigation is confirmed
})
```

### Using route meta for guard decisions

Instead of hardcoding guard logic per route, you can use `meta` fields:

```typescript
// Route definition
{
  path: '/dashboard',
  name: 'dashboard',
  component: () => import('@/views/DashboardView.vue'),
  meta: { requiresAuth: true },
}

// Global guard reads meta
router.beforeEach((to) => {
  if (to.meta.requiresAuth && !useAuthStore().isAuthenticated) {
    return { name: 'login' }
  }
})
```

Our application uses the per-route `beforeEnter` approach because it's more explicit — you can see exactly which guards protect each route by looking at the route definition.

### Guard types summary

| Type | Registration | Scope | Use case |
|------|-------------|-------|----------|
| `beforeEnter` | Route definition | Single route | Route-specific auth, permissions |
| `beforeEach` | `router.beforeEach()` | All routes | Global auth, analytics |
| `beforeResolve` | `router.beforeResolve()` | All routes | Final checks after component resolution |
| `afterEach` | `router.afterEach()` | All routes | Analytics, page title, scroll |

**References**:
- [Vue Router Navigation Guards](https://router.vuejs.org/guide/advanced/navigation-guards.html)
- [Route Meta Fields](https://router.vuejs.org/guide/advanced/meta.html)
- [NavigationGuard API](https://router.vuejs.org/api/#navigationguard)

---

## 7. Router View & Router Link

### `<RouterView />`

The component that renders the matched route's component. Placed in the layout where routed content should appear:

**Source**: `vue-student-dashboard/src/App.vue`

```html
<template>
  <RouterView />
</template>
```

You can also use `<RouterView>` with a scoped slot for transitions:

```html
<RouterView v-slot="{ Component }">
  <transition name="fade" mode="out-in">
    <component :is="Component" />
  </transition>
</RouterView>
```

### `<RouterLink />`

Declarative navigation in templates. Renders an `<a>` tag with proper href:

**Source**: `vue-student-dashboard/src/views/NotFoundView.vue`

```html
<RouterLink to="/dashboard">Go to Dashboard</RouterLink>
```

With named routes:
```html
<RouterLink :to="{ name: 'courseDetail', params: { id: course.id } }">
  {{ course.name }}
</RouterLink>
```

`RouterLink` automatically applies `router-link-active` class when the link's target route matches the current route, enabling active-state styling.

**References**:
- [RouterView](https://router.vuejs.org/api/#routerview)
- [RouterLink](https://router.vuejs.org/api/#routerlink)

---

## 8. Angular Comparison

The routing concepts are virtually identical between the two frameworks — both support lazy-loaded routes, parameterized paths, query parameters, redirect routes, wildcard catch-alls, and route guards. The configuration format is similar: an array of route objects with `path`, `component`, and guard properties. The key syntactic differences are: Angular uses `loadComponent` and `canActivate`, Vue uses `component` and `beforeEnter`; Angular's wildcard is `**`, Vue's is `/:pathMatch(.*)*`; Angular reads params via `inject(ActivatedRoute).snapshot.paramMap.get('id')`, Vue reads them via `useRoute().params.id`; Angular navigates with `router.navigate(['/course', id])`, Vue navigates with `router.push({ name: 'courseDetail', params: { id } })`.

Guard architecture shows a meaningful divergence. Angular provides multiple dedicated guard types (`CanActivateFn`, `CanDeactivateFn`, `CanMatchFn`, `ResolveFn`) that separate concerns by lifecycle phase — you use `canActivate` for entry checks, `canDeactivate` for "unsaved changes" prompts, and `resolve` for data pre-fetching. Vue has a single guard function type (`NavigationGuard`) used across all contexts — the distinction comes from *where* you register it: `beforeEnter` on a route for per-route checks, `router.beforeEach()` for global checks, or `onBeforeRouteLeave()` inside a component for deactivation logic. Both models achieve the same outcomes, but Angular's approach is more declarative at the route level (you see `canDeactivate: [guard]` and know the intent), while Vue's is more flexible (the same guard function can serve any purpose depending on registration point).

**References**:
- [Angular Router Guide](https://angular.dev/guide/routing)
- [Angular Route Guards](https://angular.dev/guide/routing/common-router-tasks#preventing-unauthorized-access)
- [Vue Router Guide](https://router.vuejs.org/guide/)

---

**Previous**: [Component Patterns](./02-vue-component-patterns.md)
**Next**: [State Management with Pinia](./04-vue-state-management.md)
