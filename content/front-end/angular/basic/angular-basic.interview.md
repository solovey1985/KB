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

## Framework Basics

```interview-question
What is Angular and what are its key features?
---
answer:
Angular is a TypeScript-first framework for building client applications with components, templates, dependency injection, routing, forms, and HTTP tooling.

Its main value is that it gives a complete application model instead of only a view layer.
hints:
- Think full framework, not only UI library.
- Components and templates are only part of it.
- Routing, DI, and forms are core features too.
```

```interview-question
What does Angular application architecture look like at a high level?
---
answer:
An Angular app is usually organized as a tree of components started from a root application entry point.

Services provide shared logic through dependency injection, while routing, forms, and HTTP features support application behavior around that component tree.
hints:
- Start from the component tree.
- Shared logic usually does not live in every component.
- Think root app plus services plus platform features.
```

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

## Directives and Interaction

```interview-question
What are directives in Angular, and which kinds are commonly used?
---
answer:
Directives are features that attach behavior to templates or elements.

Common categories are components, attribute directives such as `NgClass` and `NgStyle`, and structural control flow such as the older `*ngIf` and `*ngFor` style or the newer built-in `@if` and `@for` blocks.
hints:
- Components are also a special directive type.
- Some directives change appearance.
- Some directives control whether or how content renders.
```

```interview-question
How do you handle events in Angular templates?
---
answer:
Angular handles template events with event binding syntax such as `(click)="save()"`.

The template listens to a DOM or component event and calls component logic when that event fires.
hints:
- Parentheses are the main clue.
- Think user interaction such as clicks and submits.
- The handler usually calls a component method.
```

```interview-choice
Which syntax listens for a button click in Angular?
---
options:
- `[click]="save()"`
- `(click)="save()"`
- `{{ click: save() }}`
correct: 1
explanation:
Event binding uses parentheses, so `(click)="save()"` runs component logic when the button is clicked.
```

```interview-question
What is two-way binding and how is it commonly implemented in Angular?
---
answer:
Two-way binding keeps view state and component state in sync in both directions.

The classic Angular example is `[(ngModel)]`, which combines property binding and event updates for template-driven forms.
hints:
- It updates from component to view and back.
- It combines two simpler binding ideas.
- `ngModel` is the common interview example.
```

```interview-choice
Which syntax is the standard Angular shorthand for two-way binding?
---
options:
- `[value]="name"`
- `(input)="name = $event"`
- `[(ngModel)]="name"`
correct: 2
explanation:
`[(ngModel)]` is Angular's standard two-way binding shorthand in template-driven forms.
```

```interview-question
What is the difference between an Angular component and a directive?
---
answer:
A component is a directive with its own template and is used as a UI building block.

A directive without its own view usually changes behavior, appearance, or structure of existing DOM and template content.
hints:
- One has its own rendered view.
- The other usually enhances or controls existing elements.
- Components are the main UI unit.
```

## Data Formatting

```interview-question
What are pipes in Angular and where would you use them?
---
answer:
Pipes transform values for display inside templates.

They are useful for formatting dates, numbers, currency, text casing, or other presentation-focused transformations without moving that formatting logic into the component class.
hints:
- Think display transformation.
- Formatting is the common use case.
- The change usually stays in the template layer.
```
