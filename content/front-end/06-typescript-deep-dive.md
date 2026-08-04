# TypeScript for Front-End Interviews — Complete Guide

> All code samples are from the **angular-student-dashboard** application.
> Each section links to the exact source file so you can read the full context.

---

## Table of Contents

1. [Interfaces, Types & Classes — When to Use Which](#1-interfaces-types--classes)
2. [Enums & Const Enums](#2-enums--const-enums)
3. [Union & Intersection Types](#3-union--intersection-types)
4. [Discriminated Unions & Exhaustive Matching](#4-discriminated-unions--exhaustive-matching)
5. [Type Guards & Narrowing](#5-type-guards--narrowing)
6. [Generics](#6-generics)
7. [Utility Types](#7-utility-types)
8. [Mapped & Conditional Types](#8-mapped--conditional-types)
9. [Template Literal Types](#9-template-literal-types)
10. [keyof, typeof & Indexed Access Types](#10-keyof-typeof--indexed-access-types)
11. [The `satisfies` Operator](#11-the-satisfies-operator)
12. [Access Modifiers & Abstract Classes](#12-access-modifiers--abstract-classes)
13. [Function Overloads](#13-function-overloads)
14. [Decorators](#14-decorators)
15. [`never`, `unknown` & Type Assertion Functions](#15-never-unknown--type-assertion-functions)
16. [Branded (Nominal) Types](#16-branded-nominal-types)
17. [Common Interview Questions & Answers](#17-common-interview-questions--answers)

---

## 1. Interfaces, Types & Classes

> **Source:** `src/app/models/course.model.ts`, `src/app/models/student.model.ts`

### Interface

Interfaces define the *shape* of an object. They are open — you can re-declare them and they will merge (declaration merging).

```typescript
// src/app/models/student.model.ts:11-18
export interface Student {
  id: string;
  name: string;
  email: string;
  gpa: number;
  enrollmentYear: number;
  major: string;
}
```

### Type Alias

Type aliases name any type — primitives, unions, intersections, tuples, mapped types. They **cannot** be re-opened for merging.

```typescript
// src/app/models/course.model.ts:29
export type CourseCategory = 'math' | 'science' | 'humanities' | 'engineering' | 'arts';
```

### Class

Classes produce both a **type** (the instance shape) and a **value** (the constructor). They can implement interfaces.

```typescript
// src/app/models/student.model.ts:61-74
export class StudentUser extends BaseUser implements Student {
  private _loginCount = 0;

  constructor(
    id: string,
    name: string,
    email: string,
    public readonly gpa: number,
    public readonly enrollmentYear: number,
    public readonly major: string,
  ) {
    super(id, name, email);
  }
  // ...
}
```

### When to use which

| Feature | Interface | Type Alias | Class |
|---|---|---|---|
| Object shape | Yes | Yes | Yes |
| Union / intersection | No | Yes | No |
| Declaration merging | Yes | No | No |
| `implements` | Yes | Yes* | Yes |
| `extends` | Yes (interfaces) | No (use `&`) | Yes (single class) |
| Runtime value | No | No | Yes |
| Instantiation (`new`) | No | No | Yes |
| Mapped types | No | Yes | No |

\* A class can `implements` a type alias if it describes an object shape.

**Rule of thumb:**
- Use **interfaces** for object shapes (DTOs, API contracts, component props).
- Use **type aliases** for unions, intersections, mapped types, conditional types.
- Use **classes** when you need instances with behavior (methods, constructors, inheritance).

> **Official docs:** [Interfaces vs Types](https://www.typescriptlang.org/docs/handbook/2/everyday-types.html#differences-between-type-aliases-and-interfaces)

---

## 2. Enums & Const Enums

> **Source:** `src/app/models/course.model.ts:11-25`

### Regular Enum

Compiled to a JavaScript object with forward and reverse mapping. You can iterate over values at runtime.

```typescript
// src/app/models/course.model.ts:11-16
export enum CourseLevel {
  Introductory = 100,
  Intermediate = 200,
  Advanced = 300,
  Graduate = 400,
}
```

Compiled output:
```javascript
var CourseLevel;
(function (CourseLevel) {
  CourseLevel[CourseLevel["Introductory"] = 100] = "Introductory";
  CourseLevel[CourseLevel["Intermediate"] = 200] = "Intermediate";
  // ...
})(CourseLevel || (CourseLevel = {}));
```

### Const Enum

Fully erased at compile-time. Every usage is replaced with the literal value. **Smaller bundle**, but no reverse-mapping or runtime iteration.

```typescript
// src/app/models/course.model.ts:20-25
export const enum CreditWeight {
  Light = 1,
  Standard = 3,
  Heavy = 4,
  DoubleHeavy = 6,
}
```

### String Literal Union (preferred alternative)

Many teams prefer string literal unions over string enums because they don't create runtime overhead and work naturally with JSON:

```typescript
// src/app/models/course.model.ts:29
export type CourseCategory = 'math' | 'science' | 'humanities' | 'engineering' | 'arts';
```

### Comparison

| Feature | `enum` | `const enum` | String union |
|---|---|---|---|
| Runtime object | Yes | No (erased) | No |
| Reverse mapping | Yes (numeric) | No | No |
| Bundle size | Larger | Zero | Zero |
| JSON-friendly | String enums yes | No | Yes |
| Iterable at runtime | Yes | No | No |
| Tree-shakable | No | Yes | Yes |

> **Official docs:** [Enums](https://www.typescriptlang.org/docs/handbook/enums.html)

---

## 3. Union & Intersection Types

> **Source:** `src/app/models/course.model.ts:29, 54-59, 97`

### Union Type (`|`)

A value can be **one of** several types. Used for parameters that accept multiple types.

```typescript
// src/app/models/course.model.ts:97
export type EnrollmentStatus = 'active' | 'dropped' | 'completed' | 'waitlisted';
```

```typescript
// src/app/services/auth.service.ts:13
private readonly currentStudent = signal<Student | null>(null);
```

### Intersection Type (`&`)

Combines multiple types into one — the resulting type has **all** properties of each constituent.

```typescript
// src/app/models/course.model.ts:54-59
type Timestamped = {
  createdAt: Date;
  updatedAt: Date;
};

export type CourseWithTimestamps = Course & Timestamped;
// Result: has ALL Course properties + createdAt + updatedAt
```

### Union vs Intersection

| Operator | Meaning | Values | Properties |
|---|---|---|---|
| `A \| B` | A **or** B | Wider set (more values) | Narrower (only shared properties safe) |
| `A & B` | A **and** B | Narrower set (fewer values) | Wider (all properties from both) |

### Interface Extension vs Intersection

```typescript
// Interface extension — for interfaces only:
export interface GraduateStudent extends Student {
  advisor: string;
  thesisTitle: string;
}

// Intersection — works with any type alias:
type CourseWithTimestamps = Course & Timestamped;
```

Both produce the same shape, but `extends` gives better error messages on conflicts.

> **Official docs:** [Union Types](https://www.typescriptlang.org/docs/handbook/2/everyday-types.html#union-types), [Intersection Types](https://www.typescriptlang.org/docs/handbook/2/objects.html#intersection-types)

---

## 4. Discriminated Unions & Exhaustive Matching

> **Source:** `src/app/models/course.model.ts:99-147`, `src/app/services/course.service.ts:148-164`

A **discriminated union** is a union where each member has a common literal property (the *discriminant*) that TypeScript can use to narrow the type.

### Defining the union

```typescript
// src/app/models/course.model.ts:102-139
export interface ActiveEnrollment {
  kind: 'active';        // ← discriminant
  courseId: CourseCode;
  studentId: string;
  enrolledAt: Date;
}

export interface DroppedEnrollment {
  kind: 'dropped';       // ← discriminant
  courseId: CourseCode;
  studentId: string;
  enrolledAt: Date;
  droppedAt: Date;
  reason: string;
}

export interface CompletedEnrollment {
  kind: 'completed';     // ← discriminant
  courseId: CourseCode;
  studentId: string;
  enrolledAt: Date;
  completedAt: Date;
  grade: string;
}

export interface WaitlistedEnrollment {
  kind: 'waitlisted';    // ← discriminant
  courseId: CourseCode;
  studentId: string;
  enrolledAt: Date;
  position: number;
}

export type Enrollment =
  | ActiveEnrollment
  | DroppedEnrollment
  | CompletedEnrollment
  | WaitlistedEnrollment;
```

### Exhaustive switch with `never`

```typescript
// src/app/models/course.model.ts:145-147
export function assertNever(value: never): never {
  throw new Error(`Unhandled discriminated union member: ${JSON.stringify(value)}`);
}
```

```typescript
// src/app/services/course.service.ts:151-164
getEnrollmentLabel(enrollment: Enrollment): string {
  switch (enrollment.kind) {
    case 'active':
      return `Enrolled since ${enrollment.enrolledAt.toLocaleDateString()}`;
    case 'dropped':
      return `Dropped: ${enrollment.reason}`;     // TS knows `reason` exists
    case 'completed':
      return `Completed — Grade: ${enrollment.grade}`;  // TS knows `grade` exists
    case 'waitlisted':
      return `Waitlisted (position #${enrollment.position})`;
    default:
      return assertNever(enrollment);  // compile error if a case is missing
  }
}
```

If you add a new member to `Enrollment` (e.g., `TransferredEnrollment`) and forget to handle it, the `default` branch receives a non-`never` type, and the compiler emits an error.

> **Official docs:** [Discriminated Unions](https://www.typescriptlang.org/docs/handbook/2/narrowing.html#discriminated-unions)

---

## 5. Type Guards & Narrowing

> **Source:** `src/app/models/student.model.ts:120-122`, `src/app/services/course.service.ts:120-121`, `src/app/services/auth.service.ts:70-92`

TypeScript narrows types through control-flow analysis. You can also write custom type guards.

### Built-in narrowing techniques

```typescript
// typeof check
if (typeof value === 'string') { /* value is string here */ }

// instanceof check
if (value instanceof Date) { /* value is Date here */ }

// truthiness check — src/app/services/auth.service.ts
const student = this.currentStudent();
if (student) { /* student is Student (not null) */ }

// `in` operator check
if ('advisor' in student) { /* student has advisor property */ }

// optional chaining + nullish coalescing
enrollment?.kind ?? null;  // src/app/services/course.service.ts:170
```

### User-defined type guard (`is` keyword)

Returns a *type predicate* that narrows the type for the caller:

```typescript
// src/app/models/student.model.ts:120-122
export function isGraduateStudent(student: Student): student is GraduateStudent {
  return 'advisor' in student && 'thesisTitle' in student;
}
```

### Type predicate in filter callbacks

```typescript
// src/app/services/course.service.ts:120-122
const activeEnrollments = this.enrollmentsSignal().filter(
  (e): e is ActiveEnrollment => e.kind === 'active'
);
// activeEnrollments is ActiveEnrollment[] — not Enrollment[]
```

### `as` type assertions

Use sparingly — they bypass the compiler:

```typescript
// src/app/pages/login/login.component.ts:118
this.email.set((event.target as HTMLInputElement).value);

// src/app/services/course.service.ts:26
id: 'CS-101' as CourseCode,
```

> **Official docs:** [Narrowing](https://www.typescriptlang.org/docs/handbook/2/narrowing.html)

---

## 6. Generics

> **Source:** `src/app/utils/collection.ts`, `src/app/models/student.model.ts:126-128`

### Generic function

```typescript
// src/app/models/student.model.ts:126-128
export function findById<T extends { id: string }>(items: T[], id: string): T | undefined {
  return items.find(item => item.id === id);
}
```

The constraint `T extends { id: string }` ensures `T` has an `id` property. The function works with `Student[]`, `Course[]`, or any identifiable array.

### Generic class

```typescript
// src/app/utils/collection.ts:18-87
export class Collection<T extends Identifiable> {
  private items: Map<string, T> = new Map();

  get(id: string): T | undefined {
    return this.items.get(id);
  }

  getAll(): readonly T[] {
    return Array.from(this.items.values());
  }

  // Accepts Partial<T> — generic utility type composition
  update(id: string, updates: Partial<T>): T | undefined {
    const existing = this.items.get(id);
    if (!existing) return undefined;
    const updated = { ...existing, ...updates };
    this.items.set(id, updated);
    return updated;
  }

  // Generic method with a DIFFERENT type parameter U
  map<U>(transform: (item: T) => U): U[] {
    return Array.from(this.items.values()).map(transform);
  }
}
```

### Generic function with multiple type parameters

```typescript
// src/app/utils/collection.ts:91-107
export function innerJoin<A extends Identifiable, B extends Identifiable>(
  collectionA: Collection<A>,
  collectionB: Collection<B>,
  matchFn: (a: A, b: B) => boolean,
): Array<{ left: A; right: B }> {
  const results: Array<{ left: A; right: B }> = [];
  for (const a of collectionA.getAll()) {
    for (const b of collectionB.getAll()) {
      if (matchFn(a, b)) {
        results.push({ left: a, right: b });
      }
    }
  }
  return results;
}
```

### Generic interface with default

```typescript
// src/app/utils/collection.ts:111-117
export interface PaginatedResult<T = unknown> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
  hasNext: boolean;
}
// PaginatedResult           → items is unknown[]
// PaginatedResult<Course>   → items is Course[]
```

### Angular signals use generics

```typescript
// src/app/services/course.service.ts:113-114
private readonly coursesSignal = signal<Course[]>(MOCK_COURSES);
private readonly enrollmentsSignal = signal<Enrollment[]>([]);
```

> **Official docs:** [Generics](https://www.typescriptlang.org/docs/handbook/2/generics.html)

---

## 7. Utility Types

> **Source:** `src/app/models/course.model.ts:61-78`

TypeScript provides built-in generic types that transform other types.

```typescript
// src/app/models/course.model.ts:62-78

// Partial<T> — all properties become optional
export type CourseUpdatePayload = Partial<Pick<Course, 'name' | 'description' | 'maxStudents' | 'schedule'>>;

// Pick<T, K> — select a subset of properties
export type CourseSummary = Pick<Course, 'id' | 'name' | 'credits' | 'category'>;

// Omit<T, K> — exclude specific properties
export type CourseCreatePayload = Omit<Course, 'id' | 'enrolledCount'>;

// Required<T> — make all properties required (reverses Partial)
export type RequiredCourseUpdate = Required<CourseUpdatePayload>;

// Readonly<T> — make all properties readonly
export type FrozenCourse = Readonly<Course>;

// Record<K, V> — construct a type with keys K and values V
export type CategoryLabels = Record<CourseCategory, string>;
```

### Practical usage in services

```typescript
// src/app/services/course.service.ts:241-245
getCourseSummaries(): CourseSummary[] {
  return this.coursesSignal().map(({ id, name, credits, category }) => ({
    id, name, credits, category,
  }));
}
```

```typescript
// src/app/utils/collection.ts:40-47
update(id: string, updates: Partial<T>): T | undefined {
  const existing = this.items.get(id);
  if (!existing) return undefined;
  const updated = { ...existing, ...updates };
  this.items.set(id, updated);
  return updated;
}
```

### Quick reference

| Utility | Effect | Example |
|---|---|---|
| `Partial<T>` | All props optional | Form edit states |
| `Required<T>` | All props required | Validated payloads |
| `Readonly<T>` | All props readonly | Immutable state |
| `Pick<T, K>` | Keep only K props | API response shaping |
| `Omit<T, K>` | Remove K props | Create payloads (no id) |
| `Record<K, V>` | Map keys → values | Lookup tables |
| `Extract<U, T>` | Keep members matching T | Filter union members |
| `Exclude<U, T>` | Remove members matching T | Filter union members |
| `ReturnType<F>` | Get return type of function | Type inference from functions |
| `Parameters<F>` | Get parameter types tuple | Type inference from functions |

> **Official docs:** [Utility Types](https://www.typescriptlang.org/docs/handbook/utility-types.html)

---

## 8. Mapped & Conditional Types

> **Source:** `src/app/models/course.model.ts:158-170`, `src/app/utils/type-utils.ts`

### Mapped Type

Iterates over keys of a type and transforms each one:

```typescript
// src/app/models/course.model.ts:159-161
export type Nullable<T> = {
  [P in keyof T]: T[P] | null;
};

export type NullableCourse = Nullable<CourseSummary>;
// { id: CourseCode | null; name: string | null; credits: number | null; category: CourseCategory | null }
```

### Mapped type with key remapping (template literal)

```typescript
// src/app/utils/type-utils.ts:49-51
export type Getters<T> = {
  [P in keyof T as `get${Capitalize<string & P>}`]: () => T[P];
};
// Getters<{name: string; age: number}>
// → { getName: () => string; getAge: () => number }
```

### Recursive Mapped Types

```typescript
// src/app/utils/type-utils.ts:13-19
export type DeepReadonly<T> = {
  readonly [P in keyof T]: T[P] extends object
    ? T[P] extends Function
      ? T[P]
      : DeepReadonly<T[P]>
    : T[P];
};
```

### Conditional Type

Resolves to different types based on a condition — like a ternary at the type level:

```typescript
// src/app/models/course.model.ts:167
export type IsString<T> = T extends string ? true : false;
// IsString<'hello'>  → true
// IsString<42>       → false
```

### Conditional type with `infer`

The `infer` keyword lets you extract a type variable from within a conditional type:

```typescript
// src/app/models/course.model.ts:170
export type ArrayElement<T> = T extends readonly (infer U)[] ? U : never;
// ArrayElement<string[]>  → string
// ArrayElement<Course['prerequisiteIds']>  → CourseCode
```

```typescript
// src/app/utils/type-utils.ts:57
export type UnwrapPromise<T> = T extends Promise<infer U> ? U : T;
// UnwrapPromise<Promise<string>>  → string
// UnwrapPromise<number>           → number
```

### Custom utility: PartialBy

```typescript
// src/app/models/student.model.ts:132
export type PartialBy<T, K extends keyof T> = Omit<T, K> & Partial<Pick<T, K>>;

export type StudentCreatePayload = PartialBy<Student, 'id'>;
// All Student fields required EXCEPT `id` which is optional
```

> **Official docs:** [Mapped Types](https://www.typescriptlang.org/docs/handbook/2/mapped-types.html), [Conditional Types](https://www.typescriptlang.org/docs/handbook/2/conditional-types.html)

---

## 9. Template Literal Types

> **Source:** `src/app/models/course.model.ts:33`, `src/app/utils/type-utils.ts:46, 49-51`

Template literal types combine string literal types using template syntax:

```typescript
// src/app/models/course.model.ts:33
export type CourseCode = `${Uppercase<string>}-${number}`;
// Valid: 'CS-101', 'MATH-201'
// Invalid: 'cs-101' (lowercase), '101' (no dash)
```

### Generating event names

```typescript
// src/app/utils/type-utils.ts:46
export type EventName<T extends string> = `on${Capitalize<T>}`;
// EventName<'click' | 'hover'>  → 'onClick' | 'onHover'
```

### Key remapping in mapped types

```typescript
// src/app/utils/type-utils.ts:49-51
export type Getters<T> = {
  [P in keyof T as `get${Capitalize<string & P>}`]: () => T[P];
};
```

> **Official docs:** [Template Literal Types](https://www.typescriptlang.org/docs/handbook/2/template-literal-types.html)

---

## 10. keyof, typeof & Indexed Access Types

> **Source:** `src/app/models/course.model.ts:94, 151, 155`

### `keyof` — union of property names

```typescript
// src/app/models/course.model.ts:155
export type CourseProperty = keyof Course;
// 'id' | 'name' | 'instructor' | 'description' | 'credits' | ...
```

### `typeof` — extract type from a runtime value

```typescript
// src/app/models/course.model.ts:94
export type CategoryLabelKeys = keyof typeof CATEGORY_LABELS;
// 'math' | 'science' | 'humanities' | 'engineering' | 'arts'
```

### Indexed access types

```typescript
// src/app/models/course.model.ts:151
export type EnrollmentKind = Enrollment['kind'];
// 'active' | 'dropped' | 'completed' | 'waitlisted'
```

### Combining `keyof` with generics

```typescript
// src/app/models/student.model.ts:110
export function formatStudentField(student: Student, field: keyof Student): string | number {
  const value = student[field];  // type: Student[keyof Student]
  // ...
}
```

> **Official docs:** [Keyof](https://www.typescriptlang.org/docs/handbook/2/keyof-types.html), [Typeof](https://www.typescriptlang.org/docs/handbook/2/typeof-types.html), [Indexed Access](https://www.typescriptlang.org/docs/handbook/2/indexed-access-types.html)

---

## 11. The `satisfies` Operator

> **Source:** `src/app/models/course.model.ts:84-90`, `src/app/services/course.service.ts:189, 195, 234`

`satisfies` (TypeScript 4.9+) validates that a value matches a type **without widening** it. The variable retains its narrower literal types.

### Without `satisfies`

```typescript
// Using type annotation — widens to Record<CourseCategory, string>
const labels: CategoryLabels = {
  math: 'Mathematics',
  // ...
};
labels.math  // type: string (widened — lost literal 'Mathematics')
```

### With `satisfies`

```typescript
// src/app/models/course.model.ts:84-90
export const CATEGORY_LABELS = {
  math: 'Mathematics',
  science: 'Natural Sciences',
  humanities: 'Humanities',
  engineering: 'Engineering & CS',
  arts: 'Fine Arts',
} satisfies CategoryLabels;

CATEGORY_LABELS.math  // type: 'Mathematics' (literal preserved!)
```

### In object construction

```typescript
// src/app/services/course.service.ts:189, 195
const enrollment: Enrollment = isFull
  ? {
      kind: 'waitlisted',
      courseId: courseId as CourseCode,
      studentId,
      enrolledAt: new Date(),
      position: this.getWaitlistPosition(courseId),
    } satisfies WaitlistedEnrollment
  : {
      kind: 'active',
      courseId: courseId as CourseCode,
      studentId,
      enrolledAt: new Date(),
    } satisfies ActiveEnrollment;
```

> **Official docs:** [satisfies Operator](https://www.typescriptlang.org/docs/handbook/release-notes/typescript-4-9.html#the-satisfies-operator)

---

## 12. Access Modifiers & Abstract Classes

> **Source:** `src/app/models/student.model.ts:31-101`

### Access Modifiers

| Modifier | Class | Subclass | Outside |
|---|---|---|---|
| `public` (default) | Yes | Yes | Yes |
| `protected` | Yes | Yes | No |
| `private` | Yes | No | No |
| `readonly` | Cannot be reassigned after initialization |

### Abstract Class

Cannot be instantiated directly. Defines a contract that subclasses must fulfill.

```typescript
// src/app/models/student.model.ts:31-57
export abstract class BaseUser {
  constructor(
    public readonly id: string,        // public + readonly
    public readonly name: string,
    public readonly email: string,
  ) {}

  // protected — accessible in subclasses but not from outside
  protected lastActivityTimestamp: Date = new Date();

  // Concrete method — shared by all subclasses
  getDisplayName(): string {
    return `${this.name} (${this.id})`;
  }

  // Abstract method — MUST be implemented by subclasses
  abstract getRole(): string;

  // Abstract getter
  abstract get permissions(): readonly string[];
}
```

### Concrete subclass

```typescript
// src/app/models/student.model.ts:61-101
export class StudentUser extends BaseUser implements Student {
  // private — only accessible within this class
  private _loginCount = 0;

  constructor(
    id: string, name: string, email: string,
    public readonly gpa: number,           // constructor parameter property
    public readonly enrollmentYear: number,
    public readonly major: string,
  ) {
    super(id, name, email);
  }

  // Implement abstract method
  getRole(): string {
    return 'student';
  }

  // Implement abstract getter — `as const` gives readonly tuple type
  get permissions(): readonly string[] {
    return ['view:courses', 'enroll:courses', 'view:grades'] as const;
  }

  // Getter — accessed like a property: user.loginCount
  get loginCount(): number {
    return this._loginCount;
  }

  recordLogin(): void {
    this._loginCount++;
    this.lastActivityTimestamp = new Date();  // can access protected
  }
}
```

### Usage in AuthService

```typescript
// src/app/services/auth.service.ts:34-47
const user = new StudentUser(
  studentData.id, studentData.name, studentData.email,
  studentData.gpa, studentData.enrollmentYear, studentData.major,
);
user.recordLogin();
console.log(user.getDisplayName());  // "Alice Johnson (student-1)"
console.log(user.getRole());         // "student"
console.log(user.permissions);       // ['view:courses', 'enroll:courses', 'view:grades']
```

> **Official docs:** [Classes](https://www.typescriptlang.org/docs/handbook/2/classes.html)

---

## 13. Function Overloads

> **Source:** `src/app/models/student.model.ts:107-116`, `src/app/utils/collection.ts:78-86`

Function overloads let a single function have multiple call signatures with different parameter/return types.

### Declaration

```typescript
// src/app/models/student.model.ts:107-116
export function formatStudentField(student: Student, field: 'name'): string;
export function formatStudentField(student: Student, field: 'gpa'): number;
export function formatStudentField(student: Student, field: 'enrollmentYear'): number;
export function formatStudentField(student: Student, field: keyof Student): string | number {
  const value = student[field];
  if (typeof value === 'number') {
    return field === 'gpa' ? Math.round(value * 100) / 100 : value;
  }
  return String(value);
}
```

### How it works

- The **overload signatures** (lines 1-3) are what callers see. TypeScript selects the matching signature.
- The **implementation signature** (line 4) is hidden from callers. It must be compatible with all overloads.

```typescript
formatStudentField(student, 'name');            // returns string
formatStudentField(student, 'gpa');             // returns number
formatStudentField(student, 'enrollmentYear');  // returns number
```

### Method overloads on a class

```typescript
// src/app/utils/collection.ts:78-86
getOrThrow(id: string, errorMessage?: string): T;
getOrThrow(id: string, errorMessage: string): T;
getOrThrow(id: string, errorMessage = 'Item not found'): T {
  const item = this.items.get(id);
  if (!item) {
    throw new Error(`${errorMessage}: ${id}`);
  }
  return item;
}
```

> **Official docs:** [Function Overloads](https://www.typescriptlang.org/docs/handbook/2/functions.html#function-overloads)

---

## 14. Decorators

> **Source:** `src/app/decorators/index.ts`, `src/app/services/course.service.ts`

Decorators are functions that modify classes, methods, or properties at definition time. Angular uses them extensively (`@Component`, `@Injectable`, `@Pipe`).

### Angular built-in decorators

```typescript
// Class decorator — marks a class as an Angular component
@Component({
  selector: 'app-course-card',
  imports: [CategoryLabelPipe, CreditFormatPipe],
  templateUrl: './course-card.component.html',
  styleUrl: './course-card.component.scss',
})
export class CourseCardComponent { }

// Class decorator — marks a class as an injectable service
@Injectable({ providedIn: 'root' })
export class CourseService { }

// Class decorator — marks a class as a pipe
@Pipe({ name: 'categoryLabel' })
export class CategoryLabelPipe implements PipeTransform { }
```

### Custom method decorator: `@Log`

Wraps a method to log its calls — useful for debugging:

```typescript
// src/app/decorators/index.ts:13-29
export function Log(
  target: object,
  propertyKey: string,
  descriptor: PropertyDescriptor,
): PropertyDescriptor {
  const originalMethod = descriptor.value as (...args: unknown[]) => unknown;

  descriptor.value = function (this: unknown, ...args: unknown[]): unknown {
    const className = target.constructor.name;
    console.log(`[${className}.${propertyKey}] called with:`, args);
    const result = originalMethod.apply(this, args);
    console.log(`[${className}.${propertyKey}] returned:`, result);
    return result;
  };

  return descriptor;
}
```

**Usage:**
```typescript
// src/app/services/course.service.ts:174-175
@Log
enroll(courseId: string, studentId: string): boolean { ... }
```

### Custom method decorator: `@Memoize`

Caches results based on arguments:

```typescript
// src/app/decorators/index.ts:34-54
export function Memoize(
  _target: object,
  propertyKey: string,
  descriptor: PropertyDescriptor,
): PropertyDescriptor {
  const originalMethod = descriptor.value as (...args: unknown[]) => unknown;
  const cache = new Map<string, unknown>();

  descriptor.value = function (this: unknown, ...args: unknown[]): unknown {
    const key = JSON.stringify(args);
    if (cache.has(key)) {
      console.log(`[Memoize] Cache hit for ${propertyKey}(${key})`);
      return cache.get(key);
    }
    const result = originalMethod.apply(this, args);
    cache.set(key, result);
    return result;
  };

  return descriptor;
}
```

### Custom class decorator: `@Sealed`

```typescript
// src/app/decorators/index.ts:59-62
export function Sealed(constructor: Function): void {
  Object.seal(constructor);
  Object.seal(constructor.prototype);
}
```

### Custom property decorator: `@MinValue`

Decorator factory (parameterized decorator):

```typescript
// src/app/decorators/index.ts:67-85
export function MinValue(min: number) {
  return function (target: object, propertyKey: string): void {
    let currentValue: number;

    Object.defineProperty(target, propertyKey, {
      get: () => currentValue,
      set: (newValue: number) => {
        if (newValue < min) {
          throw new Error(
            `Property "${propertyKey}" cannot be less than ${min}. Got: ${newValue}`,
          );
        }
        currentValue = newValue;
      },
      enumerable: true,
      configurable: true,
    });
  };
}
```

### Decorator types summary

| Type | Signature | Applied to |
|---|---|---|
| Class | `(constructor: Function) => void` | `@Sealed class Foo {}` |
| Method | `(target, key, descriptor) => descriptor` | `@Log method() {}` |
| Property | `(target, key) => void` | `@MinValue(0) credits` |
| Parameter | `(target, key, index) => void` | `method(@Inject(TOKEN) dep)` |

> **Note:** TypeScript requires `experimentalDecorators: true` in `tsconfig.json` for legacy decorators. TC39 Stage 3 decorators use a different API.

> **Official docs:** [Decorators](https://www.typescriptlang.org/docs/handbook/decorators.html)

---

## 15. `never`, `unknown` & Type Assertion Functions

> **Source:** `src/app/utils/type-utils.ts:69-95, 100-112`, `src/app/services/auth.service.ts:61-92`

### `never` — the bottom type

Represents values that **never occur**. A function returning `never` never returns normally (it throws or loops forever).

```typescript
// src/app/models/course.model.ts:145-147
export function assertNever(value: never): never {
  throw new Error(`Unhandled discriminated union member: ${JSON.stringify(value)}`);
}
```

Common uses:
- Exhaustive switch checks (see [Section 4](#4-discriminated-unions--exhaustive-matching))
- Functions that always throw
- Impossible code paths

### `unknown` — the top type

The type-safe counterpart of `any`. You **must narrow** `unknown` before using it:

```typescript
// src/app/utils/type-utils.ts:100-112
export function processUnknown(value: unknown): string {
  // value.toString()  ← compiler ERROR — cannot use unknown directly

  if (typeof value === 'string') {
    return value.toUpperCase();          // narrowed to string
  }
  if (typeof value === 'number') {
    return value.toFixed(2);             // narrowed to number
  }
  if (value instanceof Date) {
    return value.toISOString();          // narrowed to Date
  }
  return String(value);
}
```

### Practical `unknown` usage — parsing external data

```typescript
// src/app/services/auth.service.ts:70-92
parseExternalAuth(response: unknown): Student | null {
  if (
    typeof response === 'object' &&
    response !== null &&
    'id' in response &&
    'name' in response &&
    'email' in response
  ) {
    const data = response as Record<string, unknown>;
    return {
      id: String(data['id']),
      name: String(data['name']),
      email: String(data['email']),
      gpa: typeof data['gpa'] === 'number' ? data['gpa'] : 0,
      // ...
    };
  }
  return null;
}
```

### `any` vs `unknown`

| Feature | `any` | `unknown` |
|---|---|---|
| Assignable from anything | Yes | Yes |
| Assignable to anything | Yes | **No** (must narrow first) |
| Property access | Allowed (unsafe) | **Blocked** until narrowed |
| Use in interviews | Red flag | Best practice |

### Type assertion functions (`asserts`)

```typescript
// src/app/utils/type-utils.ts:88-95
export function assertDefined<T>(
  value: T | null | undefined,
  message = 'Value is null or undefined',
): asserts value is T {
  if (value === null || value === undefined) {
    throw new Error(message);
  }
}
```

After calling `assertDefined`, TypeScript narrows the type in the calling scope:

```typescript
// src/app/services/auth.service.ts:61-66
getAuthenticatedStudent(): Student {
  const student = this.currentStudent();    // Student | null
  assertDefined(student, 'User is not authenticated');
  return student;                            // Student (null removed)
}
```

> **Official docs:** [never](https://www.typescriptlang.org/docs/handbook/2/narrowing.html#the-never-type), [unknown](https://www.typescriptlang.org/docs/handbook/2/functions.html#unknown)

---

## 16. Branded (Nominal) Types

> **Source:** `src/app/utils/type-utils.ts:73-84`

TypeScript uses **structural typing** — two types with the same shape are compatible. Branded types add an invisible marker to create **nominal** distinction:

```typescript
// src/app/utils/type-utils.ts:77-84
declare const __brand: unique symbol;
export type Brand<T, B extends string> = T & { readonly [__brand]: B };

export type StudentId = Brand<string, 'StudentId'>;
export type CourseId = Brand<string, 'CourseId'>;
```

Now `StudentId` and `CourseId` are both strings structurally, but the compiler treats them as incompatible:

```typescript
function getStudent(id: StudentId): Student { ... }
function getCourse(id: CourseId): Course { ... }

const sid = 'student-1' as StudentId;
const cid = 'CS-101' as CourseId;

getStudent(sid);  // OK
getStudent(cid);  // ERROR — CourseId is not assignable to StudentId
```

This prevents accidental parameter swapping in functions with multiple string IDs — a common bug source.

> **Official docs:** (Community pattern — no official page. See [TypeScript playground example](https://www.typescriptlang.org/play))

---

## 17. Common Interview Questions & Answers

### Q1: What's the difference between `interface` and `type`?

**Answer:** Both describe object shapes. Interfaces support declaration merging and `extends`; type aliases support unions, intersections, mapped types, and conditional types. Use interfaces for public API contracts, types for complex type transformations. See [Section 1](#1-interfaces-types--classes).

### Q2: What's the difference between `any` and `unknown`?

**Answer:** Both accept any value, but `unknown` requires narrowing before use. `any` disables type checking entirely. Always prefer `unknown` for values of uncertain type. See [Section 15](#15-never-unknown--type-assertion-functions).

### Q3: What is a discriminated union and why use it?

**Answer:** A union type where each member has a common literal property (discriminant). It enables exhaustive pattern matching in switch statements. If you forget a case, the compiler catches it. See [Section 4](#4-discriminated-unions--exhaustive-matching).

### Q4: How do generics work? When would you use them?

**Answer:** Generics are type parameters that make functions, classes, and interfaces work with any type while preserving type safety. Use them for reusable containers (`Collection<T>`), utility functions (`findById<T>`), and any code that operates on a shape rather than a specific type. See [Section 6](#6-generics).

### Q5: What's the difference between `enum` and string literal union?

**Answer:** Enums generate runtime JavaScript objects; string unions are erased at compile-time. Unions are lighter and JSON-friendly. Use enums when you need runtime iteration or reverse-mapping; unions for everything else. See [Section 2](#2-enums--const-enums).

### Q6: What are type guards?

**Answer:** Expressions or functions that narrow a type within a conditional block. Built-in: `typeof`, `instanceof`, `in`, truthiness checks. User-defined: functions returning `value is Type`. See [Section 5](#5-type-guards--narrowing).

### Q7: Explain `Partial`, `Pick`, `Omit`, `Record`.

**Answer:** Built-in utility types. `Partial<T>` makes all props optional. `Pick<T, K>` keeps only K props. `Omit<T, K>` removes K props. `Record<K, V>` creates a type mapping K keys to V values. See [Section 7](#7-utility-types).

### Q8: What is `never` used for?

**Answer:** `never` represents impossible values. Used for exhaustive checks (unhandled switch cases), functions that always throw, and conditional types that filter out members. See [Section 15](#15-never-unknown--type-assertion-functions).

### Q9: What does `satisfies` do?

**Answer:** Validates that a value matches a type without widening it. The value keeps its literal types for autocomplete and type inference, while the compiler still checks the constraint. See [Section 11](#11-the-satisfies-operator).

### Q10: How do decorators work in TypeScript?

**Answer:** Decorators are functions applied to classes, methods, properties, or parameters at definition time. They receive metadata about the target and can modify behavior. Angular uses them for `@Component`, `@Injectable`, `@Pipe`. Custom decorators can add logging, caching, or validation. See [Section 14](#14-decorators).

### Q11: What are mapped types?

**Answer:** Types that iterate over the keys of another type and transform each one. Built-in examples: `Partial`, `Readonly`, `Required`. Custom: `Nullable<T>`, `DeepReadonly<T>`. Can use `as` for key remapping. See [Section 8](#8-mapped--conditional-types).

### Q12: How does structural typing differ from nominal typing?

**Answer:** TypeScript uses structural typing — two types are compatible if their shapes match, regardless of name. This differs from languages like Java (nominal typing). Branded types simulate nominal typing when needed. See [Section 16](#16-branded-nominal-types).

### Q13: What is the `infer` keyword?

**Answer:** Used within conditional types to extract and name a type variable. Examples: `UnwrapPromise<T>` extracts the resolved type from `Promise<T>`, `ArrayElement<T>` extracts the element type from an array. See [Section 8](#8-mapped--conditional-types).

### Q14: What's the difference between `readonly` properties and `Readonly<T>`?

**Answer:** `readonly` on a property prevents reassignment. `Readonly<T>` applies `readonly` to all properties of T — but only one level deep. For deep immutability, you need `DeepReadonly<T>` (custom recursive mapped type). See [Sections 7](#7-utility-types) and [8](#8-mapped--conditional-types).

---

## File Reference

| File | Key TS Features |
|---|---|
| `src/app/models/course.model.ts` | Interfaces, types, enums, const enum, discriminated union, intersection, utility types, satisfies, keyof, typeof, mapped type, conditional type, template literal type, never |
| `src/app/models/student.model.ts` | Interface extension, abstract class, access modifiers, constructor parameter properties, generics, function overloads, type guard, index signatures |
| `src/app/utils/collection.ts` | Generic class, generic constraints, generic methods, multiple type parameters, function overloads, conditional type |
| `src/app/utils/type-utils.ts` | DeepReadonly, DeepPartial, Extract, Exclude, ReturnType, Parameters, template literal types, infer, branded types, assertion functions, unknown |
| `src/app/decorators/index.ts` | Method decorator, class decorator, property decorator (factory) |
| `src/app/services/course.service.ts` | @Injectable, @Log, @Memoize, discriminated union switch, exhaustive check, type predicates, satisfies, computed signals |
| `src/app/services/auth.service.ts` | @Injectable, abstract class instantiation, assertDefined, unknown narrowing |
| `src/app/pipes/category-label.pipe.ts` | @Pipe, satisfies-checked constant |

> **External references:**
> - [TypeScript Handbook](https://www.typescriptlang.org/docs/handbook/intro.html)
> - [TypeScript Playground](https://www.typescriptlang.org/play)
> - [Angular TypeScript Configuration](https://angular.dev/reference/configs/angular-compiler-options)
