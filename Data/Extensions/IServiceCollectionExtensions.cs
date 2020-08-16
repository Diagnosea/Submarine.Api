using System;
using Microsoft.Extensions.DependencyInjection;
using Submarine.Data.Builders;

namespace Submarine.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineData(this IServiceCollection serviceCollection, Action<SubmarineDatabaseBuilder> builder) 
        {
            var submarineDatabaseBuilder = new SubmarineDatabaseBuilder();
            builder(submarineDatabaseBuilder);

            var database = submarineDatabaseBuilder.Build();
            serviceCollection.AddSingleton(database);
        }
    }
}