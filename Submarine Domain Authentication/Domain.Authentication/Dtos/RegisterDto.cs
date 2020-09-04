namespace Diagnosea.Submarine.Domain.Authentication.Dtos
{
    public class RegisterDto
    {
        public string EmailAddress { get; set; }
        public string PlainTextPassword { get; set; }
        public string UserName { get; set; }
    }
}