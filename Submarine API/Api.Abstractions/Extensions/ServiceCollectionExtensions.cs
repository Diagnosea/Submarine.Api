using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnosea.Submarine.Api.Abstractions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineControllers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddRouting(SetRoutingOptions);
            serviceCollection.AddControllers().AddJsonOptions(SetJsonOptions);
            serviceCollection.AddApiVersioning();
        }

        private static void SetRoutingOptions(RouteOptions options)
        {
            options.LowercaseUrls = true;
        }

        private static void SetJsonOptions(JsonOptions options)
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        }
    }
}
