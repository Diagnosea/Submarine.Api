namespace Diagnosea.Submarine.Domain.Authentication.Settings
{
    public interface ISubmarineJwtSettings
    { 
        string Secret { get; set; }
        int ExpirationInDays { get; set; }
    }
}