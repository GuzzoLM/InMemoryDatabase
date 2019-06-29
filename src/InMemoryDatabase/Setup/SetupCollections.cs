namespace InMemoryDatabase.Setup
{
    using InMemoryDatabase.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public static class SetupCollections
    {
        public static IServiceCollection SetupInMemoryCollection<T>(this IServiceCollection services)
        {
            var collection = new InMemoryCollection<T>();
            services.AddSingleton<IInMemoryCollection<T>>(collection);
            return services;
        }
    }
}