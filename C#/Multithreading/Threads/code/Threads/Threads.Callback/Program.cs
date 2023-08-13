using System.Threading;

namespace Threads.Callback
{
    internal class Program
    {
        delegate void Callback(int sum);
        static int _sum;
        static void Main(string[] args)
        {

            Console.WriteLine("Callback demo");
            
            var calculator = new Calculator(2, 3, (int sum) => Console.WriteLine($"Sum: {sum}"));
            var calculator2 = new Calculator(200, 300, DisplayResult);
            ThreadStart threadStart = new ThreadStart(calculator.Sum);
            ThreadStart threadStart2 = new ThreadStart(calculator2.Sum);
            
            var thread = new Thread(threadStart);
            var thread2 = new Thread(threadStart2);
            thread.Start();
            thread2.Start();


            Console.ReadLine();
        }

        static void DisplayResult(int result)
        {
            Console.WriteLine($"The Display Sum: {result}");
        }

        class Calculator
        {
            Callback _callback;
            int _a, _b;
            public Calculator(int a, int b, Callback callback)
            {
                _a = a;
                _b = b;
                _callback = callback;
            }

            internal void Sum()
            {
                if (_callback != null)
                {
                    _callback.Invoke(_a + _b);
                }
            }
        }
    }
}