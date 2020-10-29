using System;
using Diagnosea.Submarine.Abstractions.Interchange.Attributes;

namespace Diagnosea.Submarine.Abstractions.Interchange.Requests.License
{
    public class CreateLicenseRequest
    {
        [DiagnoseaRequired]
        public Guid? UserId { get; set; }
    }
}