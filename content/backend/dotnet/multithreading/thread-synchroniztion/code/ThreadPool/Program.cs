using System;
using System.Threading;

// Thread pool.

namespace ThreadPoolNs
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Program start");
            ShowThreadInfo();
            Console.WriteLine("Starting Task1 in a thread from the thread pool");
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task1));
            ShowThreadInfo();
            Console.WriteLine("Starting Task2 in a thread from the thread pool");
            Thread.Sleep(1000);
            ThreadPool.QueueUserWorkItem(Task2);
            ShowThreadInfo();
            Console.WriteLine("Main thread.");

            Thread.Sleep(1000);

            Console.WriteLine("Main thread finished.\n");

            // Delay.
            Console.WriteLine("Task1 and Task2 finished their work");
            ShowThreadInfo();
            Console.ReadKey();
        }

        static void Task1(Object state)
        {
            Thread.CurrentThread.Name = "1";
            Console.WriteLine("Thread {0}:{1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine();
            Thread.Sleep(200);
        }

        static void Task2(Object state)
        {
            Thread.CurrentThread.Name = "2";
            Console.WriteLine("Thread {0}:{1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(200);
        }

        // When the GetAvailableThreads method returns a value, the variable specified by the workerThreads parameter 
        // contains the number of additional worker threads that can be started, and the variable specified by the 
        // completionPortThreads parameter contains the number of additional asynchronous I/O threads that can be started.

        // If available threads are absent, additional requests to the thread pool will remain in the queue until 
        // available threads appear in the thread pool.
        static void ShowThreadInfo()
        {
            int availableWorkThreads, availableIOThreads, maxWorkThreads, maxIOThreads;
            ThreadPool.GetAvailableThreads(out availableWorkThreads, out availableIOThreads);
            ThreadPool.GetMaxThreads(out maxWorkThreads, out maxIOThreads);
            Console.WriteLine("-------------Available worker threads in the pool: {0} out of {1}", availableWorkThreads, maxWorkThreads);
            Console.WriteLine("-------------Available I/O threads in the pool: {0} out of {1}\n", availableIOThreads, maxIOThreads);
        }
    }
}
