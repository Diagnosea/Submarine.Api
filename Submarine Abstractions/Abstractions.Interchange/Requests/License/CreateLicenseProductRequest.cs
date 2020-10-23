using System;

namespace Diagnosea.Submarine.Abstractions.Interchange.Requests.License
{
    public class CreateLicenseProductRequest
    {
        public string Name { get; set; }
        public DateTime? Expiration { get; set; }
    }
}