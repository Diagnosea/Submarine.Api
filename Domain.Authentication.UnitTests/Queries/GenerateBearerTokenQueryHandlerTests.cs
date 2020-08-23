using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken;
using Diagnosea.Submarine.Domain.Authentication.Settings;
using Diagnosea.Submarine.Domain.User.Enums;
using NUnit.Framework;

namespace Domain.Authentication.UnitTests.Queries
{
    [TestFixture]
    public class GenerateBearerTokenQueryHandlerTests
    {
        private GenerateBearerTokenQueryHandler _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            var settings = new SubmarineTestJwtSettings();
            _classUnderTest = new GenerateBearerTokenQueryHandler(settings);
        }
        
        public class Handle : GenerateBearerTokenQueryHandlerTests
        {
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerToken()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser
                    }
                };
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert 
                
            }
        }
        
        private class SubmarineTestJwtSettings : ISubmarineJwtSettings
        {
            public string Secret { get; set;  }= "thisIsATestSecret";
            public int ExpirationInDays { get; set; } = 1;
            public DateTime GetExpiryDate() => DateTime.UtcNow.AddDays(-ExpirationInDays);
            public byte[] GetDecodedSecret() => Encoding.UTF8.GetBytes(Secret);
        }
    }
}