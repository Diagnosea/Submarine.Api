using Microsoft.Extensions.DependencyInjection;

namespace Diagnosea.Submarine.Api.Abstractions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineControllers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddRouting(options => options.LowercaseUrls = true);
            serviceCollection.AddControllers();
            serviceCollection.AddApiVersioning();
        }
    }
}
