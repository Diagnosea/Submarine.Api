using System.Reflection;
using Diagnosea.Submarine.Domain.Authentication.Dtos;
using Diagnosea.Submarine.Domain.Instructors.Authentication;
using Diagnosea.Submarine.Domain.Instructors.Tank;
using Diagnosea.Submarine.Domain.Instructors.User;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.Tank.Entities;
using Diagnosea.Submarine.Domain.User.Dtos;
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
            serviceCollection.AddTransient<ITankInstructor, TankInstructor>();
        }

        public static void AddSubmarineMediator(this IServiceCollection serviceCollection)
        {
            var user = Assembly.GetAssembly(typeof(UserDto));
            var authentication = Assembly.GetAssembly(typeof(AuthenticateDto));
            var licensing = Assembly.GetAssembly(typeof(LicenseEntity));
            var tank = Assembly.GetAssembly(typeof(TankEntity));
            
            serviceCollection.AddMediatR(user, authentication, licensing, tank);
        }
    }
}