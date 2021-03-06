﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Instructors.User;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Queries.GetUserById;
using Diagnosea.Submarine.UnitTestPack.Extensions;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Diagnosea.Domain.Instructors.UnitTests.Instructors
{
    [TestFixture]
    public class UserInstructorTests
    {
        private Mock<IMediator> _mediator;
        private UserInstructor _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _classUnderTest = new UserInstructor(_mediator.Object);
        }

        public class GetAsync : UserInstructorTests
        {
            [Test]
            public void GivenInvalidId_ThrowsSubmarineEntityNotFoundException()
            {
                // Assert
                var cancellationToken = new CancellationToken();
                var userId = Guid.NewGuid();

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<EntityNotFoundException>(() => _classUnderTest.GetAsync(userId, cancellationToken));

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.User.UserNotFound));
                });
            }
            
            [Test]
            public async Task GivenValidId_ReturnsUser()
            {
                // Assert
                var cancellationToken = new CancellationToken();
                var userId = Guid.NewGuid();

                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    UserName = "This is a user name",
                    FriendlyName = "This is a friendly name"
                };

                _mediator
                    .SetupHandler<GetUserByIdQuery, UserEntity>()
                    .ReturnsAsync(user);
                
                // Act
                var result = await _classUnderTest.GetAsync(userId, cancellationToken);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Id, Is.EqualTo(user.Id));
                    Assert.That(result.UserName, Is.EqualTo(user.UserName));
                    Assert.That(result.FriendlyName, Is.EqualTo(user.FriendlyName));
                });
            }
        }
    }
}