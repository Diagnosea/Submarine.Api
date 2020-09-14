using Diagnosea.Submarine.Domain.License.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnosea.Submarine.Domain.License.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineLicenseSettings(this IServiceCollection services, ILicenseSettings settings)
        {
            services.AddSingleton(settings);
        }
    }
}