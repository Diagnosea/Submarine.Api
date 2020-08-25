using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.Authentication.Settings
{
    public interface ISubmarineJwtSettings
    { 
        string Secret { get; set; }
        int ExpirationInDays { get; set; }
        IList<string> Audiences { get; set; }
        string Issuer { get; set; }
    }
}