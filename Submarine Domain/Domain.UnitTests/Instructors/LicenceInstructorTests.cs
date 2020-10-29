using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Instructors.License;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Dtos;
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
                    var exception = Assert.ThrowsAsync<SubmarineEntityNotFoundException>(
                        () => _classUnderTest.CreateAsync(createLicense, CancellationToken.None));

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(UserExceptionMessages.UserNotFound));
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