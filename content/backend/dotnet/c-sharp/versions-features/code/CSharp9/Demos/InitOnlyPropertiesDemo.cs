using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9.Demos
{
    public class Product
    {
        public string Name { get; init; } = string.Empty; // Make assignment as there is no default constructor to setup this non-nullable property.
        public decimal Price{ get; init; }
    }
    internal class InitOnlyPropertiesDemo
    {

        public void Display()
        {
            var product = new Product() {Name = "Some product", Price = 12m };
            Console.WriteLine($"Name = {product.Name}, Price = {product.Price}");
            // Cannot assign new value
            //product.Price = 12;
        }
    }
}
