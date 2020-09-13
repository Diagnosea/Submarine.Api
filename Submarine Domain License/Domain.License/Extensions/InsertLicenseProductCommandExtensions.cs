using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Entities;

namespace Diagnosea.Submarine.Domain.License.Extensions
{
    public static class InsertLicenseProductCommandExtensions
    {
        public static LicenseProductEntity ToEntity(this InsertLicenseProductCommand command)
        {
            return new LicenseProductEntity
            {
                Name = command.Name,
                Key = command.Key,
                Expiration = command.Expiration
            };
        }
    }
}