using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8.Demos
{
    public class InterpolationDemo
    {
        public void Display(double value)
        {
            Console.WriteLine($"Value: {value:F2}");  // Formats value to 2 decimal places
        }
    }
}
