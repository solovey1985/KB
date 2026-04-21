using System;
using System;
using System.Threading;

namespace AsynchronousProgramming
{

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Primary thread: Id {0}", Thread.CurrentThread.ManagedThreadId);

            var myDelegate = new Action(Method);

            // Execute the Method in a separate thread, taken from the thread pool.
            myDelegate.BeginInvoke(null, null);
            Console.WriteLine("Main");
            // Delay.
            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(50);
                Console.Write(".");
            }
            Console.ReadKey();
        }

        // Method to be executed in a separate thread.
        static void Method()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAsynchronous method started.");
            Console.WriteLine("\nSecondary thread: Id {0}", Thread.CurrentThread.ManagedThreadId);

            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(60);
                Console.Write("A");
            }

            Console.WriteLine("Asynchronous operation completed.\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
