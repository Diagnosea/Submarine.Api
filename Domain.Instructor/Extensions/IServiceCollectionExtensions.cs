using System;
using System.Reflection;
using Diagnosea.Submarine.Domain.Instructors.Builders;
using Diagnosea.Submarine.Domain.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnosea.Submarine.Domain.Instructors.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static void AddSubmarineMediator(this IServiceCollection serviceCollection)
        {
            var user = Assembly.GetAssembly(typeof(ExampleUserCommand));
            serviceCollection.AddMediatR(user);
        }
        
        internal static void AddSubmarineDatabase(this IServiceCollection serviceCollection, Action<SubmarineDatabaseBuilder> builder) 
        {
            var submarineDatabaseBuilder = new SubmarineDatabaseBuilder();
            builder(submarineDatabaseBuilder);

            var database = submarineDatabaseBuilder.Build();
            serviceCollection.AddSingleton(database);
        }
    }
}