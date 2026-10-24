using System;
#region Q6
/*
What is a generic interface? Write IRepository.
Answer:
An interface that uses a type parameter T to define a standard set of operations for any data type.
*/
#endregion
class Program
{
    public interface IRepository<T>
    {
        void Add(T item);
        T GetById(int id);
    }

    public static void Main()
    {

    }
}
