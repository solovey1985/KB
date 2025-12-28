using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8.Demos
{
    public class UsingDeclarationDemo
    {
        public void ProcessFile(string filePath)
        {
            using var file = new System.IO.StreamReader(filePath);
            while (!file.EndOfStream)
            {
                Console.WriteLine(file.ReadLine());
            }
            // file will be disposed here, at the end of the method or the scope.
        }
    }
}
