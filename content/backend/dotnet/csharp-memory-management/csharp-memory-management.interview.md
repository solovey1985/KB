---
title: C# Memory Management Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to practise C# memory management, garbage collection, finalizers, and the dispose pattern.

Study pages: [Memory Management Notes](csharp-memory-management.md) | [Concept Map](csharp-memory-management.concept.md)

## Managed Memory And GC

```interview-question
What is the role of the garbage collector in .NET?
---
answer:
The garbage collector automatically manages managed heap memory by finding objects that are no longer reachable and reclaiming their memory.

It reduces manual memory-management bugs, but it does not provide deterministic cleanup for unmanaged resources such as file handles, sockets, or database connections.
hints:
- It works on managed heap memory.
- Reachability determines whether objects survive.
- It does not replace `Dispose`.
```

Related concepts: [Garbage Collector](csharp-memory-management.concept.md#garbage-collector), [IDisposable](csharp-memory-management.concept.md#idisposable)

```interview-question
What does it mean for an object to be reachable?
---
answer:
An object is reachable when it can be accessed from GC roots such as thread stacks, static fields, GC handles, or active closures.

Reachable objects are considered alive and cannot be collected, even if the application no longer logically needs them.
hints:
- Think GC roots.
- Static fields are common roots.
- Reachable is not always useful.
```

Related concepts: [Object Reachability](csharp-memory-management.concept.md#object-reachability), [Memory Leak In .NET](csharp-memory-management.concept.md#memory-leak)

```interview-choice
Which statement about forcing garbage collection is most accurate?
---
options:
- It is usually a good way to improve normal application performance.
- It is rarely needed and can disrupt the GC's own heuristics.
- It immediately disposes all unmanaged resources.
correct: 1
explanation:
`GC.Collect()` is rarely appropriate in normal application code. It can hurt throughput and does not replace deterministic cleanup with `Dispose`.
```

```interview-question
Why does .NET use GC generations?
---
answer:
.NET uses generations because most objects are short-lived.

Collecting Gen 0 frequently and older generations less often lets the GC reclaim short-lived objects efficiently while avoiding full-heap work on every collection.
hints:
- Most objects die young.
- Gen 0 is collected often.
- Gen 2 is for longer-lived objects.
```

Related concepts: [GC Generation](csharp-memory-management.concept.md#gc-generation)

## Stack, Heap, And Allocation

```interview-question
Describe the stack and heap in .NET memory management.
---
answer:
The stack is associated with method calls, call frames, and many local variables.

The managed heap stores most objects created with `new`. References to heap objects can live in locals, fields, arrays, closures, or static fields.
hints:
- Method frames are stack-associated.
- Objects usually live on the managed heap.
- References can be stored in many places.
```

Related concepts: [Stack And Heap](csharp-memory-management.concept.md#stack-and-heap)

```interview-choice
Which statement about value types is most precise?
---
options:
- Value types always live on the stack.
- Value types are copied by value, but their storage location depends on context.
- Value types are always collected by the GC separately from their containing object.
correct: 1
explanation:
Value-type semantics mean copying by value. Storage depends on context: a struct field inside a class is stored as part of the heap object.
```

```interview-question
What is the large object heap and why can it matter?
---
answer:
The large object heap stores large managed objects, commonly arrays or buffers around 85 KB or larger.

Frequent large allocations can increase Gen 2 pressure and fragmentation concerns, so streaming or buffer reuse may be better for hot paths.
hints:
- Large arrays and buffers are typical.
- It is tied to Gen 2 behavior.
- Reuse can reduce pressure.
```

Related concepts: [Large Object Heap](csharp-memory-management.concept.md#large-object-heap)

## Finalizers And Dispose

```interview-question
What are finalizers in C#?
---
answer:
Finalizers are nondeterministic cleanup hooks that run before the GC reclaims an object.

They are a fallback for unmanaged cleanup, not a general cleanup mechanism. They can delay collection and should be avoided unless truly needed.
hints:
- They use destructor syntax.
- Timing is not deterministic.
- They are fallback cleanup.
```

Related concepts: [Finalizer](csharp-memory-management.concept.md#finalizer)

```interview-question
What is `IDisposable` used for?
---
answer:
`IDisposable` is used for deterministic cleanup through a `Dispose` method.

It is appropriate when a type owns resources that should be released promptly, such as streams, handles, database connections, timers, or subscriptions.
hints:
- It is deterministic cleanup.
- It releases resources, not just memory.
- `using` calls it automatically.
```

Related concepts: [IDisposable](csharp-memory-management.concept.md#idisposable), [using Statement](csharp-memory-management.concept.md#using-statement)

```interview-choice
Which cleanup mechanism should you prefer for a `FileStream`?
---
options:
- Wait for the garbage collector to eventually collect it.
- Use `using` or explicitly call `Dispose`.
- Add a finalizer to every class that uses it.
correct: 1
explanation:
`FileStream` owns OS resources that should be released deterministically. `using` is the normal way to call `Dispose` reliably.
```

```interview-question
When do you need the full dispose pattern instead of a simple `Dispose` method?
---
answer:
Use the full dispose pattern when a type is inheritable or owns unmanaged resources directly.

Simple sealed types that only own managed disposable fields can often implement `Dispose` directly by disposing those fields.
hints:
- Inheritance changes the design.
- Direct unmanaged ownership changes the design.
- Sealed managed-only wrappers are simpler.
```

Related concepts: [Dispose Pattern](csharp-memory-management.concept.md#dispose-pattern)

```interview-code
language: cs
prompt: Complete the simple disposable wrapper so it disposes the owned writer.
starter:
sealed class ReportWriter : IDisposable
{
    private readonly StreamWriter _writer;

    public ReportWriter(Stream stream)
    {
        _writer = new StreamWriter(stream);
    }

    public void Dispose()
    {
    }
}
solution:
sealed class ReportWriter : IDisposable
{
    private readonly StreamWriter _writer;

    public ReportWriter(Stream stream)
    {
        _writer = new StreamWriter(stream);
    }

    public void Dispose()
    {
        _writer.Dispose();
    }
}
checks:
- includes: IDisposable
- includes: _writer.Dispose();
```

```interview-question
Why call `GC.SuppressFinalize(this)` after successful disposal when a finalizer exists?
---
answer:
It tells the GC that finalization is no longer needed because cleanup already happened deterministically.

That avoids unnecessary finalizer queue work and prevents duplicate cleanup attempts.
hints:
- Cleanup already happened.
- It avoids finalizer queue work.
- It matters only when finalization is relevant.
```

## Leaks And Diagnostics

```interview-question
What is a memory leak in .NET?
---
answer:
A .NET memory leak occurs when objects are no longer needed but remain reachable, so the GC cannot collect them.

Common causes include static collections, unbounded caches, event subscriptions, timers, captured closures, and undisposed resources.
hints:
- The object is still reachable.
- The application no longer needs it.
- The GC cannot collect reachable objects.
```

Related concepts: [Memory Leak In .NET](csharp-memory-management.concept.md#memory-leak)

```interview-choice
Which pattern commonly causes a .NET memory leak?
---
options:
- A short-lived local variable that goes out of scope.
- A singleton cache that keeps adding entries without eviction.
- A disposed stream inside a `using` block.
correct: 1
explanation:
A singleton cache is rooted for the application lifetime. Without eviction, it can keep objects reachable indefinitely.
```

```interview-question
How would you start diagnosing a suspected memory leak in a C# application?
---
answer:
Collect evidence first: monitor memory and GC counters, take a memory dump or GC dump, inspect object counts and retention paths, and compare snapshots over time.

Useful tools include Visual Studio Diagnostic Tools, `dotnet-counters`, `dotnet-gcdump`, `dotnet-dump`, and PerfView.
hints:
- Measure before guessing.
- Compare snapshots.
- Retention paths explain why objects survive.
```

```interview-code
language: cs
prompt: Complete the `using` declaration for deterministic stream cleanup.
starter:
var stream = File.OpenRead("data.json");
solution:
using var stream = File.OpenRead("data.json");
checks:
- includes: using var stream
- includes: File.OpenRead
```
