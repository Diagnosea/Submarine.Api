using System;
using System.Reflection;
using Diagnosea.Submarine.Domain.Authentication;
using Diagnosea.Submarine.Domain.Builders;
using Diagnosea.Submarine.Domain.Instructors.Authentication;
using Diagnosea.Submarine.Domain.Instructors.User;
using Diagnosea.Submarine.Domain.License;
using Diagnosea.Submarine.Domain.User;
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
            var user = Assembly.GetAssembly(typeof(UserExceptionMessages));
            var authentication = Assembly.GetAssembly(typeof(AuthenticationExceptionMessages));
            var licensing = Assembly.GetAssembly(typeof(LicenseExceptionMessages));
            
            serviceCollection.AddMediatR(user, authentication, licensing);
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