using Diagnosea.Submarine.Domain.Authentication.Settings;

namespace Diagnosea.Submarine.Api.Settings
{
    public class AuthenticationSettings : ISubmarineAuthenticationSettings
    {
        public string Secret { get; set; }
        public int ExpirationInDays { get; set; }
        public string Issuer { get; set; }
        public int SaltingRounds { get; set; }
    }
}