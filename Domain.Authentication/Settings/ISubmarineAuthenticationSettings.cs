using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.Authentication.Settings
{
    public interface ISubmarineAuthenticationSettings
    { 
        string Secret { get; set; }
        int ExpirationInDays { get; set; }
        IList<string> ValidAudiences { get; set; }
        string Issuer { get; set; }
    }
}