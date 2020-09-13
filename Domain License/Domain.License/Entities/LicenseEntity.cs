using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Domain.License.Stubs;

namespace Diagnosea.Submarine.Domain.License.Entities
{
    public class LicenseEntity
    {
        public Guid Id { get; set; }
        
        public IList<LicenseProductStub> Products { get; set; } = new List<LicenseProductStub>();
        
        public Guid UserId { get; set; }
    }
}