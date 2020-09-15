using Diagnosea.Submarine.Abstractions.Interchange.Attributes;

namespace Diagnosea.Submarine.Abstractions.Interchange.Authentication.Register
{
    public class RegisterRequest
    {
        [SubmarineRequired]
        [SubmarineEmailAddress]
        public string EmailAddress { get; set; }
        
        [SubmarineRequired]
        public string Password { get; set; }
        
        public string UserName { get; set; }
        
        public string FriendlyName { get; set; }
    }
}