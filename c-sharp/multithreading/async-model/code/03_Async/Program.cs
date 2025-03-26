using System;
using System.Threading;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main()
        {
            var myDelegate = new Func<int, int, int>(Add);

            // Since the delegate class communicates with methods that take two integer parameters, the BeginInvoke method also
            // starts to accept two additional parameters, besides the last two constant arguments.
            IAsyncResult asyncResult = myDelegate.BeginInvoke(1, 2, null, null);

            // Waiting for the completion of the asynchronous operation and getting the result of the method's work.
            int result = myDelegate.EndInvoke(asyncResult);

            Console.WriteLine("Result = " + result);

            // Delay.
            Console.ReadKey();
        }

        // Method to be executed in a separate thread.
        static int Add(int a, int b)
        {
            Thread.Sleep(2000);
            return a + b;
        }
    }
}

