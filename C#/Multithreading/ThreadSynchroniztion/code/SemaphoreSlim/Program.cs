using System;
using System.Threading;

namespace SemaphoreSlimSample
{
    class Program
    {
        // SemaphoreSlim - a lightweight semaphore class that doesn't use kernel synchronization objects.
        static readonly SemaphoreSlim slim = new SemaphoreSlim(1, 2);

        static void Main()
        {
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start();
            }

            Thread.Sleep(1000);
            slim.Release();  // A forced reset from the semaphore owner thread is possible.

            // Delay.
            Console.ReadKey();
        }

        static void Function()
        {
            slim.Wait();

            Console.WriteLine("Thread {0} started.", Thread.CurrentThread.Name);
            Thread.Sleep(1000);
            Console.WriteLine("Thread {0} finished.\n", Thread.CurrentThread.Name);

            slim.Release();
        }
    }
}
