using System;
using Diagnosea.Submarine.Abstractions.Interchange.Attributes;

namespace Diagnosea.Submarine.Abstractions.Interchange.Requests.License
{
    public class CreateLicenseProductRequest
    {
        [DiagnoseaRequired]
        [DiagnoseaStringLength(50)]
        public string Name { get; set; }
        
        [DiagnoseaAfterNow]
        public DateTime? Expiration { get; set; }
    }
}