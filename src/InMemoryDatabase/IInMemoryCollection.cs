namespace InMemoryDatabase
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An in-memory persisted collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInMemoryCollection<T>
    {
        /// <summary>
        /// Persist the entity in the collection.
        /// </summary>
        /// <param name="entity">The entity to persist.</param>
        /// <returns>The resulting ID is returned.</returns>
        string Save(T entity);

        /// <summary>
        /// Update the entity in the collection. The former enitty will be entirely replaced by the new one.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The new entity is returned.</returns>
        T Update(T entity);

        /// <summary>
        /// Get an entity by its ID.
        /// </summary>
        /// <param name="id">The entity's ID.</param>
        /// <returns>The found entity.</returns>
        T Get(string id);

        /// <summary>
        /// Filter entities based on a filter.
        /// </summary>
        /// <param name="filter">The predicate to be applied as a filter.</param>
        /// <returns>An enumeration with all entities found.</returns>
        IEnumerable<T> Where(Func<T, bool> filter);

        /// <summary>
        /// Get all stored entities.
        /// </summary>
        /// <returns>An enumeration with all entities found.</returns>
        IEnumerable<T> All();

        /// <summary>
        /// Delete the entity corresponding to the supplyed ID.
        /// </summary>
        /// <param name="id">The entity's ID.</param>
        void Delete(string id);
    }
}