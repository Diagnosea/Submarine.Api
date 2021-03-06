﻿using Diagnosea.Submarine.Domain.Authentication.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnosea.Submarine.Domain.Authentication.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineAuthenticationSettings(this IServiceCollection serviceCollection, ISubmarineAuthenticationSettings submarineAuthenticationSettings)
        {
            serviceCollection.AddSingleton(submarineAuthenticationSettings);
        }
    }
}