namespace Abstractions.Traffic.Authentication
{
    public class AuthenticateRequest
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}