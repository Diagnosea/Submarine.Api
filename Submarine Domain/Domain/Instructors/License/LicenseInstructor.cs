using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.License;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Dtos;
using Diagnosea.Submarine.Domain.User.Queries.GetUserById;
using MediatR;

namespace Diagnosea.Submarine.Domain.Instructors.License
{
    public class LicenseInstructor : ILicenseInstructor
    {
        private readonly IMediator _mediator;
        private readonly ILicenseSettings _licenseSettings;

        public LicenseInstructor(IMediator mediator, ILicenseSettings licenseSettings)
        {
            _mediator = mediator;
            _licenseSettings = licenseSettings;
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

            ValidateProductsAreValid(createLicense);

            foreach (var product in createLicense.Products)
            {
                var licenseProductKey = await GenerateLicenseProductKey(createLicense.UserId, product.Name, token);
                
                var insertLicenseProductCommand =  new InsertLicenseProductCommandBuilder()
                    .WithName(product.Name)
                    .WithKey(licenseProductKey)
                    .WithCreated(DateTime.UtcNow)
                    .WithExpiration(product.Expiration)
                    .Build();

                insertLicenseCommandBuilder.WithProduct(insertLicenseProductCommand);
            }

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

        private void ValidateProductsAreValid(CreateLicenseDto createLicense)
        {
            if (!createLicense.Products.All(product => _licenseSettings.AvailableProducts.Contains(product.Name)))
            {
                throw new SubmarineDataMismatchException(
                    $"All {createLicense.Products.Count} Products Are Not Valid", 
                    LicenseExceptionMessages.InvalidProductName);
            }
        }

        private async Task<string> GenerateLicenseKey(Guid userId, CancellationToken token)
        {
            var hashTextQuery = new HashTextQueryBuilder()
                .WithText(userId.ToString())
                .Build();

            return await _mediator.Send(hashTextQuery, token);
        }
        
        private async Task<string> GenerateLicenseProductKey(Guid userId, string productName, CancellationToken token)
        {
            var licenseProductKey = $"{userId}-{productName}";
                
            var hashTextQuery = new HashTextQueryBuilder()
                .WithText(licenseProductKey)
                .Build();

            return await _mediator.Send(hashTextQuery, token);
        }
    }
}