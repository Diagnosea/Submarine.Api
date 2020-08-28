using System.Collections.Generic;
using Diagnosea.Submarine.Api.Abstractions.Settings;

namespace Diagnosea.Submarine.Api.Options
{
    public class ApplicationOptions : ISubmarineAuthenticationSettings
    {
        public string PathBase { get; set; }
        public string PrivateSigningKey { get; set; }
        public List<string> Issuers { get; set; }
        public List<string> Audiences { get; set; }
        public string SubmarineConnectionString { get; set; }
    }
}