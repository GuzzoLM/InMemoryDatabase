namespace InMemoryDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using InMemoryDatabase.Exceptions;
    using InMemoryDatabase.Attributes;
    using InMemoryDatabase.Interfaces;

    internal class InMemoryCollection<T> : IInMemoryCollection<T>
    {
        private IDictionary<string, T> _data { get; set; }

        private IList<Func<T, string>> _idGenerator { get; set; }

        public InMemoryCollection()
        {
            _data = new Dictionary<string, T>();
            _idGenerator = GetIdGenerator();
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
            var id = string.Empty;

            foreach(var prop in _idGenerator)
            {
                var idProp = prop(entity);
                id = string.IsNullOrEmpty(id)
                    ? idProp
                    : $"{id}-{idProp}";
            }

            return id;
        }

        private IList<Func<T, string>> GetIdGenerator()
        {
            var props = typeof(T)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(IdentifierAttribute)))
                .GroupBy(prop => GetIdOrder(prop))
                .ToDictionary(
                    x => x.Key,
                    x => (IEnumerable<PropertyInfo>)x.ToList());

            Validate(props);

            return props
                .OrderBy(x => x.Key)
                .SelectMany(x => x.Value)
                .Select(prop => GetPropertyAccessor(prop))
                .ToList();
        }

        private int GetIdOrder(PropertyInfo prop)
        {
            return ((IdentifierAttribute)Attribute.GetCustomAttribute(prop, typeof(IdentifierAttribute))).Order;
        }

        private Func<T, string> GetPropertyAccessor(PropertyInfo prop)
        {
            return (T x) => prop.GetValue(x).ToString();
        }

        private void Validate(IDictionary<int, IEnumerable<PropertyInfo>> props)
        {
            var properties = new List<string>();
            var message = "Found identifier properties with same order number";

            properties.AddRange(props
                .Where(x => x.Value.Count() > 1)
                .SelectMany(x => x.Value)
                .Select(prop => prop.Name));

            if (properties.Count > 0)
            {
                throw new InvalidIdentityException(message, properties.ToArray());
            }

            message = "Class must have an Identifier";

            if (props.SelectMany(x => x.Value).Count() < 1)
            {
                throw new InvalidIdentityException(message);
            }
        }
    }
}