using System;
#region Q10
/*
What is the interface constraint? Write an example.
Answer:
A constraint that ensures the type parameter T implements a specific interface.
*/
#endregion
class Program
{
    public class Processor<T> where T : IComparable
    {
        // Code here
    }

    public static void Main()
    {

    }
}
