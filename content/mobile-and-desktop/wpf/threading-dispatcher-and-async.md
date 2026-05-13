# Threading Dispatcher And Async

WPF has UI thread affinity: UI elements must be accessed on the thread that created them. Long-running work should run away from the UI thread, then marshal UI updates back through the dispatcher or the captured UI synchronization context.

Related topics: [WPF Overview And Runtime](wpf-overview-and-runtime.md), [Data Binding And Collection Views](data-binding-and-collection-views.md)

## UI Thread Affinity

Most WPF objects derive from `DispatcherObject`. They are tied to a `Dispatcher` and can only be used from that dispatcher's thread.

```csharp
if (!Dispatcher.CheckAccess())
{
    await Dispatcher.InvokeAsync(() => StatusText.Text = "Done");
    return;
}

StatusText.Text = "Done";
```

Trying to update a UI element from a background thread typically throws an `InvalidOperationException`.

## Dispatcher

The dispatcher is the prioritized message queue for the UI thread. It processes input, layout, rendering work, data binding work, timers, and your queued callbacks.

Use `InvokeAsync` for asynchronous marshalling instead of blocking the caller.

```csharp
await Dispatcher.InvokeAsync(() =>
{
    ProgressText.Text = "Import completed";
});
```

Avoid `Dispatcher.Invoke` from background operations unless you truly need synchronous blocking. Blocking the wrong thread can cause responsiveness problems or deadlocks.

## Async/Await In WPF

`await` captures the current synchronization context by default. In a WPF event handler, continuation code after `await` usually resumes on the UI thread.

```csharp
private async void LoadButton_Click(object sender, RoutedEventArgs e)
{
    IsEnabled = false;

    try
    {
        var orders = await _orderService.LoadOrdersAsync();
        OrdersList.ItemsSource = orders;
    }
    finally
    {
        IsEnabled = true;
    }
}
```

Use `async void` only for event handlers. Application logic should return `Task` so it can be awaited and tested.

## Background Work

Use naturally asynchronous APIs for I/O. Use `Task.Run` for CPU-bound work that would otherwise block input and rendering.

```csharp
private async Task<IReadOnlyList<Result>> CalculateAsync(Input input)
{
    return await Task.Run(() => ExpensiveCalculator.Calculate(input));
}
```

Do not update observable collections from a background thread unless collection synchronization is configured. A safer pattern is to compute data in the background, then assign or update the bound collection on the UI thread.

```csharp
var items = await Task.Run(LoadItems);
Items = new ObservableCollection<ItemViewModel>(items);
```

## Cancellation And Progress

Desktop operations should usually be cancellable and report progress without blocking the dispatcher.

```csharp
private CancellationTokenSource? _loadCts;

private async Task LoadAsync()
{
    _loadCts = new CancellationTokenSource();
    var progress = new Progress<int>(value => ProgressValue = value);

    try
    {
        await _loader.LoadAsync(progress, _loadCts.Token);
    }
    catch (OperationCanceledException)
    {
        Status = "Canceled";
    }
}
```

`Progress<T>` captures the current synchronization context when it is created, so create it on the UI thread when it updates bound UI state.

## Common Pitfalls

- Blocking the UI thread with `.Result`, `.Wait()`, `Thread.Sleep`, or long loops.
- Calling `Dispatcher.Invoke` too often from background loops.
- Updating `ObservableCollection<T>` from a background thread.
- Creating many small dispatcher operations instead of batching UI updates.
- Doing expensive work inside property getters used by bindings.

## Resources

- WPF threading model: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/threading-model
- Dispatcher class: https://learn.microsoft.com/dotnet/api/system.windows.threading.dispatcher
- Task asynchronous programming model: https://learn.microsoft.com/dotnet/csharp/asynchronous-programming/task-asynchronous-programming-model
- Progress reporting: https://learn.microsoft.com/dotnet/api/system.progress-1
