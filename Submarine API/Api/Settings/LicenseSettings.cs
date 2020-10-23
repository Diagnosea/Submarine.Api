using System.Collections.Generic;
using Diagnosea.Submarine.Domain.License;

namespace Diagnosea.Submarine.Api.Settings
{
    public class LicenseSettings : ILicenseSettings
    {
        public IList<string> AvailableProducts { get; set; } = new List<string>();
    }
}