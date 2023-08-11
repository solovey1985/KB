using System;
using System.Threading;

// ManualResetEvent

namespace Event
{
    class Work
    {
        readonly ManualResetEvent manual;
        readonly Thread thread;

        public Work(string name, ManualResetEvent manual)
        {
            this.thread = new Thread(this.Run) {Name = name};
            this.manual = manual;
            this.thread.Start();
        }

        void Run()
        {
            Console.WriteLine("Thread " + thread.Name + " started");

            for (int i = 0; i < 10; i++)
            {
                Console.Write(". ");
                Thread.Sleep(200);
            }

            Console.WriteLine("\nThread " + thread.Name + " finished");

            // Notification about the event is done using the Set() method
            manual.Set();
        }
    }

    class ManualEventDemo
    {
        static void Main()
        {
            // false - sets the initial state to nonsignaled.
            var manual = new ManualResetEvent(false);

            var thread = new Work("1", manual);
            Console.WriteLine("Main thread is waiting for the event.\n");

            manual.WaitOne(); // Wait for the event notification.
            Console.WriteLine("\nMain thread received event notification from the first thread.\n");

            manual.Reset(); // Resets the event to the nonsignaled state.

            thread = new Work("2", manual);
            Console.WriteLine("Main thread is waiting for the event.\n");

            manual.WaitOne(); // Wait for the event notification.
            Console.WriteLine("\nMain thread received event notification from the second thread.");

            // Delay.
            Console.ReadKey();
        }
    }
}
