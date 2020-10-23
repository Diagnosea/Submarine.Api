using System.Collections.Generic;
using Diagnosea.Submarine.Domain.License;

namespace Diagnosea.Domain.Instructors.UnitTests
{
    public class TestLicenseSettings : ILicenseSettings
    {
        public IList<string> AvailableProducts { get; set; } = new List<string>
        {
            "Submarine",
            "SubmarinePremium"
        };
    }
}