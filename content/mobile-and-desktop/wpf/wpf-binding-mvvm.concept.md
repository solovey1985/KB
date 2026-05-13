---
title: WPF Binding And MVVM Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This page organizes the concepts behind WPF binding, collection views, MVVM state flow, and command-based interaction.

Study pages: [Section Index](index.md) | [Data Binding And Collection Views](data-binding-and-collection-views.md) | [MVVM Commands And Navigation](mvvm-commands-and-navigation.md) | [Threading Dispatcher And Async](threading-dispatcher-and-async.md)

## Binding And MVVM Map

```concept-card
id: wpf-binding-mvvm
term: WPF Binding And MVVM
children:
- data-binding
- collection-view
- mvvm
- command-pattern
- dispatcher-affinity
summary:
WPF binding and MVVM organize how UI state, user actions, and screen updates flow between views and view models.
details:
These concepts work together so views stay declarative while view models expose state and behavior in a testable way.
example:
A customer screen can bind text fields to a view model, expose a save command, and update loading state without control-specific logic in the view model.
mnemonic:
Bind state, expose actions, keep views thin.
recall:
- What major concepts sit under WPF binding and MVVM?
- Why does this model help keep UI logic testable?
```

```concept-card
id: data-binding
term: Data Binding
parents:
- wpf-binding-mvvm
children:
- binding-mode
- update-source-trigger
- notify-property-changed
summary:
Data binding connects a view property to a source property so UI and state stay synchronized.
details:
Bindings can be one-way, two-way, one-time, or one-way-to-source, and WPF resolves them through data context and property paths.
example:
`Text="{Binding Name, UpdateSourceTrigger=PropertyChanged}"` keeps a text box and the `Name` property synchronized.
mnemonic:
Bind the property, not the control event.
recall:
- What does data binding synchronize?
- Which supporting concepts make binding updates work correctly?
```

```concept-card
id: binding-mode
term: Binding Mode
parents:
- data-binding
summary:
Binding mode defines the direction of value flow between the source and the target.
details:
Common modes are `OneWay`, `TwoWay`, `OneTime`, and `OneWayToSource`, each chosen based on whether the UI should display, edit, or push values.
example:
A `TextBox.Text` binding is often `TwoWay`, while a `TextBlock.Text` summary is often `OneWay`.
mnemonic:
Pick the direction on purpose.
recall:
- Which binding mode is most common for editable form fields?
- When is `OneTime` a better fit than `OneWay`?
```

```concept-card
id: update-source-trigger
term: Update Source Trigger
parents:
- data-binding
summary:
Update source trigger controls when target changes are written back to the source.
details:
For example, a text box may update on every keystroke or only when focus is lost, depending on the cost and UX requirements.
example:
A search box may wait until `LostFocus`, while a live filter box updates on every keystroke.
mnemonic:
Direction is not timing.
recall:
- What problem does update source trigger solve?
- Why might immediate updates be undesirable for some inputs?
```

```concept-card
id: notify-property-changed
term: INotifyPropertyChanged
parents:
- data-binding
summary:
`INotifyPropertyChanged` lets a source object tell WPF when a bound property value changed.
details:
Without it, mutable view-model properties may change in memory without the UI updating to reflect the new value.
example:
Changing `CustomerName` in a view model without raising `PropertyChanged` leaves the bound `TextBlock` stale.
mnemonic:
No notification, no fresh UI.
recall:
- Why is `INotifyPropertyChanged` critical for mutable view-model state?
- What happens if a bound property changes silently?
```

```concept-card
id: collection-view
term: CollectionView
parents:
- wpf-binding-mvvm
related:
- observable-collection
summary:
`CollectionView` adds sorting, filtering, grouping, and current-item behavior on top of a collection data source.
details:
It lets the UI shape and navigate collection data without forcing every screen to mutate the underlying source collection directly.
example:
A `ListCollectionView` can group customers by country while leaving the original customer list unchanged.
mnemonic:
Same items, different lens.
recall:
- What behavior does `CollectionView` add beyond a raw collection?
- Why is it useful for UI-specific shaping of data?
```

```concept-card
id: observable-collection
term: ObservableCollection
parents:
- collection-view
summary:
`ObservableCollection<T>` notifies the UI when items are added, removed, or moved.
details:
It is the standard WPF collection type for dynamic item lists, though it does not notify for property changes inside the items themselves.
example:
Adding a new order to `ObservableCollection<OrderViewModel>` updates a bound `ListView` immediately.
mnemonic:
Collection changes, not item internals.
recall:
- What kind of changes does `ObservableCollection<T>` report?
- What does it not solve on its own?
```

```concept-card
id: mvvm
term: MVVM
parents:
- wpf-binding-mvvm
children:
- view-model
- command-pattern
summary:
MVVM separates the view, view model, and model so UI markup and application state evolve with less coupling.
details:
The view handles layout and view-only concerns, the view model exposes state and commands, and the model represents domain data or business behavior.
example:
A login screen keeps button placement in XAML, username/password state in the view model, and authentication logic in services or models.
mnemonic:
View displays, ViewModel coordinates, Model represents.
recall:
- What does each layer in MVVM own?
- Why does MVVM fit WPF especially well?
```

```concept-card
id: view-model
term: ViewModel
parents:
- mvvm
summary:
A view model exposes bindable state and commands without directly depending on concrete UI controls.
details:
Well-structured view models avoid references to `TextBox`, `Window`, and similar view objects, which keeps them easier to test and reuse.
example:
A `CustomerDetailsViewModel` exposes `CustomerName` and `SaveCommand` rather than reading directly from a `TextBox`.
mnemonic:
Represent the screen, not the controls.
recall:
- What should a view model avoid referencing directly?
- Why are view models more testable than views?
```

```concept-card
id: command-pattern
term: Command Pattern In WPF
parents:
- mvvm
related:
- dispatcher-affinity
summary:
Commands let the view invoke behavior exposed by the view model without direct event-handler coupling.
details:
WPF commands integrate with buttons, menus, keyboard gestures, and executability checks, making them a natural fit for MVVM interactions.
example:
A `SaveCommand` can disable the Save button until validation passes and then execute the save workflow.
mnemonic:
Bind the action, not the click handler.
recall:
- Why are commands preferable to many code-behind click handlers?
- What extra behavior do commands support beyond invocation?
```

```concept-card
id: dispatcher-affinity
term: Dispatcher Affinity
parents:
- wpf-binding-mvvm
summary:
WPF UI objects have thread affinity and must be accessed on the dispatcher-owned UI thread.
details:
Async loading and command execution need to respect dispatcher rules so UI updates remain safe and responsive.
example:
A background data load can fetch records on a worker thread, but adding them to a bound UI collection must happen on the dispatcher thread.
mnemonic:
Background work is fine, UI updates come home.
recall:
- Why can background threads not update WPF controls directly?
- How does dispatcher affinity affect async MVVM workflows?
```
