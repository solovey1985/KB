# Angular vs Vue.js: A Side-by-Side Comparison

> **Perspective**: Written for developers with Angular experience exploring Vue.js.
> **Context**: Both code samples come from identical "Student Dashboard + Class Enrollment" applications built with TypeScript, covering the same features: authentication, course browsing, enrollment management, and route protection.

---

## Table of Contents

1. [Project Structure](#1-project-structure)
2. [Components](#2-components)
3. [Lifecycle Hooks](#3-lifecycle-hooks)
4. [Routing](#4-routing)
5. [Parent-Child Interaction](#5-parent-child-interaction)
6. [Guards](#6-guards)
7. [Pipes vs Composables](#7-pipes-vs-composables)
8. [State Management](#8-state-management)
9. [Summary Table](#9-summary-table)

---

## 1. Project Structure

Both apps follow a feature-oriented structure, but the conventions differ.

### Angular

```
src/app/
├── models/              # TypeScript interfaces
│   ├── course.model.ts
│   └── student.model.ts
├── services/            # Injectable singletons (@Injectable)
│   ├── auth.service.ts
│   └── course.service.ts
├── pipes/               # @Pipe transform classes
│   ├── category-label.pipe.ts
│   ├── credit-format.pipe.ts
│   └── enrollment-percent.pipe.ts
├── guards/              # CanActivateFn functions
│   ├── auth.guard.ts
│   └── enrollment.guard.ts
├── components/          # Presentational (child) components
│   ├── course-card/
│   ├── enrollment-status/
│   └── student-profile/
├── pages/               # Routed (smart/container) components
│   ├── dashboard/
│   ├── enrollment/
│   ├── course-detail/
│   ├── login/
│   └── not-found/
├── app.routes.ts        # Route definitions
├── app.config.ts        # Provider configuration
└── app.ts               # Root component
```

**Key trait**: Angular separates template (`.html`), logic (`.ts`), and style (`.scss`) into distinct files per component. Each component is a *class* decorated with `@Component`.

### Vue

```
src/
├── models/              # TypeScript interfaces (identical)
│   ├── course.model.ts
│   └── student.model.ts
├── stores/              # Pinia stores (replaces Angular services)
│   ├── auth.ts
│   └── course.ts
├── composables/         # Reusable logic functions (replaces pipes)
│   └── useFormatters.ts
├── guards/              # Navigation guard functions
│   ├── auth.guard.ts
│   └── enrollment.guard.ts
├── components/          # Presentational (child) components
│   ├── CourseCard.vue
│   ├── EnrollmentStatus.vue
│   └── StudentProfile.vue
├── views/               # Routed page components
│   ├── DashboardView.vue
│   ├── EnrollmentView.vue
│   ├── CourseDetailView.vue
│   ├── LoginView.vue
│   └── NotFoundView.vue
├── router/index.ts      # Route definitions
├── main.ts              # App bootstrap
└── App.vue              # Root component
```

**Key trait**: Vue uses Single-File Components (`.vue`) that co-locate `<script>`, `<template>`, and `<style>` in one file. No decorators — composition is function-based.

### What's common

- Both use TypeScript interfaces for models (identical files)
- Both separate presentational components from routed page components
- Both have a dedicated routing configuration file
- Both place guards in a standalone module

### What's different

| Aspect | Angular | Vue |
|--------|---------|-----|
| Component file | `.ts` + `.html` + `.scss` (3 files) | Single `.vue` file (1 file) |
| Component definition | Class + `@Component` decorator | `<script setup>` (no class) |
| Shared state | `@Injectable` service classes | Pinia `defineStore()` functions |
| Data transforms | `@Pipe` classes | Composable functions |
| Bootstrap | `app.config.ts` with providers | `main.ts` with `app.use()` plugins |

---

## 2. Components

### Angular Component (class-based with decorator)

```typescript
// course-card.component.ts
@Component({
  selector: 'app-course-card',
  imports: [CategoryLabelPipe, CreditFormatPipe, EnrollmentPercentPipe, EnrollmentStatusComponent],
  templateUrl: './course-card.component.html',
  styleUrl: './course-card.component.scss',
})
export class CourseCardComponent implements OnInit, OnDestroy {
  // Signal-based inputs (Angular 17+)
  readonly course = input.required<Course>();
  readonly isEnrolled = input<boolean>(false);

  // Signal-based outputs
  readonly enrollClicked = output<string>();

  // Computed values derived from inputs
  readonly isFull = computed(
    () => this.course().enrolledCount >= this.course().maxStudents
  );

  ngOnInit(): void {
    console.log(`Rendering "${this.course().name}"`);
  }

  ngOnDestroy(): void {
    console.log(`Destroying "${this.course().name}"`);
  }

  onEnroll(): void {
    this.enrollClicked.emit(this.course().id);
  }
}
```

**Template** (separate `.html` file):
```html
<div class="course-card" [class.enrolled]="isEnrolled()">
  <span>{{ course().category | categoryLabel }}</span>
  <h3>{{ course().name }}</h3>

  @if (canEnroll()) {
    <button (click)="onEnroll()">Enroll</button>
  }
</div>
```

### Vue Component (composition API with `<script setup>`)

```vue
<!-- CourseCard.vue — all in one file -->
<script setup lang="ts">
import { computed, onMounted, onUnmounted } from 'vue'
import type { Course } from '@/models/course.model'
import { useFormatters } from '@/composables/useFormatters'

// Props (equivalent of Angular input())
const props = defineProps<{
  course: Course
  isEnrolled: boolean
}>()

// Emits (equivalent of Angular output())
const emit = defineEmits<{
  enrollClicked: [courseId: string]
}>()

// Composable replaces Angular Pipe
const { formatCategoryLabel } = useFormatters()

// Computed (same concept as Angular computed())
const isFull = computed(() => props.course.enrolledCount >= props.course.maxStudents)
const canEnroll = computed(() => !props.isEnrolled && !isFull.value)

onMounted(() => {
  console.log(`Rendering "${props.course.name}"`)
})

onUnmounted(() => {
  console.log(`Destroying "${props.course.name}"`)
})
</script>

<template>
  <div class="course-card" :class="{ enrolled: isEnrolled }">
    <span>{{ formatCategoryLabel(course.category) }}</span>
    <h3>{{ course.name }}</h3>

    <button v-if="canEnroll" @click="emit('enrollClicked', course.id)">Enroll</button>
  </div>
</template>

<style scoped>
/* styles here — scoped to this component */
</style>
```

### Key Differences

| Feature | Angular | Vue |
|---------|---------|-----|
| Definition | Class + `@Component` decorator | `<script setup>` function scope |
| Template | Separate `.html` file or inline `template:` | `<template>` block in `.vue` file |
| Styles | Separate `.scss` file | `<style scoped>` block in `.vue` file |
| Imports for template | Listed in `imports: []` metadata | Auto-available from `<script setup>` |
| Reactivity | Signals: `signal()`, `computed()`, `input()` | Refs: `ref()`, `computed()`, `defineProps()` |
| Accessing inputs | Function call: `this.course()` | Direct property: `props.course` |
| Class binding | `[class.enrolled]="isEnrolled()"` | `:class="{ enrolled: isEnrolled }"` |
| Event binding | `(click)="onEnroll()"` | `@click="emit('enrollClicked', ...)"` |
| Conditionals | `@if (condition) { ... }` (control flow) | `v-if="condition"` (directive) |
| Loops | `@for (item of items; track item.id) { }` | `v-for="item in items" :key="item.id"` |

### What's Common

- Both use TypeScript with strong typing for props/inputs
- Both support computed/derived values from reactive state
- Both have scoped styling (Angular via `ViewEncapsulation`, Vue via `<style scoped>`)
- Both distinguish between "smart" (container) and "presentational" components

---

## 3. Lifecycle Hooks

### Angular Lifecycle

Angular provides class-based lifecycle hooks implemented via interfaces:

```typescript
export class CourseCardComponent implements OnInit, OnChanges, AfterViewInit, OnDestroy {

  ngOnChanges(changes: SimpleChanges): void {
    // Called BEFORE ngOnInit and on every input change.
    // Receives a SimpleChanges object with previous/current values.
  }

  ngOnInit(): void {
    // Called once after the first ngOnChanges.
    // Use for initialization logic (fetching data, setting up subscriptions).
  }

  ngAfterViewInit(): void {
    // Called once after the component's view and child views are initialized.
    // Use for DOM manipulation or ViewChild queries.
  }

  ngOnDestroy(): void {
    // Called just before the component is destroyed.
    // Use for cleanup: unsubscribe, detach event listeners.
  }
}
```

Full Angular lifecycle order:
1. `ngOnChanges` — input binding change
2. `ngOnInit` — component initialised
3. `ngDoCheck` — custom change detection
4. `ngAfterContentInit` — projected content initialised
5. `ngAfterContentChecked` — projected content checked
6. `ngAfterViewInit` — view initialised
7. `ngAfterViewChecked` — view checked
8. `ngOnDestroy` — before destruction

### Vue Lifecycle

Vue uses standalone functions imported from `vue`, called inside `<script setup>`:

```typescript
import { onMounted, onUpdated, onBeforeUnmount, onUnmounted } from 'vue'

// Equivalent of ngOnInit + ngAfterViewInit combined
onMounted(() => {
  console.log('Component mounted to DOM')
})

// Fires after every reactive re-render (loosely like ngOnChanges but post-render)
onUpdated(() => {
  console.log('Component re-rendered')
})

// Equivalent of ngOnDestroy (fires before removal)
onBeforeUnmount(() => {
  console.log('About to unmount')
})

// Fires after unmounting (no Angular equivalent)
onUnmounted(() => {
  console.log('Unmounted and cleaned up')
})
```

Full Vue lifecycle order:
1. `setup()` / `<script setup>` — runs during creation (closest to constructor)
2. `onBeforeMount` — before first render
3. `onMounted` — after first render (DOM available)
4. `onBeforeUpdate` — before reactive re-render
5. `onUpdated` — after reactive re-render
6. `onBeforeUnmount` — before removal
7. `onUnmounted` — after removal

### Comparison

| Angular Hook | Vue Equivalent | Notes |
|-------------|----------------|-------|
| `constructor` | `<script setup>` body | Both run during component creation |
| `ngOnChanges` | `watch()` / `watchEffect()` | Vue has no single "changes" hook; use watchers per-prop |
| `ngOnInit` | `onMounted` | Vue's closest equivalent; DOM is available in `onMounted` |
| `ngAfterViewInit` | `onMounted` | Vue combines init + view-ready into one hook |
| `ngAfterViewChecked` | `onUpdated` | Both fire after the view is updated |
| `ngOnDestroy` | `onBeforeUnmount` | Both for cleanup before removal |
| *(none)* | `onUnmounted` | Vue adds a post-removal hook |
| `ngDoCheck` | *(none)* | Vue's reactivity system handles this automatically |

### Key Insight for Angular Developers

Angular's `ngOnChanges` with `SimpleChanges` gives you old/new values for each input. Vue doesn't have this — instead, use `watch()` to observe specific reactive values:

```typescript
// Vue: equivalent of ngOnChanges for a specific prop
import { watch } from 'vue'

watch(() => props.course, (newVal, oldVal) => {
  console.log('Course changed from', oldVal, 'to', newVal)
})
```

---

## 4. Routing

### Angular Routing

```typescript
// app.routes.ts
import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';
import { enrollmentGuard } from './guards/enrollment.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () =>
      import('./pages/login/login.component').then(m => m.LoginComponent),
  },
  {
    path: 'dashboard',
    loadComponent: () =>
      import('./pages/dashboard/dashboard.component').then(m => m.DashboardComponent),
    canActivate: [authGuard],
  },
  {
    path: 'enrollment',
    loadComponent: () =>
      import('./pages/enrollment/enrollment.component').then(m => m.EnrollmentComponent),
    canActivate: [authGuard, enrollmentGuard],
  },
  {
    path: 'course/:id',
    loadComponent: () =>
      import('./pages/course-detail/course-detail.component').then(m => m.CourseDetailComponent),
    canActivate: [authGuard],
  },
  { path: '**', loadComponent: () =>
      import('./pages/not-found/not-found.component').then(m => m.NotFoundComponent) },
];

// app.config.ts — registering the router
export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes)],
};
```

**Reading route params:**
```typescript
// Angular — inject ActivatedRoute
private readonly route = inject(ActivatedRoute);

ngOnInit(): void {
  const id = this.route.snapshot.paramMap.get('id');
}
```

**Programmatic navigation:**
```typescript
private readonly router = inject(Router);

this.router.navigate(['/course', courseId]);
this.router.navigate(['/dashboard'], { queryParams: { maxCredits: 'true' } });
```

### Vue Routing

```typescript
// router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import { authGuard } from '@/guards/auth.guard'
import { enrollmentGuard } from '@/guards/enrollment.guard'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', redirect: '/dashboard' },
    {
      path: '/login',
      name: 'login',
      component: () => import('@/views/LoginView.vue'),
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: () => import('@/views/DashboardView.vue'),
      beforeEnter: [authGuard],
    },
    {
      path: '/enrollment',
      name: 'enrollment',
      component: () => import('@/views/EnrollmentView.vue'),
      beforeEnter: [authGuard, enrollmentGuard],
    },
    {
      path: '/course/:id',
      name: 'courseDetail',
      component: () => import('@/views/CourseDetailView.vue'),
      beforeEnter: [authGuard],
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'notFound',
      component: () => import('@/views/NotFoundView.vue'),
    },
  ],
})

export default router

// main.ts — registering the router
app.use(router)
```

**Reading route params:**
```typescript
// Vue — useRoute() composable
const route = useRoute()
const courseId = route.params.id as string
```

**Programmatic navigation:**
```typescript
const router = useRouter()

router.push({ name: 'courseDetail', params: { id: courseId } })
router.push({ name: 'dashboard', query: { maxCredits: 'true' } })
```

### Comparison

| Feature | Angular | Vue |
|---------|---------|-----|
| Router creation | `provideRouter(routes)` in providers | `createRouter()` factory function |
| History mode | Default (uses `LocationStrategy`) | Explicit: `createWebHistory()` |
| Lazy loading | `loadComponent: () => import(...)` | `component: () => import(...)` |
| Route guard attachment | `canActivate: [guard]` | `beforeEnter: [guard]` |
| Wildcard route | `path: '**'` | `path: '/:pathMatch(.*)*'` |
| Named routes | Not typically used | Common: `name: 'dashboard'` |
| Param access | `inject(ActivatedRoute).snapshot.paramMap.get()` | `useRoute().params.id` |
| Programmatic nav | `router.navigate(['/path', param])` | `router.push({ name, params })` |
| Router outlet | `<router-outlet />` | `<RouterView />` |

### What's Common

- Both support lazy loading with dynamic `import()` (producing separate chunks)
- Both support route parameters (`:id`)
- Both support query parameters
- Both support redirect routes
- Both support wildcard/catch-all routes
- Both attach guard functions to specific routes

---

## 5. Parent-Child Interaction

Both apps demonstrate a three-level hierarchy:
**Dashboard (parent) → CourseCard (child) → EnrollmentStatus (grandchild)**

### Angular: input() + output()

**Parent template** (dashboard.component.html):
```html
<!-- Parent passes data DOWN via property binding -->
<app-course-card
  [course]="course"
  [isEnrolled]="true"
  [enrollmentStatus]="getEnrollmentStatus(course.id)"
  (dropClicked)="onDropCourse($event)"
  (viewDetails)="onViewCourseDetails($event)"
/>
```

**Child component** (course-card.component.ts):
```typescript
// Data IN — signal-based inputs (Angular 17+)
readonly course = input.required<Course>();
readonly isEnrolled = input<boolean>(false);
readonly enrollmentStatus = input<string | null>(null);

// Data OUT — signal-based outputs
readonly enrollClicked = output<string>();
readonly dropClicked = output<string>();
readonly viewDetails = output<string>();

// Emit to parent
onEnroll(): void {
  this.enrollClicked.emit(this.course().id);
}
```

**Child passes data further DOWN to grandchild:**
```html
<app-enrollment-status
  [status]="enrollmentStatus()"
  [isFull]="isFull()"
/>
```

### Vue: defineProps() + defineEmits()

**Parent template** (DashboardView.vue):
```html
<!-- Parent passes data DOWN via prop binding (v-bind / :) -->
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

**Child component** (CourseCard.vue):
```typescript
// Data IN — defineProps (compile-time macro)
const props = defineProps<{
  course: Course
  isEnrolled: boolean
  enrollmentStatus: string | null
}>()

// Data OUT — defineEmits (compile-time macro)
const emit = defineEmits<{
  enrollClicked: [courseId: string]
  dropClicked: [courseId: string]
  viewDetails: [courseId: string]
}>()

// Emit to parent
emit('enrollClicked', props.course.id)
```

**Child passes data further DOWN to grandchild:**
```html
<EnrollmentStatus :status="enrollmentStatus" :is-full="isFull" />
```

### Comparison

| Aspect | Angular | Vue |
|--------|---------|-----|
| Data in (parent → child) | `input()` / `input.required()` | `defineProps<{ ... }>()` |
| Data out (child → parent) | `output<T>()` + `.emit(value)` | `defineEmits<{ ... }>()` + `emit('name', value)` |
| Syntax in parent template | `[prop]="value"` / `(event)="handler($event)"` | `:prop="value"` / `@event="handler"` |
| Required props | `input.required<T>()` | Required by default in `defineProps` (add `?` for optional) |
| Default values | `input<boolean>(false)` | `withDefaults(defineProps<{...}>(), { ... })` |
| Accessing input values | `this.course()` (function call — it's a signal) | `props.course` (direct property access) |
| Type safety | Full TypeScript (generic signals) | Full TypeScript (generic defineProps) |
| Naming convention | camelCase in TS, camelCase in template | camelCase in TS, kebab-case in template (auto-converted) |

### What's Common

- Both enforce a one-way data flow: props/inputs flow down, events/outputs flow up
- Both support typed payloads on events
- Both allow multi-level prop drilling (parent → child → grandchild)
- Both can declare required vs optional inputs/props with defaults

---

## 6. Guards

### Angular: Functional Guards (CanActivateFn)

```typescript
// auth.guard.ts
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isAuthenticated()) {
    return true;
  }
  return router.createUrlTree(['/login']);
};
```

```typescript
// enrollment.guard.ts
export const enrollmentGuard: CanActivateFn = () => {
  const courseService = inject(CourseService);
  const router = inject(Router);

  if (courseService.totalCredits() < 18) {
    return true;
  }
  return router.createUrlTree(['/dashboard'], {
    queryParams: { maxCredits: 'true' },
  });
};
```

**Attaching to route:**
```typescript
{ path: 'enrollment', canActivate: [authGuard, enrollmentGuard], ... }
```

### Vue: Navigation Guards (beforeEnter)

```typescript
// auth.guard.ts
import type { NavigationGuardWithThis } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

export const authGuard: NavigationGuardWithThis<undefined> = (to, from) => {
  const authStore = useAuthStore()

  if (authStore.isAuthenticated) {
    return true
  }
  return { name: 'login' }
}
```

```typescript
// enrollment.guard.ts
export const enrollmentGuard: NavigationGuardWithThis<undefined> = (to, from) => {
  const courseStore = useCourseStore()

  if (courseStore.totalCredits < 18) {
    return true
  }
  return { name: 'dashboard', query: { maxCredits: 'true' } }
}
```

**Attaching to route:**
```typescript
{ path: '/enrollment', beforeEnter: [authGuard, enrollmentGuard], ... }
```

### Comparison

| Aspect | Angular | Vue |
|--------|---------|-----|
| Guard type | `CanActivateFn` (also `CanDeactivateFn`, `CanMatchFn`) | `NavigationGuardWithThis` or plain `(to, from) => ...` |
| DI access | `inject(ServiceClass)` inside the function | Call Pinia store composable: `useAuthStore()` |
| Allow | `return true` | `return true` |
| Redirect | `return router.createUrlTree(['/login'])` | `return { name: 'login' }` |
| Route attachment | `canActivate: [guard]` | `beforeEnter: [guard]` |
| Global guard | Via `provideRouter(routes, withGuards(...))` | `router.beforeEach(guard)` |
| Guard types available | `canActivate`, `canDeactivate`, `canMatch`, `resolve` | `beforeEnter` (per-route), `beforeEach`, `beforeResolve`, `afterEach` (global) |
| Multiple guards | Array: `canActivate: [a, b]` — run sequentially | Array: `beforeEnter: [a, b]` — run sequentially |

### What's Common

- Both are simple functions that return `true` (allow) or a redirect
- Both support chaining multiple guards on a single route
- Both can access shared state (services/stores) inside the guard
- Both pass redirect destinations as route objects with optional query params

### What's Different

Angular has **more granular guard types** (`canActivate`, `canDeactivate`, `canMatch`, `resolve`). Vue has fewer dedicated types but compensates with **global hooks** (`beforeEach`, `afterEach`) which can implement any guard pattern.

---

## 7. Pipes vs Composables

This is one of the most significant architectural differences.

### Angular: @Pipe Classes

Angular has a first-class `Pipe` concept — a class with `@Pipe` decorator and a `transform()` method, usable directly in templates with the `|` operator:

```typescript
// category-label.pipe.ts
@Pipe({ name: 'categoryLabel' })
export class CategoryLabelPipe implements PipeTransform {
  private readonly labels: Record<CourseCategory, string> = {
    math: 'Mathematics',
    science: 'Natural Sciences',
    humanities: 'Humanities',
    engineering: 'Engineering & CS',
    arts: 'Fine Arts',
  };

  transform(value: CourseCategory): string {
    return this.labels[value] ?? value;
  }
}

// credit-format.pipe.ts
@Pipe({ name: 'creditFormat' })
export class CreditFormatPipe implements PipeTransform {
  transform(value: number): string {
    return `${value} Credit${value !== 1 ? 's' : ''}`;
  }
}

// enrollment-percent.pipe.ts
@Pipe({ name: 'enrollmentPercent' })
export class EnrollmentPercentPipe implements PipeTransform {
  transform(course: Course): string {
    const percent = Math.round((course.enrolledCount / course.maxStudents) * 100);
    return `${percent}%`;
  }
}
```

**Usage in template:**
```html
<span>{{ course().category | categoryLabel }}</span>
<span>{{ course().credits | creditFormat }}</span>
<span>{{ course() | enrollmentPercent }}</span>
```

**Registration**: Listed in component's `imports: [CategoryLabelPipe, ...]`.

### Vue: Composable Functions

Vue 3 has **no built-in pipe concept**. Instead you use plain functions, typically organised as composables:

```typescript
// useFormatters.ts
export function useFormatters() {
  const categoryLabels: Record<CourseCategory, string> = { ... }

  function formatCategoryLabel(category: CourseCategory): string {
    return categoryLabels[category] ?? category
  }

  function formatCredits(value: number): string {
    return `${value} Credit${value !== 1 ? 's' : ''}`
  }

  function formatEnrollmentPercent(course: Course): string {
    const percent = Math.round((course.enrolledCount / course.maxStudents) * 100)
    return `${percent}%`
  }

  return { formatCategoryLabel, formatCredits, formatEnrollmentPercent }
}
```

**Usage in template:**
```html
<script setup>
const { formatCategoryLabel, formatCredits, formatEnrollmentPercent } = useFormatters()
</script>

<template>
  <span>{{ formatCategoryLabel(course.category) }}</span>
  <span>{{ formatCredits(course.credits) }}</span>
  <span>{{ formatEnrollmentPercent(course) }}</span>
</template>
```

### Comparison

| Aspect | Angular Pipe | Vue Composable |
|--------|-------------|----------------|
| Definition | `@Pipe` class with `transform()` | Plain exported function |
| Template syntax | `{{ value \| pipeName }}` | `{{ functionName(value) }}` |
| Chaining | `{{ value \| pipe1 \| pipe2 }}` | `{{ pipe2(pipe1(value)) }}` |
| Registration | Component `imports: [PipeName]` | Import in `<script setup>` |
| Pure/memoised | `pure: true` (default) — Angular caches results | Manual: use `computed()` to memoise |
| Framework support | First-class language feature | Convention, not a framework feature |
| Reusability | Inject into services or other pipes via DI | Import anywhere — it's just a function |

### What's Common

- Both achieve the same goal: transform data for display without mutating it
- Both are reusable across multiple components
- Both support TypeScript generics and strong return types

### Key Insight for Angular Developers

The `|` pipe syntax doesn't exist in Vue templates. This is a deliberate design choice — Vue prefers explicit function calls over "magic" template syntax. For Angular developers, the mental model shift is:

```
Angular: {{ value | transform }}
Vue:     {{ transform(value) }}
```

If you want pipe-like memoisation in Vue, wrap the call in `computed()`:
```typescript
const formattedCategory = computed(() => formatCategoryLabel(props.course.category))
```

---

## 8. State Management

### Angular: @Injectable Services with Signals

```typescript
@Injectable({ providedIn: 'root' })
export class CourseService {
  private readonly coursesSignal = signal<Course[]>(MOCK_COURSES);
  private readonly enrollmentsSignal = signal<Enrollment[]>([]);

  // Derived state
  readonly enrolledCourses = computed(() => {
    const active = this.enrollmentsSignal().filter(e => e.status === 'active');
    return this.coursesSignal().filter(c => active.some(e => e.courseId === c.id));
  });

  readonly totalCredits = computed(() =>
    this.enrolledCourses().reduce((sum, c) => sum + c.credits, 0)
  );

  // Mutation via signal.update()
  enroll(courseId: string, studentId: string): boolean {
    this.enrollmentsSignal.update(list => [...list, newEnrollment]);
    this.coursesSignal.update(list =>
      list.map(c => c.id === courseId ? { ...c, enrolledCount: c.enrolledCount + 1 } : c)
    );
    return true;
  }
}
```

**Usage in component:**
```typescript
private readonly courseService = inject(CourseService);
readonly enrolledCourses = this.courseService.enrolledCourses;
```

### Vue: Pinia Stores

```typescript
export const useCourseStore = defineStore('course', () => {
  const courses = ref<Course[]>(MOCK_COURSES)
  const enrollments = ref<Enrollment[]>([])

  // Derived state
  const enrolledCourses = computed(() => {
    const active = enrollments.value.filter(e => e.status === 'active')
    return courses.value.filter(c => active.some(e => e.courseId === c.id))
  })

  const totalCredits = computed(() =>
    enrolledCourses.value.reduce((sum, c) => sum + c.credits, 0)
  )

  // Direct mutation (Pinia allows it)
  function enroll(courseId: string, studentId: string): boolean {
    enrollments.value.push(newEnrollment)
    const course = courses.value.find(c => c.id === courseId)
    if (course) course.enrolledCount++
    return true
  }

  return { courses, enrollments, enrolledCourses, totalCredits, enroll }
})
```

**Usage in component:**
```typescript
const courseStore = useCourseStore()
// Access: courseStore.enrolledCourses  (no function call — it's a computed ref)
```

### Comparison

| Aspect | Angular Service | Pinia Store |
|--------|----------------|-------------|
| Definition | `@Injectable` class | `defineStore()` function |
| Singleton scope | `providedIn: 'root'` | Automatic (per store ID) |
| State | `signal<T>(initial)` | `ref<T>(initial)` |
| Derived state | `computed(() => ...)` | `computed(() => ...)` |
| Mutations | `signal.update(fn)` (immutable) | Direct mutation or `$patch()` |
| Access in component | `inject(ServiceClass)` | `useStoreHook()` composable |
| DevTools | Angular DevTools | Vue DevTools + Pinia DevTools (time-travel) |
| Read syntax | `this.service.enrolledCourses()` (call) | `store.enrolledCourses` (property) |

---

## 9. Summary Table

| Concept | Angular | Vue.js | Common Ground |
|---------|---------|--------|---------------|
| **Component** | Class + `@Component` decorator | `<script setup>` SFC | Both have templates, logic, scoped styles |
| **Reactivity** | Signals: `signal()`, `computed()` | Refs: `ref()`, `computed()` | Both are fine-grained reactive primitives |
| **Lifecycle** | Class methods: `ngOnInit`, `ngOnDestroy`, etc. | Functions: `onMounted`, `onUnmounted`, etc. | Same conceptual phases (create → mount → update → destroy) |
| **Routing** | `@angular/router` with `provideRouter()` | `vue-router` with `createRouter()` | Both support lazy loading, params, guards, redirects |
| **Props (in)** | `input()` / `input.required()` | `defineProps<{}>()` | Both are typed, one-way-down bindings |
| **Events (out)** | `output<T>()` + `.emit()` | `defineEmits<{}>()` + `emit()` | Both emit typed events up to parent |
| **Guards** | `CanActivateFn` on `canActivate` | `NavigationGuard` on `beforeEnter` | Both are functions returning `true` or redirect |
| **Pipes / Transforms** | `@Pipe` class + `\|` in template | Composable function + call in template | Both transform display data without mutation |
| **State management** | `@Injectable` service + signals | Pinia `defineStore()` + refs | Both are singleton stores with reactive derived state |
| **DI / Access** | `inject(Token)` — framework DI container | `useStore()` / `useComposable()` — function call | Both provide singleton access in components |
| **Template syntax** | `[prop]`, `(event)`, `@if`, `@for` | `:prop`, `@event`, `v-if`, `v-for` | Both bind data and handle events declaratively |
| **Style scoping** | `ViewEncapsulation.Emulated` (default) | `<style scoped>` | Both prevent style leakage between components |
| **Lazy loading** | `loadComponent: () => import(...)` | `component: () => import(...)` | Both produce separate chunks for code-splitting |

---

## Final Perspective

Coming from Angular, the biggest mental shifts when working with Vue are:

1. **No classes, no decorators** — Everything in Vue 3 Composition API is functions. There's no `@Component`, no `@Injectable`, no `@Pipe`. You define reactive state with `ref()`, derive values with `computed()`, and export functions.

2. **No DI container** — Angular's hierarchical dependency injection is replaced by simple function imports. Pinia stores are singletons by convention, not by framework DI mechanics.

3. **No pipe syntax** — The `|` operator doesn't exist in Vue templates. Call formatting functions directly. This is more explicit but less "magical".

4. **Single-file components** — Instead of 3 files per component, Vue co-locates everything in one `.vue` file. This feels unfamiliar at first but reduces context-switching.

5. **Signals ≈ Refs** — Angular's `signal()` and Vue's `ref()` are conceptually identical. Both are reactive containers that trigger re-renders. Angular reads with `signal()` (function call), Vue reads with `ref.value` (property access, auto-unwrapped in templates).

The core principles — component architecture, unidirectional data flow, reactive state, route protection — are the same in both frameworks. The difference is in the API surface: Angular is convention-heavy with decorators and DI, Vue is function-heavy with composition and explicit imports.
