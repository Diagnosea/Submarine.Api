using System;
using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.License.Dtos
{
    public class LicenseDto
    {
        public Guid Id { get; set; }
        public IList<LicenseProductDto> Products { get; set; } = new List<LicenseProductDto>();
    }
}