namespace Diagnosea.Submarine.Domain.Authentication.Dtos
{
    public class AuthenticationDto
    {
        public string EmailAddress { get; set; }
        public string PlainTextPassword { get; set; }
        public string AudienceId { get; set; }
    }
}