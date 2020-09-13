using System;
using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.License.Entities
{
    public class LicenseEntity
    {
        public Guid Id { get; set; }
        public IList<LicenseProductEntity> Products { get; set; } = new List<LicenseProductEntity>();
        public Guid UserId { get; set; }
    }
}