using Diagnosea.Submarine.Domain.Authentication.Dtos;

namespace Domain.Authentication.TestPack.Builders
{
    public class TestAuthenticationDtoBuilder
    {
        private string _emailAddress;
        private string _plainTextPassword;

        public TestAuthenticationDtoBuilder WithEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public TestAuthenticationDtoBuilder WithPlainTextPassword(string plainTextPassword)
        {
            _plainTextPassword = plainTextPassword;
            return this;
        }
        
        public AuthenticationDto Build()
        {
            return new AuthenticationDto
            {
                EmailAddress = _emailAddress ?? "This is an email address",
                PlainTextPassword = _plainTextPassword ?? "This is a password"
            };
        }
    }
}