using System;
#region Q11
/*
What is the base class constraint? Write an example.
Answer:
A constraint that specifies that T must inherit from a specific base class.
*/
#endregion
class Program
{
    public class EmployeeManager<T> where T : Person
    {
        // Code here
    }

    public static void Main()
    {

    }
}
