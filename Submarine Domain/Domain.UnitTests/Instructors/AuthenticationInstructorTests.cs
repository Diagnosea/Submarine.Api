using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
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
            public void GivenEmailAlreadyExists_ThrowsSubmarineEntityFoundException()
            {
                // Arrange
                var register = new RegisterDto
                {
                    EmailAddress = "This is an email",
                    PlainTextPassword = "This is a password",
                    UserName = "This is a username"
                };

                var user = new UserEntity
                {
                    EmailAddress = "This is an email"
                };

                _mediator.SetupHandler<GetUserByEmailQuery, UserEntity>().ReturnsAsync(user);

                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.ThrowsAsync<DataAlreadyExistsException>(() => _classUnderTest.RegisterAsync(register, CancellationToken.None));

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.DataAlreadyExists));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.User.UserExistsWithEmail));
                });

                _mediator.VerifyHandler<GetUserByEmailQuery, UserEntity>(query => query.EmailAddress == register.EmailAddress, Times.Once());
            }
            
            [Test]
            public async Task GivenRegisterUserDto_InsertsUserIntoDatabase()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                
                var register = new RegisterDto
                {
                    EmailAddress = "This is an email",
                    PlainTextPassword = "This is a password",
                    UserName = "This is a username"
                };

                const string hashedPassword = "This is a hashed password";
                
                _mediator.SetupHandler<HashTextQuery, string>().ReturnsAsync(hashedPassword);

                // Act
                await _classUnderTest.RegisterAsync(register, cancellationToken);
                
                // Assert
                _mediator.VerifyHandler<HashTextQuery, string>(query => query.Text == register.PlainTextPassword, Times.Once());
                _mediator.VerifyHandler<InsertUserCommand>(command => VerifyInsertUserCommand(command, register), Times.Once());
            }

            private static bool VerifyInsertUserCommand(InsertUserCommand command, RegisterDto register)
            {
                return command.Id != null &&
                       command.EmailAddress == register.EmailAddress &&
                       command.Password != null && // Handled by BCrypt in the integration test.
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
                    var exception = Assert.ThrowsAsync<EntityNotFoundException>(() => _classUnderTest.AuthenticateAsync(authentication, CancellationToken.None));
                    
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.User.UserNotFound));
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
                    var exception = Assert.ThrowsAsync<DataMismatchException>(() => 
                        _classUnderTest.AuthenticateAsync(authentication, CancellationToken.None));

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.DataMismatchException));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.Authentication.PasswordIsIncorrect));
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
                    var exception = Assert.ThrowsAsync<EntityNotFoundException>(() =>
                        _classUnderTest.AuthenticateAsync(authentication, CancellationToken.None));

                    Assert.That(exception.ExceptionCode, Is.EqualTo((int) ExceptionCode.EntityNotFound));
                    Assert.That(exception.TechnicalMessage, Is.Not.Null);
                    Assert.That(exception.UserMessage, Is.EqualTo(ExceptionMessages.License.NoLicenseWithUserId));
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

                var unexpiredProduct = new TestLicenseProductEntityBuilder()
                    .WithName("Unexpired Product Name")
                    .WithExpiration(DateTime.UtcNow.AddDays(1))
                    .Build();

                var expiredProduct = new TestLicenseProductEntityBuilder()
                    .WithName("Expired Product Name")
                    .WithExpiration(DateTime.UtcNow.AddDays(-2))
                    .Build();

                var license = new TestLicenseEntityBuilder()
                    .WithKey(licenseKey)
                    .WithProduct(unexpiredProduct)
                    .WithProduct(expiredProduct)
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
                       query.Name == user.EmailAddress &&
                       query.Audience == audience &&
                       query.Products.Contains("Unexpired Product Name") &&
                       !query.Products.Contains("Expired Product Name") && 
                       query.Roles.Contains(UserRole.Standard.ToString());
            }
        }
    }
}