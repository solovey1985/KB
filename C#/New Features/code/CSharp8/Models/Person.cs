using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8.Models
{
    public class Person
    {
        public string Name { get; set; }      // Non-nullable
        public string? Nickname { get; set; }  // Nullable
    }
}
