---
title: C# Memory Management Concept Map
techMemory:
  persistProgress: true
  defaultReveal: term
  showHierarchy: true
  showRelated: true
  enableRecallMode: true
  sectionTitle: Technical Memory
---

This map covers managed memory, garbage collection, finalization, and deterministic disposal in C#.

Study pages: [Memory Management Notes](csharp-memory-management.md) | [Interview Practice](csharp-memory-management.interview.md)

```concept-card
id: csharp-memory-management
term: C# Memory Management
children:
- managed-memory
- stack-and-heap
- garbage-collector
- object-reachability
- finalizer
- idisposable
- dispose-pattern
- memory-leak
summary:
C# memory management combines automatic managed heap cleanup with explicit cleanup for unmanaged resources.
details:
The garbage collector reclaims unreachable managed objects, while `IDisposable`, `using`, and the dispose pattern release resources that need deterministic cleanup.
example:
using var stream = File.OpenRead("data.json");
mnemonic:
GC owns memory; Dispose owns resources.
recall:
- What does the GC clean up automatically?
- What still needs explicit cleanup?
- Why are memory and resources not the same thing?
```

```concept-card
id: managed-memory
term: Managed Memory
parents:
- csharp-memory-management
related:
- garbage-collector
summary:
Managed memory is memory allocated and tracked by the .NET runtime.
details:
The runtime controls object allocation and reclamation, reducing manual allocation errors such as use-after-free.
example:
var order = new Order();
mnemonic:
Managed means runtime-tracked.
recall:
- What makes memory managed?
- Why does managed memory reduce use-after-free bugs?
```

```concept-card
id: stack-and-heap
term: Stack And Heap
parents:
- csharp-memory-management
summary:
The stack holds call frames and many locals, while the managed heap stores most objects created with `new`.
details:
Value types are copied by value, but their storage location depends on context. A struct local may be stack-associated, while a struct field inside a class is part of a heap object.
example:
int count = 42; var customer = new Customer();
mnemonic:
Frames stack, objects heap.
recall:
- Why is it incomplete to say all value types are on the stack?
- What typically lives on the managed heap?
```

```concept-card
id: garbage-collector
term: Garbage Collector
aliases:
- GC
parents:
- csharp-memory-management
children:
- gc-generation
- large-object-heap
related:
- object-reachability
summary:
The garbage collector automatically reclaims managed heap objects that are no longer reachable.
details:
It marks reachable objects from roots, reclaims unreachable objects, and may compact memory. It does not guarantee prompt release of unmanaged resources.
example:
Objects no longer referenced by live code become eligible for collection.
mnemonic:
Reachable survives, unreachable dies.
recall:
- What makes an object eligible for collection?
- Why does the GC not replace `Dispose`?
```

```concept-card
id: gc-generation
term: GC Generation
parents:
- garbage-collector
summary:
GC generations group objects by survival age to make collection efficient.
details:
Gen 0 is collected most often for short-lived objects, Gen 1 is transitional, and Gen 2 contains longer-lived objects collected less frequently.
example:
Short-lived request objects usually die in Gen 0.
mnemonic:
Young dies fast; old lives longer.
recall:
- Why does .NET use generations?
- Which generation is collected most often?
```

```concept-card
id: large-object-heap
term: Large Object Heap
aliases:
- LOH
parents:
- garbage-collector
summary:
The large object heap stores large managed objects, commonly arrays or buffers around 85 KB or larger.
details:
Large allocations can increase Gen 2 pressure and fragmentation concerns. Reuse buffers or stream data when large allocations are frequent.
example:
byte[] buffer = new byte[100_000];
mnemonic:
Large buffers live large consequences.
recall:
- What kind of allocation tends to hit the LOH?
- Why can repeated large allocations hurt performance?
```

```concept-card
id: object-reachability
term: Object Reachability
parents:
- csharp-memory-management
related:
- garbage-collector
- memory-leak
summary:
Object reachability determines whether the GC considers an object still alive.
details:
Objects reachable from roots such as stack variables, static fields, GC handles, or active closures cannot be collected.
example:
A static list holding service instances keeps those instances reachable.
mnemonic:
If something points to it, it lives.
recall:
- What are common GC roots?
- Why can static fields cause leaks?
```

```concept-card
id: finalizer
term: Finalizer
aliases:
- destructor syntax
parents:
- csharp-memory-management
related:
- dispose-pattern
summary:
A finalizer is a last-chance cleanup hook that runs before the GC reclaims an object.
details:
Finalizers are nondeterministic and delay collection, so they should be reserved for unmanaged resource cleanup when safer wrappers such as `SafeHandle` are not enough.
example:
~NativeBuffer() { /* unmanaged cleanup fallback */ }
mnemonic:
Finalizers are fallback, not flow control.
recall:
- Why are finalizers nondeterministic?
- Why can finalizers make collection slower?
```

```concept-card
id: idisposable
term: IDisposable
parents:
- csharp-memory-management
children:
- using-statement
- async-disposable
related:
- dispose-pattern
summary:
`IDisposable` provides deterministic cleanup through a `Dispose` method.
details:
Use it for objects that own resources needing prompt release, such as streams, database connections, timers, subscriptions, or native handles.
example:
public void Dispose() { _stream.Dispose(); }
mnemonic:
Dispose now, not someday.
recall:
- What kinds of resources need `IDisposable`?
- Why is deterministic cleanup useful?
```

```concept-card
id: dispose-pattern
term: Dispose Pattern
parents:
- csharp-memory-management
related:
- finalizer
- idisposable
summary:
The dispose pattern is the conventional implementation shape for releasing managed and unmanaged resources safely.
details:
Simple sealed types can implement `Dispose` directly. Inheritable types or direct unmanaged ownership may need `Dispose(bool disposing)`, a finalizer, and `GC.SuppressFinalize(this)`.
example:
Dispose(); GC.SuppressFinalize(this);
mnemonic:
Dispose deterministic, finalizer fallback.
recall:
- When is the full dispose pattern needed?
- Why call `GC.SuppressFinalize` after successful disposal?
```

```concept-card
id: using-statement
term: using Statement
parents:
- idisposable
summary:
The `using` statement or declaration calls `Dispose` automatically at the end of a scope.
details:
It is the preferred way to ensure deterministic cleanup for disposable objects in synchronous code.
example:
using var stream = File.OpenRead("data.json");
mnemonic:
Using scopes disposal.
recall:
- What does `using` call automatically?
- Why is `using` safer than manual cleanup?
```

```concept-card
id: async-disposable
term: IAsyncDisposable
parents:
- idisposable
summary:
`IAsyncDisposable` supports asynchronous cleanup with `DisposeAsync` and `await using`.
details:
Use it when cleanup itself may need asynchronous work, such as flushing or closing asynchronous resources.
example:
await using var connection = await OpenConnectionAsync();
mnemonic:
Async resource, async dispose.
recall:
- When is `IAsyncDisposable` appropriate?
- What syntax disposes asynchronously?
```

```concept-card
id: memory-leak
term: Memory Leak In .NET
parents:
- csharp-memory-management
related:
- object-reachability
summary:
A .NET memory leak occurs when unused objects remain reachable and therefore cannot be collected.
details:
Common causes include static collections, unbounded caches, event subscriptions, timers, captured closures, and undisposed resources.
example:
A singleton cache keeps adding entries without eviction.
mnemonic:
Reachable but useless is leaked.
recall:
- Why can the GC not collect reachable unused objects?
- Which common patterns keep objects alive accidentally?
```
