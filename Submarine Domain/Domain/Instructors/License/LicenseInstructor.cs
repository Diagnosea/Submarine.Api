using System;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Domain.Authentication.Queries.HashText;
using Diagnosea.Submarine.Domain.License;
using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Dtos;
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

        public async Task<CreatedLicenseDto> CreateAsync(CreateLicenseDto createLicense)
        {
            var licenseId = Guid.NewGuid();
            
            var insertLicenseCommandBuilder = new InsertLicenseCommandBuilder()
                .WithId(licenseId)
                .WithCreated(DateTime.UtcNow)
                .WithUserId(createLicense.UserId);

            if (!createLicense.Products.All(product => _licenseSettings.AvailableProducts.Contains(product.Name)))
            {
                throw new SubmarineDataMismatchException(
                    "Not All Products Are Valid", 
                    LicenseExceptionMessages.InvalidProductName);
            }
            
            foreach (var product in createLicense.Products)
            {
                var insertLicenseProductCommand = await CreateLicenseProductCommandAsync(createLicense.UserId, product);

                insertLicenseCommandBuilder.WithProduct(insertLicenseProductCommand);
            }

            var insertLicenseCommand = insertLicenseCommandBuilder.Build();

            await _mediator.Send(insertLicenseCommand);

            return new CreatedLicenseDto {LicenseId = licenseId};
        }

        private async Task<InsertLicenseProductCommand> CreateLicenseProductCommandAsync(Guid userId, CreateLicenseProductDto createLicenseProduct)
        {
            var licenseKey = $"{userId}-{createLicenseProduct.Name}";
                
            var hashTextQuery = new HashTextQueryBuilder()
                .WithText(licenseKey)
                .Build();

            var hashedLicenseKey = await _mediator.Send(hashTextQuery);

            return new InsertLicenseProductCommandBuilder()
                .WithName(createLicenseProduct.Name)
                .WithKey(hashedLicenseKey)
                .WithCreated(DateTime.UtcNow)
                .WithExpiration(createLicenseProduct.Expiration)
                .Build();
        }
    }
}