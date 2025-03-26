using System;
using System.Threading;

// Timer - provides a mechanism to execute a method at specified intervals.

namespace TimerSample
{
    class Program
    {
        static void Main()
        {
            AutoResetEvent auto = new AutoResetEvent(false);
            StatusChecker checker = new StatusChecker(15);

            // Represents a method that handles calls from a Timer event.
            TimerCallback checkStatus = new TimerCallback(checker.CheckStatus);

            Console.WriteLine("Creating a timer.\n");

            // Create a timer that signals the delegate to call CheckStatus after one second, and every 1/4 second thereafter.

            // Parameters:
            // checkStatus - TimerCallback delegate representing the method to execute.
            // auto - An object passed to the callback method or null.
            // dueTime: 1000 - Amount of time to delay before checkStatus is invoked, in milliseconds.
            //          (Timeout.Infinite - prevent the timer from starting. Value (0) - start the timer immediately.)
            // period: 250 - Time interval between invocations of the callback method, in milliseconds.
            //          (Timeout.Infinite - disable periodic signaling.)
            var timer = new Timer(checkStatus, auto, 1000, 250);

            // WaitOne - blocks the current thread until the current WaitHandle receives a signal.
            // 5000 - The number of milliseconds to wait, or Timeout.Infinite (-1) to wait indefinitely.
            auto.WaitOne(5000);

            Console.WriteLine("\nChanging the period to 1/2 second.\n");

            // 0 - Amount of time to delay before the callback method is invoked, in milliseconds.
            // 500 - Time interval between invocations of the callback method, in milliseconds.
            timer.Change(0, 500);

            auto.WaitOne(15000);

            Console.WriteLine("\nDestroying the timer.");
            timer.Dispose();

            // Delay.
            Console.ReadKey();
        }
    }

    class StatusChecker
    {
        private int maxCount;
        private int invokeCount;

        public StatusChecker(int maxCount)
        {
            this.maxCount = maxCount;
        }

        public void CheckStatus(Object stateInfo)
        {
            Thread.Sleep(10000);
            // Counting method calls.
            Console.WriteLine("Checking status {0}.", ++invokeCount);
            
            if (invokeCount == maxCount)
            {
                invokeCount = 0;                   // Reset the counter.
                ((AutoResetEvent)stateInfo).Set(); // Send a signal.
            }
        }
    }
}
