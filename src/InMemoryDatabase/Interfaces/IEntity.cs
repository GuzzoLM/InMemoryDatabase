namespace InMemoryDatabase.Interfaces
{
    using System;

    public interface IEntity
    {
        Guid Id { get; set; }
    }
}