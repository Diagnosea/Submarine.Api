using System;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Dtos;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.Mappers;
using Diagnosea.Submarine.Domain.User;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Enums;
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
            var insertUserCommand = new InsertUserCommandBuilder()
                .WithId(Guid.NewGuid())
                .WithEmailAddress(register.EmailAddress)
                .WithUserName(register.UserName)
                .WithRole(UserRole.Standard);

            var hashTextQuery = new HashTextQueryBuilder().WithPlainText(register.PlainTextPassword);
            
            var hashedPassword = await _mediator.Send(hashTextQuery.Build(), cancellationToken);
            insertUserCommand.WithPassword(hashedPassword);
            
            await _mediator.Send(insertUserCommand.Build(), cancellationToken);
        }

        public async Task<string> AuthenticateAsync(AuthenticationDto authentication, CancellationToken cancellationToken)
        {
            await ValidateAudienceAsync(authentication, cancellationToken);

            var user = await GetUserAsync(authentication, cancellationToken);

            await ValidatePasswordAsync(authentication, user, cancellationToken);
            
            var bearerToken = await _mediator.Send(user.ToGenerateBearerTokenQuery(authentication.AudienceId), cancellationToken);

            return bearerToken; 
        }
        
        private async Task ValidateAudienceAsync(AuthenticationDto authentication, CancellationToken token)
        {
            var validateAudienceQuery = authentication.ToValidateAudienceQuery();
            
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
            var compareHashTextQuery = authetnicate.ToCompareHashTextQuery(user.Password);

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
            var getUserByEmailQuery = authentication.ToGetUserByEmailQuery();

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