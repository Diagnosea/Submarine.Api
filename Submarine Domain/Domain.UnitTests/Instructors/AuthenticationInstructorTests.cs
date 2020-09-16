﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Abstractions.Exceptions.Messages;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.Authentication.Dtos;
using Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText;
using Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.Instructors.Authentication;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseByUserId;
using Diagnosea.Submarine.Domain.License.TestPack.Builders;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Queries.GetUserByEmail;
using Diagnosea.Submarine.Domain.User.TestPack.Builders;
using Diagnosea.Submarine.UnitTestPack.Extensions;
using Domain.Authentication.TestPack.Builders;
using MediatR;
using Moq;
using NUnit.Framework;

namespace Diagnosea.Domain.Instructors.UnitTests.Instructors
{
    [TestFixture]
    public class AuthenticationInstructorTests
    {
        private Mock<IMediator> _mediator;
        private AuthenticationInstructor _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
            _classUnderTest = new AuthenticationInstructor(_mediator.Object);
        }

        public class RegisterAsync : AuthenticationInstructorTests
        {
            [Test]
            public async Task GivenRegisterUserDto_InsertsUserIntoDatabase()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                
                var registerUser = new RegisterDto
                {
                    EmailAddress = "This is an email",
                    PlainTextPassword = "This is a password",
                    UserName = "This is a username"
                };

                const string hashedPassword = "This is a hashed password";

                _mediator
                    .Setup(x => x.Send(It.IsAny<HashTextQuery>(), cancellationToken))
                    .ReturnsAsync(hashedPassword);

                // Act
                await _classUnderTest.RegisterAsync(registerUser, cancellationToken);
                
                // Assert
                _mediator.Verify(x => x.Send(It.Is<HashTextQuery>(
                    htq => VerifyHashTextQuery(htq, registerUser)), cancellationToken), Times.Once);
                
                _mediator.Verify(x => x.Send(It.Is<InsertUserCommand>(
                    iuc => VerifyInsertUserCommand(iuc, registerUser, hashedPassword)), cancellationToken), Times.Once);
            }

            private static bool VerifyHashTextQuery(HashTextQuery query, RegisterDto register)
            {
                return query.Text == register.PlainTextPassword;
            }

