using System;
using System.Threading;

// Locking threads that are waiting indefinitely for access to a kernel object is an inefficient use of memory.
// The thread pool offers a mechanism to call a method.

namespace RegistredWaitHandleNs
{
    class Program
    {
        static void Main()
        {
            AutoResetEvent auto = new AutoResetEvent(false);
            WaitOrTimerCallback callback = new WaitOrTimerCallback(CallbackMethod);

            // auto - who to wait for a signal from
            // callback - what to execute
            // null - 1st argument of the Callback method
            // 1000 - interval between calls to the Callback method
            // if true - call the Callback method once. If false - call the Callback method at intervals.
            // ThreadPool.RegisterWaitForSingleObject(auto, callback, null, Timeout.Infinite, true);

            var waitHandle = ThreadPool.RegisterWaitForSingleObject(auto, callback, null, 1000, false); 
        
            Console.WriteLine("S - signal, Q - exit");

            while (true)
            {
                string operation = Console.ReadKey(true).KeyChar.ToString().ToUpper();

                if (operation == "S")
                {
                   auto.Set();
                }
                if (operation == "Q")
                {
                    waitHandle.Unregister(auto);
                    break;
                }
            }
            Console.ReadKey();
        }

        static void CallbackMethod(object state, bool timedOut)
        {
            Thread.Sleep(5000);
            Console.WriteLine("Signal");
        }
    }
}
