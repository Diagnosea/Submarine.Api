using System;
using System.Reflection;
using Diagnosea.Submarine.Domain.Instructors.Builders;
using Diagnosea.Submarine.Domain.User.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnosea.Submarine.Domain.Instructors.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineMediator(this IServiceCollection serviceCollection)
        {
            var user = Assembly.GetAssembly(typeof(UserEntity));
            serviceCollection.AddMediatR(user);
        }
        
        public static void AddSubmarineDatabase(this IServiceCollection serviceCollection, Action<SubmarineDatabaseBuilder> builder) 
        {
            var submarineDatabaseBuilder = new SubmarineDatabaseBuilder();
            builder(submarineDatabaseBuilder);

            var database = submarineDatabaseBuilder.Build();
            serviceCollection.AddSingleton(database);
        }
    }
}