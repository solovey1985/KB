using System;
using System.Globalization;
using System.Threading;

namespace SimpleLock
{
    class Program
    {
        static readonly HybridLock block = new HybridLock();

        static void Main()
        {
            Thread[] threads = { new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString(CultureInfo.InvariantCulture);
                threads[i].Start();
            }

            // Delay.
            Thread.Sleep(1000);
        }

        static void Function()
        {
            block.Enter();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Thread {0} completed the operation.", Thread.CurrentThread.Name);
            } 
            block.Leave();
        }
    }

    class HybridLock : IDisposable
    {
        private int count; // for using the user-mode primitive.
        private readonly AutoResetEvent auto = new AutoResetEvent(false);

        public void Enter()
        {
            if (Interlocked.Increment(ref count) == 1)
                return; 

            auto.WaitOne();
        }

        public void Leave()
        {
            if (Interlocked.Decrement(ref count) == 0)
                return;

            auto.Set();
        }

        public void Dispose()
        {
            auto.Dispose();
        }
    }
}
