namespace InMemoryDatabase.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using InMemoryDatabase.Attributes;
    using InMemoryDatabase.Exceptions;

    public static class IdentifierExtensions
    {
        /// <summary>
        /// Get the calculated unique identifier for an entity.
        /// </summary>
        public static string GetIdentifier(this object entity)
        {
            var generator = GetIdGenerator(entity.GetType());
            return GetId(generator, entity);
        }

        internal static string GetId(IList<Func<object, string>> generator, object entity)
        {
            var id = string.Empty;

            foreach (var prop in generator)
            {
                var idProp = prop(entity);
                id = string.IsNullOrEmpty(id)
                    ? idProp
                    : $"{id}-{idProp}";
            }

            return id;
        }

        internal static IList<Func<object, string>> GetIdGenerator(Type type)
        {
            var props = type
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

        private static int GetIdOrder(PropertyInfo prop)
        {
            return ((IdentifierAttribute)Attribute.GetCustomAttribute(prop, typeof(IdentifierAttribute))).Order;
        }

        private static Func<object, string> GetPropertyAccessor(PropertyInfo prop)
        {
            return x => prop.GetValue(x).ToString();
        }

        private static void Validate(IDictionary<int, IEnumerable<PropertyInfo>> props)
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