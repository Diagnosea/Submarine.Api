using Diagnosea.Submarine.Abstractions.Interchange.Requests.Authentication;

namespace Diagnosea.Submarine.Abstractions.Interchange.TestPack.Builders
{
    public class TestRegisterRequestBuilder
    {
        private string _emailAddress;
        private string _password;
        private string _userName;
        private string _friendlyName;

        public TestRegisterRequestBuilder WithEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public TestRegisterRequestBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public TestRegisterRequestBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }

        public TestRegisterRequestBuilder WithFriendlyName(string friendlyName)
        {
            _friendlyName = friendlyName;
            return this;
        }
        
        public RegisterRequest Build()
        {
            return new RegisterRequest
            {
                EmailAddress = _emailAddress,
                Password = _password,
                UserName = _userName,
                FriendlyName = _friendlyName
            };
        }
    }
}