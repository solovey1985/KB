using System.Diagnostics;

namespace Threads.SharedResourceAccess
{
    internal class Program
    {
        public static int sum = 0;
        public static object _lock = new object();
        static void Main(string[] args)
        {
            Console.WriteLine("Shared Resource Access");
            Console.WriteLine("With Interlocked.Increment:");
            Stopwatch sw = Stopwatch.StartNew();
            var t1 = new Thread(AddWithInterlocked);
            var t2 = new Thread(AddWithInterlocked);
            var t3 = new Thread(AddWithInterlocked);

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            sw.Stop();
            Console.WriteLine($"Result {sum}; Elapsed: {sw.ElapsedTicks}");

            Console.WriteLine("With lock object:");
            Stopwatch sw1 = Stopwatch.StartNew();
            var w1 = new Thread(AddWithLock);
            var w2 = new Thread(AddWithLock);
            var w3 = new Thread(AddWithLock);
            sum = 0;
            w1.Start();
            w2.Start();
            w3.Start();

            w1.Join();
            w2.Join();
            w3.Join();
            sw1.Stop();
            Console.WriteLine($"Result {sum}; Elapsed: {sw1.ElapsedTicks}");


            Console.WriteLine("With Monitor:");
            Stopwatch sw2 = Stopwatch.StartNew();
            var m1 = new Thread(AddWithMonitor);
            var m2 = new Thread(AddWithMonitor);
            var m3 = new Thread(AddWithMonitor);
            sum = 0;
            m1.Start();
            m2.Start();
            m3.Start();

            m1.Join();
            m2.Join();
            m3.Join();
            sw2.Stop();
            Console.WriteLine($"Result {sum}; Elapsed: {sw2.ElapsedTicks}");

            Console.ReadLine();
        }

        static void AddWithInterlocked()
        {
            for (int i = 0; i < 500000; i++)
            {
                // sum++; // Each time give a different result.
                Interlocked.Increment(ref sum); // Assures the thread safe addition to the sum.
            }
        }

        static void AddWithLock()
        {
            for (int i = 0; i < 500000; i++)
            {
                lock (_lock)
                {
                    sum++;
                }
            }
        }
        static void AddWithMonitor()
        {
            for (int i = 0; i < 500000; i++)
            {
                bool lockTaken = false;
                // Monitor.Enter(_lock);
                Monitor.Enter(_lock, ref lockTaken);
                try
                {
                    sum++;
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(_lock);
                    }

                }
            }
        }
    }
}