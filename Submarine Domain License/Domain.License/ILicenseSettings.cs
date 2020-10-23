using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.License
{
    public interface ILicenseSettings
    {
        IList<string> AvailableProducts { get; set; }
    }
}