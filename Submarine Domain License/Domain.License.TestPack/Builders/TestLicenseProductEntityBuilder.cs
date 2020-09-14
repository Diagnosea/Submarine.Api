using System;
using Diagnosea.Submarine.Domain.License.Entities;

namespace Domain.License.TestPack.Builders
{
    public class TestLicenseProductEntityBuilder
    {
        private string _name;
        private string _key;
        private DateTime? _expiration;

        public TestLicenseProductEntityBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public TestLicenseProductEntityBuilder WithKey(string key)
        {
            _key = key;
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
                Key = _key ?? "This is a product key",
                Expiration = _expiration ?? DateTime.UtcNow.AddDays(1)
            };
        }
    }
}