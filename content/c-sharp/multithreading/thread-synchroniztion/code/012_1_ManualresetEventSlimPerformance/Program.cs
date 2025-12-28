using System;
using System.Diagnostics;
using System.Threading;

namespace _012_1_ManualresetEventSlimPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            var mres = new ManualResetEventSlim(false);
            var mres2 = new ManualResetEventSlim(false);

            var mre = new ManualResetEvent(false);

            long total = 0;
            int COUNT = 5;

            for (int i = 0; i < COUNT; i++)
            {
                mres2.Reset();
                // elapsed time counter
                Stopwatch sw = Stopwatch.StartNew();

                // start setting in a thread pool thread
                ThreadPool.QueueUserWorkItem((obj) =>
                {
                  //  MethodSlim(mres, true);
                    Method(mre, true);
                    mres2.Set();
                });
                // start reset in the main thread
              //  MethodSlim(mres, false);
                Method(mre, false);

                // Wait for the thread pool thread to complete
                mres2.Wait();
                sw.Stop();

                Console.WriteLine("Pass {0}: {1} ms", i, sw.ElapsedMilliseconds);
                total += sw.ElapsedMilliseconds;
            }

            Console.WriteLine();
            Console.WriteLine("===============================");
            Console.WriteLine("Done in average=" + total / (double)COUNT);
            Console.ReadLine();
        }

         // work with ManualResetEventSlim
      private static void MethodSlim(ManualResetEventSlim mre, bool value)
      {
        // repeat the action a sufficiently large number of times in a loop
        for (int i = 0; i < 9000000; i++)
        {
          if (value)
          {
            mre.Set();
          }
          else
          {
            mre.Reset();
          }
        }
      }

      // work with the classic ManualResetEvent
      private static void Method(ManualResetEvent mre, bool value)
      {
        // repeat the action a sufficiently large number of times in a loop
        for (int i = 0; i < 9000000; i++)
        {
          if (value)
          {
            mre.Set();
          }
          else
          {
            mre.Reset();
          }
        }
      }
    }
}
