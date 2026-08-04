# Vue.js Data Transforms & Composables

> **Series**: Vue.js Comprehensive Guide (5 of 5)
> **Audience**: Developers with Angular experience transitioning to Vue.js
> **Code context**: Student Dashboard + Class Enrollment application

### Document Navigation

| # | Document | Focus |
|---|----------|-------|
| 1 | [Core Fundamentals](./01-vue-core-fundamentals.md) | Reactivity, templates, single-file components |
| 2 | [Component Patterns](./02-vue-component-patterns.md) | Props, emits, lifecycle hooks, component design |
| 3 | [Routing & Navigation Guards](./03-vue-routing-and-guards.md) | vue-router, lazy loading, route guards |
| 4 | [State Management with Pinia](./04-vue-state-management.md) | Stores, reactive state, actions, getters |
| **5** | **Data Transforms & Composables** (this document) | Composables, formatting, reusable logic |

See also: [Angular vs Vue.js Side-by-Side Comparison](./COMPARISON.md)

---

## Table of Contents

1. [What are Composables](#1-what-are-composables)
2. [Building a Formatter Composable](#2-building-a-formatter-composable)
3. [Using Composables in Components](#3-using-composables-in-components)
4. [Composable vs Pipe — The Mental Model](#4-composable-vs-pipe--the-mental-model)
5. [Advanced Composable Patterns](#5-advanced-composable-patterns)
6. [When to Use What](#6-when-to-use-what)
7. [Angular Comparison](#7-angular-comparison)

---

## 1. What are Composables

A composable is a function that encapsulates and reuses stateful or stateless logic using Vue's Composition API. The convention is to name them with a `use` prefix: `useFormatters`, `useMouse`, `useFetch`.

Composables serve multiple purposes in Vue:
- **Data formatting** (replacing Angular pipes)
- **Reusable reactive state** (shared timers, form validation)
- **Abstracting external integrations** (localStorage, WebSocket, API calls)
- **Extracting component logic** (keeping components lean)

Composables are not a framework feature — they're a convention built on top of Vue's reactivity system. Any function that uses `ref()`, `computed()`, `watch()`, or lifecycle hooks qualifies.

**References**:
- [Vue.js Composables](https://vuejs.org/guide/reusability/composables.html)
- [Composition API FAQ](https://vuejs.org/guide/extras/composition-api-faq.html)

---

## 2. Building a Formatter Composable

Our application needs three data transforms for displaying course information. In Angular, each would be a separate `@Pipe` class. In Vue, they're grouped into a single composable.

**Source**: `vue-student-dashboard/src/composables/useFormatters.ts`

```typescript
import type { CourseCategory } from '@/models/course.model'
import type { Course } from '@/models/course.model'

export function useFormatters() {
  const categoryLabels: Record<CourseCategory, string> = {
    math: 'Mathematics',
    science: 'Natural Sciences',
    humanities: 'Humanities',
    engineering: 'Engineering & CS',
    arts: 'Fine Arts',
  }

  /** Maps a category key to a human-readable label */
  function formatCategoryLabel(category: CourseCategory): string {
    return categoryLabels[category] ?? category
  }

  /** Formats credit count with proper singular/plural */
  function formatCredits(value: number): string {
    return `${value} Credit${value !== 1 ? 's' : ''}`
  }

  /** Calculates enrollment percentage as a display string */
  function formatEnrollmentPercent(course: Course): string {
    if (!course || course.maxStudents === 0) return '0%'
    const percent = Math.round((course.enrolledCount / course.maxStudents) * 100)
    return `${percent}%`
  }

  return {
    formatCategoryLabel,
    formatCredits,
    formatEnrollmentPercent,
  }
}
```

### Anatomy of this composable

| Part | Purpose |
|------|---------|
| `export function useFormatters()` | Factory function — called per component that needs it |
| `categoryLabels` | Lookup data — created once per call, private to the closure |
| `formatCategoryLabel()` | Pure transform function — no side effects, no state |
| `return { ... }` | Public API — only returned functions are accessible |

### Why a function wrapper?

Even though these are stateless pure functions, wrapping them in `useFormatters()` provides:
1. **Consistent API** with other composables that *do* hold state
2. **Namespace grouping** — related functions returned as a single object
3. **Future extensibility** — if a formatter later needs reactive state (e.g., locale), the API doesn't change

For truly stateless utilities, you *could* also export bare functions. Both approaches are valid:

```typescript
// Approach A: Composable (convention for Vue-specific code)
export function useFormatters() {
  function formatCredits(v: number) { return `${v} Credits` }
  return { formatCredits }
}

// Approach B: Plain exports (fine for pure utilities)
export function formatCredits(v: number) { return `${v} Credits` }
```

Our application uses Approach A to establish the composable pattern consistently.

---

## 3. Using Composables in Components

### Import and destructure

**Source**: `vue-student-dashboard/src/components/CourseCard.vue`

```typescript
<script setup lang="ts">
import { useFormatters } from '@/composables/useFormatters'

const { formatCategoryLabel, formatCredits, formatEnrollmentPercent } = useFormatters()
</script>
```

### Call in templates

The destructured functions are directly available in the template:

```html
<template>
  <div class="card-header">
    <span class="category-badge">{{ formatCategoryLabel(course.category) }}</span>
    <span class="credits">{{ formatCredits(course.credits) }}</span>
  </div>

  <div class="enrollment-bar">
    <div class="enrollment-fill" :style="{ width: formatEnrollmentPercent(course) }"></div>
    <span class="enrollment-text">
      {{ course.enrolledCount }}/{{ course.maxStudents }} enrolled
      ({{ formatEnrollmentPercent(course) }})
    </span>
  </div>
</template>
```

### Composable used in the enrollment filter bar

**Source**: `vue-student-dashboard/src/views/EnrollmentView.vue`

```typescript
const { formatCategoryLabel } = useFormatters()    // Only need one function here
```

```html
<button
  v-for="cat in categories"
  :key="cat"
  class="filter-btn"
  :class="{ active: selectedCategory === cat }"
  @click="selectedCategory = cat"
>
  {{ cat === 'all' ? 'All' : formatCategoryLabel(cat) }}
</button>
```

### Composable used in the course detail page

**Source**: `vue-student-dashboard/src/views/CourseDetailView.vue`

```typescript
const { formatCategoryLabel, formatCredits, formatEnrollmentPercent } = useFormatters()
```

```html
<span class="category-badge">{{ formatCategoryLabel(course.category) }}</span>
<span class="credits">{{ formatCredits(course.credits) }}</span>
<div class="enrollment-fill" :style="{ width: formatEnrollmentPercent(course) }"></div>

<li v-for="prereq in prerequisites" :key="prereq.id">
  {{ prereq.name }} ({{ formatCredits(prereq.credits) }})
</li>
```

---

## 4. Composable vs Pipe — The Mental Model

For Angular developers, the key conceptual mapping is:

```
Angular Pipe                    →    Vue Composable Function
────────────────────────────         ────────────────────────────
@Pipe({ name: 'creditFormat' })      export function useFormatters() {
class CreditFormatPipe {               function formatCredits(v) {
  transform(value: number) {             return `${v} Credits`
    return `${value} Credits`           }
  }                                    return { formatCredits }
}                                    }

Template usage:                      Template usage:
{{ credits | creditFormat }}         {{ formatCredits(credits) }}
```

### Mapping each Angular pipe to its Vue equivalent

| Angular Pipe | Template syntax | Vue Function | Template syntax |
|-------------|----------------|--------------|----------------|
| `CategoryLabelPipe` | `{{ cat \| categoryLabel }}` | `formatCategoryLabel()` | `{{ formatCategoryLabel(cat) }}` |
| `CreditFormatPipe` | `{{ n \| creditFormat }}` | `formatCredits()` | `{{ formatCredits(n) }}` |
| `EnrollmentPercentPipe` | `{{ course \| enrollmentPercent }}` | `formatEnrollmentPercent()` | `{{ formatEnrollmentPercent(course) }}` |

### What about pipe chaining?

Angular:
```html
{{ value | pipe1 | pipe2 | pipe3 }}
```

Vue (function composition):
```html
{{ pipe3(pipe2(pipe1(value))) }}
```

Or with a computed property for readability:
```typescript
const formatted = computed(() => pipe3(pipe2(pipe1(value.value))))
```

### What about pipe purity / caching?

Angular pipes are `pure: true` by default — Angular caches the result and only re-runs `transform()` when the input reference changes. Vue composable functions execute on every render unless you wrap the call in `computed()`:

```typescript
// Runs on every render (fine for cheap transforms)
<span>{{ formatCredits(course.credits) }}</span>

// Cached — only re-evaluates when course.credits changes
const formattedCredits = computed(() => formatCredits(props.course.credits))
<span>{{ formattedCredits }}</span>
```

For simple string transforms like ours, the overhead is negligible and `computed()` wrapping is unnecessary. Reserve it for expensive operations.

---

## 5. Advanced Composable Patterns

While `useFormatters` is a stateless composable, composables can also hold reactive state, making them much more powerful than pipes.

### Stateful composable: `useDebounce`

```typescript
import { ref, watch } from 'vue'
import type { Ref } from 'vue'

export function useDebounce<T>(source: Ref<T>, delay: number = 300): Ref<T> {
  const debounced = ref(source.value) as Ref<T>
  let timeout: ReturnType<typeof setTimeout>

  watch(source, (newValue) => {
    clearTimeout(timeout)
    timeout = setTimeout(() => {
      debounced.value = newValue
    }, delay)
  })

  return debounced
}
```

Usage:
```typescript
const searchTerm = ref('')
const debouncedSearch = useDebounce(searchTerm, 300)

// debouncedSearch updates 300ms after searchTerm stops changing
```

### Composable with lifecycle hooks: `useWindowSize`

```typescript
import { ref, onMounted, onUnmounted } from 'vue'

export function useWindowSize() {
  const width = ref(window.innerWidth)
  const height = ref(window.innerHeight)

  function update() {
    width.value = window.innerWidth
    height.value = window.innerHeight
  }

  onMounted(() => window.addEventListener('resize', update))
  onUnmounted(() => window.removeEventListener('resize', update))

  return { width, height }
}
```

### Composable accessing stores

```typescript
import { computed } from 'vue'
import { useCourseStore } from '@/stores/course'
import { useAuthStore } from '@/stores/auth'

export function useEnrollmentInfo(courseId: string) {
  const courseStore = useCourseStore()
  const authStore = useAuthStore()

  const status = computed(() => {
    if (!authStore.student) return null
    return courseStore.getEnrollmentStatus(courseId, authStore.student.id)
  })

  const isEnrolled = computed(() => status.value === 'active')

  return { status, isEnrolled }
}
```

### Key principle

Any logic that uses Vue's reactivity primitives (`ref`, `computed`, `watch`) or lifecycle hooks and is needed in multiple places should be extracted into a composable. This keeps components focused on wiring UI to data.

**References**:
- [Vue.js Composables](https://vuejs.org/guide/reusability/composables.html)
- [VueUse — Collection of Composables](https://vueuse.org/)

---

## 6. When to Use What

| Need | Vue Solution | File location |
|------|-------------|---------------|
| Format data for display | Composable with pure functions | `composables/useFormatters.ts` |
| Shared application state | Pinia store | `stores/course.ts` |
| Reusable reactive logic (debounce, polling) | Stateful composable | `composables/useDebounce.ts` |
| Component-local state | `ref()` / `reactive()` in `<script setup>` | Inside the component |
| Component-local derived value | `computed()` in `<script setup>` | Inside the component |
| Side effect on data change | `watch()` / `watchEffect()` | Inside composable or component |

### Composables vs Pinia stores

| Aspect | Composable | Pinia Store |
|--------|-----------|-------------|
| Instance | New instance per component call (unless using shared refs) | Singleton (one instance per store ID) |
| Scope | Scoped to the calling component's lifetime | Application-wide, persists across navigations |
| DevTools | Not visible in Vue DevTools | Full Pinia DevTools integration (state inspection, time-travel) |
| Best for | Stateless transforms, component-scoped reactive logic | Shared application state, cross-component data |

---

## 7. Angular Comparison

Angular pipes and Vue composables solve the same problem — transforming data for display — but differ fundamentally in their relationship to the framework. Angular pipes are a first-class framework concept: `@Pipe` is a decorator recognized by the compiler, the `|` operator is built into the template parser, and the framework manages pipe purity and caching automatically. Vue composables are a community convention built on standard JavaScript functions and Vue's reactivity primitives; there is no `@Composable` decorator, no template-level pipe operator, and no automatic caching. This means Angular pipes are more concise in templates (`{{ value | transform }}` vs `{{ transform(value) }}`), especially when chaining multiple transforms, while Vue composables are more flexible because they can hold state, use lifecycle hooks, and access stores — capabilities that would require injecting services into an Angular pipe.

The practical impact is that Angular developers will write more code per transform in Vue (importing, destructuring, calling functions explicitly) but gain more versatility. A Vue composable like `useDebounce` or `useWindowSize` combines reactive state, watchers, and lifecycle cleanup in a single reusable function — something that in Angular would require a separate service plus manual subscription management. The trade-off is explicit: Angular pipes are optimized for the specific use case of display transforms with built-in memoisation, while Vue composables are a general-purpose abstraction that covers display transforms *and* much more, at the cost of requiring `computed()` wrappers when caching matters.

**References**:
- [Angular Pipes](https://angular.dev/guide/pipes)
- [Angular Custom Pipes](https://angular.dev/guide/pipes/custom-pipes)
- [Vue.js Composables](https://vuejs.org/guide/reusability/composables.html)
- [VueUse Library](https://vueuse.org/) — Production-ready composable collection with 200+ functions

---

**Previous**: [State Management with Pinia](./04-vue-state-management.md)

---

## Series Summary

This five-document series covered:

1. **[Core Fundamentals](./01-vue-core-fundamentals.md)** — Bootstrap, SFCs, reactivity (`ref`, `computed`, `watch`), template syntax
2. **[Component Patterns](./02-vue-component-patterns.md)** — Props (`defineProps`), emits (`defineEmits`), lifecycle hooks, smart vs presentational
3. **[Routing & Navigation Guards](./03-vue-routing-and-guards.md)** — `vue-router`, lazy loading, route params, per-route and global guards
4. **[State Management with Pinia](./04-vue-state-management.md)** — Stores, reactive state, getters, actions, usage in components and guards
5. **[Data Transforms & Composables](./05-vue-composables-and-transforms.md)** — Formatter composables, stateful composables, composable vs pipe model

For a feature-by-feature comparison with Angular code samples: [Angular vs Vue.js Side-by-Side Comparison](./COMPARISON.md)
