using System;
using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.License.Dtos
{
    public class CreateLicenseDto
    {
        public Guid UserId { get; set; }
        public IList<CreateLicenseProductDto> Products { get; set; } = new List<CreateLicenseProductDto>();
    }
}