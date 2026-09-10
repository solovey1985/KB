# C# Interview Questions and Short Answers

Concise interview-style answers grouped by topic. Each answer includes the corresponding question so the context is preserved during review.

## 1. Value Types, Reference Types, and Equality

### 1. What is boxing in C#, and what is its performance cost?

Boxing converts a value type to `object` or an implemented interface. It copies the value into a heap object, causing an allocation and later GC pressure.

### 2. What happens when a struct is used through an interface?

Calling an interface member on a struct normally boxes the struct. A generic constraint such as `where T : IFoo` can avoid boxing.

### 3. What is a `ref struct`, and what restrictions does it have?

A `ref struct` is a stack-only type, such as `Span<T>`. It cannot be boxed, stored in normal heap objects, used as a generic type argument, or cross most `await` and `yield` boundaries.

### 4. What is the difference between a `record struct` and a `record class`?

A `record struct` is a value type copied by value; a `record class` is a reference type. Both generate value-based equality, but large structs may be expensive to copy.

### 5. What happens if you override `Equals()` but not `GetHashCode()`?

Hash collections may place equal objects in different buckets. Equal objects must return the same hash code.

### 6. How does the `==` operator compare strings in C#?

When both operands are statically typed as `string`, `==` compares their text. If both are typed as `object`, it normally compares their references.

### 7. What is string interning?

Identical compile-time literals may reference the same interned object. Runtime-created equal strings usually do not unless they are explicitly interned.

### 8. What are the differences between `float`, `double`, and `decimal`?

`float` is 32-bit and `double` is 64-bit binary floating point, while `decimal` is a 128-bit decimal-based type. Use `decimal` for financial calculations where decimal precision matters.

### 9. When should you use `DateTime`, `DateTimeOffset`, `DateOnly`, and `TimeOnly`?

`DateTime` stores a date and time with limited time-zone context; `DateTimeOffset` also includes a UTC offset. `DateOnly` and `TimeOnly` represent only those individual concepts.

## 2. Generics, Variance, and Interfaces

### 10. How does the JIT compile generic code for reference types and value types?

Reference-type instantiations usually share machine code. Value-type instantiations normally receive specialized code, avoiding boxing but increasing the amount of generated code.

### 11. What are covariance and contravariance in C# generics?

Covariance (`out`) allows `IEnumerable<Dog>` to be assigned to `IEnumerable<Animal>`. Contravariance (`in`) allows `IComparer<Animal>` to be assigned to `IComparer<Dog>`.

### 12. What is array covariance, and why is it unsafe?

Array covariance exists mainly for historical compatibility: `string[]` can be assigned to `object[]`. It is unsafe because an invalid write produces an `ArrayTypeMismatchException` at runtime.

### 13. What are static abstract interface members, and where are they useful?

They define static operations that implementing types must provide. Generic code can call them through a constrained type parameter, which enables features such as generic math.

## 3. Async and Task-Based Programming

### 14. Does `async` create a new thread, and what happens at `await`?

`async` does not automatically create a thread. At an incomplete `await`, the method registers a continuation and returns; execution resumes when the awaited operation completes.

### 15. What does the compiler-generated async state machine contain?

It contains the method parameters, local state, current execution step, an async method builder, and awaiters required to suspend and resume the method.

### 16. What is the difference between `Task<T>` and `ValueTask<T>`?

`Task<T>` is simpler and safely reusable. `ValueTask<T>` can avoid an allocation when completion is often synchronous, but it is larger and generally should be awaited only once.

### 17. Why should `async void` usually be avoided?

It cannot be awaited, composed, or easily tested, and its exceptions go directly to the synchronization context. Use it only for event handlers.

### 18. Why can `.Result` or `.Wait()` cause a deadlock?

The calling thread blocks while the awaited continuation tries to resume on the same synchronization context. Prefer async all the way through the call chain.

### 19. What does `ConfigureAwait(false)` do?

It tells the await not to resume on the captured synchronization context. It does not move work to a background thread.

### 20. When should you use `Task.Run()` in server-side code?

