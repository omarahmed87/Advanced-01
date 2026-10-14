using System;
#region Q2
/*
Write a generic class Container with Add and Get methods.
*/
#endregion
class Program
{
    public class Container<T>
    {
        private T item;
        public void Add(T item) => this.item = item;
        public T Get() => item;
    }
    public static void Main()
    {

    }
}