using System;
using System.Threading;

namespace WaitHandleNs
{
    class Program
    {
        static WaitHandle[] events = new WaitHandle[] { new AutoResetEvent(false), new AutoResetEvent(false) };

        static Random random = new Random();

        static void Main()
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine("Main thread is waiting for the completion of BOTH tasks.\n");

            // Queue for two tasks in two different threads.
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task1), events[0]);
            ThreadPool.QueueUserWorkItem(Task2, events[1]);

            // Wait until all tasks are completed.
            WaitHandle.WaitAll(events);

            // The time displayed below should match the duration of the longest task.
            Console.WriteLine("Both tasks are completed (waiting time = {0})", (DateTime.Now - dateTime).TotalMilliseconds);

            dateTime = DateTime.Now;

            Console.WriteLine("\nWaiting for the completion of one of the tasks.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task1), events[0]);
            ThreadPool.QueueUserWorkItem(Task2, events[1]);

            // Wait until one of the tasks is completed.
            int index = WaitHandle.WaitAny(events);

            // The time displayed below should match the duration of the shortest task.
            Console.WriteLine("Task {0} finished first (waiting time = {1}).", index + 1, (DateTime.Now - dateTime).TotalMilliseconds);

            // Delay.
            Console.ReadKey();
        }

        static void Task1(Object state)
        {
            var auto = (AutoResetEvent)state;
            int time = 1000 * random.Next(2, 10);
            Thread.Sleep(time);
            Console.WriteLine("Task 1 completed in {0} milliseconds.", time);
            auto.Set();
        }

        static void Task2(Object state)
        {
            var auto = (AutoResetEvent)state;
            int time = 1000 * random.Next(2, 10);
            Thread.Sleep(time);
            Console.WriteLine("Task 2 completed in {0} milliseconds.", time);
            auto.Set();
        }
    }
}
