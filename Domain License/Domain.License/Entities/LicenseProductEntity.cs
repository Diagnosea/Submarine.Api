using System;

namespace Diagnosea.Submarine.Domain.License.Entities
{
    public class LicenseProductEntity
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime? Expiration { get; set; }
    }
}