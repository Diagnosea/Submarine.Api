using System.Collections.Generic;

namespace Diagnosea.Submarine.Api.Abstractions.Settings
{
    public interface ISubmarineAuthenticationSettings
    {
        string PathBase { get; set; }
        string PrivateSigningKey { get; set; }
        List<string> Issuers { get; set; }
        List<string> Audiences { get; set; }
        string SubmarineConnectionString { get; set; }
    }
}
