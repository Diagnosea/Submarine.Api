using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.IntegrationTestPack;
using Diagnosea.IntegrationTestPack.Builders;
using Diagnosea.IntegrationTestPack.Extensions;
using Diagnosea.Submarine.Abstraction.Routes;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Interchange.Requests.License;
using Diagnosea.Submarine.Abstractions.Interchange.Responses;
using Diagnosea.Submarine.Abstractions.Interchange.Responses.License;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.TestPack;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.IntegrationTests.Controllers
{
    [TestFixture]
    public class LicenseControllerTests : WebApiIntegrationTests<Startup>
    {
        private IMongoCollection<LicenseEntity> _licenseCollection;
        private IMongoCollection<UserEntity> _userCollecion;

        [OneTimeSetUp]
        public void SetUp()
        {
            _licenseCollection = MongoDatabase.GetEntityCollection<LicenseEntity>();
            _userCollecion = MongoDatabase.GetEntityCollection<UserEntity>();
        }

        [TearDown]
        public void TearDown()
        {
            HttpClient.ClearBearerToken();

            _licenseCollection.DeleteMany(FilterDefinition<LicenseEntity>.Empty);
            _userCollecion.DeleteMany(FilterDefinition<UserEntity>.Empty);
        }

        public class GetLicenseByIdAsync : LicenseControllerTests
        {
            private readonly string _url = $"{RouteConstants.Version1}/{RouteConstants.License.Base}";
            
            private string GetUrl(Guid id) => $"{_url}/{id}";

            [Test]
            public async Task GivenNoBearerToken_RespondsWithUnauthorized()
            {
                // Arrange
                var licenseId = Guid.NewGuid();
                var url = GetUrl(licenseId);
                
                // Act
                var response = await HttpClient.GetAsync(url);
                
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            }

            [Test]
            public async Task GivenInvalidRole_RespondsWithForbidden()
            {
                // Arrange
                var licenseId = Guid.NewGuid();
                var url = GetUrl(licenseId);
                var bearerToken = new TestBearerTokenBuilder()
                    .Build();
                
                HttpClient.SetBearerToken(bearerToken);
                
                // Act
                var response = await HttpClient.GetAsync(url);
                
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            }

            [Test]
            public async Task GivenLicenseDoesNotExist_RespondsWithNotFound()
            {
                // Arrange
                var licenseId = Guid.NewGuid();
                var url = GetUrl(licenseId);
                var bearerToken = new TestBearerTokenBuilder()
                    .WithRole(UserRole.Standard)
                    .Build();
                
                HttpClient.SetBearerToken(bearerToken);
                
                // Act
                var response = await HttpClient.GetAsync(url);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ExceptionResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(responseData.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.EntityNotFound));
                    Assert.That(responseData.TechnicalMessage, Is.Not.Null);
                    Assert.That(responseData.UserMessage, Is.EqualTo(LicenseExceptionMessages.NoLicenseWithId));
                });
            }

            [Test]
            public async Task GivenLicenseExists_RespondsWithLicense()
            {
                // Arrange
                var licenseId = Guid.NewGuid();
                var url = GetUrl(licenseId);
                var bearerToken = new TestBearerTokenBuilder()
                    .WithRole(UserRole.Standard)
                    .Build();

                var license = new LicenseEntity
                {
                    Id = licenseId,
                    Key = "This is a key",
                    Created = DateTime.UtcNow,
                    Products = new List<LicenseProductEntity>
                    {
                        new LicenseProductEntity
                        {
                            Name = "Product Name",
                            Key = "This is a key",
                            Created = DateTime.UtcNow,
                            Expiration = DateTime.UtcNow
                        }
                    }
                };

                await _licenseCollection.InsertOneAsync(license);
                
                HttpClient.SetBearerToken(bearerToken);

                // Act
                var response = await HttpClient.GetAsync(url);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<LicenseResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(responseData.Id, Is.EqualTo(licenseId));
                    Assert.That(responseData.Products[0].Name, Is.EqualTo(license.Products[0].Name));
                    DiagnoseaAssert.That(responseData.Products[0].Expiration, Is.EqualTo(license.Products[0].Expiration));
                });
            }
        }

        public class CreateLicenseAsync : LicenseControllerTests
        {
            private readonly string _url = $"{RouteConstants.Version1}/{RouteConstants.License.Base}";

            [Test]
            public async Task GivenNoBearerToken_RespondsWithUnauthorized()
            {
                // Arrange
                var request = new CreateLicenseRequest();
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(_url, request);
                
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            }

            [Test]
            public async Task GivenInvalidRole_RespondsWithForbidden()
            {
                // Arrange
                var bearerToken = new TestBearerTokenBuilder().Build();
                var request = new CreateLicenseRequest();

                HttpClient.SetBearerToken(bearerToken);
                
                // Act
                var response = await HttpClient.PostAsJsonAsync(_url, request);
                
                // Assert
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            }

            [Test]
            public async Task GivenNoUserId_RespondsWithBadRequestAndModelState()
            {
                // Arrange
                SetStandardBearerToken();

                var request = new CreateLicenseRequest();

                // Act
                var response = await HttpClient.PostAsJsonAsync(_url, request);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ValidationResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    DiagnoseaAssert.Contains(responseData.Errors, nameof(CreateLicenseRequest.UserId), InterchangeExceptionMessages.Required);
                });
            }

            [Test]
            public async Task GivenEmptyUserId_RespondsWithBadRequestAndModelState()
            {
                // Arrange
                SetStandardBearerToken();

                var request = new CreateLicenseRequest
                {
                    UserId = Guid.Empty
                };

                // Act
                var response = await HttpClient.PostAsJsonAsync(_url, request);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<ValidationResponse>();
                
                Assert.Multiple(() =>
                {
                    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                    DiagnoseaAssert.Contains(responseData.Errors, nameof(CreateLicenseRequest.UserId), InterchangeExceptionMessages.Required);
                });
            }

            [Test]
            public async Task GivenValidRequest_CreatesLicense()
            {
                // Arrange
                SetStandardBearerToken();

                var userId = Guid.NewGuid();

                var request = new CreateLicenseRequest
                {
                    UserId = userId
                };

                var user = new UserEntity
                {
                    Id = userId
                };

                await _userCollecion.InsertOneAsync(user);

                // Act
                var response = await HttpClient.PostAsJsonAsync(_url, request);
                
                // Assert
                var responseData = await response.Content.ReadFromJsonAsync<CreatedLicenseResponse>();
            
                var license = await _licenseCollection
                    .Find(x => x.Id == responseData.LicenseId)
                    .FirstOrDefaultAsync();
                
                Assert.Multiple(() =>
                {
                    AssertCreatedLicenseResponse(responseData);
                    AssertCreatedLicense(license, request);
                });
            }
        }
        
        private void SetStandardBearerToken()
        {
            var bearerToken = new TestBearerTokenBuilder()
                .WithRole(UserRole.Standard)
                .Build();
                
            HttpClient.SetBearerToken(bearerToken);
        }

        private static void AssertCreatedLicenseResponse(CreatedLicenseResponse response)
        {
            Assert.That(response.LicenseId, Is.Not.Null);
            Assert.That(response.LicenseId, Is.Not.EqualTo(Guid.Empty));
        }

        private static void AssertCreatedLicense(LicenseEntity license, CreateLicenseRequest request)
        {
            DiagnoseaAssert.That(license.Created, Is.EqualTo(DateTime.UtcNow));
            Assert.That(BCrypt.Net.BCrypt.Verify(request.UserId.ToString(), license.Key));
            Assert.That(license.UserId, Is.EqualTo(request.UserId));
        }
    }
}