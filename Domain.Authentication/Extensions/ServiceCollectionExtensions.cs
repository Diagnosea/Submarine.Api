using Diagnosea.Submarine.Domain.Authentication.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnosea.Submarine.Domain.Authentication.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineJwt(this IServiceCollection serviceCollection, ISubmarineJwtSettings submarineJwtSettings)
        {
            serviceCollection.AddSingleton(submarineJwtSettings);
        }
    }
}