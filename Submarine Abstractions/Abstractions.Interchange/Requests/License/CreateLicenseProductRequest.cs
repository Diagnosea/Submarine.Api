using System;
using System.ComponentModel.DataAnnotations;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Abstractions.Interchange.Requests.License
{
    public class CreateLicenseProductRequest
    {
        [Required(ErrorMessage = InterchangeExceptionMessages.Required)]
        public string Name { get; set; }
        public DateTime? Expiration { get; set; }
    }
}