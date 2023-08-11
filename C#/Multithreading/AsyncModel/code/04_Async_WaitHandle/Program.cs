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
            Console.WriteLine("Main method is waiting for the completion of the asynchronous task.");

            Console.WriteLine(asyncResult.AsyncWaitHandle.GetType());

            // AsyncWaitHandle of type WaitHandle, goes into a signaling state when the asynchronous operation is completed.
            asyncResult.AsyncWaitHandle.WaitOne();
            Console.WriteLine("Asynchronous method completed.");

            // Getting the result of the asynchronous operation.
            int result = myDelegate.EndInvoke(asyncResult);

            // Closing the WaitHandle.
            asyncResult.AsyncWaitHandle.Close();

            Console.WriteLine("Result = " + result);

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

