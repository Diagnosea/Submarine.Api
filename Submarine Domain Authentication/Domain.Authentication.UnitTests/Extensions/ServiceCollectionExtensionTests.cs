using System.Collections.Generic;
using Diagnosea.Submarine.Domain.Authentication.Extensions;
using Diagnosea.Submarine.Domain.Authentication.Settings;
using Domain.Authentication.UnitTests.Settings;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Domain.Authentication.UnitTests.Extensions
{
    [TestFixture]
    public class ServiceCollectionExtensionTests
    {
        public class AddSubmarineAuthenticationSettings : ServiceCollectionExtensionTests
        {
            [Test]
            public void GivenSettings_AddsSettingsToCollection()
            {
                // Arrange
                var services = new ServiceCollection();

                var settings = new SubmarineTestAuthenticationSettings
                {
                    ExpirationInDays = 1,
                    ValidAudiences = new List<string> {"test-audience"},
                    Issuer = "test-issuer",
                    Secret = "test-secret"
                };

                // Act
                services.AddSubmarineAuthenticationSettings(settings);
                
                // Assert
                var provider = services.BuildServiceProvider();
                var service = provider.GetRequiredService<ISubmarineAuthenticationSettings>();

                Assert.That(service, Is.Not.Null);
            }
        }
    }
}