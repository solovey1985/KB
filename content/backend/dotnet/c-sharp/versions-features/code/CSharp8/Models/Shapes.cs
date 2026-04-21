using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8.Models
{
    public class Shape
    {
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

}
