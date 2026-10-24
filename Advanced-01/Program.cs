using System;
#region Q5
/*
Write a generic method FindMax that finds maximum value
Answer:
A generic method that compares two values and returns the maximum, requiring IComparable.
*/
#endregion
class Program
{
    public T FindMax<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? a : b;
    }

    public static void Main()
    {

    }
}
