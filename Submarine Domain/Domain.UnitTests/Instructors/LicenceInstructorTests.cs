using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Instructors.License;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Dtos;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseById;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseByUserId;
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
        private LicenseInstructor _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _classUnderTest = new LicenseInstructor(_mediator.Object);
        }

        public class GetByIdAsync : LicenceInstructorTests
        {
            [Test]
            public void GivenInvalidLicenseId_ThrowsEntityNotFoundException()
            {
                // Arrange
                var licenseId = Guid.NewGuid();

                _mediator
                    .SetupHandler<GetLicenseByIdQuery, LicenseEntity>();

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<EntityNotFoundException>(
                        () => _classUnderTest.GetByIdAsync(licenseId, CancellationToken.None));
                    
                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.License.NoLicenseWithId));
                });
            }

            [Test]
            public async Task GivenValidLicense_ReturnsLicense()
            {
                // Arrange
                var licenseId = Guid.NewGuid();

                var license = new LicenseEntity
                {
                    Id = licenseId
                };

                _mediator
                    .SetupHandler<GetLicenseByIdQuery, LicenseEntity>()
                    .ReturnsAsync(license);

                // Act
                var result = await _classUnderTest.GetByIdAsync(licenseId, CancellationToken.None);

                // Assert
                Assert.That(result.Id, Is.EqualTo(license.Id));
            }
        }

        public class GetByUserIdAsync : LicenceInstructorTests
        {
            [Test]
            public void GivenInvalidUserId_ThrowsEntityNotFoundException()
            {
                var userId = Guid.NewGuid();

                _mediator
                    .SetupHandler<GetUserByIdQuery, UserEntity>();
                
                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<EntityNotFoundException>(
                        () => _classUnderTest.GetByUserIdAsync(userId, CancellationToken.None));
                    
                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.User.UserNotFound));
                });
            }

            [Test]
            public void GivenNoLicenseWithUserId_ThrowsEntityNotFoundException()
            {
                // Arrange
                var userId = Guid.NewGuid();

                var user = new UserEntity
                {
                    Id = userId
                };

                _mediator
                    .SetupHandler<GetUserByIdQuery, UserEntity>()
                    .ReturnsAsync(user);

                _mediator
                    .SetupHandler<GetLicenseByUserIdQuery, LicenseEntity>();

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<EntityNotFoundException>(
                        () => _classUnderTest.GetByUserIdAsync(userId, CancellationToken.None));
                    
                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.License.NoLicenseWithUserId));
                });
            }

            [Test]
            public async Task GivenLicenseWithUserId_ReturnsLicense()
            {
                // Arrange
                var licenseId = Guid.NewGuid();
                var userId = Guid.NewGuid();

                var user = new UserEntity
                {
                    Id = userId
                };

                var license = new LicenseEntity
                {
                    Id = licenseId,
                    UserId = userId
                };

                _mediator
                    .SetupHandler<GetUserByIdQuery, UserEntity>()
                    .ReturnsAsync(user);

                _mediator
                    .SetupHandler<GetLicenseByUserIdQuery, LicenseEntity>()
                    .ReturnsAsync(license);
                
                // Act
                var result = await _classUnderTest.GetByUserIdAsync(userId, CancellationToken.None);
                
                // Assert
                Assert.That(result.Id, Is.EqualTo(licenseId));
            }
        }

        public class CreateAsync : LicenceInstructorTests
        {
            [Test]
            public void GivenInvalidUserId_ThrowsEntityNotFoundException()
            {
                // Arrange
                var createLicense = new CreateLicenseDto
                {
                    UserId = Guid.NewGuid()
                };
                
                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<EntityNotFoundException>(
                        () => _classUnderTest.CreateAsync(createLicense, CancellationToken.None));

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.User.UserNotFound));
                });
            }

            [Test]
            public async Task GivenValidLicense_CreatesLicense()
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
        }
    }
}