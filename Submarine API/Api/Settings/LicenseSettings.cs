using Diagnosea.Submarine.Domain.License.Settings;

namespace Diagnosea.Submarine.Api.Settings
{
    public class LicenseSettings : ILicenseSettings
    {
        public string LicensedProductName { get; set; }
    }
}