using Diagnosea.Submarine.Abstractions.Interchange.Attributes;

namespace Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication
{
    public class AuthenticateRequest
    {
        [DiagnoseaRequired]
        [DiagnoseaEmailAddress]
        public string EmailAddress { get; set; }
        
        [DiagnoseaRequired]
        public string Password { get; set; }
    }
}