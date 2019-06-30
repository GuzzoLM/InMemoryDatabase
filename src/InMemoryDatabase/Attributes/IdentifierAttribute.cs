namespace InMemoryDatabase.Attributes
{
    using System;

    /// <summary>
    /// Mark a property as part of the class unique identifier.
    /// </summary>
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