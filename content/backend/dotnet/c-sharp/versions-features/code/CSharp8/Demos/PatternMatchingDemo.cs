using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSharp8.Models;

namespace CSharp8.Demos
{
    internal class PatternMatchingDemo
    {
        public void DisplayShape(Shape shape)
        {
            switch (shape)
            {
                case Circle c:
                    Console.WriteLine($"Circle with radius {c.Radius}");
                    break;
                case Rectangle r when r.Width == r.Height:
                    Console.WriteLine("Square");
                    break;
                case Rectangle r:
                    Console.WriteLine($"Rectangle with width {r.Width} and height {r.Height}");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(shape));
                default:
                    Console.WriteLine("Unknown shape");
                    break;
            }
        }
    }
}
