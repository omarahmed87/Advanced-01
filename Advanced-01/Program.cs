using System;
#region Q14
/*
Write a SafeList that returns default when the index is invalid.
A safe list class that returns the default value if the index is out of range.
*/
#endregion
class Program
{
    public class SafeList<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item) => items.Add(item);

        public T Get(int index)
        {
            if (index >= 0 && index < items.Count)
                return items[index];
            return default(T);
        }
    }
    public static void Main()
    {

    }
}
