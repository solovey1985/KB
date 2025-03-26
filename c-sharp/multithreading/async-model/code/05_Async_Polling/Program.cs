using System;
using System.Threading;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main()
        {
            var myDelegate = new Func<int, int, int>(Add);

            // Starting an asynchronous task.
            IAsyncResult asyncResult = myDelegate.BeginInvoke(1, 2, null, null);

            Console.WriteLine("Asynchronous method started. Main method continues to work.");

            // Execute the loop until the asynchronous operation is running.
            while (!asyncResult.IsCompleted)
            {
                Thread.Sleep(100);
                Console.Write(".");
            }

            // Getting the result of the asynchronous operation.
            int result = myDelegate.EndInvoke(asyncResult);

            Console.WriteLine("\nResult = " + result);

            // Delay.
            Console.ReadKey();
        }

        // Method to be executed in a separate thread.
        static int Add(int a, int b)
        {
            Thread.Sleep(3000);
            return a + b;
        }
    }
}
