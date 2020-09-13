using System;
using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.License.Commands.InsertLicense
{
    public class InsertLicenseCommandBuilder
    {
        private Guid _id;
        private Guid _userId;
        private readonly IList<InsertLicenseProductCommand> _products = new List<InsertLicenseProductCommand>();
        
        public InsertLicenseCommandBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public InsertLicenseCommandBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public InsertLicenseCommandBuilder WithProduct(InsertLicenseProductCommand command)
        {
            _products.Add(command);
            return this;
        }
        
        public InsertLicenseCommand Build()
        {
            return new InsertLicenseCommand
            {
                Id = _id,
                UserId = _userId,
                Products = _products
            };
        }
    }
}