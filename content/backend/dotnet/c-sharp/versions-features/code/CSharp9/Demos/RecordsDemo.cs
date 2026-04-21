using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9.Demos
{
    public record Person(string FirstName, string LastName);
    internal class RecordsDemo
    {
        public static void Display()
        {
            Console.WriteLine("New Record");
            var person = new Person("John", "Doe");
            //Updating the person with new value;
            var updatedPerson = person with { FirstName = "Jane" };

        }
    }
}
