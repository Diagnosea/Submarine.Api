using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Dtos;
using Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText;
using Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.Authentication.Queries.ValidateAudience;
using Diagnosea.Submarine.Domain.Instructors.Authentication;
using Diagnosea.Submarine.Domain.User;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Enums;
using Diagnosea.Submarine.Domain.User.Queries.GetUserByEmail;
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
                return query.PlainText == register.PlainTextPassword;
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
            public void GivenInvalidAudience_ThrowsSubmarineArgumentException()
            {
                  // Arrange
                var cancellationToken = new CancellationToken();
                
                var authentication = new AuthenticationDto
                {
                    AudienceId = "This is an audience ID",
                    EmailAddress = "This is an email address",
                    Password = "This is a plain text password"
                };

                const bool isValueAudience = false;

                _mediator
                    .Setup(x => x.Send(It.IsAny<ValidateAudienceQuery>(), cancellationToken))
                    .ReturnsAsync(isValueAudience);

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<SubmarineArgumentException>(() => _classUnderTest.AuthenticateAsync(authentication, cancellationToken));

                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(AuthenticationExceptionMessages.InvalidAudience));
                });
                
                _mediator.Verify(x => x.Send(
                    It.Is<ValidateAudienceQuery>(vaq => 
                        ValidateValidateAudienceQuery(vaq, authentication.AudienceId)), cancellationToken), 
                    Times.Once);
            }

            [Test]
            public void GivenInvalidUser_ThrowsSubmarineEntityNotFoundException()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                
                var authentication = new AuthenticationDto
                {
                    AudienceId = "This is an audience ID",
                    EmailAddress = "This is an email address",
                    Password = "This is a plain text password"
                };

                const bool isValueAudience = true;

                _mediator
                    .Setup(x => x.Send(It.IsAny<ValidateAudienceQuery>(), cancellationToken))
                    .ReturnsAsync(isValueAudience);

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<SubmarineEntityNotFoundException>(() => _classUnderTest.AuthenticateAsync(authentication, cancellationToken));

                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(UserExceptionMessages.UserNotFound));
                });
                
                _mediator.Verify(x => x.Send(
                    It.Is<ValidateAudienceQuery>(vaq => 
                        ValidateValidateAudienceQuery(vaq, authentication.AudienceId)), cancellationToken), 
                    Times.Once);
                
                _mediator.Verify(x => x.Send(
                    It.Is<GetUserByEmailQuery>(gubeq =>
                        ValidateGetUserByEmailQuery(gubeq, authentication.EmailAddress)), cancellationToken),
                    Times.Once);
            }

            [Test]
            public void GivenInvalidPassword_ThrowsSubmarineArgumentException()
            {
                                // Arrange
                var cancellationToken = new CancellationToken();
                
                var authentication = new AuthenticationDto
                {
                    AudienceId = "This is an audience ID",
                    EmailAddress = "This is an email address",
                    Password = "This is a plain text password"
                };

                const bool isValueAudience = true;

                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = authentication.EmailAddress,
                    Password = "This is typically a hashed password",
                    UserName = "This is a username",
                    Roles = new List<UserRole> {UserRole.Standard}
                };

                const bool isValidPassword = false;

                _mediator
                    .Setup(x => x.Send(It.IsAny<ValidateAudienceQuery>(), cancellationToken))
                    .ReturnsAsync(isValueAudience);

                _mediator
                    .Setup(x => x.Send(It.IsAny<GetUserByEmailQuery>(), cancellationToken))
                    .ReturnsAsync(user);

                _mediator
                    .Setup(x => x.Send(It.IsAny<CompareHashTextQuery>(), cancellationToken))
                    .ReturnsAsync(isValidPassword);

                // AAct & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<SubmarineArgumentException>(() => _classUnderTest.AuthenticateAsync(authentication, cancellationToken));

                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(AuthenticationExceptionMessages.InvalidPassword));
                });

                _mediator.Verify(x => x.Send(
                    It.Is<ValidateAudienceQuery>(vaq => 
                        ValidateValidateAudienceQuery(vaq, authentication.AudienceId)), cancellationToken), 
                    Times.Once);
                
                _mediator.Verify(x => x.Send(
                    It.Is<GetUserByEmailQuery>(gubeq =>
                        ValidateGetUserByEmailQuery(gubeq, authentication.EmailAddress)), cancellationToken),
                    Times.Once);
                
                _mediator.Verify(x => x.Send(
                    It.Is<CompareHashTextQuery>(chtq => 
                        ValidateCompareHashTextQuery(chtq, authentication.Password, user.Password)), cancellationToken),
                    Times.Once);
            }
            
            [Test]
            public async Task GivenValidAuthentication_ReturnsBearerToken()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                
                var authentication = new AuthenticationDto
                {
                    AudienceId = "This is an audience ID",
                    EmailAddress = "This is an email address",
                    Password = "This is a plain text password"
                };

                const bool isValueAudience = true;

                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = authentication.EmailAddress,
                    Password = "This is typically a hashed password",
                    UserName = "This is a username",
                    Roles = new List<UserRole> {UserRole.Standard}
                };

                const bool isValidPassword = true;

                const string bearerToken = "This is a JWT";

                _mediator
                    .Setup(x => x.Send(It.IsAny<ValidateAudienceQuery>(), cancellationToken))
                    .ReturnsAsync(isValueAudience);

                _mediator
                    .Setup(x => x.Send(It.IsAny<GetUserByEmailQuery>(), cancellationToken))
                    .ReturnsAsync(user);

                _mediator
                    .Setup(x => x.Send(It.IsAny<CompareHashTextQuery>(), cancellationToken))
                    .ReturnsAsync(isValidPassword);

                _mediator
                    .Setup(x => x.Send(It.IsAny<GenerateBearerTokenQuery>(), cancellationToken))
                    .ReturnsAsync(bearerToken);
                
                // Act
                var result = await _classUnderTest.AuthenticateAsync(authentication, cancellationToken);
                
                // Assert
                Assert.That(result, Is.EqualTo(bearerToken));
                
                _mediator.Verify(x => x.Send(
                    It.Is<ValidateAudienceQuery>(vaq => 
                        ValidateValidateAudienceQuery(vaq, authentication.AudienceId)), cancellationToken), 
                    Times.Once);
                
                _mediator.Verify(x => x.Send(
                    It.Is<GetUserByEmailQuery>(gubeq =>
                        ValidateGetUserByEmailQuery(gubeq, authentication.EmailAddress)), cancellationToken),
                    Times.Once);
                
                _mediator.Verify(x => x.Send(
                    It.Is<CompareHashTextQuery>(chtq => 
                        ValidateCompareHashTextQuery(chtq, authentication.Password, user.Password)), cancellationToken),
                    Times.Once);
                
                _mediator.Verify(x => x.Send(It.Is<GenerateBearerTokenQuery>(gbtq =>
                    ValidateGenerateBearerTokenQuery(gbtq, user, authentication.AudienceId)), cancellationToken),
                    Times.Once);
            }

            private static bool ValidateValidateAudienceQuery(ValidateAudienceQuery query, string audienceId)
            {
                return query.AudienceId == audienceId;
            }

            private static bool ValidateGetUserByEmailQuery(GetUserByEmailQuery query, string emailAddress)
            {
                return query.EmailAddress == emailAddress;
            }

            private static bool ValidateCompareHashTextQuery(CompareHashTextQuery query, string plainText, string hash)
            {
                return query.Text == plainText && query.Hash == hash;
            }

            private static bool ValidateGenerateBearerTokenQuery(GenerateBearerTokenQuery query, UserEntity user, string audience)
            {
                return query.Subject == user.Id.ToString() &&
                       query.Name == user.UserName &&
                       query.AudienceId == audience &&
                       query.Roles.Contains(UserRole.Standard.ToString());
            }
        }
    }
}