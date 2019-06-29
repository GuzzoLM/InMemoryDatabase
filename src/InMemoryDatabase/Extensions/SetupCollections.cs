using System;
using System.Collections.Generic;
using System.Text;
using InMemoryDatabase.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InMemoryDatabase.Extensions
{
    public static class SetupCollections
    {
        public static IServiceCollection SetupInMemoryCollection<T>(this IServiceCollection services)
        {
            services.AddSingleton<IInMemoryCollection<T>, InMemoryCollection<T>>();
            return services;
        }
    }
}
