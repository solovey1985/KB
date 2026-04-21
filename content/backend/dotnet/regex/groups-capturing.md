# Groups of Regular Expressions in C# and .NET

Below are two examples that illustrate how to use capturing groups in .NET regular expressions, along with details on how to retrieve the captured groups and their individual captures.

## Example 1: Using Named Capturing Groups

In this example, we use a named group to extract parts of a formatted string such as a person's full name and phone number. The named groups make it easier to reference the captured parts by name rather than by number.

```csharp
using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Input string containing a full name and a phone number.
        string input = "John Smith, 123-4567";
        
        // Pattern with named capturing groups:
        // (?<FirstName>\w+) captures the first name,
        // (?<LastName>\w+) captures the last name,
        // (?<Phone>\d{3}-\d{4}) captures the phone number.
        string pattern = @"(?<FirstName>\w+)\s(?<LastName>\w+),\s(?<Phone>\d{3}-\d{4})";
        
        Regex regex = new Regex(pattern);
        Match match = regex.Match(input);
        
        if (match.Success)
        {
            // Retrieve the captured values using the group names.
            string firstName = match.Groups["FirstName"].Value;
            string lastName = match.Groups["LastName"].Value;
            string phone = match.Groups["Phone"].Value;
            
            Console.WriteLine("Matched using named groups:");
            Console.WriteLine($"First Name: {firstName}");
            Console.WriteLine($"Last Name: {lastName}");
            Console.WriteLine($"Phone Number: {phone}");
        }
        else
        {
            Console.WriteLine("No match found.");
        }
    }
}
```

**Explanation:**

- **Named Groups:**  
  The pattern uses `(?<GroupName>pattern)` to define named groups. For example, `(?<FirstName>\w+)` matches one or more word characters and labels it "FirstName".

- **Accessing Captured Groups:**  
  Once a match is found, you access the captured text with `match.Groups["GroupName"].Value`. This improves readability, especially when multiple groups are involved.

- **Use Case:**  
  Named groups are ideal for when you need to extract and identify specific portions of a match (e.g., separating components of structured data).

---

## Example 2: Capturing Multiple Occurrences with a Single Group

When a capturing group is repeated (for example, matching individual digits), only the final captured match is returned by the group’s `Value` property. However, the `Captures` collection holds all the matches made by that group.

```csharp
using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Input string of digits.
        string input = "1234";
        
        // Pattern with a capturing group that is repeated.
        // Each digit is captured by the group named "digit".
        string pattern = @"(?<digit>\d)";
        
        Regex regex = new Regex(pattern);
        Match match = regex.Match(input);
        
        if (match.Success)
        {
            // The .Value returns the last capture ("4" in this case)
            Group digitGroup = match.Groups["digit"];
            Console.WriteLine("Last capture (using .Value): " + digitGroup.Value);
            
            // The Captures collection holds every individual capture ("1", "2", "3", "4").
            Console.WriteLine("All captures for group 'digit':");
            foreach (Capture capture in digitGroup.Captures)
            {
                Console.WriteLine(capture.Value);
            }
        }
        else
        {
            Console.WriteLine("No match found.");
        }
    }
}
```

**Explanation:**

- **Repeating Capturing Groups:**  
  The pattern `(?<digit>\d)` is applied repeatedly over the input string "1234". Although `digitGroup.Value` returns only the last captured value ("4"), the `digitGroup.Captures` collection includes every individual capture.

- **Using the `Captures` Collection:**  
  By iterating over the `Captures` collection, you can access every instance that matched the capturing group in the overall pattern. This is useful when your pattern is designed to capture multiple, non-overlapping segments of the input.

- **Practical Uses:**  
  This method is particularly helpful in scenarios where a regex pattern is designed to match a sequence of items (such as digits, words, or tokens) and you need to process each captured occurrence separately.

---

## Summary

- **Group Usage:**  
  Capturing groups in .NET regular expressions allow you to isolate portions of the matched text. You can define groups using parentheses (`()`) and label them with `(?<Name>...)` for easier retrieval.

- **Accessing Captured Data:**  
  Use `match.Groups["Name"].Value` (or `match.Groups[index].Value`) for the most recent capture, and iterate over `match.Groups["Name"].Captures` to obtain all captures for a group that occurs multiple times.

These examples should help you understand how to define, capture, and extract group data using regular expressions in C# with the .NET framework.
