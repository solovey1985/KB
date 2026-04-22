# Angular Basic

Angular applications are built primarily from components.

Angular is a full client application framework, so its basic interview topics usually include templates, events, directives, pipes, and the overall component-driven architecture as well.

A component combines:

- a TypeScript class for behavior
- a template for rendering
- metadata describing how Angular should use it

## Component anatomy

```ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-profile-card',
  template: `
    <article>
      <h2>{{ name }}</h2>
      <button (click)="toggleActive()">
        {{ isActive ? 'Disable' : 'Enable' }}
      </button>
    </article>
  `,
})
export class ProfileCardComponent {
  name = 'Mira';
  isActive = true;

  toggleActive() {
    this.isActive = !this.isActive;
  }
}
```

## Templates and binding

Angular templates support several essential binding forms:

- interpolation: `{{ value }}`
- property binding: `[disabled]="isSaving"`
- event binding: `(click)="save()"`
- two-way binding where appropriate, commonly `[(ngModel)]`

```html
<button [disabled]="isSaving" (click)="save()">
  Save
</button>
```

## Directives and composition

Angular templates also use directives and pipes.

Common interview distinctions here are:

- a component is a directive with its own template
- attribute directives adjust behavior or styling of existing elements
- structural directives or built-in control flow decide what gets rendered

With modern Angular, components are standalone by default, so dependencies are imported directly into the component.

```ts
import { Component } from '@angular/core';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-audit-log',
  standalone: true,
  imports: [DatePipe],
  template: `<p>Updated {{ updatedAt | date:'medium' }}</p>`,
})
export class AuditLogComponent {
  updatedAt = new Date();
}
```

Pipes format values for display in templates, such as dates, currency, and text transformations.

```html
<p>Updated {{ updatedAt | date:'medium' }}</p>
```

## User interaction

Angular handles browser interaction through event binding.

```html
<button (click)="toggleActive()">Toggle</button>
```

Two-way binding is used when the view and component state should stay synchronized in both directions.

## Application structure

Angular applications are composed as a tree of components.

That tree is important for:

- UI composition
- dependency injection boundaries
- change detection and rendering behavior

## Interview reminders

- component is the unit of UI composition
- template binding types should be named clearly
- Angular is not only about decorators; it is also about a component tree and dependency injection model
