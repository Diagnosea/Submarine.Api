using Diagnosea.Submarine.Abstractions.Interchange.Attributes;

namespace Diagnosea.Submarine.Abstractions.Interchange.Authentication.Authenticate
{
    public class AuthenticateRequest
    {
        [SubmarineRequired]
        [SubmarineEmailAddress]
        public string EmailAddress { get; set; }
        
        [SubmarineRequired]
        public string Password { get; set; }
    }
}