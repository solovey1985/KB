using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace AsynchronousProgramming
{
    class Program
    {
        // Метод для выполнения в отдельном потоке.
        static int Method(int a, int b)
        {
           // Thread.CurrentThread.IsBackground = false;

            Console.WriteLine("Вторичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            return a + b;
        }

        // Метод обработки завершения асинхронной операции.
        static void CallBack(IAsyncResult iAsyncResult)
        {
            // Получение экземпляра делегата, на котором была вызвана асинхронная операция.
            var asyncResult = iAsyncResult as AsyncResult;
            var caller = (Func<int,int,int>)asyncResult.AsyncDelegate;

            // Получение результатов асинхронной операции.
            int sum = caller.EndInvoke(iAsyncResult);
            
            string result = string.Format(iAsyncResult.AsyncState.ToString(), sum);
            Console.WriteLine("Результат асинхронной операции: " + result);
        }

        static void Main()
        {
            Console.WriteLine("Первичный поток: Id {0}", Thread.CurrentThread.ManagedThreadId);

            var myDelegate = new Func<int,int,int>(Method);

            myDelegate.BeginInvoke(1, 2, CallBack, "a + b = {0}");
            
            Console.WriteLine("Первичный поток завершил работу."); 
            
            // Delay.
            Console.ReadKey();
        }
    }
}
