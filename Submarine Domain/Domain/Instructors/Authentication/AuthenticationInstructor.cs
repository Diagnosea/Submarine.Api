﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.Authentication.Dtos;
using Diagnosea.Submarine.Domain.Authentication.Queries.CompareHashText;
using Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.License.Queries.GetLicenseByUserId;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Queries.GetUserByEmail;
using MediatR;

namespace Diagnosea.Submarine.Domain.Instructors.Authentication
{
    public class AuthenticationInstructor : IAuthenticationInstructor
    {
        private readonly IMediator _mediator;

        public AuthenticationInstructor(IMediator mediator) => _mediator = mediator;

        public async Task RegisterAsync(RegisterDto register, CancellationToken token)
        {
            await ValidateUserDoesNotExist(register.EmailAddress, token);
            
            var insertUserCommandBuilder = new InsertUserCommandBuilder()
                .WithId(Guid.NewGuid())
                .WithEmailAddress(register.EmailAddress)
                .WithUserName(register.UserName)
                .WithFriendlyName(register.FriendlyName)
                .WithRole(UserRole.Standard);

            var hashTextQuery = new HashTextQueryBuilder()
                .WithText(register.PlainTextPassword)
                .Build();

            var hashedPassword = await _mediator.Send(hashTextQuery, token);
            
            var insertUserCommand = insertUserCommandBuilder
                .WithPassword(hashedPassword)
                .Build();
            
            await _mediator.Send(insertUserCommand, token);
        }
        
        public async Task<AuthenticatedDto> AuthenticateAsync(AuthenticateDto authenticate, CancellationToken token)
        {
            var user = await GetUserByEmailAsync(authenticate.EmailAddress, token);

            await ValidatePasswordAsync(authenticate.PlainTextPassword, user.Password, token);
            
            var license = await GetLicenseByUserIdAsync(user.Id, token);

            var productNames = license.Products
                .Where(x => x.Expiration > DateTime.UtcNow)
                .Select(product => product.Name);
            
            var generateBearerTokenQuery = new GenerateBearerTokenQueryBuilder()
                .WithSubject(user.Id.ToString())
                .WithName(user.EmailAddress)
                .WithRoles(user.Roles.AsStrings())
                .WithProducts(productNames)
                .WithAudience(license.Key)
                .Build();
            
            var bearerToken = await _mediator.Send(generateBearerTokenQuery, token);

            return new AuthenticatedDto
            {
                BearerToken = bearerToken
            };
        }

        private async Task ValidateUserDoesNotExist(string emailAddress, CancellationToken token)
        {
            var getUserByEmailQuery = new GetUserByEmailQueryBuilder()
                .WithEmailAddress(emailAddress)
                .Build();

            var user = await _mediator.Send(getUserByEmailQuery, token);

            if (user != null)
            {
                throw new DataAlreadyExistsException(
                    $"User Already Exists For Email: '{user.EmailAddress}'",
                    ExceptionMessages.User.UserExistsWithEmail);
            }
        }
        
        private async Task<UserEntity> GetUserByEmailAsync(string emailAddress, CancellationToken token)
        {
            var getUserByEmailQuery = new GetUserByEmailQueryBuilder()
                .WithEmailAddress(emailAddress)
                .Build();

            var user = await _mediator.Send(getUserByEmailQuery, token);

            if (user == null)
            {
                throw new EntityNotFoundException(
                    $"No User Found With Email: '{emailAddress}'",
                    ExceptionMessages.User.UserNotFound);
            }

            return user;
        }
        
        private async Task ValidatePasswordAsync(string plainTextPassword, string hashedPassword, CancellationToken token)
        {
            var compareHashTextQuery = new CompareHashTextQueryBuilder()
                .WithHash(hashedPassword)
                .WithText(plainTextPassword)
                .Build();

            var isValidPassword = await _mediator.Send(compareHashTextQuery, token);
            if (!isValidPassword)
            {
                throw new DataMismatchException(
                    "Invalid Password Provided",
                    ExceptionMessages.Authentication.PasswordIsIncorrect);
            }
        }

        private async Task<LicenseEntity> GetLicenseByUserIdAsync(Guid userId, CancellationToken token)
        {
            var getLicenseByUserId = new GetLicenseByUserIdQueryBuilder()
                .WithUserId(userId)
                .Build();

            var license = await _mediator.Send(getLicenseByUserId, token);
            if (license == null)
            {
                throw new EntityNotFoundException(
                    $"No Licenses Found Under User '{userId}'",
                    ExceptionMessages.License.NoLicenseWithUserId);
            }

            return license;
        }
    }
}