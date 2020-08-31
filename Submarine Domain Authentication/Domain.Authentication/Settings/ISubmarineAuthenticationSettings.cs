namespace Diagnosea.Submarine.Domain.Authentication.Settings
{
    public interface ISubmarineAuthenticationSettings
    { 
        string Secret { get; set; }
        int ExpirationInDays { get; set; }
        string Issuer { get; set; }
        int SaltingRounds { get; set; }
    }
}