using System;
using System.Threading;

// By default in the Asynchronous pattern, IsBackground = true (With the completion of the primary thread, the secondary thread also completes).
// IsBackground = false (The primary thread waits for the secondary thread to finish).

namespace AsynchronousProgramming
{
    class Program
    {
        private static void Function()
        {
            Thread.CurrentThread.IsBackground = false; // Comment this out.

            Console.WriteLine("Secondary thread started.");

            for (int i = 0; i < 240; i++)
            {
                Thread.Sleep(20);
                Console.Write(".");
            }

            Console.WriteLine("\nSecondary thread finished.");
        }

        private static void CallBack(IAsyncResult asyncResult)
        {
            var work = asyncResult.AsyncState as Action;
            if (work != null) work.EndInvoke(asyncResult);
        }

        static void Main()
        {
            Console.WriteLine("Primary thread started.");

            var work = new Action(Function);
            work.BeginInvoke(new AsyncCallback(CallBack), (object)work);

            Thread.Sleep(1000);
            Console.WriteLine("\nPrimary thread finished.\n");
        }
    }
}
