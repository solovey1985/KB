using System;
using System.Threading;

// Alternative operations VolatileWrite() and VolatileRead() to the volatile keyword.

namespace VolatileSample2
{
    class Program
    {
        // static volatile int stop = 0;
        static int stop;

        static void Main()
        {
            Console.WriteLine("Main: starting a thread for 2 seconds.");
            var t = new Thread(Worker);
            t.Start();

            Thread.Sleep(2000);

            Thread.VolatileWrite(ref stop, 1);

            Console.WriteLine("Main: waiting for the thread to finish.");
            t.Join();
        }

        private static void Worker(Object o)
        {
            int x = 0;

            while (Thread.VolatileRead(ref stop) != 1)
            {
                x++;
            }

            Console.WriteLine("Worker: stopped at x = {0}.", x);
        }
    }
}
