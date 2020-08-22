using System;
using Diagnosea.Submarine.Domain.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnosea.Submarine.Domain.Abstractions.Extensions
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