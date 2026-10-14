using System;
#region
/*
Q3: Static Members

a) What is a static field, and how is it different from an instance field?
Belongs to the class itself rather than instances.Instances have separate copies, while static shares a single value.
b) What is a static method? Can a static method directly access instance members?
A method belonging to the class. No, it cannot directly access instance members.
c) What is a static constructor, and when is it executed?
A special constructor to initialize static data, executed automatically the first time the class is accessed.
d) What is a static class? Can you create an object from a static class?
A class with only static members.No, you cannot instantiate it with new.

Q4: Extension Methods

a) What is an Extension Method?
A method that lets you add functionality to existing types without modifying their source code.
b) What keyword must be used in the first parameter of an extension method?
The this keyword followed by the type being extended.
c) Where must an extension method be declared?
Inside a static class.
d) Can an extension method access private members of the class it extends?
No, only public members and properties.

Q5: Partial Classes and Partial Methods

a) What is a Partial Class?
A class split across multiple files using partial, combined by the compiler into one.
b) Why would a developer split one class into multiple files?
To organize large codebases or allow multiple developers to work on it simultaneously.
c) What is a Partial Method?
A method declared in one part of a partial class with optional implementation in another.
d) What happens if a declared partial method has no implementation?
The compiler automatically removes its declaration and calls entirely.
*/
#endregion
class Program {
    public static void Main()
    {
       
    }
}