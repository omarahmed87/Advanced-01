using System;
#region Q8
/*
What is the 'class' constraint? Write an example.
Answer:
A constraint that restricts the type parameter T to be a reference type.
*/
#endregion
class Program
{
    public class DataManager<T> where T : class
    {
        // Code here
    }

    public static void Main()
    {

    }
}
