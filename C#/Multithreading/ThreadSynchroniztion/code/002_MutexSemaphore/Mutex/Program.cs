using System;
using System.Threading;

// Using Mutex for synchronization to access protected resources.

// Mutex - A synchronization primitive that can also be used for inter-process and inter-domain synchronization.
// MutEx - Mutual Exclusion.

namespace MyNamespace
{
    class Program
    {
        private static Mutex mutex = new Mutex();

        static void Main()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(new ThreadStart(Function));
                thread.Name = String.Format("Thread {0}", i + 1);
                thread.Start();
            }

            // Delay.
            Console.ReadKey();
        }

        private static void Function()
        {
            for (int i = 0; i < 2; i++)
            {
                UseResource();
            }
        }

        // This method represents a resource that needs to be synchronized so that only one thread can execute it at a time.
        private static void UseResource()
        {
            // The WaitOne method is used to request ownership of the mutex.
            // It blocks the current thread.
            mutex.WaitOne();

            Console.WriteLine("{0} entered the protected area.", Thread.CurrentThread.Name);
            Thread.Sleep(1000); // Performing some work...
            Console.WriteLine("{0} is leaving the protected area.\r\n", Thread.CurrentThread.Name);
            
            mutex.ReleaseMutex();  // Releasing the Mutex.

            Thread.Sleep(1000); // Performing some work...
        }
    }
}
