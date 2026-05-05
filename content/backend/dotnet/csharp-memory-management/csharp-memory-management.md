# C# Memory Management

C# uses managed memory: the runtime allocates objects and the garbage collector reclaims memory for objects that are no longer reachable. You still need to manage unmanaged resources such as file handles, sockets, database connections, native buffers, and OS handles.

## Stack, Heap, And Object Lifetime

Local variables and method frames are associated with the stack. Objects created with `new` usually live on the managed heap, and references to those objects can be stored in locals, fields, arrays, or closures.

Value types are copied by value, but their storage location depends on context. A struct local may be on the stack, while a struct field inside a class is part of the heap object.

```csharp
var customer = new Customer("Ada"); // object on managed heap
int count = 42;                      // value-type local
```

An object is collectible when it can no longer be reached from application roots such as thread stacks, static fields, GC handles, and CPU registers.

## Garbage Collection

The .NET GC tracks reachable objects, reclaims unreachable objects, and compacts memory when appropriate. It is generational: Gen 0 is for short-lived objects, Gen 1 is a buffer, and Gen 2 is for longer-lived objects.

Most objects should be short-lived. Long-lived objects promoted to Gen 2 are collected less often, so accidental references from statics, caches, or event subscriptions can keep memory alive much longer than expected.

```csharp
// Usually do not force collection in application code.
GC.Collect();
GC.WaitForPendingFinalizers();
```

Forcing collection is rarely correct. It can hurt throughput, disrupt GC heuristics, and hide the real ownership problem. Use it mainly in controlled benchmarks, tests that isolate memory behavior, or unusual interop scenarios.

## Finalizers And Unmanaged Resources

A finalizer is a fallback cleanup hook that runs before an object is reclaimed. It is written with destructor syntax, but it is not deterministic.

```csharp
sealed class NativeBuffer
{
    ~NativeBuffer()
    {
        // Last-chance cleanup for unmanaged state only.
    }
}
```

Finalizers delay collection because finalizable objects must be placed on the finalization queue first. Do not use finalizers for ordinary managed cleanup. Prefer `SafeHandle` for native handles when possible.

## IDisposable And The Dispose Pattern

`IDisposable` provides deterministic cleanup. Use it when a type owns resources that must be released promptly.

```csharp
using var stream = File.OpenRead("data.json");
// stream.Dispose() runs at the end of the scope
```

For a sealed type that owns only managed disposable fields, a simple implementation is enough.

```csharp
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
```

Use the full dispose pattern when a type may be inherited or owns unmanaged resources directly. If a finalizer exists, call `GC.SuppressFinalize(this)` after deterministic cleanup.

## Memory Leaks And Diagnostics

A .NET memory leak usually means objects remain reachable even though the application no longer needs them. The GC cannot collect reachable objects.

Common causes:

- static collections that grow without eviction
- event subscriptions not removed
- long-lived timers or background services holding references
- unbounded caches
- closures capturing large objects
- undisposed streams, connections, handles, or subscriptions

Use memory dumps, allocation profilers, and tools such as Visual Studio Diagnostic Tools, `dotnet-counters`, `dotnet-gcdump`, `dotnet-dump`, or PerfView to inspect object counts, retention paths, GC pressure, and large object heap usage.
