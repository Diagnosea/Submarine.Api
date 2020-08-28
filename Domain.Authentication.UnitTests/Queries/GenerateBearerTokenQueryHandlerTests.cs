using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken;
using Diagnosea.Submarine.Domain.User.Enums;
using Domain.Authentication.UnitTests.Settings;
using NUnit.Framework;

namespace Domain.Authentication.UnitTests.Queries
{
    [TestFixture]
    public class GenerateBearerTokenQueryHandlerTests
    {
        private SubmarineTestAuthenticationSettings _submarineTestAuthenticationSettings;
        private GenerateBearerTokenQueryHandler _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _submarineTestAuthenticationSettings = new SubmarineTestAuthenticationSettings
            {
                Secret = "thisIsATestSecret",
                ExpirationInDays = 1,
                ValidAudiences = new List<string> {"test-audience"},
                Issuer = "test-issuer"
            };
            
            _classUnderTest = new GenerateBearerTokenQueryHandler(_submarineTestAuthenticationSettings);
        }

        public class Handle : GenerateBearerTokenQueryHandlerTests
        {

            [Test]
            public void GivenGenerateBearerTokenQueryWithInvalidAudience_ShouldThrowArgumentException()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var generateBearerTokenQuery = CreateGenerateBearerTokenQuery();
                generateBearerTokenQuery.Audience = "invalid-audience";
                
                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<SubmarineArgumentException>(() => _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken));
                    Assert.That(exception.Message, Is.Not.Null);
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerToken()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var generateBearerTokenQuery = CreateGenerateBearerTokenQuery();
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithSubjectClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var generateBearerTokenQuery = CreateGenerateBearerTokenQuery();
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var subjectClaim = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

                    Assert.That(subjectClaim, Is.Not.Null);
                    Assert.That(subjectClaim.Value, Is.EqualTo(generateBearerTokenQuery.Subject));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithNameClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var generateBearerTokenQuery = CreateGenerateBearerTokenQuery();
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var subjectClaim = token.Claims.FirstOrDefault(x => x.Type == SubmarineRegisteredClaimNames.Name);

                    Assert.That(subjectClaim, Is.Not.Null);
                    Assert.That(subjectClaim.Value, Is.EqualTo(generateBearerTokenQuery.Name));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithIssuerClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var generateBearerTokenQuery = CreateGenerateBearerTokenQuery();
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var issuerClaim = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Iss);

                    Assert.That(issuerClaim, Is.Not.Null);
                    Assert.That(issuerClaim.Value, Is.EqualTo(_submarineTestAuthenticationSettings.Issuer));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithIssuedAtClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var generateBearerTokenQuery = CreateGenerateBearerTokenQuery();
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var issuedAtClaim = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Iat);

                    Assert.That(issuedAtClaim, Is.Not.Null);
                    Assert.That(token.IssuedAt.ToString(CultureInfo.InvariantCulture), Is.EqualTo(DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithExpirationClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var generateBearerTokenQuery = CreateGenerateBearerTokenQuery();
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var expirationClaim = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp);
                    var expiration = DateTime.UtcNow.AddDays(_submarineTestAuthenticationSettings.ExpirationInDays);

                    Assert.That(expirationClaim, Is.Not.Null);
                    Assert.That(token.ValidTo.ToString(CultureInfo.InvariantCulture), Is.EqualTo(expiration.ToString(CultureInfo.InvariantCulture)));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithAudienceClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var generateBearerTokenQuery = CreateGenerateBearerTokenQuery();
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var audienceClaim = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud);

                    Assert.That(audienceClaim, Is.Not.Null);
                    Assert.That(audienceClaim.Value, Is.EqualTo(_submarineTestAuthenticationSettings.ValidAudiences[0]));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithStandardUserRoleClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var generateBearerTokenQuery = CreateGenerateBearerTokenQuery();
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var roleClaims = token.Claims.Where(x => x.Type == SubmarineRegisteredClaimNames.Role);
                    var standardUserRoleClaim = roleClaims.FirstOrDefault(x => x.Value == UserRole.Standard.ToString());

                    Assert.That(standardUserRoleClaim, Is.Not.Null);
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithAdministratingUserRoleClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                var generateBearerTokenQuery = CreateGenerateBearerTokenQuery();
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var roleClaims = token.Claims.Where(x => x.Type == SubmarineRegisteredClaimNames.Role);
                    var administratorUserRoleClaim = roleClaims.FirstOrDefault(x => x.Value == UserRole.Administrator.ToString());

                    Assert.That(administratorUserRoleClaim, Is.Not.Null);
                });
            }
        }
        
        private GenerateBearerTokenQuery CreateGenerateBearerTokenQuery()
        {
            return new GenerateBearerTokenQuery
            {
                Subject = Guid.NewGuid().ToString(),
                Name = "Johnno74",
                Audience = "test-audience",
                Roles = new List<string>
                {
                    UserRole.Standard.ToString(),
                    UserRole.Administrator.ToString()
                }
            };
        }
    }
}