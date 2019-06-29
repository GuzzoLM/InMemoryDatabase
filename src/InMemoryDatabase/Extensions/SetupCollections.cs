namespace InMemoryDatabase.Extensions
{
    using InMemoryDatabase.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class SetupCollections
    {
        public static IServiceCollection SetupInMemoryCollection<T>(this IServiceCollection services)
        {
            services.AddSingleton<IInMemoryCollection<T>, InMemoryCollection<T>>();
            return services;
        }
    }
}