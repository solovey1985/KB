# Reactive Settings Panel with Signals

## Situation

You are building a settings panel where users can change theme, refresh interval, and compact mode.

The state is local to the feature and should feel simple to read in the template.

## Why this case matters

This case exercises:

- signal-based local state
- derived state with `computed`
- side effects through `effect`
- deciding when RxJS is unnecessary

## Example

```ts
import { Component, computed, effect, signal } from '@angular/core';

@Component({
  selector: 'app-settings-panel',
  standalone: true,
  template: `
    <p>{{ summary() }}</p>
    <button (click)="toggleTheme()">Toggle theme</button>
  `,
})
export class SettingsPanelComponent {
  readonly theme = signal<'light' | 'dark'>('light');
  readonly compactMode = signal(false);
  readonly refreshMs = signal(30000);

  readonly summary = computed(() =>
    `${this.theme()} · compact=${this.compactMode()} · ${this.refreshMs()}ms`
  );

  constructor() {
    effect(() => {
      console.log('Settings changed:', this.summary());
    });
  }

  toggleTheme() {
    this.theme.update(value => (value === 'light' ? 'dark' : 'light'));
  }
}
```

## Practical design notes

- keep local UI state in signals when async streams are not needed
- use `computed` for pure derivation
- reserve `effect` for real side effects

## Related concepts

- [Angular Signals](../advanced/angular-advanced.concept.md#angular-signals)
- [computed Signal](../advanced/angular-advanced.concept.md#computed-signal)
- [effect](../advanced/angular-advanced.concept.md#effect)
