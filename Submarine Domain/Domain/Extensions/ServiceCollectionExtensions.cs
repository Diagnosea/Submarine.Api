using System;
using System.Reflection;
using Diagnosea.Submarine.Domain.Authentication.Entities;
using Diagnosea.Submarine.Domain.Builders;
using Diagnosea.Submarine.Domain.Instructors.Authentication;
using Diagnosea.Submarine.Domain.Instructors.User;
using Diagnosea.Submarine.Domain.User.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnosea.Submarine.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineInstructors(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAuthenticationInstructor, AuthenticationInstructor>();
            serviceCollection.AddTransient<IUserInstructor, UserInstructor>();
        }

        public static void AddSubmarineMediator(this IServiceCollection serviceCollection)
        {
            var user = Assembly.GetAssembly(typeof(UserEntity));
            var auth = Assembly.GetAssembly(typeof(AudienceEntity));
            serviceCollection.AddMediatR(user, auth);
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