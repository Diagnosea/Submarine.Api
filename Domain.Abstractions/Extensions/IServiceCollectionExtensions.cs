using System;
using Microsoft.Extensions.DependencyInjection;
using Submarine.Domain.Abstractions.Builders;

namespace Submarine.Domain.Abstractions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineDatabase(this IServiceCollection serviceCollection, Action<SubmarineDatabaseBuilder> builder) 
        {
            var submarineDatabaseBuilder = new SubmarineDatabaseBuilder();
            builder(submarineDatabaseBuilder);

            var database = submarineDatabaseBuilder.Build();
            serviceCollection.AddSingleton(database);
        }
    }
}