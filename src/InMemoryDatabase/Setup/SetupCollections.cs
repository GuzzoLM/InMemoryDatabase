namespace InMemoryDatabase.Setup
{
    using InMemoryDatabase.Implementations;
    using Microsoft.Extensions.DependencyInjection;

    public static class SetupCollections
    {
        /// <summary>
        /// Register an in-memory collection on services collection.
        /// </summary>
        /// <typeparam name="T">The type of class that will be persisted in the collection.</typeparam>
        public static IServiceCollection SetupInMemoryCollection<T>(this IServiceCollection services)
        {
            return services.AddSingleton<IInMemoryCollection<T>, InMemoryCollection<T>>();
        }
    }
}