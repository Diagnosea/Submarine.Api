using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Dtos;
using Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText;
using Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.Authentication.Queries.ValidateAudience;
using Diagnosea.Submarine.Domain.User;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Enums;
using Diagnosea.Submarine.Domain.User.Extensions;
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

        public async Task<string> AuthenticateAsync(AuthenticationDto authentication, CancellationToken cancellationToken)
        {
            await ValidateAudienceAsync(authentication, cancellationToken);

            var user = await GetUserAsync(authentication, cancellationToken);

            await ValidatePasswordAsync(authentication, user, cancellationToken);

            var generateBearerTokenQuery = new GenerateBearerTokenQueryBuilder()
                .WithSubject(user.Id.ToString())
                .WithName(user.UserName)
                .WithRoles(user.Roles.AsStrings())
                .WithAudienceId(authentication.AudienceId)
                .Build();
            
            var bearerToken = await _mediator.Send(generateBearerTokenQuery, cancellationToken);

            return bearerToken; 
        }
        
        private async Task ValidateAudienceAsync(AuthenticationDto authentication, CancellationToken token)
        {
            var validateAudienceQuery = new ValidateAudienceQueryBuilder()
                .WithAudienceId(authentication.AudienceId)
                .Build();
            
            var isValidAudience = await _mediator.Send(validateAudienceQuery, token);
            if (!isValidAudience)
            {
                throw new SubmarineArgumentException(
                    $"No Audience With ID '{authentication.AudienceId}'", 
                    AuthenticationExceptionMessages.InvalidAudience);
            }
        }

        private async ValueTask ValidatePasswordAsync(AuthenticationDto authetnicate, UserEntity user, CancellationToken token)
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