`Task.Run()` queues work to the thread pool and adds scheduling overhead. It may help offload CPU-bound work, but it does not make synchronous I/O scalable.

### 21. What is thread-pool starvation, and how can you diagnose it?

It occurs when available worker threads are blocked while queued work waits for a worker. Diagnose it using thread-pool counters, traces, dumps, and unusually growing thread or work queues.

### 22. How should `CancellationToken` be used correctly?

Accept and propagate it to cancellable operations, check it inside long-running loops, and treat `OperationCanceledException` as cancellation rather than an ordinary failure.

### 23. How does `Task.WhenAll()` handle multiple exceptions?

`Task.WhenAll()` waits for every task, and its `Exception` contains all failures, although `await` rethrows one of them. Sequential awaits may stop early and leave later failures unobserved.

## 4. Threading and Synchronization

### 24. How does `lock` work internally in C#?

A traditional `lock` compiles to `Monitor.Enter` inside `try/finally`, followed by `Monitor.Exit`. With C# 13 and `System.Threading.Lock`, it uses `EnterScope()` and is optimized for that purpose. See the [Microsoft `lock` documentation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock).

### 25. What guarantees does `volatile` provide, and what does it not provide?

It provides visibility and ordering guarantees for individual reads and writes, not atomicity for compound operations such as `count++`. Use `Interlocked` for atomic updates.

### 26. When should you use `Channel<T>` instead of `BlockingCollection<T>`?

Use `Channel<T>` for modern asynchronous producer-consumer pipelines and async backpressure. `BlockingCollection<T>` is suitable for synchronous, blocking consumers.

### 27. What is the difference between `AsyncLocal<T>` and `ThreadLocal<T>`?

`AsyncLocal<T>` follows the logical execution context across awaits. `ThreadLocal<T>` belongs to one physical thread.

## 5. Garbage Collection and Resource Management

### 28. How do the .NET GC generations work, and why is Gen 2 more expensive?

New objects start in Gen 0, and survivors move to Gen 1 and then Gen 2. Gen 2 collections are expensive because they inspect long-lived objects and usually operate on a much larger heap area.

### 29. What is the Large Object Heap, and why can it cause performance problems?

Large objects—normally at least 85,000 bytes—go to the Large Object Heap. Frequent large allocations can cause expensive Gen 2 collections and fragmentation. See the [Microsoft GC fundamentals documentation](https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/fundamentals).

### 30. How do finalizers affect garbage collection, and why should `Dispose()` be used?

Finalizable objects usually survive at least one extra collection while waiting for finalization. `Dispose()` releases resources deterministically, and `GC.SuppressFinalize(this)` avoids unnecessary finalization. See the [Microsoft dispose documentation](https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose).

### 31. What is the difference between Workstation GC and Server GC?

Workstation GC prioritizes responsiveness and lower resource use. Server GC uses multiple heaps and GC threads to maximize throughput, typically consuming more memory.

## 6. High-Performance Memory APIs

### 32. What is `Span<T>`, and how does it improve performance?

`Span<T>` represents a safe, allocation-free view over contiguous memory. It avoids allocations from slicing, substring-like parsing, and temporary buffers.

### 33. When should you use `ArrayPool<T>`, and what precautions are required?

Use it for frequently created, short-lived large arrays. Always return the array in a `finally` block, and clear sensitive data before returning it.

## 7. Closures and LINQ

### 34. How do closures work in C#, and when can they cause allocations?

Lambdas capture variables, not snapshots of their values. The compiler usually stores captured variables in a generated closure object, which may cause a heap allocation.

### 35. What is deferred execution in LINQ?

Most LINQ queries execute when they are enumerated, not when they are declared. Re-enumeration runs the query again against the source's current state.

## 8. Compilation and Deployment

### 36. What is Native AOT, and what are its benefits and limitations?

Native AOT produces self-contained native code with fast startup, lower memory usage, and no JIT compilation. Its limitations include trimming requirements, no runtime code generation or dynamic assembly loading, and restricted reflection scenarios. See the [Microsoft Native AOT documentation](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/).

