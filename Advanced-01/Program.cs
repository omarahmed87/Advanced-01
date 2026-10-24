using System;
#region Q4
/*
What is a generic method? Write Swap method.
Answer:
A method that can be called with different types without rewriting the code for each type.
*/
#endregion
class Program
{
    public void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

public static void Main()
    {

    }
}
