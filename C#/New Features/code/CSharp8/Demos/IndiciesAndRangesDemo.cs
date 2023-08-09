using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8.Demos
{
    public class IndicesAndRangesDemo
    {
        public void Display()
        {
            var numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Using indices from end
            int lastElement = numbers[^1];  // 9

            // Using ranges
            int[] middle = numbers[2..5];   // { 2, 3, 4 }
        }
    }
}
