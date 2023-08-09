using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8.Demos
{
    public class AsyncStreamDemo
    {
        public async IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(100);  // Simulating some async work.
                yield return i;
            }
        }
    }
}
