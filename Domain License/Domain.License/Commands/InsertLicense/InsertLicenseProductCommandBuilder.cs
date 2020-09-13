using System;

namespace Diagnosea.Submarine.Domain.License.Commands.InsertLicense
{
    public class InsertLicenseProductCommandBuilder
    {
        private string _title;
        private string _key;
        private DateTime? _expiration;

        public InsertLicenseProductCommandBuilder WithName(string name)
        {
            _title = name;
            return this;
        }

        public InsertLicenseProductCommandBuilder WithKey(string key)
        {
            _key = key;
            return this;
        }


        public InsertLicenseProductCommandBuilder WithExpiration(DateTime expiration)
        {
            _expiration = expiration;
            return this;
        }
        
        public InsertLicenseProductCommand Build()
        {
            return new InsertLicenseProductCommand
            {
                Name = _title,
                Key = _key,
                Expiration = _expiration
            };
        }
    }
}