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

            // BeginInvoke - execute the Method in a separate thread, taken from the thread pool.
            // IAsyncResult - represents the state of an asynchronous operation.
            IAsyncResult asyncResult = myDelegate.BeginInvoke(null, null);

            Console.WriteLine("Primary thread continues to work.");

            // Waiting for the completion of the asynchronous operation.
            myDelegate.EndInvoke(asyncResult);

            Console.WriteLine("Primary thread has finished its work.");

            // Delay.
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
                Thread.Sleep(50);
                Console.Write(".");
            }

            Console.WriteLine("Asynchronous operation completed.\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}

