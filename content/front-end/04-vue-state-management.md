# Vue.js State Management with Pinia

> **Series**: Vue.js Comprehensive Guide (4 of 5)
> **Audience**: Developers with Angular experience transitioning to Vue.js
> **Code context**: Student Dashboard + Class Enrollment application

### Document Navigation

| # | Document | Focus |
|---|----------|-------|
| 1 | [Core Fundamentals](./01-vue-core-fundamentals.md) | Reactivity, templates, single-file components |
| 2 | [Component Patterns](./02-vue-component-patterns.md) | Props, emits, lifecycle hooks, component design |
| 3 | [Routing & Navigation Guards](./03-vue-routing-and-guards.md) | vue-router, lazy loading, route guards |
| **4** | **State Management with Pinia** (this document) | Stores, reactive state, actions, getters |
| 5 | [Data Transforms & Composables](./05-vue-composables-and-transforms.md) | Composables, formatting, reusable logic |

See also: [Angular vs Vue.js Side-by-Side Comparison](./COMPARISON.md)

---

## Table of Contents

1. [What is Pinia](#1-what-is-pinia)
2. [Store Definition — Setup Style](#2-store-definition--setup-style)
3. [State](#3-state)
4. [Getters (Computed)](#4-getters-computed)
5. [Actions (Methods)](#5-actions-methods)
6. [Using Stores in Components](#6-using-stores-in-components)
7. [Using Stores in Guards](#7-using-stores-in-guards)
8. [Store Design Patterns](#8-store-design-patterns)
9. [Angular Comparison](#9-angular-comparison)

---

## 1. What is Pinia

Pinia is the official state management library for Vue.js. It provides:

- **Reactive singleton stores** — created once, shared across all components
- **Full TypeScript support** — type inference without extra configuration
- **Vue DevTools integration** — inspect state, time-travel debugging
- **Setup-style API** — uses the same `ref()`, `computed()`, and functions as components

Pinia replaces the older Vuex library and is the recommended solution since Vue 3.

### Installation (already scaffolded)

```typescript
// main.ts
import { createPinia } from 'pinia'

const app = createApp(App)
app.use(createPinia())    // Makes Pinia available to all components
```

**References**:
- [Pinia Official Documentation](https://pinia.vuejs.org/)
- [Pinia Getting Started](https://pinia.vuejs.org/getting-started.html)

---

## 2. Store Definition — Setup Style

Pinia supports two store definition styles. Our application uses the **setup style** (also called "composition style") because it mirrors the `<script setup>` component pattern.

### Anatomy of a setup store

```typescript
import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

export const useMyStore = defineStore('storeId', () => {
  // 1. State — ref() and reactive()
  const items = ref<Item[]>([])

  // 2. Getters — computed()
  const itemCount = computed(() => items.value.length)

  // 3. Actions — plain functions
  function addItem(item: Item) {
    items.value.push(item)
  }

  // 4. Return everything that should be accessible externally
  return { items, itemCount, addItem }
})
```

### The `defineStore()` parameters

| Parameter | Type | Purpose |
|-----------|------|---------|
| First arg | `string` | Unique store ID (used for devtools and serialization) |
| Second arg | `() => { ... }` | Setup function returning state, getters, and actions |

**References**:
- [Defining Stores](https://pinia.vuejs.org/core-concepts/)
- [Setup Stores](https://pinia.vuejs.org/core-concepts/#setup-stores)

---

## 3. State

State is declared using `ref()` (for single values) or `reactive()` (for objects). It holds the source-of-truth data.

### Auth store state

**Source**: `vue-student-dashboard/src/stores/auth.ts`

```typescript
export const useAuthStore = defineStore('auth', () => {
  const currentStudent = ref<Student | null>(null)

  // ...
  return { currentStudent, /* ... */ }
})
```

A single `ref` holds the current student or `null` when not authenticated.

### Course store state

**Source**: `vue-student-dashboard/src/stores/course.ts`

```typescript
export const useCourseStore = defineStore('course', () => {
  const courses = ref<Course[]>(MOCK_COURSES)
  const enrollments = ref<Enrollment[]>([])

  // ...
  return { courses, enrollments, /* ... */ }
})
```

Two `ref` values: the course catalog and the enrollment records. Both are arrays that can be mutated directly (Vue's reactivity system tracks array mutations through the Proxy).

### Direct mutation

Unlike Angular signals (which use immutable `signal.update()` patterns), Pinia allows direct mutation of state:

```typescript
// Direct array push — Vue detects it via Proxy
enrollments.value.push({
  courseId,
  studentId,
  enrolledAt: new Date(),
  status,
})

// Direct property mutation on an object inside a ref array
course.enrolledCount++
```

This works because `ref()` wraps the value in a Proxy that intercepts all property access and mutations.

### `$patch()` for batched mutations

For multiple mutations that should trigger only one re-render:

```typescript
// Object syntax — merges partial state
store.$patch({
  courses: updatedCourses,
  enrollments: updatedEnrollments,
})

// Function syntax — access current state
store.$patch((state) => {
  state.courses.push(newCourse)
  state.enrollments.push(newEnrollment)
})
```

**References**:
- [Pinia State](https://pinia.vuejs.org/core-concepts/state.html)

---

## 4. Getters (Computed)

Getters are reactive derived values — they recalculate automatically when their dependencies change. In setup stores, they are simply `computed()` properties.

**Source**: `vue-student-dashboard/src/stores/course.ts`

```typescript
const enrolledCourses = computed(() => {
  const activeEnrollments = enrollments.value.filter((e) => e.status === 'active')
  return courses.value.filter((c) => activeEnrollments.some((e) => e.courseId === c.id))
})

const availableCourses = computed(() => {
  const enrolledIds = new Set(
    enrollments.value.filter((e) => e.status === 'active').map((e) => e.courseId),
  )
  return courses.value.filter((c) => !enrolledIds.has(c.id))
})

const totalCredits = computed(() =>
  enrolledCourses.value.reduce((sum, c) => sum + c.credits, 0),
)
```

**Source**: `vue-student-dashboard/src/stores/auth.ts`

```typescript
const isAuthenticated = computed(() => currentStudent.value !== null)
const student = computed(() => currentStudent.value)
```

### Getter dependency chain

Getters can depend on other getters. Vue tracks the full dependency graph:

```
courses + enrollments
    ↓
enrolledCourses ──→ totalCredits
    ↓
availableCourses
```

When `enrollments` changes (e.g., a student enrolls), `enrolledCourses`, `availableCourses`, and `totalCredits` all recalculate automatically. Components that read any of these values re-render.

### Getter caching

`computed()` values are cached — they only re-evaluate when a tracked dependency changes. Reading `totalCredits` ten times in a row returns the cached value without re-running the reduce.

**References**:
- [Pinia Getters](https://pinia.vuejs.org/core-concepts/getters.html)

---

## 5. Actions (Methods)

Actions are plain functions that modify state. They can contain any logic: conditionals, loops, async operations.

### Synchronous actions

**Source**: `vue-student-dashboard/src/stores/course.ts`

```typescript
function enroll(courseId: string, studentId: string): boolean {
  const course = getCourseById(courseId)
  if (!course) return false

  const isFull = course.enrolledCount >= course.maxStudents
  const status: EnrollmentStatus = isFull ? 'waitlisted' : 'active'

  enrollments.value.push({
    courseId,
    studentId,
    enrolledAt: new Date(),
    status,
  })

  if (!isFull) {
    course.enrolledCount++
  }

  return true
}

function drop(courseId: string, studentId: string): void {
  const enrollment = enrollments.value.find(
    (e) => e.courseId === courseId && e.studentId === studentId,
  )

  if (enrollment?.status === 'active') {
    const course = getCourseById(courseId)
    if (course) {
      course.enrolledCount--
    }
  }

  if (enrollment) {
    enrollment.status = 'dropped'
  }
}
```

**Source**: `vue-student-dashboard/src/stores/auth.ts`

```typescript
function login(email: string, _password: string): boolean {
  if (email && _password) {
    currentStudent.value = {
      id: 'student-1',
      name: 'Alice Johnson',
      email,
      gpa: 3.7,
      enrollmentYear: 2023,
      major: 'Computer Science',
    }
    return true
  }
  return false
}

function logout(): void {
  currentStudent.value = null
}
```

### Async actions

Actions can be `async` for API calls:

```typescript
async function fetchCourses(): Promise<void> {
  loading.value = true
  try {
    const response = await fetch('/api/courses')
    courses.value = await response.json()
  } catch (error) {
    errorMessage.value = 'Failed to load courses'
  } finally {
    loading.value = false
  }
}
```

### Helper functions (non-exported)

Functions used internally by actions but not returned from the store setup function act as private helpers:

```typescript
export const useCourseStore = defineStore('course', () => {
  // Public getter — returned and accessible outside
  function getCourseById(id: string): Course | undefined {
    return courses.value.find((c) => c.id === id)
  }

  // Used by enroll() and drop() internally AND also returned publicly
  return { getCourseById, enroll, drop, /* ... */ }
})
```

**References**:
- [Pinia Actions](https://pinia.vuejs.org/core-concepts/actions.html)

---

## 6. Using Stores in Components

### Accessing a store

Call the store's composable function inside `<script setup>`:

**Source**: `vue-student-dashboard/src/views/DashboardView.vue`

```typescript
import { useCourseStore } from '@/stores/course'
import { useAuthStore } from '@/stores/auth'

const courseStore = useCourseStore()
const authStore = useAuthStore()
```

### Reading state and getters

Access them as regular properties — no `.value` needed in templates:

```typescript
// In script — .value is auto-unwrapped by Pinia
const credits = courseStore.totalCredits     // number (not Ref<number>)
const student = authStore.student            // Student | null
```

```html
<!-- In template — direct property access -->
<h2>Enrolled Courses ({{ courseStore.totalCredits }} credits)</h2>

<CourseCard
  v-for="course in courseStore.enrolledCourses"
  :key="course.id"
  :course="course"
/>
```

### Calling actions

```typescript
function onDropCourse(courseId: string): void {
  if (authStore.student) {
    courseStore.drop(courseId, authStore.student.id)
  }
}

function onLogout(): void {
  authStore.logout()
  router.push({ name: 'login' })
}
```

### Destructuring stores

If you destructure a store, reactive properties lose their reactivity:

```typescript
// WRONG — loses reactivity
const { totalCredits } = useCourseStore()
// totalCredits is now a plain number, won't update

// CORRECT — use storeToRefs() for reactive destructuring
import { storeToRefs } from 'pinia'

const courseStore = useCourseStore()
const { totalCredits, enrolledCourses } = storeToRefs(courseStore)
// These are now Ref<> objects that stay reactive

// Actions can be destructured directly (they're not reactive)
const { enroll, drop } = courseStore
```

**References**:
- [Using Stores](https://pinia.vuejs.org/core-concepts/#using-the-store)
- [storeToRefs](https://pinia.vuejs.org/api/modules/pinia.html#storetorefs)

---

## 7. Using Stores in Guards

Stores are also accessible in navigation guards. The Pinia instance must be installed before the guard runs (which it is, since `app.use(createPinia())` happens before `app.use(router)` in `main.ts`).

**Source**: `vue-student-dashboard/src/guards/auth.guard.ts`

```typescript
import { useAuthStore } from '@/stores/auth'

export const authGuard: NavigationGuardWithThis<undefined> = (to, from) => {
  const authStore = useAuthStore()    // Access the singleton store

  if (authStore.isAuthenticated) {
    return true
  }
  return { name: 'login' }
}
```

**Source**: `vue-student-dashboard/src/guards/enrollment.guard.ts`

```typescript
import { useCourseStore } from '@/stores/course'

export const enrollmentGuard: NavigationGuardWithThis<undefined> = (to, from) => {
  const courseStore = useCourseStore()

  if (courseStore.totalCredits < 18) {
    return true
  }
  return { name: 'dashboard', query: { maxCredits: 'true' } }
}
```

This demonstrates that Pinia stores are not limited to components — they can be used anywhere in the application where the Pinia instance is available. See [Routing & Navigation Guards](./03-vue-routing-and-guards.md) for full guard documentation.

---

## 8. Store Design Patterns

### One store per domain

Our application separates concerns into domain-specific stores:

| Store | Domain | State | Getters | Actions |
|-------|--------|-------|---------|---------|
| `useAuthStore` | Authentication | `currentStudent` | `isAuthenticated`, `student` | `login`, `logout` |
| `useCourseStore` | Course catalog + enrollment | `courses`, `enrollments` | `enrolledCourses`, `availableCourses`, `totalCredits` | `enroll`, `drop`, `getCourseById`, `getEnrollmentStatus` |

### Stores calling other stores

If needed, stores can use each other:

```typescript
export const useEnrollmentStore = defineStore('enrollment', () => {
  const authStore = useAuthStore()   // Access another store

  function enrollInCourse(courseId: string) {
    if (authStore.student) {
      // ... use authStore.student.id
    }
  }

  return { enrollInCourse }
})
```

### When to use stores vs local state

| Scenario | Solution |
|----------|----------|
| Data shared across multiple components | Pinia store |
| Data needed by navigation guards | Pinia store |
| Form input values local to one page | `ref()` in the component |
| Computed value derived from props | `computed()` in the component |
| UI state (dropdowns, modals) in one component | `ref()` in the component |

**Source** — local state example: `vue-student-dashboard/src/views/EnrollmentView.vue`

```typescript
// Local to this component — not in a store
const searchTerm = ref('')
const selectedCategory = ref<CourseCategory | 'all'>('all')
```

These values don't need to persist across navigation or be shared with other components, so they stay local.

**References**:
- [Pinia Best Practices](https://pinia.vuejs.org/cookbook/)
- [Pinia Plugins](https://pinia.vuejs.org/core-concepts/plugins.html)
- [Pinia with Vue DevTools](https://pinia.vuejs.org/core-concepts/state.html#usage-with-the-options-api)

---

## 9. Angular Comparison

Pinia stores occupy the same architectural role as Angular's `@Injectable({ providedIn: 'root' })` services — both are singleton containers for shared state, derived data, and business logic. The creation pattern is similar: Angular declares a class with `@Injectable`, Vue calls `defineStore()` with a setup function. State primitives map one-to-one: Angular's `signal()` corresponds to Pinia's `ref()`, and both frameworks use `computed()` for derived values. The primary structural difference is that Angular services are classes with fields and methods, instantiated and managed by the DI container, while Pinia stores are function closures where state, getters, and actions are declared as local variables and functions, then returned as a plain object. Both are accessed via a factory call in components (`inject(ServiceClass)` vs `useMyStore()`).

The mutation model is the most practically significant difference. Angular signals enforce immutability — you must call `signal.update(fn)` and return a new value, which pushes developers toward immutable patterns like `list.map(...)` to produce new arrays. Pinia allows direct mutation: `array.push()`, `object.property++`, and in-place modifications all work because Vue's Proxy-based reactivity intercepts them. This makes Pinia code more concise for complex state updates (our `enroll()` action directly pushes to an array and increments a counter) but trades away the explicitness of immutable updates. Angular developers used to `signal.update()` should understand that in Vue, reactivity tracking is automatic — you don't need to signal the framework that a change occurred; the Proxy handles it.

**References**:
- [Angular Dependency Injection](https://angular.dev/guide/di)
- [Angular Signals](https://angular.dev/guide/signals)
- [Pinia vs Vuex Migration](https://pinia.vuejs.org/cookbook/migration-vuex.html)

---

**Previous**: [Routing & Navigation Guards](./03-vue-routing-and-guards.md)
**Next**: [Data Transforms & Composables](./05-vue-composables-and-transforms.md)
