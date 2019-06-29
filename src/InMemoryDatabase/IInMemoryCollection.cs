namespace InMemoryDatabase
{
    using System;
    using System.Collections.Generic;

    public interface IInMemoryCollection<T>
    {
        string Save(T entity);

        T Update(T entity);

        T Get(string id);

        IEnumerable<T> Where(Func<T, bool> filter);

        IEnumerable<T> All();

        void Delete(string id);
    }
}