using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Dtos;
using Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText;
using Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseByProductKey;
using Diagnosea.Submarine.Domain.User;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Queries.GetUserByEmail;
using MediatR;

namespace Diagnosea.Submarine.Domain.Instructors.Authentication
{
    public class AuthenticationInstructor : IAuthenticationInstructor
    {
        private readonly IMediator _mediator;

        public AuthenticationInstructor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task RegisterAsync(RegisterDto register, CancellationToken cancellationToken)
        {
            var insertUserCommandBuilder = new InsertUserCommandBuilder()
                .WithId(Guid.NewGuid())
                .WithEmailAddress(register.EmailAddress)
                .WithUserName(register.UserName)
                .WithRole(UserRole.Standard);

            var hashTextQuery = new HashTextQueryBuilder()
                .WithText(register.PlainTextPassword)
                .Build();

            var hashedPassword = await _mediator.Send(hashTextQuery, cancellationToken);
            
            var insertUserCommand = insertUserCommandBuilder
                .WithPassword(hashedPassword)
                .Build();
            
            await _mediator.Send(insertUserCommand, cancellationToken);
        }

        public async Task<string> AuthenticateAsync(AuthenticationDto authentication, CancellationToken token)
        {
            await ValidateLicenseAsync(authentication, token);

            var user = await GetUserAsync(authentication, token);

            await ValidatePasswordAsync(authentication, user, token);

            var generateBearerTokenQuery = new GenerateBearerTokenQueryBuilder()
                .WithSubject(user.Id.ToString())
                .WithName(user.UserName)
                .WithRoles(user.Roles.AsStrings())
                .WithAudience(authentication.ProductKey)
                .Build();
            
            var bearerToken = await _mediator.Send(generateBearerTokenQuery, token);

            return bearerToken; 
        }
        
        private async Task ValidateLicenseAsync(AuthenticationDto authentication, CancellationToken token)
        {
            var getLicenseByProductKeyQuery = new GetLicenseByProductKeyQueryBuilder()
                .WithProductKey(authentication.ProductKey)
                .Build();

            var license = await _mediator.Send(getLicenseByProductKeyQuery, token);
            if (license == null)
            {
                throw new SubmarineArgumentException(
                    "Invalid Product Key Provided",
                    AuthenticationExceptionMessages.InvalidProductKey);
            }
        }

        private async Task ValidatePasswordAsync(AuthenticationDto authetnicate, UserEntity user, CancellationToken token)
        {
            var compareHashTextQuery = new CompareHashTextQueryBuilder()
                .WithHash(user.Password)
                .WithText(authetnicate.PlainTextPassword)
                .Build();

            var isValidPassword = await _mediator.Send(compareHashTextQuery, token);
            if (!isValidPassword)
            {
                throw new SubmarineArgumentException(
                    $"Invalid Password Provided",
                    AuthenticationExceptionMessages.InvalidPassword);
            }
        }

        private async Task<UserEntity> GetUserAsync(AuthenticationDto authentication, CancellationToken token)
        {
            var getUserByEmailQuery = new GetUserByEmailQueryBuilder()
                .WithEmailAddress(authentication.EmailAddress)
                .Build();

            var user = await _mediator.Send(getUserByEmailQuery, token);

            if (user == null)
            {
                throw new SubmarineEntityNotFoundException(
                    $"No User Found With Email: '{authentication.EmailAddress}'",
                    UserExceptionMessages.UserNotFound);
            }

            return user;
        }
    }
}