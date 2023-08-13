using System.Threading;


namespace Threads.MonitorUsage
{


    internal class Program
    {
        public static int sum = 0;
        public static object _lock = new object();
        static void Main(string[] args)
        {
            Console.WriteLine("Monitor usage Demo");
            var reader = new Thread(Read);
            var writer = new Thread(Write);

            writer.Start();
            reader.Start();

            writer.Join();
            reader.Join();
            Console.ReadLine();

        }

        static void Write()
        {
            Monitor.Enter(_lock);
            for (int i = 0; i < 5; i++)
            {
                Monitor.Pulse(_lock);
                Console.WriteLine("Writing started => " + i);
                Console.WriteLine("Writing completed => " + i);
                Monitor.Wait(_lock);
            }
        }

        static void Read()
        {
            Monitor.Enter(_lock);
            for (int i = 0; i < 5; i++)
            {
                Monitor.Pulse(_lock);
                Console.WriteLine("Reading started => " + i);

                Console.WriteLine("Reading completed => " + i);
                Monitor.Wait(_lock);
            }
        }
    }
}