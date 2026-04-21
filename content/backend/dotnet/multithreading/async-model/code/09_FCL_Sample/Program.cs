using System;
using System.IO;

namespace AsynchronousProgramming
{
    class Program
    {
        static void Main()
        {
            var stream = new FileStream("file.txt", FileMode.Open, FileAccess.Read);

            var array = new byte[stream.Length];

            // Synchronous call of the method, which will read all bytes of the file.txt into the array.
            // stream.Read(array, 0, array.Length);

            // Asynchronous call of the method to read bytes from the file.
            IAsyncResult asyncResult = stream.BeginRead(array, 0, array.Length, null, null);

            Console.WriteLine("Reading the file ...");

            // Waiting for the file reading to complete.
            stream.EndRead(asyncResult);

            foreach (byte item in array)
                Console.Write(item + " ");

            stream.Close();

            // Delay.
            Console.ReadKey();
        }
    }
}
