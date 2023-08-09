using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9.Demos
{
    public class PatternDemo
    {
        public void Display(object obj)
        {
            if (obj is not null)  // `not` pattern
            {
                Console.WriteLine(obj);
            }

            if (obj is int { } i)  // Property pattern and declaration pattern combined
            {
                Console.WriteLine($"It's an integer with value: {i}");
            }
        }
    }

}
