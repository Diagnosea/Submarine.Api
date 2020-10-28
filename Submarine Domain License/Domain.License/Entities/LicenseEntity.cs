using System;
using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.License.Entities
{
    public class LicenseEntity
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public DateTime Created { get; set; }
        public Guid UserId { get; set; }
        public IList<LicenseProductEntity> Products { get; set; } = new List<LicenseProductEntity>();
    }
}