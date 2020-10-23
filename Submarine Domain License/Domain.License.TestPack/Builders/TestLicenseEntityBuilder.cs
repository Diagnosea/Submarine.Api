using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Domain.License.Entities;

namespace Diagnosea.Submarine.Domain.License.TestPack.Builders
{
    public class TestLicenseEntityBuilder
    {
        private Guid _id;
        private Guid _userId;
        private string _key;
        private readonly IList<LicenseProductEntity> _products = new List<LicenseProductEntity>();

        public TestLicenseEntityBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public TestLicenseEntityBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public TestLicenseEntityBuilder WithKey(string key)
        {
            _key = key;
            return this;
        }

        public TestLicenseEntityBuilder WithProduct(LicenseProductEntity product)
        {
            _products.Add(product);
            return this;
        }
        
        public LicenseEntity Build()
        {
            return new LicenseEntity
            {
                Id = _id,
                UserId = _userId,
                Key = _key,
                Products = _products
            };
        }
    }
}