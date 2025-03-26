using System;
using System.Threading;

// Using Mutex for synchronization access to protected resources.

// Mutex - A synchronization primitive that can also be used for inter-process and cross-domain synchronization.
// MutEx - Mutual Exclusion.

namespace MutexSample
{
    class Program
    {

        // Mutex - A synchronization primitive that can also be used for inter-processor synchronization.
        // It functions similarly to AutoResetEvent but is equipped with additional logic:
        // 1. Remembers which thread owns it. ReleaseMutex cannot be called by a thread that doesn't own the mutex.
        // 2. Manages a recursive counter indicating how many times the owning thread has already owned the object.
        private static readonly Mutex Mutex1 = new Mutex(false, "MutexSample:AAED7056-380D-412E-9608-763495211EA8");

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.ASCII;
            var threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(Function))
                {
                    Name = i.ToString()
                };
                threads[i].Start();
            }

            // Delay.
            Console.ReadKey();
        }

        static void Function()
        {
            bool myMutex = Mutex1.WaitOne();
            
            Console.WriteLine("Thread {0} entered the protected area.", Thread.CurrentThread.Name);
            Thread.Sleep(2000);
            Console.WriteLine("Thread {0} left the protected area.\n", Thread.CurrentThread.Name);
            Mutex1.ReleaseMutex();
        }
    }
}
