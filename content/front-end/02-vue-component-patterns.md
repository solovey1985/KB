# Vue.js Component Patterns

> **Series**: Vue.js Comprehensive Guide (2 of 5)
> **Audience**: Developers with Angular experience transitioning to Vue.js
> **Code context**: Student Dashboard + Class Enrollment application

### Document Navigation

| # | Document | Focus |
|---|----------|-------|
| 1 | [Core Fundamentals](./01-vue-core-fundamentals.md) | Reactivity, templates, single-file components |
| **2** | **Component Patterns** (this document) | Props, emits, lifecycle hooks, component design |
| 3 | [Routing & Navigation Guards](./03-vue-routing-and-guards.md) | vue-router, lazy loading, route guards |
| 4 | [State Management with Pinia](./04-vue-state-management.md) | Stores, reactive state, actions, getters |
| 5 | [Data Transforms & Composables](./05-vue-composables-and-transforms.md) | Composables, formatting, reusable logic |

See also: [Angular vs Vue.js Side-by-Side Comparison](./COMPARISON.md)

---

## Table of Contents

1. [Component Anatomy](#1-component-anatomy)
2. [Props — Receiving Data from Parent](#2-props--receiving-data-from-parent)
3. [Emits — Sending Events to Parent](#3-emits--sending-events-to-parent)
4. [Parent-Child Wiring in Templates](#4-parent-child-wiring-in-templates)
5. [Multi-Level Component Hierarchy](#5-multi-level-component-hierarchy)
6. [Lifecycle Hooks](#6-lifecycle-hooks)
7. [Smart vs Presentational Components](#7-smart-vs-presentational-components)
8. [Angular Comparison](#8-angular-comparison)

---

## 1. Component Anatomy

Every Vue component in `<script setup>` follows a consistent structure:

```
1. Imports (vue, router, stores, child components, composables, types)
2. Props declaration (defineProps)
3. Emits declaration (defineEmits)
4. Composables (useFormatters, useRouter, etc.)
5. Local reactive state (ref, reactive)
6. Computed properties (computed)
7. Lifecycle hooks (onMounted, onUnmounted, etc.)
8. Methods / event handlers (plain functions)
```

**Source**: `vue-student-dashboard/src/components/CourseCard.vue`

```typescript
<script setup lang="ts">
// 1. Imports
import { computed, onMounted, onUpdated, onBeforeUnmount, onUnmounted } from 'vue'
import type { Course } from '@/models/course.model'
import { useFormatters } from '@/composables/useFormatters'
import EnrollmentStatus from './EnrollmentStatus.vue'

// 2. Props
const props = defineProps<{
  course: Course
  isEnrolled: boolean
  enrollmentStatus: string | null
}>()

// 3. Emits
const emit = defineEmits<{
  enrollClicked: [courseId: string]
  dropClicked: [courseId: string]
  viewDetails: [courseId: string]
}>()

// 4. Composables
const { formatCategoryLabel, formatCredits, formatEnrollmentPercent } = useFormatters()

// 5. (No local ref state in this component)

// 6. Computed
const isFull = computed(() => props.course.enrolledCount >= props.course.maxStudents)
const canEnroll = computed(() => !props.isEnrolled && !isFull.value)

// 7. Lifecycle hooks
onMounted(() => {
  console.log(`[CourseCard] onMounted – "${props.course.name}"`)
})
onUpdated(() => {
  console.log(`[CourseCard] onUpdated – "${props.course.name}"`)
})
onBeforeUnmount(() => {
  console.log(`[CourseCard] onBeforeUnmount – "${props.course.name}"`)
})
onUnmounted(() => {
  console.log(`[CourseCard] onUnmounted – "${props.course.name}"`)
})

// 8. (Event handlers use emit directly in template for this component)
</script>
```

This ordering is not enforced by Vue but is a widely adopted convention that improves readability.

---

## 2. Props — Receiving Data from Parent

Props are the mechanism for passing data **downward** from a parent component to a child. In Vue 3 with `<script setup>`, props are declared using the `defineProps()` compiler macro.

### Type-only declaration (recommended)

```typescript
const props = defineProps<{
  course: Course          // Required — no ? mark
  isEnrolled: boolean     // Required
  enrollmentStatus: string | null   // Required (but nullable)
}>()
```

All props are **required by default** in the type-only form. Mark optional props with `?`:

```typescript
const props = defineProps<{
  course: Course
  isEnrolled?: boolean        // Optional
  enrollmentStatus?: string | null
}>()
```

### Adding defaults with `withDefaults()`

```typescript
const props = withDefaults(
  defineProps<{
    course: Course
    isEnrolled?: boolean
    enrollmentStatus?: string | null
  }>(),
  {
    isEnrolled: false,
    enrollmentStatus: null,
  }
)
```

### Accessing props

In script: access through the `props` object (or destructure — but destructured props lose reactivity unless using `toRefs()`):

```typescript
// Correct — reactive
console.log(props.course.name)
const isFull = computed(() => props.course.enrolledCount >= props.course.maxStudents)

// Loses reactivity on destructured primitives:
// const { isEnrolled } = props  // isEnrolled won't update on changes
```

In templates: props are auto-unwrapped — use them directly by name without `props.`:

```html
<template>
  <h3>{{ course.name }}</h3>                <!-- Direct access, no "props." -->
  <span>{{ formatCredits(course.credits) }}</span>
  <button v-if="!isEnrolled">Enroll</button>
</template>
```

### Prop naming convention

Props defined in camelCase in script are used as kebab-case in the parent template:

```typescript
// Child defines:
defineProps<{ isEnrolled: boolean }>()

// Parent uses:
<CourseCard :is-enrolled="true" />   <!-- kebab-case in template -->
```

Vue automatically converts between the two forms.

**Source**: `vue-student-dashboard/src/components/StudentProfile.vue`

```typescript
const props = defineProps<{
  student: Student
  totalCredits: number
}>()
```

**References**:
- [Vue.js Props](https://vuejs.org/guide/components/props.html)
- [defineProps API](https://vuejs.org/api/sfc-script-setup.html#defineprops-defineemits)

---

## 3. Emits — Sending Events to Parent

Events flow **upward** from child to parent. Declared with `defineEmits()`:

### Type-safe emit declaration

```typescript
const emit = defineEmits<{
  enrollClicked: [courseId: string]        // Event name + payload types
  dropClicked: [courseId: string]
  viewDetails: [courseId: string]
  logoutClicked: []                        // No payload
}>()
```

The generic type defines the event name as key and the payload as a tuple of argument types.

### Emitting events

In script:
```typescript
emit('enrollClicked', props.course.id)
```

In template (inline):
```html
<button @click="emit('enrollClicked', course.id)">Enroll</button>
<button @click="emit('logoutClicked')">Logout</button>
```

### Listening in the parent

The parent uses `@event-name` (kebab-case) to listen:

```html
<CourseCard
  :course="course"
  @enroll-clicked="onEnroll"       <!-- Receives courseId as argument -->
  @drop-clicked="onDropCourse"
  @view-details="onViewDetails"
/>
```

The handler function receives the emitted payload as its argument:

```typescript
function onEnroll(courseId: string): void {
  courseStore.enroll(courseId, authStore.student.id)
}
```

**Source**: `vue-student-dashboard/src/components/StudentProfile.vue`

```typescript
const emit = defineEmits<{
  logoutClicked: []
}>()
```
```html
<button class="btn-logout" @click="emit('logoutClicked')">Logout</button>
```

**Parent** (`DashboardView.vue`):
```html
<StudentProfile
  v-if="authStore.student"
  :student="authStore.student"
  :total-credits="courseStore.totalCredits"
  @logout-clicked="onLogout"
/>
```

**References**:
- [Vue.js Component Events](https://vuejs.org/guide/components/events.html)
- [defineEmits API](https://vuejs.org/api/sfc-script-setup.html#defineprops-defineemits)

---

## 4. Parent-Child Wiring in Templates

Here is the complete wiring from the dashboard page to the course card child component:

**Parent** — `vue-student-dashboard/src/views/DashboardView.vue`

```html
<CourseCard
  v-for="course in courseStore.enrolledCourses"
  :key="course.id"
  :course="course"
  :is-enrolled="true"
  :enrollment-status="getEnrollmentStatus(course.id)"
  @drop-clicked="onDropCourse"
  @view-details="onViewCourseDetails"
/>
```

Breaking this down:

| Attribute | Direction | What it does |
|-----------|-----------|-------------|
| `v-for="course in ..."` | — | Renders one CourseCard per enrolled course |
| `:key="course.id"` | — | Identity hint for Vue's diffing algorithm |
| `:course="course"` | Parent → Child | Passes course object as prop |
| `:is-enrolled="true"` | Parent → Child | Passes boolean literal as prop |
| `:enrollment-status="..."` | Parent → Child | Passes computed status string as prop |
| `@drop-clicked="onDropCourse"` | Child → Parent | Listens for drop event, calls handler |
| `@view-details="onViewCourseDetails"` | Child → Parent | Listens for details event, calls handler |

**Child handlers** in the parent script:

```typescript
function onViewCourseDetails(courseId: string): void {
  router.push({ name: 'courseDetail', params: { id: courseId } })
}

function onDropCourse(courseId: string): void {
  if (authStore.student) {
    courseStore.drop(courseId, authStore.student.id)
  }
}
```

---

## 5. Multi-Level Component Hierarchy

The application demonstrates three-level prop drilling:

```
DashboardView (grandparent)
  └─ CourseCard (child)
       └─ EnrollmentStatus (grandchild)
```

**Level 1**: Dashboard passes data to CourseCard:
```html
<CourseCard :course="course" :enrollment-status="getEnrollmentStatus(course.id)" />
```

**Level 2**: CourseCard computes derived values and passes them further to EnrollmentStatus:

**Source**: `vue-student-dashboard/src/components/CourseCard.vue` (template)

```html
<EnrollmentStatus :status="enrollmentStatus" :is-full="isFull" />
```

**Level 3**: EnrollmentStatus receives and displays:

**Source**: `vue-student-dashboard/src/components/EnrollmentStatus.vue`

```typescript
const props = defineProps<{
  status: string | null
  isFull: boolean
}>()

const label = computed(() => {
  if (props.status === 'active') return 'Enrolled'
  if (props.status === 'waitlisted') return 'Waitlisted'
  if (props.status === 'dropped') return 'Dropped'
  if (props.isFull) return 'Full'
  return 'Available'
})

const badgeClass = computed(() => {
  if (props.status === 'active') return 'status-enrolled'
  if (props.status === 'waitlisted') return 'status-waitlisted'
  if (props.status === 'dropped') return 'status-dropped'
  if (props.isFull) return 'status-full'
  return 'status-available'
})
```

```html
<template>
  <span class="status-badge" :class="badgeClass">{{ label }}</span>
</template>
```

Events flow in the opposite direction — only the parent that cares about the event listens for it. The grandchild (`EnrollmentStatus`) is purely presentational and emits no events.

### When prop drilling becomes unwieldy

For deeply nested trees, Vue provides two escape hatches:
- **`provide` / `inject`** — Ancestor provides data, any descendant injects it (no intermediate pass-through needed). Similar to Angular's hierarchical DI.
- **Pinia stores** — Global shared state accessible from any component. See [State Management with Pinia](./04-vue-state-management.md).

**References**:
- [Vue.js provide / inject](https://vuejs.org/guide/components/provide-inject.html)

---

## 6. Lifecycle Hooks

Vue lifecycle hooks are standalone functions called inside `<script setup>`. They register callbacks for specific moments in a component's existence.

### Complete lifecycle sequence

```
<script setup> body executes          ← Component instance created
        │
  onBeforeMount()                     ← Before first render
        │
  onMounted()                         ← DOM available, children mounted
        │
  ┌── onBeforeUpdate()                ← Before reactive re-render
  │       │
  └── onUpdated()                     ← After reactive re-render
        │                               (loops on each state change)
  onBeforeUnmount()                   ← Before removal from DOM
        │
  onUnmounted()                       ← Removed, fully cleaned up
```

### Hooks demonstrated in our application

**Source**: `vue-student-dashboard/src/components/CourseCard.vue`

```typescript
onMounted(() => {
  // Called once when component is inserted into the DOM.
  // Safe to access DOM elements, start timers, fetch data.
  console.log(`[CourseCard] onMounted – rendering "${props.course.name}"`)
})

onUpdated(() => {
  // Called after every re-render caused by reactive state changes.
  // The DOM has been updated to reflect the new state.
  console.log(`[CourseCard] onUpdated – "${props.course.name}"`)
})

onBeforeUnmount(() => {
  // Called before the component is removed from the DOM.
  // Use for cleanup: cancel timers, unsubscribe from events, close WebSockets.
  console.log(`[CourseCard] onBeforeUnmount – "${props.course.name}"`)
})

onUnmounted(() => {
  // Called after the component has been fully removed.
  // All reactive effects have been stopped, child components unmounted.
  console.log(`[CourseCard] onUnmounted – "${props.course.name}"`)
})
```

**Source**: `vue-student-dashboard/src/views/DashboardView.vue`

```typescript
onMounted(() => {
  console.log('[Dashboard] onMounted')
})

onUnmounted(() => {
  console.log('[Dashboard] onUnmounted')
})
```

### When to use each hook

| Hook | Typical use |
|------|------------|
| `<script setup>` body | Initialise state, declare computed/watchers. No DOM access. |
| `onMounted` | DOM queries, third-party library init, start data fetching |
| `onUpdated` | Post-render DOM measurements, analytics tracking |
| `onBeforeUnmount` | Cancel timers, unsubscribe, release resources |
| `onUnmounted` | Final cleanup, logging |

### Watching specific values (replaces Angular's ngOnChanges)

Vue has no single "all inputs changed" hook. To react to specific prop/state changes, use `watch()`:

```typescript
import { watch } from 'vue'

// Watch a single prop
watch(() => props.course, (newCourse, oldCourse) => {
  console.log(`Course changed: ${oldCourse?.name} → ${newCourse.name}`)
})

// Watch multiple sources
watch(
  [() => props.isEnrolled, () => props.enrollmentStatus],
  ([newEnrolled, newStatus], [oldEnrolled, oldStatus]) => {
    console.log('Enrollment changed:', { newEnrolled, newStatus })
  }
)

// Immediate execution (runs on initial value too)
watch(() => props.course.id, (id) => {
  fetchCourseDetails(id)
}, { immediate: true })
```

**References**:
- [Vue.js Lifecycle Hooks](https://vuejs.org/guide/essentials/lifecycle.html)
- [Lifecycle Diagram](https://vuejs.org/guide/essentials/lifecycle.html#lifecycle-diagram)
- [Vue.js Watchers](https://vuejs.org/guide/essentials/watchers.html)

---

## 7. Smart vs Presentational Components

Our application follows a pattern common to both Vue and Angular: separating **smart** (container) components from **presentational** (dumb) components.

### Presentational components

Located in `src/components/`. Pure display logic — receive data via props, emit events to parent.

| Component | Props (in) | Emits (out) | Has store access? |
|-----------|-----------|-------------|-------------------|
| `CourseCard.vue` | `course`, `isEnrolled`, `enrollmentStatus` | `enrollClicked`, `dropClicked`, `viewDetails` | No |
| `StudentProfile.vue` | `student`, `totalCredits` | `logoutClicked` | No |
| `EnrollmentStatus.vue` | `status`, `isFull` | *(none)* | No |

These components are **reusable** and **testable in isolation** because they depend only on their props.

### Smart (container) components

Located in `src/views/`. Orchestrate data flow — access stores, handle routing, wire child components.

| Component | Store access | Router access | Manages |
|-----------|-------------|---------------|---------|
| `DashboardView.vue` | `useCourseStore`, `useAuthStore` | `useRouter`, `useRoute` | Enrolled courses display, navigation |
| `EnrollmentView.vue` | `useCourseStore`, `useAuthStore` | `useRouter` | Search/filter, enrollment actions |
| `CourseDetailView.vue` | `useCourseStore`, `useAuthStore` | `useRoute`, `useRouter` | Single course detail, enroll/drop |
| `LoginView.vue` | `useAuthStore` | `useRouter` | Authentication form |

**Source**: `vue-student-dashboard/src/views/DashboardView.vue` (script)

```typescript
// Smart component: accesses stores directly
const courseStore = useCourseStore()
const authStore = useAuthStore()
const router = useRouter()
const route = useRoute()

// Passes store data as props to presentational children:
// <StudentProfile :student="authStore.student" :total-credits="courseStore.totalCredits" />
```

---

## 8. Angular Comparison

The fundamental mechanism is the same in both frameworks — data flows down through typed inputs/props and events flow up through typed outputs/emits. An Angular developer will recognise `defineProps` as the equivalent of `input()` / `input.required()`, and `defineEmits` as the equivalent of `output()`. The key API difference is that Angular uses signal-based inputs read via function calls (`this.course()`), while Vue props are plain object properties (`props.course`). Angular also requires outputs to be class fields with `.emit()` method calls, while Vue's `emit()` is a standalone function received from `defineEmits()`. Both approaches achieve identical type safety with TypeScript generics.

Lifecycle hooks map closely but differ in granularity. Angular splits the post-creation phase into more stages — `ngOnChanges`, `ngOnInit`, `ngAfterContentInit`, `ngAfterViewInit` — which lets you distinguish between input changes and DOM readiness. Vue collapses these into fewer hooks: the `<script setup>` body handles creation, `onMounted` handles both initialization and DOM readiness. The biggest gap is `ngOnChanges` with its `SimpleChanges` object, which has no direct Vue equivalent — instead you use `watch()` with explicit source tracking per property. This is a trade-off: Angular gives you a single place to react to all input changes with old/new values, while Vue forces you to be explicit about which reactive values you want to observe, which can be more verbose but is also more precise about what triggers side effects.

**References**:
- [Angular Component Interaction](https://angular.dev/guide/components/inputs)
- [Angular Lifecycle Hooks](https://angular.dev/guide/components/lifecycle)
- [Vue.js Component Basics](https://vuejs.org/guide/essentials/component-basics.html)

---

**Previous**: [Core Fundamentals](./01-vue-core-fundamentals.md)
**Next**: [Routing & Navigation Guards](./03-vue-routing-and-guards.md)
