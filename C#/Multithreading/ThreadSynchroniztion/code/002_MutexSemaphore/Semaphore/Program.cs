using System;
using System.Threading;

// The Semaphore class is used to control access to a resource pool.
// Threads occupy a semaphore slot by calling the WaitOne() method and release the occupied slot by calling the Release() method.

namespace MyNamespace
{
    public class Program
    {
        private static Semaphore pool;

        private static void Work(object number)
        {
            pool.WaitOne();

            Console.WriteLine("Thread {0} occupied a semaphore slot.", number);
            Thread.Sleep(1000);
            Console.WriteLine("Thread {0} -----> released the slot.", number);

            pool.Release();
        }

        public static void Main()
        {
            // First argument:
            // Specifies the number of slots available for immediate use (up to the maximum number).
            // Second argument:
            // Specifies the maximum number of slots for this semaphore.
            pool = new Semaphore(2, 4, "MySemafore65487563487");
     
            for (int i = 1; i <= 8; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(Work));
                thread.Start(i);
            }
            Thread.Sleep(2000);
            pool.Release(2);
            
            // Delay.
            Console.ReadKey();
        }
    }
}
