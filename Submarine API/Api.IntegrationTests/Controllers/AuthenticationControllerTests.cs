using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.IntegrationTestPack;
using Diagnosea.IntegrationTestPack.Extensions;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication;
using Diagnosea.Submarine.Abstractions.Interchange.Responses;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.Authentication;
using Diagnosea.Submarine.Abstractions.Interchange.TestPack.Builders;
using Diagnosea.Submarine.Domain.Authentication;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.License.TestPack.Builders;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.TestPack.Builders;
using Diagnosea.TestPack;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.IntegrationTests.Controllers
{
    [TestFixture]
    public class AuthenticationControllerTests : WebApiIntegrationTests<Startup>
    {
        private IMongoCollection<UserEntity> _userCollection;
        private IMongoCollection<LicenseEntity> _licenseCollection;

        [OneTimeSetUp]
        public new void OneTimeSetUp()
        {
            _userCollection = MongoDatabase.GetCollection<UserEntity>("User");
            _licenseCollection = MongoDatabase.GetCollection<LicenseEntity>("License");
        }

        [TearDown]
        public void TearDown()
        {
            _userCollection.DeleteMany(FilterDefinition<UserEntity>.Empty);
            _licenseCollection.DeleteMany(FilterDefinition<LicenseEntity>.Empty);
            
            HttpClient.ClearBearerToken();
        }

        public class RegisterAsync : AuthenticationControllerTests
        {
            private static string GetRegisterUrl()
            {
                var parts = new[]
                {
                    RouteConstants.Version1,
                    RouteConstants.Authentication.Base,
                    RouteConstants.Authentication.Register
                };
                
                return string.Join("/", parts);
            }

            private static string GetUserUrl(Guid userId)
            {
                var parts = new[]
                {
                    RouteConstants.Version1,
                    RouteConstants.User.Base,
                    userId.ToString()
                };

                return string.Join("/", parts);
            }

            [Test]
            public async Task GivenNoEmailAddress_RespondsWithModelState()
            {
                var url = GetRegisterUrl();

                const string emailAddress = null;
                const string password = "This is a password";

                var register = new TestRegisterRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, register);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ValidationResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    DiagnoseaAssert.Contains(responseData.Errors, nameof(RegisterRequest.EmailAddress), InterchangeExceptionMessages.Required);
                });
            }

            [Test]
            public async Task GivenInvalidEmailAddress_RespondsWithModelState()
            {
                var url = GetRegisterUrl();

                const string emailAddress = "This is an email address";
                const string password = "This is a password";

                var register = new TestRegisterRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, register);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ValidationResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    DiagnoseaAssert.Contains(responseData.Errors, nameof(RegisterRequest.EmailAddress), InterchangeExceptionMessages.InvalidEmailAddress);
                });
            }

            [Test]
            public async Task GivenNoPassword_RespondsWithModelState()
            {
                var url = GetRegisterUrl();

                const string emailAddress = "john.smith@example.com";
                const string password = null;

                var register = new TestRegisterRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, register);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ValidationResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    DiagnoseaAssert.Contains(responseData.Errors, nameof(RegisterRequest.Password), InterchangeExceptionMessages.Required);
                });
            }

            [Test]
            public async Task GivenEmailAlreadyUsed_RespondsWithConflict()
            {
                var url = GetRegisterUrl();

                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";

                var register = new TestRegisterRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(Guid.NewGuid())
                    .WithEmailAddress(emailAddress)
                    .Build();

                await _userCollection.InsertOneAsync(user);
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, register);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ExceptionResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
                    Assert.That(responseData.ExceptionCode, Is.EqualTo((int)SubmarineExceptionCode.DataAlreadyExists));
                    Assert.That(responseData.TechnicalMessage, Is.Not.Null);
                    Assert.That(responseData.UserMessage, Is.EqualTo(UserExceptionMessages.UserExistsWithEmail));
                });
            }
            
            [Test]
            public async Task GivenValidCredentialsWithoutOptionals_RespondsWithCreatedUserId()
            {
                var url = GetRegisterUrl();

                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";

                var register = new TestRegisterRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, register);
                
                // Assert
                var responseLocationHeaderValue = response.Headers.Location.ToString();
                
                var responseData = await response.Content.ReadFromJsonAsync<RegisteredResponse>();

                var user = await _userCollection
                    .Find(x => x.Id == responseData.UserId)
                    .FirstOrDefaultAsync();

                var userUrl = GetUserUrl(user.Id);
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                    Assert.That(responseLocationHeaderValue.Contains(userUrl));
                    
                    Assert.That(user.EmailAddress, Is.EqualTo(emailAddress));
                    Assert.That(user.UserName, Is.Null);
                    Assert.That(user.FriendlyName, Is.Null);

                    var isPasswordValue = BCrypt.Net.BCrypt.Verify(password, user.Password);
                    Assert.That(isPasswordValue);
                });
            }

            [Test]
            public async Task GivenValidCredentials_RespondsWithCreatedUserId()
            {
                var url = GetRegisterUrl();

                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";

                var register = new TestRegisterRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, register);
                
                // Assert
                var responseLocationHeaderValue = response.Headers.Location.ToString();
                
                var responseData = await response.Content.ReadFromJsonAsync<RegisteredResponse>();

                var user = await _userCollection
                    .Find(x => x.Id == responseData.UserId)
                    .FirstOrDefaultAsync();

                var userUrl = GetUserUrl(user.Id);
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                    Assert.That(responseLocationHeaderValue.Contains(userUrl));
                    
                    Assert.That(user.EmailAddress, Is.EqualTo(emailAddress));

                    var isPasswordValue = BCrypt.Net.BCrypt.Verify(password, user.Password);
                    Assert.That(isPasswordValue);
                });
            }
        }

        public class AuthenticateAsync : AuthenticationControllerTests
        {
            private static string GetAuthenticateUrl()
            {
                var parts = new[]
                {
                    RouteConstants.Version1,
                    RouteConstants.Authentication.Base,
                    RouteConstants.Authentication.Authenticate
                };
                
                return Path.Combine(parts);
            }

            [Test]
            public async Task GivenNoEmailAddress_RespondsWithModelState()
            {
                var url = GetAuthenticateUrl();

                const string password = "This is a password";

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithPassword(password)
                    .Build();
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ValidationResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    DiagnoseaAssert.Contains(
                        responseData.Errors, 
                        nameof(AuthenticateRequest.EmailAddress), 
                        InterchangeExceptionMessages.Required);
                });
            }

            [Test]
            public async Task GivenInvalidEmailAddress_RespondsWithModelState()
            {
                var url = GetAuthenticateUrl();

                const string emailAddress = "This is an email address";
                const string password = "This is a password";

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ValidationResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                    DiagnoseaAssert.Contains(responseData.Errors, nameof(AuthenticateRequest.EmailAddress), InterchangeExceptionMessages.InvalidEmailAddress);
                });
            }

            [Test]
            public async Task GivenNoPassword_RespondsWithModelState()
            {
                var url = GetAuthenticateUrl();

                const string emailAddress = "john.smith@example.com";

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .Build();
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ValidationResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
                    DiagnoseaAssert.Contains(responseData.Errors, nameof(AuthenticateRequest.Password), InterchangeExceptionMessages.Required);
                });
            }
            
            [Test]
            public async Task GivenNoUserWithEmail_RespondsWithNotFound()
            {
                // Arrange
                var url = GetAuthenticateUrl();

                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ExceptionResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(responseData.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.EntityNotFound));
                    Assert.That(responseData.TechnicalMessage, Is.Not.Null);
                    Assert.That(responseData.UserMessage, Is.EqualTo(UserExceptionMessages.UserNotFound));
                });
            }

            [Test]
            public async Task GivenInvalidPasswordForUser_RespondsWithBadRequest()
            {
                var url = GetAuthenticateUrl();

                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithEmailAddress(emailAddress)
                    .Build();

                await _userCollection.InsertOneAsync(user);
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ExceptionResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
                    Assert.That(responseData.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.DataMismatchException));
                    Assert.That(responseData.TechnicalMessage, Is.Not.Null);
                    Assert.That(responseData.UserMessage, Is.EqualTo(AuthenticationExceptionMessages.PasswordIsIncorrect));
                });
            }

            [Test]
            public async Task GivenNoLicenseForUser_RespondsWithNotFound()
            {
                // Arrange
                var url = GetAuthenticateUrl();

                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(hashedPassword)
                    .Build();

                await _userCollection.InsertOneAsync(user);
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ExceptionResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(responseData.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.EntityNotFound));
                    Assert.That(responseData.TechnicalMessage, Is.Not.Null);
                    Assert.That(responseData.UserMessage, Is.EqualTo(AuthenticationExceptionMessages.NoLicensesUnderUserWithId));
                });
            }

            [Test]
            public async Task GivenValidCredentials_RespondsWithBearerTokenWithSubjectClaim()
            {
                // Arrange
                var url = GetAuthenticateUrl();

                var userId = Guid.NewGuid();
                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";
                const string productKey = "This is a product key";
                const string testProductOneName = "Test Product One Name";
                const string testProductTwoName = "Test product Two Name";
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .WithPassword(hashedPassword)
                    .Build();
                
                await _userCollection.InsertOneAsync(user);

                var license = new TestLicenseEntityBuilder()
                    .WithUserId(userId)
                    .WithKey(productKey)
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductOneName)
                        .Build())
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductTwoName)
                        .Build())
                    .Build();

                await _licenseCollection.InsertOneAsync(license);

                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var securityToken = jwtSecurityTokenHandler.ReadJwtToken(responseData.BearerToken);
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(securityToken.Subject, Is.EqualTo(user.Id.ToString()));
                });
            }
            
            [Test]
            public async Task GivenValidCredentials_RespondsWithBearerTokenWithNameClaim()
            {
                // Arrange
                var url = GetAuthenticateUrl();

                var userId = Guid.NewGuid();
                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";
                const string productKey = "This is a product key";
                const string testProductOneName = "Test Product One Name";
                const string testProductTwoName = "Test product Two Name";
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .WithPassword(hashedPassword)
                    .Build();
                
                await _userCollection.InsertOneAsync(user);

                var license = new TestLicenseEntityBuilder()
                    .WithUserId(userId)
                    .WithKey(productKey)
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductOneName)
                        .Build())
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductTwoName)
                        .Build())
                    .Build();

                await _licenseCollection.InsertOneAsync(license);

                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();
                var jwtSecurityTokenHander = new JwtSecurityTokenHandler();
                var securityToken = jwtSecurityTokenHander.ReadJwtToken(responseData.BearerToken);
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                    var nameClaim = securityToken.Claims.FirstOrDefault(c => c.Type == SubmarineRegisteredClaimNames.Name);
                    Assert.That(nameClaim.Value, Is.EqualTo(user.EmailAddress));
                });
            }
            
            [Test]
            public async Task GivenValidCredentials_RespondsWithBearerTokenWithIssuerClaim()
            {
                // Arrange
                var url = GetAuthenticateUrl();

                var userId = Guid.NewGuid();
                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";
                const string productKey = "This is a product key";
                const string testProductOneName = "Test Product One Name";
                const string testProductTwoName = "Test product Two Name";
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .WithPassword(hashedPassword)
                    .Build();
                
                await _userCollection.InsertOneAsync(user);

                var license = new TestLicenseEntityBuilder()
                    .WithUserId(userId)
                    .WithKey(productKey)
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductOneName)
                        .Build())
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductTwoName)
                        .Build())
                    .Build();

                await _licenseCollection.InsertOneAsync(license);

                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();
                var jwtSecurityTokenHander = new JwtSecurityTokenHandler();
                var securityToken = jwtSecurityTokenHander.ReadJwtToken(responseData.BearerToken);
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(securityToken.Issuer, Is.EqualTo("submarine-api"));
                });
            }
            
            [Test]
            public async Task GivenValidCredentials_RespondsWithBearerTokenWithIssuedAtClaim()
            {
                // Arrange
                var url = GetAuthenticateUrl();

                var userId = Guid.NewGuid();
                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";
                const string productKey = "This is a product key";
                const string testProductOneName = "Test Product One Name";
                const string testProductTwoName = "Test product Two Name";
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .WithPassword(hashedPassword)
                    .Build();
                
                await _userCollection.InsertOneAsync(user);

                var license = new TestLicenseEntityBuilder()
                    .WithUserId(userId)
                    .WithKey(productKey)
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductOneName)
                        .Build())
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductTwoName)
                        .Build())
                    .Build();

                await _licenseCollection.InsertOneAsync(license);

                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();
                var jwtSecurityTokenHander = new JwtSecurityTokenHandler();
                var securityToken = jwtSecurityTokenHander.ReadJwtToken(responseData.BearerToken);
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    DiagnoseaAssert.That(securityToken.ValidFrom, Is.EqualTo(DateTime.UtcNow));
                });
            }
            
            [Test]
            public async Task GivenValidCredentials_RespondsWithBearerTokenWithExpirationClaim()
            {
                // Arrange
                var url = GetAuthenticateUrl();

                var userId = Guid.NewGuid();
                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";
                const string productKey = "This is a product key";
                const string testProductOneName = "Test Product One Name";
                const string testProductTwoName = "Test product Two Name";
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .WithPassword(hashedPassword)
                    .Build();
                
                await _userCollection.InsertOneAsync(user);

                var license = new TestLicenseEntityBuilder()
                    .WithUserId(userId)
                    .WithKey(productKey)
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductOneName)
                        .Build())
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductTwoName)
                        .Build())
                    .Build();

                await _licenseCollection.InsertOneAsync(license);

                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();
                var jwtSecurityTokenHander = new JwtSecurityTokenHandler();
                var securityToken = jwtSecurityTokenHander.ReadJwtToken(responseData.BearerToken);
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    DiagnoseaAssert.That(securityToken.ValidTo, Is.EqualTo(DateTime.UtcNow.AddDays(1)));
                });
            }
            
            [Test]
            public async Task GivenValidCredentials_RespondsWithBearerTokenWithAudienceClaim()
            {
                // Arrange
                var url = GetAuthenticateUrl();

                var userId = Guid.NewGuid();
                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";
                const string productKey = "This is a product key";
                const string testProductOneName = "Test Product One Name";
                const string testProductTwoName = "Test product Two Name";
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .WithPassword(hashedPassword)
                    .Build();
                
                await _userCollection.InsertOneAsync(user);

                var license = new TestLicenseEntityBuilder()
                    .WithUserId(userId)
                    .WithKey(productKey)
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductOneName)
                        .Build())
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductTwoName)
                        .Build())
                    .Build();

                await _licenseCollection.InsertOneAsync(license);

                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();
                var jwtSecurityTokenHander = new JwtSecurityTokenHandler();
                var securityToken = jwtSecurityTokenHander.ReadJwtToken(responseData.BearerToken);
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(securityToken.Audiences.Contains(license.Key));
                });
            }
            
            [Test]
            public async Task GivenValidCredentials_RespondsWithBearerTokenWithRolesClaim()
            {
                // Arrange
                var url = GetAuthenticateUrl();

                var userId = Guid.NewGuid();
                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";
                const string productKey = "This is a product key";
                const string testProductOneName = "Test Product One Name";
                const string testProductTwoName = "Test product Two Name";
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .WithPassword(hashedPassword)
                    .WithRole(UserRole.Administrator)
                    .Build();
                
                await _userCollection.InsertOneAsync(user);

                var license = new TestLicenseEntityBuilder()
                    .WithUserId(userId)
                    .WithKey(productKey)
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductOneName)
                        .Build())
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductTwoName)
                        .Build())
                    .Build();

                await _licenseCollection.InsertOneAsync(license);

                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();
                var jwtSecurityTokenHander = new JwtSecurityTokenHandler();
                var securityToken = jwtSecurityTokenHander.ReadJwtToken(responseData.BearerToken);
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                    var roleClaims = securityToken.Claims.Where(x => x.Type == SubmarineRegisteredClaimNames.Roles);
                    var administratorRoleClaim = roleClaims.FirstOrDefault(x => x.Value == UserRole.Administrator.ToString());
                    
                    Assert.That(administratorRoleClaim, Is.Not.Null);
                });
            }
            
            [Test]
            public async Task GivenValidCredentials_RespondsWithBearerTokenWithProductClaim()
            {
                // Arrange
                var url = GetAuthenticateUrl();

                var userId = Guid.NewGuid();
                const string emailAddress = "john.smith@example.com";
                const string password = "This is a password";
                const string productKey = "This is a product key";
                const string testProductOneName = "Test Product One Name";
                const string testProductTwoName = "Test product Two Name";
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var authenticate = new TestAuthenticateRequestBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPassword(password)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .WithPassword(hashedPassword)
                    .WithRole(UserRole.Administrator)
                    .Build();
                
                await _userCollection.InsertOneAsync(user);

                var license = new TestLicenseEntityBuilder()
                    .WithUserId(userId)
                    .WithKey(productKey)
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductOneName)
                        .Build())
                    .WithProduct(new TestLicenseProductEntityBuilder()
                        .WithName(testProductTwoName)
                        .Build())
                    .Build();

                await _licenseCollection.InsertOneAsync(license);

                // Act
                var response = await HttpClient.PostAsJsonAsync(url, authenticate);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<AuthenticatedResponse>();
                var jwtSecurityTokenHander = new JwtSecurityTokenHandler();
                var securityToken = jwtSecurityTokenHander.ReadJwtToken(responseData.BearerToken);
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                    var productClaims = securityToken.Claims
                        .Where(x => x.Type == SubmarineRegisteredClaimNames.Products)
                        .ToList();
                    
                    var testProductOneClaim = productClaims.FirstOrDefault(x => x.Value == testProductOneName);
                    var testProductTwoClaim = productClaims.FirstOrDefault(x => x.Value == testProductTwoName);
                    
                    Assert.That(testProductOneClaim, Is.Not.Null);
                    Assert.That(testProductTwoClaim, Is.Not.Null);
                });
            }
        }
    }
}