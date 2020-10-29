﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Dtos;
using Diagnosea.Submarine.Domain.User.Queries.GetUserById;
using MediatR;

namespace Diagnosea.Submarine.Domain.Instructors.License
{
    public class LicenseInstructor : ILicenseInstructor
    {
        private readonly IMediator _mediator;

        public LicenseInstructor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CreatedLicenseDto> CreateAsync(CreateLicenseDto createLicense, CancellationToken token)
        {
            await ValidateUserExistsAsync(createLicense.UserId, token);
            
            var licenseId = Guid.NewGuid();
            var licenseKey = await GenerateLicenseKey(createLicense.UserId, token);
            
            var insertLicenseCommandBuilder = new InsertLicenseCommandBuilder()
                .WithId(licenseId)
                .WithKey(licenseKey)
                .WithCreated(DateTime.UtcNow)
                .WithUserId(createLicense.UserId);

            var insertLicenseCommand = insertLicenseCommandBuilder.Build();

            await _mediator.Send(insertLicenseCommand, token);

            return new CreatedLicenseDto {LicenseId = licenseId};
        }
        
        private async Task ValidateUserExistsAsync(Guid userId, CancellationToken token)
        {
            var getUserByIdQuery = new GetUserByIdQueryBuilder()
                .WithId(userId)
                .Build();
            
            var user = await _mediator.Send(getUserByIdQuery, token);

            if (user == null)
            {
                throw new SubmarineEntityNotFoundException(
                    $"No User With ID: '{userId}' Found",
                    UserExceptionMessages.UserNotFound);
            }
        }

        private async Task<string> GenerateLicenseKey(Guid userId, CancellationToken token)
        {
            var hashTextQuery = new HashTextQueryBuilder()
                .WithText(userId.ToString())
                .Build();

            return await _mediator.Send(hashTextQuery, token);
        }
    }
}