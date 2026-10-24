using System;
#region Q7
/*
What is the 'struct' constraint? Write an example.
Answer:
A constraint that restricts the type parameter T to be a value type.
*/
#endregion
class Program
{
    public class Calculator<T> where T : struct
    {
        // Code here
    }

    public static void Main()
    {

    }
}
