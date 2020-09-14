using Abstractions.Traffic.Authentication;

namespace Abstractions.Traffic.TestPack.Builders
{
    public class TestAuthenticateRequestBuilder
    {
        private string _emailAddress;
        private string _password;

        public TestAuthenticateRequestBuilder WithEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public TestAuthenticateRequestBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public AuthenticateRequest Build()
        {
            return new AuthenticateRequest
            {
                EmailAddress = _emailAddress ?? "This is an email address",
                Password = _password ?? "This is a password"
            };
        }
    }
}