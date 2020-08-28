using System.Collections.Generic;
using Diagnosea.Submarine.Domain.Authentication.Settings;

namespace Domain.Authentication.UnitTests.Settings
{
    internal class SubmarineTestAuthenticationSettings : ISubmarineAuthenticationSettings
    {
        public string Secret { get; set; }
        public int ExpirationInDays { get; set; }
        public IList<string> ValidAudiences { get; set; } = new List<string>();
        public string Issuer { get; set; } 
    }
}