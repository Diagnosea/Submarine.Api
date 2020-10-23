using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Diagnosea.Submarine.Abstractions.Interchange.Requests.License
{
    public class CreateLicenseRequest
    {
        [Required]
        public Guid? UserId { get; set; }
        
        public IList<CreateLicenseProductRequest> Products { get; set; } = new List<CreateLicenseProductRequest>();
    }
}