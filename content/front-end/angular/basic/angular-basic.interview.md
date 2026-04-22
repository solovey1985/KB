---
title: Angular Basic Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise Angular fundamentals based on official docs concepts.

Relevant concept maps:

- [Concept Map](angular-basic.concept.md)

## Components and Templates

```interview-question
What is an Angular component?
---
answer:
An Angular component is the basic building block of the UI.

It combines a TypeScript class, an HTML template, and component metadata so Angular can render and manage a piece of the application.
hints:
- It is the main UI unit.
- Think class plus template plus metadata.
- Components compose the app tree.
```

Related concepts: [Angular Component](angular-basic.concept.md#angular-component), [Component Tree](angular-basic.concept.md#component-tree)

```interview-question
What binding types are commonly used in Angular templates?
---
answer:
Common binding forms are interpolation, property binding, event binding, and two-way binding.

They let templates read component state and react to user interaction declaratively.
hints:
- Think reading state and reacting to events.
- `{{ }}` is one of them.
- Brackets and parentheses matter.
```

Related concepts: [Template Binding](angular-basic.concept.md#template-binding)

```interview-choice
Which syntax is used for property binding in Angular?
---
options:
- `(disabled)="isSaving"`
- `[disabled]="isSaving"`
- `{{ disabled: isSaving }}`
correct: 1
explanation:
Square brackets are used for property binding, which sets a DOM or component property from Angular state.
```

```interview-question
Why are standalone components important in modern Angular?
---
answer:
Standalone components reduce dependence on NgModules by letting components import their own dependencies directly.

This makes composition more explicit and aligns with the current Angular default development style.
hints:
- Think less NgModule ceremony.
- Dependencies are imported directly.
- Modern Angular treats this as the default path.
```

Related concepts: [Standalone Component](angular-basic.concept.md#standalone-component)

```interview-code
language: ts
prompt: Complete the component metadata so it defines a standalone component.
starter:
@Component({
  selector: 'app-example',
  
  template: `<p>Hello</p>`,
})
export class ExampleComponent {}
solution:
@Component({
  selector: 'app-example',
  standalone: true,
  template: `<p>Hello</p>`,
})
export class ExampleComponent {}
checks:
- includes: standalone: true
```
