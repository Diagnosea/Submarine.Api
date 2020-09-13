using Diagnosea.Submarine.Domain.License.Commands.InsertLicense;
using Diagnosea.Submarine.Domain.License.Stubs;

namespace Diagnosea.Submarine.Domain.License.Extensions
{
    public static class InsertLicenseProductCommandExtensions
    {
        public static LicenseProductStub ToEntity(this InsertLicenseProductCommand command)
        {
            return new LicenseProductStub
            {
                Name = command.Name,
                Key = command.Key,
                Expiration = command.Expiration
            };
        }
    }
}