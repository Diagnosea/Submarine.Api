using System;

namespace Diagnosea.Submarine.Domain.License.Commands.InsertLicense
{
    public class InsertLicenseProductCommand
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Expiration { get; set; }
    }
}