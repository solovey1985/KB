using System;
using System.Threading;

namespace EventSlim
{
    class Program
    {
        // ManualResetEventSlim initially uses a SpinWait lock for 1000 iterations,
        // after which synchronization occurs using a kernel object.
        static ManualResetEventSlim slim = new ManualResetEventSlim(false, 1000);

        static void Main()
        {
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start();
            }

            Console.ReadKey();
            slim.Set();

            // Delay.
            Console.ReadKey();
        }

        static void Function()
        {
            slim.Wait();
            Console.WriteLine("Thread {0} started.", Thread.CurrentThread.Name);
        }
    }
}
