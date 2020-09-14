namespace Diagnosea.Submarine.Domain.Authentication.Dtos
{
    public class AuthenticateDto
    {
        public string EmailAddress { get; set; }
        public string PlainTextPassword { get; set; }
    }
}