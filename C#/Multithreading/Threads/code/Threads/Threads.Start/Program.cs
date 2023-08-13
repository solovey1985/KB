namespace Threads.Start
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //----Synchronous approach------
            //Method1();
            //Console.WriteLine("Main: Method1 finished");
            //Method2();
            //Console.WriteLine("Main: Method2 finished");

            // ------Multithreading approach--------
            Thread thread = new Thread(Method1);
            thread.Name = "DefaultThread";               // Assign name for the thread.

            ThreadStart threadStart = new ThreadStart(Method1);
            Thread threadStartThread = new Thread(threadStart);
            threadStartThread.Name = "threadStartThread";
            ThreadStart delegateStart = delegate () { Method1(); };

            Thread delegateThread = new Thread(delegateStart);
            delegateThread.Name = "delegateThread";

            // ----Parametrized Start----------
            ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(Method2);
            Thread parametrizedThread = new Thread(parameterizedThreadStart);
            parametrizedThread.Name = "parametrizedThread";

            // --- Type-Safe approach for parametrized start.----
            var helper = new NumberHelper(10);
            Thread safeThread = new Thread(helper.Method3);
            safeThread.Name = "safeThread";


            thread.Start();
            threadStartThread.Start();
            delegateThread.Start();
            parametrizedThread.Start(10);
            
            safeThread.Start();
            parametrizedThread.Join(2000); // Wait for 2 seconds;
            if (parametrizedThread.IsAlive)
            {
                Console.WriteLine("parametrizedThread is running");
            }
            else
            {
                Console.WriteLine("parametrizedThread has completed.");
            }
            safeThread.Join(); // Waits until thread finished its task then executes the next line of code.
            

            Console.WriteLine("Main method finished.");
            Console.ReadLine();
        }

        static void Method1()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: Executing Method1 =>");
            for (int i = 0; i < 10; i++)
            {
                if (i % 3 == 0)
                {
                    Thread.Sleep(1000);
                }
                Console.WriteLine($"{Thread.CurrentThread.Name}: {i}.");
            }
            Console.WriteLine($"=> {Thread.CurrentThread.Name}: Method1 finished.");
        }

        static void Method2(object? number)
        {
            Console.WriteLine($"{Thread.CurrentThread.Name}: Executing Method2 =>");
            for (int i = 0; i < (int)number!; i++) // Possible FormatException. Fixed in Method3
            {
                if (i % 2 == 0)
                {
                    Thread.Sleep(1000); // Some long running operation
                }
                Console.WriteLine($"{Thread.CurrentThread.Name}: {i}.");
            }
            Console.WriteLine($"=>{Thread.CurrentThread.Name}: Method2 finished.");
        }

        internal class NumberHelper
        {
            private int _number;
            public NumberHelper(int number)
            {
                _number = number;
            }

            internal void Method3()
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: Executing Method3 =>");
                for (int i = 0; i < _number; i++)
                {
                    if (i % 2 == 0)
                    {
                        Thread.Sleep(1000); // Some long running operation
                    }
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {i}.");
                }
                Console.WriteLine($"=>{Thread.CurrentThread.Name} Method3 finished.");
            }
        }
    }
}