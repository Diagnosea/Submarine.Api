using System;
using System.Collections.Generic;

namespace Diagnosea.Submarine.Abstractions.Interchange.Responses.License
{
    public class LicenseResponse
    {
        public Guid Id { get; set; }
        public IList<LicenseProductResponse> Products { get; set; } = new List<LicenseProductResponse>();
    }
}