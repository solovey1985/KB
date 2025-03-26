using System;
using System.Threading;

/*   
    The volatile keyword can be applied to fields of the following types:
    1. Reference types.
    2. Types such as sbyte, byte, short, ushort, int, uint, char, float, and bool.
    3. An enumeration type with one of the following base types: byte, sbyte, short, ushort, int, or uint.
    4. Generic type parameters that are reference types.
    
    The volatile keyword can only be applied to fields of a class or structure.
    Local variables cannot be declared as volatile.
*/

namespace VolatileSample
{
    class Program
    {
        // Fields declared as volatile are not subject to compiler optimization that assumes access by a separate thread. 
        // This ensures that the most up-to-date value is present in the field at all times.
        // The keyword ensures that when reading and writing, manipulation will occur directly with memory and not with values 
        // that are cached in processor registers.

        static volatile bool stop; // Without JIT optimization.
        //static bool stop; // With JIT optimization.

        static void Main()
        {
            Console.WriteLine("Main: starting a thread for 2 seconds.");
            var thread = new Thread(Worker);
            thread.Start();

            Thread.Sleep(2000);

            stop = true;
            Console.WriteLine("Main: waiting for the thread to finish");
            thread.Join();
        }

        private static void Worker(Object o)
        {
            // When compiling this code, the JIT compiler will detect that the stop variable does not change in the method.
            // The JIT compiler can create code that pre-checks the state of the stop variable
            // and if it's true, it will immediately output the result "Worker: stopped at x = 0".
            // Otherwise, the JIT compiler creates code that enters an infinite loop and infinitely increments the x variable.

            // ATTENTION! Optimization is not performed in debug mode (DEBUG).
            int x = 0;

            while (!stop)
            {
                // checked
                {
                    x++;
                }
            }

            // Code after JIT optimization, if stop is not volatile:
            // (If stop is volatile, then JIT compiler optimization will not be performed.)
            // int x = 0;         
            // if (stop != true)
            // {
            //     while (true)
            //     {
            //         x++;
            //     }
            // }            

            Console.WriteLine("Worker: stopped at x = {0}.", x);
        }
    }
}