            private static bool VerifyInsertUserCommand(InsertUserCommand command, RegisterDto register, string hashedPassword)
            {
                return command.Id != Guid.Empty &&
                       command.EmailAddress == register.EmailAddress &&
                       command.Password == hashedPassword &&
                       command.UserName == register.UserName &&
                       command.Roles.Contains(UserRole.Standard);
            }
        }

        public class AuthenticateAsync : AuthenticationInstructorTests
        {
            [Test]
            public void GivenInvalidUser_ThrowsSubmarineEntityNotFoundException()
            {
                // Arrange
                const string emailAddress = "This is an email address";
                
                var authentication = new TestAuthenticationDtoBuilder()
                    .WithEmailAddress(emailAddress)
                    .Build();
                
                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<SubmarineEntityNotFoundException>(() => _classUnderTest.AuthenticateAsync(authentication, CancellationToken.None));
                    
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(UserExceptionMessages.UserNotFound));
                });
                
                _mediator.Verify(x =>
                    x.Send(It.Is<GetUserByEmailQuery>(query => query.EmailAddress == authentication.EmailAddress), CancellationToken.None), Times.Once);
            }

            [Test]
            public void GivenInvalidPassword_ThrowsSubmarineArgumentException()
            {
                // Arrange
                var userId = Guid.NewGuid();
                const string emailAddress = "This is an email address";

                var authentication = new TestAuthenticationDtoBuilder()
                    .WithEmailAddress(emailAddress)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .Build();
                
                _mediator.SetupHandler<GetUserByEmailQuery, UserEntity>().ReturnsAsync(user);
                
                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<SubmarineArgumentException>(() => 
                        _classUnderTest.AuthenticateAsync(authentication, CancellationToken.None));

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.ArgumentException));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(AuthenticationExceptionMessages.PasswordIsIncorrect));
                });
                
                _mediator.VerifyHandler<GetUserByEmailQuery, UserEntity>(query => query.EmailAddress == emailAddress, Times.Once());
            }

            [Test]
            public void GivenNoLicenseForUser_ThrowsSubmarineEntityNotFoundException()
            {
                // Arrange
                var userId = Guid.NewGuid();
                const string emailAddress = "This is an email address";
                const string plainTextPassword = "This is a password";
                
                var authentication = new TestAuthenticationDtoBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPlainTextPassword(plainTextPassword)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .Build();

                const bool isPasswordValid = true;
                
                _mediator.SetupHandler<GetUserByEmailQuery, UserEntity>().ReturnsAsync(user);
                _mediator.SetupHandler<CompareHashTextQuery, bool>().ReturnsAsync(isPasswordValid);
                
                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<SubmarineEntityNotFoundException>(() =>
                        _classUnderTest.AuthenticateAsync(authentication, CancellationToken.None));

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) SubmarineExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(AuthenticationExceptionMessages.NoLicensesUnderUserWithId));
                });

                _mediator.VerifyHandler<GetUserByEmailQuery, UserEntity>(query => query.EmailAddress == emailAddress, Times.Once());
                _mediator.VerifyHandler<CompareHashTextQuery, bool>(query => query.Hash == user.Password && query.Text == plainTextPassword, Times.Once());
            }

            [Test]
            public async Task GivenValidCredentials_ReturnsBearerToken()
            {
                // Arrange
                var userId = Guid.NewGuid();
                const string emailAddress = "This is an email address";
                const string plainTextPassword = "This is a password";
                const string licenseKey = "This is a license key";
                
                var authentication = new TestAuthenticationDtoBuilder()
                    .WithEmailAddress(emailAddress)
                    .WithPlainTextPassword(plainTextPassword)
                    .Build();

                var user = new TestUserEntityBuilder()
                    .WithId(userId)
                    .WithEmailAddress(emailAddress)
                    .WithRole(UserRole.Standard)
                    .Build();

                const bool isPasswordValid = true;

                var license = new TestLicenseEntityBuilder()
                    .WithKey(licenseKey)
                    .Build();

                const string bearerToken = "This is a bearer token";
                
                _mediator.SetupHandler<GetUserByEmailQuery, UserEntity>().ReturnsAsync(user);
                _mediator.SetupHandler<CompareHashTextQuery, bool>().ReturnsAsync(isPasswordValid);
                _mediator.SetupHandler<GetLicenseByUserIdQuery, LicenseEntity>().ReturnsAsync(license);
                _mediator.SetupHandler<GenerateBearerTokenQuery, string>().ReturnsAsync(bearerToken);
                
                // Act
                var result = await _classUnderTest.AuthenticateAsync(authentication, CancellationToken.None);
                
                // Assert
                Assert.That(result.BearerToken, Is.EqualTo(bearerToken));
                
                _mediator.VerifyHandler<GetUserByEmailQuery, UserEntity>(query => query.EmailAddress == emailAddress, Times.Once());
                _mediator.VerifyHandler<CompareHashTextQuery, bool>(query => query.Hash == user.Password && query.Text == plainTextPassword, Times.Once());
                _mediator.VerifyHandler<GenerateBearerTokenQuery, string>(query => ValidateGenerateBearerTokenQuery(query, user, licenseKey), Times.Once());
            }
            
            private static bool ValidateGenerateBearerTokenQuery(GenerateBearerTokenQuery query, UserEntity user, string audience)
            {
                return query.Subject == user.Id.ToString() &&
                       query.Name == user.UserName &&
                       query.Audience == audience &&
                       query.Roles.Contains(UserRole.Standard.ToString());
            }
        }
    }
}