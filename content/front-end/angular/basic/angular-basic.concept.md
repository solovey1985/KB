---
title: Angular Basic Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes Angular fundamentals around components and templates.

Study pages: [Section Index](index.md) | [Material Notes](angular-basic.md) | [Interview Practice](angular-basic.interview.md)

## Basic Map

```concept-card
id: angular-component
term: Angular Component
children:
- component-metadata
- template-binding
- standalone-component
- component-tree
summary:
An Angular component is the core unit of UI composition in an Angular application.
details:
It combines behavior, template rendering, and metadata so Angular can instantiate and manage a piece of the interface.
example:
`@Component({ selector: 'app-profile-card', template: '<h2>{{ name }}</h2>' })`
mnemonic:
Class plus template plus metadata equals component.
recall:
- What three parts make up a component?
- Why is the component the core Angular UI unit?
```

```concept-card
id: component-metadata
term: Component Metadata
parents:
- angular-component
summary:
Component metadata tells Angular how a component should be created and rendered.
details:
It includes properties such as selector, template, styles, and imported dependencies.
example:
`selector`, `templateUrl`, and `imports` are common metadata fields.
mnemonic:
Metadata tells Angular how to use the class.
recall:
- Why is metadata necessary in Angular?
- Which fields are most common in component metadata?
```

```concept-card
id: template-binding
term: Template Binding
parents:
- angular-component
summary:
Template binding is Angular's mechanism for connecting component state and browser events to the template.
details:
It includes interpolation, property binding, event binding, and other declarative patterns.
example:
`<button [disabled]="isSaving" (click)="save()">Save</button>`
mnemonic:
Templates read state and react to events.
recall:
- What are the main Angular binding forms?
- Why is template binding central to Angular templates?
```

```concept-card
id: standalone-component
term: Standalone Component
parents:
- angular-component
summary:
A standalone component imports its own dependencies directly instead of relying on a declaring NgModule.
details:
This is the modern Angular default and makes dependencies more explicit at the component level.
example:
`@Component({ standalone: true, imports: [DatePipe] })`
mnemonic:
Declare self, import what you need.
recall:
- What problem do standalone components reduce?
- Why are they now the normal Angular approach?
```

```concept-card
id: component-tree
term: Component Tree
parents:
- angular-component
summary:
An Angular application can be understood as a tree of nested components.
details:
This structure helps explain composition, rendering, and many dependency injection relationships.
example:
An app shell component may render a layout component, which renders a dashboard component, which renders card components.
mnemonic:
Apps render as trees, not flat screens.
recall:
- Why is the component tree a useful mental model?
- What framework behaviors depend on the component hierarchy?
```
