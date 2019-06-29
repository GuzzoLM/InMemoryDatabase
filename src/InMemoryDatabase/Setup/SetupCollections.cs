namespace InMemoryDatabase.Setup
{
    using InMemoryDatabase.Implementations;
    using Microsoft.Extensions.DependencyInjection;

    public static class SetupCollections
    {
        public static IServiceCollection SetupInMemoryCollection<T>(this IServiceCollection services)
        {
            return services.AddSingleton<IInMemoryCollection<T>, InMemoryCollection<T>>();
        }
    }
}