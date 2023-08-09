using System;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

using KB.Helper;
namespace CSharp8
{
    public class Program
    {
        static void Main(string[] args)
        {
            DisplayHelper display = new DisplayHelper();
            display.AddItemsToDisplay(1, "Default Interface Members");
            display.AddItemsToDisplay(2, "Pattern Matching Enhancements");
            display.AddItemsToDisplay(3, "Using Declarations");
            display.AddItemsToInvoke(1, DefaultInterfaceMembersDemo);
            display.AddItemsToInvoke(2, PatternMatchingEnhancementsDemo);
            display.AddItemsToInvoke(3, UsingDeclarationsDemo);
            string input = string.Empty;
            Console.WriteLine("Select the feature to demo.");
            Console.WriteLine(display.GenerateIntroScreen());
            while (input != "q")
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out var demo))
                {
                    display.InvokeItem(demo);
                }
                Console.WriteLine("Select Another example");
                input = Console.ReadLine();
            }
        }

        static void DefaultInterfaceMembersDemo()
        {
            Console.WriteLine("1");
        }
        static void PatternMatchingEnhancementsDemo()
        {
            Console.WriteLine("2");
        }
        static void UsingDeclarationsDemo()
        {
            Console.WriteLine("3");
        }
    }
}