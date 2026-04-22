# Angular Basic

Angular applications are built primarily from components.

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
- two-way binding where appropriate

```html
<button [disabled]="isSaving" (click)="save()">
  Save
</button>
```

## Directives and composition

Angular templates also use directives and pipes.

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
