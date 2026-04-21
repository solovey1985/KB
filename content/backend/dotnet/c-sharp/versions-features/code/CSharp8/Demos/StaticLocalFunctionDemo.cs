using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8.Demos
{
    public class StaticLocalFunctionDemo
    {
        public int Calculate(int x, int y)
        {
            return Add(x, y);

            static int Add(int a, int b)
            {
                return a + b;
            }
        }
    }
}
