using System.Collections.Generic;
using Diagnosea.Submarine.Domain.Authentication.Settings;

namespace Domain.Authentication.UnitTests.Settings
{
    internal class SubmarineTestJwtSettings : ISubmarineJwtSettings
    {
        public string Secret { get; set;  } = "thisIsATestSecret";
        public int ExpirationInDays { get; set; } = 1;
        public IList<string> Audiences { get; set; } = new List<string> { "test-audience" };
        public string Issuer { get; set; } = "test-issuer";
    }
}