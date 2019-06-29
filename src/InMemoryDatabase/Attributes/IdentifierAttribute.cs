namespace InMemoryDatabase.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class IdentifierAttribute : Attribute
    {
        public int Order { get; set; }

        public IdentifierAttribute(int order = 1)
        {
            Order = order;
        }
    }
}