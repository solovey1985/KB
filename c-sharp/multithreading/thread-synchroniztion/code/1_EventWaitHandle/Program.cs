using System;
using System.Threading;

namespace EventWaitHandleNs
{
    class Program
    {
        // AutoResetEvent - Notifies a waiting thread that an event has occurred.
        static readonly AutoResetEvent auto = new AutoResetEvent(false);

        static void Main()
        {
            Console.WriteLine("Press any key to set the AutoResetEvent to the signaled state.\n");
        
            var thread = new Thread(Function1);
            thread.Start();
            
            Console.ReadKey();
            auto.Set(); // Send a signal to the first thread

            Console.ReadKey();
            auto.Set(); // Send a signal to the second thread
 
            // Delay.
            Console.ReadKey();
        }

        static void Function1()
        {
            Console.WriteLine("Red light");
            auto.WaitOne(); // After WaitOne() completes, AutoResetEvent automatically returns to the non-signaled state.
            Console.WriteLine("Yellow");
            auto.WaitOne(); // After WaitOne() completes, AutoResetEvent automatically returns to the non-signaled state.
            Console.WriteLine("Green");
        }
    }
}
