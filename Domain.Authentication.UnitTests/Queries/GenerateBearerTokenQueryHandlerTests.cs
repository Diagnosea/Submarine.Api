using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private SubmarineTestJwtSettings _submarineTestJwtSettings;
        private GenerateBearerTokenQueryHandler _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _submarineTestJwtSettings = new SubmarineTestJwtSettings();
            _classUnderTest = new GenerateBearerTokenQueryHandler(_submarineTestJwtSettings);
        }
        
        public class Handle : GenerateBearerTokenQueryHandlerTests
        {
            [Test]
            public async Task GivenGenerateBearerTokenQueryWithInvalidAudience_ShouldThrowArgumentException()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Audience = "invalid-audience",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser,
                        UserRole.AdministratingUser
                    }
                };

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<ArgumentException>(() => _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken));
                    Assert.That(exception.Message, Is.Not.Null);
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerToken()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Audience = "test-audience",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser
                    }
                };
                
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

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Audience = "test-audience",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser
                    }
                };
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var subjectClaim = token.Claims.FirstOrDefault(x => x.Type == AuthenticationConstants.ClaimTypes.Subject);

                    Assert.That(subjectClaim, Is.Not.Null);
                    Assert.That(subjectClaim.Value, Is.EqualTo(generateBearerTokenQuery.Id.ToString()));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithNameClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Audience = "test-audience",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser
                    }
                };
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var subjectClaim = token.Claims.FirstOrDefault(x => x.Type == AuthenticationConstants.ClaimTypes.Name);

                    Assert.That(subjectClaim, Is.Not.Null);
                    Assert.That(subjectClaim.Value, Is.EqualTo(generateBearerTokenQuery.Name));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithIssuerClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Audience = "test-audience",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser
                    }
                };
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var issuerClaim = token.Claims.FirstOrDefault(x => x.Type == AuthenticationConstants.ClaimTypes.Issuer);

                    Assert.That(issuerClaim, Is.Not.Null);
                    Assert.That(issuerClaim.Value, Is.EqualTo(_submarineTestJwtSettings.Issuer));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithIssuedAtClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Audience = "test-audience",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser
                    }
                };
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var issuedAtClaim = token.Claims.FirstOrDefault(x => x.Type == AuthenticationConstants.ClaimTypes.IssuedAt);

                    Assert.That(issuedAtClaim, Is.Not.Null);
                    Assert.That(token.IssuedAt.ToString(), Is.EqualTo(DateTime.UtcNow.ToString()));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithExpirationClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Audience = "test-audience",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser
                    }
                };
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var expirationClaim = token.Claims.FirstOrDefault(x => x.Type == AuthenticationConstants.ClaimTypes.Expiration);
                    var expiration = DateTime.UtcNow.AddDays(_submarineTestJwtSettings.ExpirationInDays);

                    Assert.That(expirationClaim, Is.Not.Null);
                    Assert.That(token.ValidTo.ToString(), Is.EqualTo(expiration.ToString()));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithAudienceClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Audience = "test-audience",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser
                    }
                };
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var audienceClaim = token.Claims.FirstOrDefault(x => x.Type == AuthenticationConstants.ClaimTypes.Audience);

                    Assert.That(audienceClaim, Is.Not.Null);
                    Assert.That(audienceClaim.Value, Is.EqualTo(_submarineTestJwtSettings.Audiences[0]));
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithStandardUserRoleClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Audience = "test-audience",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser,
                        UserRole.AdministratingUser
                    }
                };
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var roleClaims = token.Claims.Where(x => x.Type == AuthenticationConstants.ClaimTypes.Role);
                    var standardUserRoleClaim = roleClaims.FirstOrDefault(x => x.Value == UserRole.StandardUser.ToString());

                    Assert.That(standardUserRoleClaim, Is.Not.Null);
                });
            }
            
            [Test]
            public async Task GivenGenerateBearerTokenQuery_GeneratesBearerTokenWithAdministratingUserRoleClaim()
            {
                // Arrange
                var cancellationToken = new CancellationToken();

                var generateBearerTokenQuery = new GenerateBearerTokenQuery
                {
                    Id = Guid.NewGuid(),
                    Name = "Johnno74",
                    Audience = "test-audience",
                    Roles = new List<UserRole>
                    {
                        UserRole.StandardUser,
                        UserRole.AdministratingUser
                    }
                };
                
                // Act
                var result = await _classUnderTest.Handle(generateBearerTokenQuery, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadToken(result) as JwtSecurityToken;

                    Assert.That(token, Is.Not.Null);

                    var roleClaims = token.Claims.Where(x => x.Type == AuthenticationConstants.ClaimTypes.Role);
                    var standardUserRoleClaim = roleClaims.FirstOrDefault(x => x.Value == UserRole.AdministratingUser.ToString());

                    Assert.That(standardUserRoleClaim, Is.Not.Null);
                });
            }
        }
    }
}