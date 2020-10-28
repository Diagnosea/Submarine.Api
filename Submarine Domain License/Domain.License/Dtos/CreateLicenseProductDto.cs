using System;

namespace Diagnosea.Submarine.Domain.License.Dtos
{
    public class CreateLicenseProductDto
    {
        public string Name { get; set; }
        public DateTime Expiration { get; set; }
    }
}