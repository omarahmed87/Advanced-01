using System;
#region Q9
/*
What is the 'new()' constraint? Write an example.
Answer:
A constraint that requires the type parameter T to have a public parameterless constructor.
*/
#endregion
class Program
{
    public class Creator<T> where T : new()
    {
        public T CreateInstance()
        {
            return new T();
        }
    }

    public static void Main()
    {

    }
}
