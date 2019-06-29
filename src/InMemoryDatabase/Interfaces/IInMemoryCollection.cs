namespace InMemoryDatabase.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IInMemoryCollection<T>
    {
        string Save(T entity);

        T Update(T entity);

        IEnumerable<T> Where(Func<T, bool> filter);

        IEnumerable<T> All();

        void Delete(string id);
    }
}