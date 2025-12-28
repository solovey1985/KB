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

            // Delegate, the method of which will be launched upon completion of the asynchronous operation.
            var callback = new AsyncCallback(HandleCompletion);

            // First parameter: 
            // Accepts a callback method that should be triggered upon completion of the asynchronous operation.
            // Second parameter: 
            // An additional object storing the state, which will be available in the callback method.
            myDelegate.BeginInvoke(callback, null);

            Console.WriteLine("Primary thread continues to work.");

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

        // Callback method to handle the completion of the asynchronous operation.
        static void HandleCompletion(IAsyncResult asyncResult)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Callback method. Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
