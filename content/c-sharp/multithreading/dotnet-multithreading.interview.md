---
title: .NET Multithreading Interview Practice
interview:
  persistProgress: true
  shuffleChoices: false
  showScore: true
  allowRevealAll: true
  markCompleteOnCheck: true
---

Use this page to rehearse the core distinctions that usually matter in interviews:

- thread basics and execution models
- synchronization primitives and their tradeoffs
- async patterns in .NET
- practical code-level concurrency decisions

## Foundations

```interview-question
What is the difference between a process and a thread?
---
answer:
A **process** is an isolated execution environment with its own memory space and operating system resources.

A **thread** is a unit of execution inside a process.

Multiple threads in the same process share memory and other process resources, which makes communication cheaper but also introduces synchronization risks.
hints:
- One boundary is heavier and isolates memory.
- The smaller unit shares the parent process resources.
- Shared memory is useful, but it creates race-condition risk.
```

```interview-question
When would you choose `Task` or the thread pool instead of creating a `Thread` manually?
---
answer:
Use `Task` or thread-pool-backed work for most short-lived concurrent operations because the runtime can reuse worker threads, schedule work efficiently, and integrate with cancellation and composition APIs such as `Task.WhenAll`.

Create a dedicated `Thread` only when you need thread-specific control such as a foreground thread, a custom apartment state, long-lived ownership, or direct thread identity.
hints:
- Think about reuse versus manual lifecycle management.
- One option integrates naturally with modern async APIs.
- Dedicated threads are usually for exceptional cases, not the default.
```

```interview-question
Why is `async` not the same thing as multithreading?
---
answer:
`async` is primarily a **non-blocking coordination model**, while multithreading is about **multiple threads of execution**.

An asynchronous operation may complete without keeping a thread busy for the entire wait, especially for I/O-bound work.

By contrast, CPU-bound multithreading uses multiple threads to execute work concurrently.
hints:
- One concept is about waiting efficiently.
- The other is about how many execution threads are involved.
- I/O-bound and CPU-bound workloads are the key distinction.
```

```interview-question
What is a race condition?
---
answer:
A **race condition** occurs when the result of a program depends on the timing or interleaving of concurrent operations.

It usually appears when multiple threads read and modify shared state without proper synchronization, causing lost updates, inconsistent state, or non-deterministic bugs.
hints:
- The same input can produce different results.
- Shared mutable state is usually involved.
- Timing changes the outcome.
```

## Synchronization Primitives

```interview-question
What does the `volatile` keyword guarantee in C#, and what does it not guarantee?
---
answer:
`volatile` tells the runtime and compiler not to cache the field in a way that would hide the latest value from other threads, improving visibility of reads and writes.

It does **not** make compound operations atomic and does **not** replace locking for protecting invariants across multiple steps.
hints:
- It helps with visibility.
- It does not make `count++` safe.
- Think about atomicity versus memory ordering.
```

```interview-question
How do `ManualResetEvent` and `AutoResetEvent` differ?
---
answer:
`ManualResetEvent` stays signaled until `Reset()` is called, so it can release multiple waiting threads.

`AutoResetEvent` releases one waiting thread when signaled and then automatically returns to the non-signaled state.
hints:
- One behaves like opening a gate until you close it.
- The other wakes one waiter per signal.
- The reset behavior is the main distinction.
```

```interview-question
When is a `Mutex` a better fit than `lock`?
---
answer:
Use a `Mutex` when synchronization may need to cross **process boundaries** or when you need a named operating-system-level primitive.

Use `lock` or `Monitor` for normal in-process mutual exclusion because they are lighter and better suited for protecting shared state inside one process.
hints:
- One primitive can be named system-wide.
- The lighter option is usually enough inside a single process.
- Cross-process coordination is the deciding factor.
```

## Multiple Choice Questions

```interview-choice
Which primitive releases exactly one waiting thread each time it is signaled?
---
options:
- ManualResetEvent
- AutoResetEvent
- SemaphoreSlim
correct: 1
explanation:
`AutoResetEvent` wakes one waiter per signal and then resets automatically.

`ManualResetEvent` can release multiple waiting threads while it remains signaled.
```

```interview-choice
Which option is the modern .NET asynchronous model recommended for new code in most cases?
---
options:
- APM using `BeginXxx` and `EndXxx`
- EAP using completion events
- TAP using `Task` and `async`/`await`
correct: 2
explanation:
The **Task-based Asynchronous Pattern** is the standard model for modern .NET code.

It composes well, supports cancellation, and integrates with `async` and `await`.
```

```interview-choice
Which statement about `lock` is correct?
---
options:
- It can synchronize threads across different processes.
- It is the common in-process mechanism for protecting a critical section.
- It guarantees fairness between waiting threads.
correct: 1
explanation:
`lock` is the normal in-process primitive for mutual exclusion around shared state.

It does not provide cross-process synchronization and does not guarantee fair scheduling.
```

```interview-choice
Which primitive is best suited for limiting concurrency to a fixed number of simultaneous workers?
---
options:
- Mutex
- SemaphoreSlim
- volatile
correct: 1
explanation:
`SemaphoreSlim` tracks a counter and allows up to a configured number of concurrent entrants.

That makes it a practical way to throttle parallel access.
```

## Code Completion Questions

```interview-code
language: cs
prompt: Complete the critical section so incrementing the shared counter is thread-safe.
starter:
private static readonly object _gate = new object();
private static int _counter;

public static void Increment()
{
  
}
solution:
private static readonly object _gate = new object();
private static int _counter;

public static void Increment()
{
  lock (_gate)
  {
    _counter++;
  }
}
checks:
- includes: lock (_gate)
- includes: _counter++
```

```interview-code
language: cs
prompt: Complete the method so it limits access to 3 concurrent callers using `SemaphoreSlim`.
starter:
private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(3, 3);

public static async Task RunAsync()
{
  await 
  try
  {
    Console.WriteLine("working");
  }
  finally
  {
    
  }
}
solution:
private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(3, 3);

public static async Task RunAsync()
{
  await _semaphore.WaitAsync();
  try
  {
    Console.WriteLine("working");
  }
  finally
  {
    _semaphore.Release();
  }
}
checks:
- includes: WaitAsync
- includes: Release
- includes: finally
```

```interview-code
language: cs
prompt: Complete the method so it starts both operations and awaits them together.
starter:
public static async Task LoadAsync()
{
  Task first = LoadUsersAsync();
  Task second = LoadOrdersAsync();

  await 
}
solution:
public static async Task LoadAsync()
{
  Task first = LoadUsersAsync();
  Task second = LoadOrdersAsync();

  await Task.WhenAll(first, second);
}
checks:
- includes: Task.WhenAll
- includes: first
- includes: second
```

## Pattern Evolution

```interview-question
How do APM, EAP, and TAP relate to each other in .NET history?
---
answer:
They represent the major generations of asynchronous programming models in .NET.

- **APM** uses `BeginXxx` and `EndXxx` with `IAsyncResult`.
- **EAP** uses events such as `Completed` to signal completion.
- **TAP** uses `Task`, `Task<T>`, and `async`/`await`.

TAP is the current preferred model for most new code because it is easier to compose and reason about.
hints:
- Think of three generations, not three unrelated features.
- One uses `IAsyncResult`.
- The newest one is built around `Task`.
```

## Study Notes

Use the long-form pages in this section together with this practice page:

- [Threads](threads/Threads.md)
- [Thread Synchronization](thread-synchroniztion/ThreadsSyncroniztation.md)
- [Asynchronous Programming Model](async-model/AsyncProgrammingModel.md)

This page is strongest when you use it to test distinctions, not just definitions.
