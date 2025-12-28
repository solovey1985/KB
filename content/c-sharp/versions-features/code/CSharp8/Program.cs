using System;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

using CSharp8.Demos;
using CSharp8.Interfaces;
using CSharp8.Models;

using KB.Helper;
namespace CSharp8
{
    public class Program
    {
        static void Main(string[] args)
        {
            DisplayHelper display = new DisplayHelper();
            display.AddItemsForDisplay(1, "Nullable Reference Types", Demo.DefaultInterfaceMembersDemo);
            display.AddItemsForDisplay(2, "Default Interface Members", Demo.DefaultInterfaceMembersDemo);
            display.AddItemsForDisplay(3, "Pattern Matching Enhancements", Demo.PatternMatchingEnhancementsDemo);
            display.AddItemsForDisplay(4, "Using Declarations", Demo.UsingDeclarationsDemo);
            display.AddItemsForDisplay(5, "Using Declarations", Demo.UsingDeclarationsDemo);
            display.AddItemsForDisplay(6, "Using Declarations", Demo.UsingDeclarationsDemo);
            display.AddItemsForDisplay(7, "Using Declarations", Demo.UsingDeclarationsDemo);
            display.AddItemsForDisplay(8, "Using Declarations", Demo.UsingDeclarationsDemo);
            display.AddItemsForDisplay(9, "Using Declarations", Demo.UsingDeclarationsDemo);

            string input = string.Empty;
            Console.WriteLine("Select the feature to demo.");
            Console.WriteLine(display.GenerateIntroScreen());
            while (input != "q")
            {
                input = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(input, out var demo))
                {
                    Console.WriteLine(display.DisplayDemoTitle(demo) ?? demo.ToString());
                    display.InvokeItem(demo);
                }
                else
                {
                    Console.WriteLine("Invalid input. Select from expected values.");
                }
                Console.WriteLine("Select Another example");
            }
        }
        internal static class Demo
        {
            internal static void NullableReferenceTypesDemo()
            {

            }
            internal static void DefaultInterfaceMembersDemo()
            {
                // Use intergace as variable type otherwise it will not be accessible.
                IGreeter greeter = new Greeter();
                Console.WriteLine("Calling the class implementation.");
                greeter.Greet();
                Console.WriteLine("Calling the default interface implementation.");
                greeter.SayBye();
            }
            internal static void PatternMatchingEnhancementsDemo()
            {
                var patternMatchingDemo = new PatternMatchingDemo();
                var circle = new Circle() { Radius = 1 };
                patternMatchingDemo.DisplayShape(circle);
                var rectangle = new Rectangle() { Width = 10, Height = 10 };
                patternMatchingDemo.DisplayShape(rectangle);

            }
            internal static void UsingDeclarationsDemo()
            {
                Console.WriteLine("Processing some file.");
                var file = File.Create("some file.txt");
                file.Close();
                var demo = new UsingDeclarationDemo();
                demo.ProcessFile(file.Name);
            }

            internal static void AsyncStreamDemo()
            {
                Console.WriteLine("");
            }

            internal static void IndicesAndRangesDemo()
            {

            }
            internal static void StaticLocalFunctionDemo()
            {

            }
            internal static void InterpolationDemo()
            {

            }

        }
    }
}