using System;

namespace Diagnosea.Submarine.Abstractions.Interchange.Responses.License
{
    public class LicenseProductResponse
    {
        public string Name { get; set; }
        public DateTime Expiration { get; set; }
    }
}