using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.Instructors.License;
using Diagnosea.Submarine.Domain.License;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Dtos;
using Diagnosea.Submarine.Domain.License.TestPack.Builders;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Queries.GetUserById;
using Diagnosea.Submarine.UnitTestPack.Extensions;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Diagnosea.Domain.Instructors.UnitTests.Instructors
{
    [TestFixture]
    public class LicenceInstructorTests
    {
        private Mock<IMediator> _mediator;
        private ILicenseSettings _licenseSettings;
        private LicenseInstructor _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _licenseSettings = new TestLicenseSettings();
            _classUnderTest = new LicenseInstructor(_mediator.Object, _licenseSettings);
        }

        public class CreateAsync : LicenceInstructorTests
        {
            [Test]
            public void GivenInvalidUserId_ThrowsEntityNotFoundException()
            {
                // Arrange
                var createLicenseProduct = new CreateLicenseProductDto
                {
                    Name = "Not a Real Product",
                    Expiration = DateTime.UtcNow
                };

                var createLicense = new CreateLicenseDto
                {
                    UserId = Guid.NewGuid(),
                    Products = new List<CreateLicenseProductDto> {createLicenseProduct}
                };
                
                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<SubmarineEntityNotFoundException>(
                        () => _classUnderTest.CreateAsync(createLicense, CancellationToken.None));

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(UserExceptionMessages.UserNotFound));
                });
            }
            
            [Test]
            public void GivenInvalidProductName_ThrowsMismatchDataException()
            {
                // Arrange
                var userId = Guid.NewGuid();
                
                var createLicenseProduct = new CreateLicenseProductDto
                {
                    Name = "Not a Real Product",
                    Expiration = DateTime.UtcNow
                };

                var createLicense = new CreateLicenseDto
                {
                    UserId = userId,
                    Products = new List<CreateLicenseProductDto> {createLicenseProduct}
                };

                var user = new UserEntity
                {
                    Id = userId
                };

                _mediator
                    .SetupHandler<GetUserByIdQuery, UserEntity>()
                    .ReturnsAsync(user);
                
                // Act && Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<SubmarineDataMismatchException>(
                        () => _classUnderTest.CreateAsync(createLicense, CancellationToken.None));

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.DataMismatchException));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(LicenseExceptionMessages.InvalidProductName));
                });
            }

            [Test]
            public async Task GivenValidLicenseWithoutProducts_CreatesLicenseWithoutProducts()
            {
                // Arrange
                var userId = Guid.NewGuid();
                
                var createLicense = new CreateLicenseDto
                {
                    UserId = userId
                };
                
                var user = new UserEntity
                {
                    Id = userId
                };

                _mediator
                    .SetupHandler<GetUserByIdQuery, UserEntity>()
                    .ReturnsAsync(user);

                // Act
                await _classUnderTest.CreateAsync(createLicense, CancellationToken.None);
                
                // Assert
                _mediator.VerifyHandler<InsertLicenseCommand>(command => command.UserId == userId, Times.Once());
            }
            
            [Test]
            public async Task GivenLiceneWithProducts_CreatesLicenseWithProducts()
            {
                // Arrange
                var userId = Guid.NewGuid();
                const string submarineProductName = "Submarine";
                const string submarinePremiumProductName = "SubmarinePremium";
                var submarineLicensedProductKey = $"{userId}-{submarineProductName}";
                var submarinePremiumLicensedProductKey = $"{userId}-{submarinePremiumProductName}";
                var hashedLicenseKey = Guid.NewGuid().ToBase64String();
                var hashedSubmarineLicensedProductKey = Guid.NewGuid().ToBase64String();
                var hashedSubmarinePremiumLicensedProductKey = Guid.NewGuid().ToBase64String();

                var createSubmarineLicensedProduct = new TestCreateLicenseProductDtoBuilder()
                    .WithName(submarineProductName)
                    .Build();

                var createSubmarinePremiumLicensedProduct = new TestCreateLicenseProductDtoBuilder()
                    .WithName(submarinePremiumProductName)
                    .Build();

                var createLicense = new CreateLicenseDto
                {
                    UserId = userId,
                    Products = new List<CreateLicenseProductDto>
                    {
                        createSubmarineLicensedProduct,
                        createSubmarinePremiumLicensedProduct
                    }
                };
                
                var user = new UserEntity
                {
                    Id = userId
                };

                _mediator
                    .SetupHandler<GetUserByIdQuery, UserEntity>()
                    .ReturnsAsync(user);

                _mediator
                    .SetupHandler<HashTextQuery, string>(query => query.Text == userId.ToString())
                    .ReturnsAsync(hashedLicenseKey);

                _mediator
                    .SetupHandler<HashTextQuery, string>(query => query.Text == submarineLicensedProductKey)
                    .ReturnsAsync(hashedSubmarineLicensedProductKey);

                _mediator
                    .SetupHandler<HashTextQuery, string>(query => query.Text == submarinePremiumLicensedProductKey)
                    .ReturnsAsync(hashedSubmarinePremiumLicensedProductKey);
                
                // Act
                await _classUnderTest.CreateAsync(createLicense, CancellationToken.None);
                
                // Assert
                _mediator.VerifyHandler<HashTextQuery, string>(query => query.Text == submarineLicensedProductKey, Times.Once());
                _mediator.VerifyHandler<HashTextQuery, string>(query => query.Text == submarinePremiumLicensedProductKey, Times.Once());
                _mediator.VerifyHandler<InsertLicenseCommand>(command => 
                    command.Id != Guid.Empty &&
                    command.UserId == userId &&
                    command.Key == hashedLicenseKey &&
                    command.Products[0].Name == submarineProductName &&
                    command.Products[0].Key == hashedSubmarineLicensedProductKey &&
                    command.Products[0].Created != null &&
                    command.Products[0].Expiration == createSubmarineLicensedProduct.Expiration &&
                    command.Products[1].Name == submarinePremiumProductName &&
                    command.Products[1].Key == hashedSubmarinePremiumLicensedProductKey &&
                    command.Products[1].Created != null &&
                    command.Products[1].Expiration == createSubmarinePremiumLicensedProduct.Expiration,
                    Times.Once());
            }
        }
    }
}