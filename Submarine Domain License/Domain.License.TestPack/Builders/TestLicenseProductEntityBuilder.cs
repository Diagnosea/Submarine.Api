using System;
using Diagnosea.Submarine.Domain.License.Entities;

namespace Diagnosea.Submarine.Domain.License.TestPack.Builders
{
    public class TestLicenseProductEntityBuilder
    {
        private string _name;
        private DateTime? _expiration;

        public TestLicenseProductEntityBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public TestLicenseProductEntityBuilder WithExpiration(DateTime expiration)
        {
            _expiration = expiration;
            return this;
        }
        
        public LicenseProductEntity Build()
        {
            return new LicenseProductEntity
            {
                Name = _name ?? "This is a product name",
                Expiration = _expiration ?? DateTime.UtcNow.AddDays(1)
            };
        }
    }
}