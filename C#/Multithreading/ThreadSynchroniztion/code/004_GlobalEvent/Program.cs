using System;
using System.Threading;

// To test this example, run multiple instances of this application.

namespace GlobalEventNs
{
    class Program
    {
        static EventWaitHandle manual = null;

        static void Main()
        {
            // If a kernel object named "GlobalEvent" already exists, a reference to it will be obtained.
            // false - non-signaled state.
            // ManualReset - event type.
            // GlobalEvent::GUID - the name by which all applications will listen to the event.
            manual = new EventWaitHandle(false, EventResetMode.ManualReset, "GlobalEvent::GUID");

            Thread thread = new Thread(Function);
            thread.IsBackground = true;
            thread.Start();

            Console.WriteLine("Press any key to start the thread.");
            Console.ReadKey();

            // Setting the event to a signaled state.
            // All applications using the event named "GlobalEvent" will be notified of the transition to the signaled state.
            manual.Set();

            // Delay.
            Console.ReadKey();
        }

        static void Function()
        {
            manual.WaitOne();

            while (true)
            {
                Console.WriteLine("Hello world!");
                Thread.Sleep(300);
            }
        }
    }
}
