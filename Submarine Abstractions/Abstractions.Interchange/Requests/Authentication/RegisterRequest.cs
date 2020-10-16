using System.ComponentModel.DataAnnotations;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = InterchangeExceptionMessages.Required)]
        [EmailAddress(ErrorMessage = InterchangeExceptionMessages.InvalidEmailAddress)]
        public string EmailAddress { get; set; }
        
        [Required(ErrorMessage = InterchangeExceptionMessages.Required)]
        public string Password { get; set; }
    }
}