using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Interchange.Attributes;

namespace Diagnosea.Submarine.Abstractions.Interchange.Requests.License
{
    public class CreateLicenseRequest
    {
        [DiagnoseaRequired]
        public Guid? UserId { get; set; }
        
        public IList<CreateLicenseProductRequest> Products { get; set; } = new List<CreateLicenseProductRequest>();
    }
}