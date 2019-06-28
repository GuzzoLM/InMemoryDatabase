namespace InMemoryDatabase.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IInMemoryCollection<T> where T : IEntity
    {
        Guid Save(T entity);

        T Update(T entity);

        IEnumerable<T> Where(Func<T, bool> filter);

        IEnumerable<T> All();

        void Delete(Guid id);
    }
}