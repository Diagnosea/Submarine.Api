using System;
using Diagnosea.Submarine.Domain.License.Dtos;

namespace Diagnosea.Submarine.Domain.License.TestPack.Builders
{
    public class TestCreateLicenseProductDtoBuilder
    {
        private string _name;
        private DateTime? _expiration;

        public TestCreateLicenseProductDtoBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public TestCreateLicenseProductDtoBuilder WithExpiration(DateTime expiration)
        {
            _expiration = expiration;
            return this;
        }

        public CreateLicenseProductDto Build()
        {
            return new CreateLicenseProductDto()
            {
                Name = _name,
                Expiration = _expiration ?? DateTime.UtcNow
            };
        }
    }
}