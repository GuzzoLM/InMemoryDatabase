namespace InMemoryDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using InMemoryDatabase.Exceptions;
    using InMemoryDatabase.Attributes;
    using InMemoryDatabase.Interfaces;
    using InMemoryDatabase.Extensions;

    internal class InMemoryCollection<T> : IInMemoryCollection<T>
    {
        private IDictionary<string, T> _data { get; set; }

        private IList<Func<object, string>> _idGenerator { get; set; }

        public InMemoryCollection()
        {
            _data = new Dictionary<string, T>();
            _idGenerator = IdentifierExtensions.GetIdGenerator(typeof(T));
        }

        public string Save(T entity)
        {
            var id = GetId(entity);

            _data.Add(id, entity);

            return id;
        }

        public T Update(T entity)
        {
            var id = GetId(entity);

            IDictionary<string, T> old = new Dictionary<string, T>(_data);
            try
            {
                _data.Remove(id);
                _data.Add(id, entity);
                return entity;
            }
            catch
            {
                _data = new Dictionary<string, T>(old);
                throw;
            }
        }

        public IEnumerable<T> Where(Func<T, bool> filter)
        {
            return _data.Values.Where(filter);
        }

        public IEnumerable<T> All()
        {
            return _data.Values;
        }

        public void Delete(string id)
        {
            _data.Remove(id);
        }

        private string GetId(T entity)
        {
            return IdentifierExtensions.GetId(_idGenerator, entity);
        }
    }
}