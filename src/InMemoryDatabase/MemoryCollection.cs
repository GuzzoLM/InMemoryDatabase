namespace InMemoryDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using InMemoryDatabase.Interfaces;

    internal class MemoryCollection<T> : IInMemoryCollection<T> where T : IEntity
    {
        private IDictionary<Guid, T> _data { get; set; }

        public MemoryCollection()
        {
            _data = new Dictionary<Guid, T>();
        }

        public Guid Save(T entity)
        {
            if (entity is IEntity idEntity)
            {
                _data.Add(idEntity.Id, entity);
                return idEntity.Id;
            }

            throw new ArgumentException("Cannot persist entities without Id");
        }

        public T Update(T entity)
        {
            if (entity is IEntity idEntity)
            {
                IDictionary<Guid, T> old = new Dictionary<Guid, T>(_data);
                try
                {
                    _data.Remove(idEntity.Id);
                    _data.Add(idEntity.Id, entity);
                    return entity;
                }
                catch
                {
                    _data = new Dictionary<Guid, T>(old);
                    throw;
                }
            }

            throw new ArgumentException("Cannot persist entities without Id");
        }

        public IEnumerable<T> Where(Func<T, bool> filter)
        {
            return _data.Values.Where(filter);
        }

        public IEnumerable<T> All()
        {
            return _data.Values;
        }

        public void Delete(Guid id)
        {
            _data.Remove(id);
        }
    }
}