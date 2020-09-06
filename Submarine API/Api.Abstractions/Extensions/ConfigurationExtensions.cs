using System;
using Microsoft.Extensions.Configuration;

namespace Diagnosea.Submarine.Api.Abstractions.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TSettings GetSettings<TSettings>(this IConfiguration configuration)
        {
            var settingsType = typeof(TSettings);

            var name = settingsType.Name
                .Replace("Submarine", string.Empty)
                .Replace("Settings", string.Empty);

            var instance = Activator.CreateInstance<TSettings>();
            
            configuration.GetSection(name).Bind(instance);

            return instance;
        }
    }
}