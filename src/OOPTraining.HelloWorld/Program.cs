using System;
using System.Collections.Generic;

// Top-level statements (C# 9+) - no need for Main method or class declaration
Console.WriteLine("=== Modern C# Hello World Program ===\n");

// String interpolation and null-conditional operators
string? userName = null;
Console.WriteLine($"Welcome {userName ?? "Guest"}!");

// Collection expressions (C# 12)
int[] numbers = [1, 2, 3, 4, 5];
List<string> names = ["Alice", "Bob", "Charlie"];

// Pattern matching with switch expressions
string GetDayType(DayOfWeek day) => day switch
{
    DayOfWeek.Saturday or DayOfWeek.Sunday => "Weekend",
    DayOfWeek.Monday => "Monday blues",
    _ => "Regular weekday"
};

Console.WriteLine($"Today is {DateTime.Now.DayOfWeek}, which is a {GetDayType(DateTime.Now.DayOfWeek)}");

// Create instances using records
var people = new Person[]
{
    new("John", "Doe", 25),
    new("Jane", "Smith", 17),
    new("Bob", "Johnson", 70)
};

Console.WriteLine("\n=== People Information ===");
foreach (var person in people)
{
    // String interpolation with expression
    Console.WriteLine($"{person.FullName} ({person.Age}) - {person.GetAgeCategory()}");
}

// LINQ with var type inference
var adults = people.Where(p => p.Age >= 18).ToArray();
Console.WriteLine($"\nAdults count: {adults.Length}");

// Nullable reference types and null-conditional operator
string? FindPersonByName(string searchName)
{
    var found = people.FirstOrDefault(p => p.FirstName.Equals(searchName, StringComparison.OrdinalIgnoreCase));
    return found?.FullName;
}

Console.WriteLine($"Searching for 'John': {FindPersonByName("John") ?? "Not found"}");
Console.WriteLine($"Searching for 'Mike': {FindPersonByName("Mike") ?? "Not found"}");

// Using statement for resource management
using var writer = new StringWriter();
writer.WriteLine("This demonstrates using declaration");
Console.WriteLine($"Writer content: {writer.ToString().Trim()}");

// Local functions
int CalculateSum(params int[] values)
{
    return values.Sum();
}

Console.WriteLine($"\nSum of numbers: {CalculateSum(1, 2, 3, 4, 5)}");

// Target-typed new expressions (C# 9)
Dictionary<string, int> ageLookup = new();
foreach (var person in people)
{
    ageLookup[person.FirstName] = person.Age;
}

Console.WriteLine("\n=== Age Lookup ===");
foreach (var (name, age) in ageLookup)  // Tuple deconstruction
{
    Console.WriteLine($"{name}: {age} years old");
}

// Range and index operators
Console.WriteLine($"\nFirst person: {people[0].FullName}");
Console.WriteLine($"Last person: {people[^1].FullName}");  // Index from end
Console.WriteLine($"First two people: {string.Join(", ", people[..2].Select(p => p.FirstName))}");

// Span and Memory for performance (working with arrays efficiently)
ReadOnlySpan<int> numberSpan = numbers.AsSpan();
Console.WriteLine($"Sum using Span: {numberSpan[1..4].ToArray().Sum()}");  // Slice of span

Console.WriteLine("\n=== Program completed successfully! ===");

// Record types (C# 9)
public record Person(string FirstName, string LastName, int Age)
{
    // Expression-bodied property
    public string FullName => $"{FirstName} {LastName}";

    // Method with pattern matching
    public string GetAgeCategory() => Age switch
    {
        < 13 => "Child",
        >= 13 and < 20 => "Teenager",
        >= 20 and < 65 => "Adult",
        _ => "Senior"
    };
}