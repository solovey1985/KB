# Vue.js Core Fundamentals

> **Series**: Vue.js Comprehensive Guide (1 of 5)
> **Audience**: Developers with Angular experience transitioning to Vue.js
> **Code context**: Student Dashboard + Class Enrollment application

### Document Navigation

| # | Document | Focus |
|---|----------|-------|
| **1** | **Core Fundamentals** (this document) | Reactivity, templates, single-file components |
| 2 | [Component Patterns](./02-vue-component-patterns.md) | Props, emits, lifecycle hooks, component design |
| 3 | [Routing & Navigation Guards](./03-vue-routing-and-guards.md) | vue-router, lazy loading, route guards |
| 4 | [State Management with Pinia](./04-vue-state-management.md) | Stores, reactive state, actions, getters |
| 5 | [Data Transforms & Composables](./05-vue-composables-and-transforms.md) | Composables, formatting, reusable logic |

See also: [Angular vs Vue.js Side-by-Side Comparison](./COMPARISON.md)

---

## Table of Contents

1. [Application Bootstrap](#1-application-bootstrap)
2. [Single-File Components (SFCs)](#2-single-file-components-sfcs)
3. [The Reactivity System](#3-the-reactivity-system)
4. [Template Syntax](#4-template-syntax)
5. [Style Scoping](#5-style-scoping)
6. [Project Structure Conventions](#6-project-structure-conventions)
7. [Angular Comparison](#7-angular-comparison)

---

## 1. Application Bootstrap

Every Vue application starts with `createApp()`, which creates an application instance and mounts it to a DOM element.

**Source**: `vue-student-dashboard/src/main.ts`

```typescript
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(createPinia())   // Install state management plugin
app.use(router)           // Install routing plugin

app.mount('#app')         // Mount to <div id="app"> in index.html
```

### What happens here

1. **`createApp(App)`** — Creates a new Vue application instance with `App.vue` as the root component. This is a factory function, not a class constructor.

2. **`app.use(plugin)`** — Installs plugins. Plugins are the extension mechanism for Vue. Both Pinia (state) and vue-router (routing) are installed this way. Each plugin can provide globally available features, inject state, or add lifecycle hooks.

3. **`app.mount('#app')`** — Finds the DOM element matching the selector and renders the component tree into it. This triggers the first render cycle.

### The root component

**Source**: `vue-student-dashboard/src/App.vue`

```vue
<script setup lang="ts">
import { RouterView } from 'vue-router'
</script>

<template>
  <RouterView />
</template>

<style>
/* Unscoped: global styles applied to entire app */
*, *::before, *::after {
  box-sizing: border-box;
}
body {
  margin: 0;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background: #f5f5f5;
}
</style>
```

The root component is minimal — it renders `<RouterView />`, which is the outlet where routed page components appear. Global styles are placed here without the `scoped` attribute so they apply everywhere.

**References**:
- [Vue.js Application Instance](https://vuejs.org/guide/essentials/application.html)
- [Vue.js Plugin System](https://vuejs.org/guide/reusability/plugins.html)

---

## 2. Single-File Components (SFCs)

The `.vue` file format is Vue's defining feature. A single file contains three optional blocks:

```
┌──────────────────────────────────┐
│  <script setup lang="ts">        │  ← Logic (TypeScript)
│    // imports, props, state       │
│  </script>                        │
├──────────────────────────────────┤
│  <template>                       │  ← Markup (HTML + directives)
│    <div>{{ message }}</div>        │
│  </template>                      │
├──────────────────────────────────┤
│  <style scoped>                   │  ← Styles (CSS/SCSS, scoped)
│    div { color: blue; }           │
│  </style>                         │
└──────────────────────────────────┘
```

### The `<script setup>` block

`<script setup>` is a compile-time syntactic sugar that:
- Automatically exposes all top-level bindings (variables, functions, imports) to the template
- Enables `defineProps()` and `defineEmits()` compiler macros
- Runs once when the component instance is created (equivalent to the `setup()` function)

**Source**: `vue-student-dashboard/src/views/LoginView.vue`

```typescript
<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const authStore = useAuthStore()     // Pinia store access
const router = useRouter()           // Router access

const email = ref('')                // Reactive state
const password = ref('')
const errorMessage = ref('')

function onSubmit(): void {          // Method — auto-available in template
  if (!email.value || !password.value) {
    errorMessage.value = 'Please enter email and password.'
    return
  }
  const success = authStore.login(email.value, password.value)
  if (success) {
    router.push({ name: 'dashboard' })
  }
}
</script>
```

Everything declared at the top level — `email`, `password`, `errorMessage`, `onSubmit`, `authStore` — is directly usable in the `<template>` block without any explicit registration.

### The `<template>` block

The template contains standard HTML enhanced with Vue directives and interpolation:

```html
<template>
  <div class="login-page">
    <div v-if="errorMessage" class="error">{{ errorMessage }}</div>

    <form @submit.prevent="onSubmit">
      <input type="email" v-model="email" placeholder="Email" />
      <input type="password" v-model="password" placeholder="Password" />
      <button type="submit">Sign In</button>
    </form>
  </div>
</template>
```

### The `<style>` block

Styles can be `scoped` (applied only to this component's elements) or global (no attribute). You can also use preprocessors:

```html
<style scoped>
.login-page { display: flex; }
</style>

<!-- Or with SCSS (requires sass as a dev dependency) -->
<style scoped lang="scss">
.login-page { display: flex; }
</style>
```

**References**:
- [Vue.js SFC Specification](https://vuejs.org/api/sfc-spec.html)
- [`<script setup>` Documentation](https://vuejs.org/api/sfc-script-setup.html)

---

## 3. The Reactivity System

Vue's reactivity is built on JavaScript Proxy objects. When you wrap a value with `ref()` or `reactive()`, Vue tracks which components depend on it and re-renders them when the value changes.

### `ref()` — Single-value reactivity

Creates a reactive reference to a single value. Access and mutation go through `.value`:

```typescript
import { ref } from 'vue'

const searchTerm = ref('')                    // ref<string>
const selectedCategory = ref<string>('all')   // explicit generic

// Read
console.log(searchTerm.value)  // ''

// Write — triggers re-render of any component using this ref
searchTerm.value = 'physics'
```

**In templates**, `.value` is auto-unwrapped. You write `{{ searchTerm }}`, not `{{ searchTerm.value }}`.

**Source**: `vue-student-dashboard/src/views/EnrollmentView.vue:29-30`

```typescript
const searchTerm = ref('')
const selectedCategory = ref<CourseCategory | 'all'>('all')
```

### `reactive()` — Object-level reactivity

Wraps an entire object in a reactive proxy. No `.value` needed, but the object cannot be reassigned:

```typescript
import { reactive } from 'vue'

const form = reactive({
  email: '',
  password: '',
})

form.email = 'alice@university.edu'  // Triggers reactivity
// form = { ... }  // ERROR — cannot reassign a reactive object
```

### `computed()` — Derived reactive values

Creates a read-only reactive value derived from other reactive sources. Re-evaluates automatically when dependencies change:

**Source**: `vue-student-dashboard/src/components/CourseCard.vue:28-29`

```typescript
const isFull = computed(() => props.course.enrolledCount >= props.course.maxStudents)
const canEnroll = computed(() => !props.isEnrolled && !isFull.value)
```

`isFull` updates automatically whenever `props.course.enrolledCount` or `props.course.maxStudents` changes. Components that read `isFull` re-render when its value changes.

### `watch()` and `watchEffect()` — Side effects

For running code in response to reactive changes (API calls, logging, etc.):

```typescript
import { watch, watchEffect } from 'vue'

// watch — explicit source, old/new values
watch(() => props.course, (newCourse, oldCourse) => {
  console.log('Course changed:', oldCourse?.name, '→', newCourse.name)
})

// watchEffect — auto-tracks all reactive dependencies used inside
watchEffect(() => {
  console.log('Current search:', searchTerm.value)
  // Runs immediately, then re-runs whenever searchTerm changes
})
```

### Reactivity summary

| Primitive | Use case | Read | Write |
|-----------|----------|------|-------|
| `ref(value)` | Any single value (string, number, object) | `.value` (auto-unwrapped in templates) | `.value = newValue` |
| `reactive(obj)` | Object/array without reassignment | Direct property access | Direct property mutation |
| `computed(fn)` | Derived value from other reactive sources | `.value` (read-only) | Not writable (by default) |
| `watch(source, cb)` | Side effects on change | Callback receives new/old | N/A |
| `watchEffect(fn)` | Auto-tracked side effects | Runs immediately, re-runs on dependency change | N/A |

**References**:
- [Vue.js Reactivity Fundamentals](https://vuejs.org/guide/essentials/reactivity-fundamentals.html)
- [Computed Properties](https://vuejs.org/guide/essentials/computed.html)
- [Watchers](https://vuejs.org/guide/essentials/watchers.html)
- [Reactivity in Depth](https://vuejs.org/guide/extras/reactivity-in-depth.html)

---

## 4. Template Syntax

Vue templates are valid HTML enhanced with directives and interpolation.

### Text interpolation

Double curly braces for reactive text rendering:

```html
<h1>{{ course.name }}</h1>
<p>{{ formatCredits(course.credits) }}</p>
```

### Attribute binding (`v-bind` / `:`)

Dynamically bind HTML attributes:

```html
<!-- Full syntax -->
<div v-bind:class="dynamicClass">...</div>

<!-- Shorthand (used everywhere in practice) -->
<div :class="dynamicClass">...</div>
<div :style="{ width: percentage }">...</div>
<img :src="imageUrl" :alt="altText" />
```

**Source**: `vue-student-dashboard/src/components/CourseCard.vue` (template)

```html
<div class="course-card" :class="{ enrolled: isEnrolled, full: isFull }">
  <div class="enrollment-fill" :style="{ width: formatEnrollmentPercent(course) }"></div>
</div>
```

### Event handling (`v-on` / `@`)

```html
<!-- Full syntax -->
<button v-on:click="handleClick">Click</button>

<!-- Shorthand (standard in practice) -->
<button @click="handleClick">Click</button>

<!-- With event modifiers -->
<form @submit.prevent="onSubmit">...</form>    <!-- Calls preventDefault() -->
<input @keyup.enter="search" />                <!-- Only on Enter key -->
```

**Source**: `vue-student-dashboard/src/views/LoginView.vue` (template)

```html
<form @submit.prevent="onSubmit">
  <input type="email" v-model="email" placeholder="student@university.edu" />
  <button type="submit" class="btn-login">Sign In</button>
</form>
```

### Conditional rendering (`v-if` / `v-else` / `v-show`)

```html
<!-- v-if: adds/removes element from DOM -->
<div v-if="errorMessage" class="error">{{ errorMessage }}</div>

<!-- v-if / v-else-if / v-else chain -->
<p v-if="courses.length === 0">No courses enrolled.</p>
<div v-else class="course-grid">...</div>

<!-- v-show: toggles CSS display (element stays in DOM) -->
<div v-show="isLoading">Loading...</div>
```

**Source**: `vue-student-dashboard/src/views/DashboardView.vue` (template)

```html
<div v-if="maxCreditsWarning" class="alert alert-warning">
  You've reached the maximum credit limit (18 credits).
</div>

<p v-if="courseStore.enrolledCourses.length === 0" class="empty-message">
  You haven't enrolled in any courses yet.
</p>
<div v-else class="course-grid">
  <!-- courses rendered here -->
</div>
```

### List rendering (`v-for`)

```html
<CourseCard
  v-for="course in courseStore.enrolledCourses"
  :key="course.id"
  :course="course"
  :is-enrolled="true"
/>
```

The `:key` attribute is required — it tells Vue how to track identity of each element for efficient DOM updates.

### Two-way binding (`v-model`)

`v-model` is syntactic sugar for `:value` + `@input` combined:

```html
<!-- These two are equivalent -->
<input v-model="searchTerm" />
<input :value="searchTerm" @input="searchTerm = $event.target.value" />
```

**Source**: `vue-student-dashboard/src/views/EnrollmentView.vue` (template)

```html
<input
  type="text"
  class="search-input"
  placeholder="Search courses or instructors..."
  v-model="searchTerm"
/>
```

### Template syntax reference

| Syntax | Purpose | Angular equivalent |
|--------|---------|-------------------|
| `{{ expr }}` | Text interpolation | `{{ expr }}` |
| `:attr="expr"` | Attribute binding | `[attr]="expr"` |
| `@event="handler"` | Event binding | `(event)="handler()"` |
| `v-if="cond"` | Conditional rendering | `@if (cond) { }` |
| `v-for="item in list"` | List rendering | `@for (item of list; track item.id) { }` |
| `v-model="value"` | Two-way binding | `[(ngModel)]="value"` |
| `v-show="cond"` | Toggle CSS display | `[hidden]="!cond"` |
| `@event.prevent` | Event modifier | Manual `event.preventDefault()` |

**References**:
- [Vue.js Template Syntax](https://vuejs.org/guide/essentials/template-syntax.html)
- [Conditional Rendering](https://vuejs.org/guide/essentials/conditional.html)
- [List Rendering](https://vuejs.org/guide/essentials/list.html)
- [Event Handling](https://vuejs.org/guide/essentials/event-handling.html)
- [Form Input Bindings](https://vuejs.org/guide/essentials/forms.html)

---

## 5. Style Scoping

Vue offers three modes of style application:

### Scoped styles (most common)

The `scoped` attribute restricts CSS to the current component. Vue achieves this by adding a unique `data-v-xxxxx` attribute to each element:

```html
<style scoped>
.course-card { border: 1px solid #e0e0e0; }  /* Only affects this component */
</style>
```

Compiled output:
```html
<div class="course-card" data-v-7a7a37b1>...</div>
```
```css
.course-card[data-v-7a7a37b1] { border: 1px solid #e0e0e0; }
```

### Global styles

Omit `scoped` to apply styles globally. Typically used only in `App.vue` for base resets:

```html
<style>
body { margin: 0; font-family: sans-serif; }
</style>
```

### Deep selectors

To style child component internals from a parent, use `:deep()`:

```html
<style scoped>
.parent :deep(.child-class) {
  color: red;   /* Penetrates child component scope */
}
</style>
```

**References**:
- [Vue.js Scoped CSS](https://vuejs.org/api/sfc-css-features.html#scoped-css)
- [CSS Modules in Vue](https://vuejs.org/api/sfc-css-features.html#css-modules)

---

## 6. Project Structure Conventions

Our application follows standard Vue community conventions:

```
src/
├── App.vue              # Root component
├── main.ts              # Bootstrap — createApp + plugins
├── models/              # TypeScript interfaces (pure types, no Vue imports)
├── stores/              # Pinia stores (global state)
├── composables/         # Reusable logic functions (useXxx naming)
├── guards/              # Route guard functions
├── components/          # Reusable presentational components
├── views/               # Page-level components (one per route)
└── router/              # Route definitions
    └── index.ts
```

### Naming conventions

| Item | Convention | Example |
|------|-----------|---------|
| Component files | PascalCase `.vue` | `CourseCard.vue` |
| View (page) files | PascalCase + `View` suffix | `DashboardView.vue` |
| Composables | `use` prefix, camelCase | `useFormatters.ts` |
| Stores | camelCase, no prefix | `auth.ts`, `course.ts` |
| Store hooks | `use` prefix + `Store` suffix | `useAuthStore()` |
| Props in templates | kebab-case | `:is-enrolled="true"` |
| Events in templates | kebab-case | `@enroll-clicked="handler"` |
| Props in script | camelCase | `defineProps<{ isEnrolled: boolean }>()` |

**References**:
- [Vue.js Style Guide](https://vuejs.org/style-guide/)
- [Vue.js Project Scaffolding](https://vuejs.org/guide/quick-start.html)

---

## 7. Angular Comparison

For Angular developers, the most fundamental shift in Vue is the absence of the decorator + class pattern. Angular defines components as decorated classes where metadata drives the framework's behavior — the `@Component` decorator specifies selector, template URL, style URLs, and dependency imports. In Vue, there is no decorator, no class, and no metadata object. A `.vue` file co-locates template, logic, and style in three blocks, and the `<script setup>` body is just a function scope where everything declared at the top level automatically becomes available in the template. Where Angular requires explicit listing of dependencies in `imports: [...]` for the template to use them, Vue's compiler handles this implicitly from the `<script setup>` imports.

The reactivity model is conceptually aligned between both frameworks — Angular's `signal()` maps directly to Vue's `ref()`, and both use `computed()` identically for derived state. The syntactic difference is that Angular signals are read by calling them as functions (`this.count()`), while Vue refs are read via `.value` in script and auto-unwrapped in templates (`count` rather than `count.value`). Template syntax diverges in shorthand notation (`[prop]` vs `:prop`, `(event)` vs `@event`, `@if` vs `v-if`), but the capabilities are equivalent. Vue's `v-model` for two-way binding is more concise than Angular's `[(ngModel)]` and works without importing a forms module. Angular's template syntax is richer in some respects (e.g., built-in pipe chaining with `|`), while Vue favors explicitness (calling functions directly in expressions).

**References**:
- [Angular Component Overview](https://angular.dev/guide/components)
- [Angular Signals](https://angular.dev/guide/signals)
- [Vue.js vs Other Frameworks](https://vuejs.org/guide/extras/composition-api-faq.html)

---

**Next**: [Component Patterns](./02-vue-component-patterns.md) — Props, emits, lifecycle hooks, and component design patterns.
