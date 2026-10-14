using System;
#region Q3
/*
What are multiple type parameters? Write Pair<TKey, TValue>.

Answer: Using more than one independent type parameter in a class (like <T1, T2> or <TKey, TValue>).
*/
#endregion
class Program
{
    public class Pair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
    public static void Main()
    {

    }
}