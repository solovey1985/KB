using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8.Interfaces
{
    public interface IGreeter
    {
        void Greet();
        void SayBye() { Console.WriteLine("Goodbye!"); }  // Default implementation
    }

    public class Greeter : IGreeter
    {
        public void Greet()
        {
            Console.WriteLine("Hello, World!");
        }

    }
}
