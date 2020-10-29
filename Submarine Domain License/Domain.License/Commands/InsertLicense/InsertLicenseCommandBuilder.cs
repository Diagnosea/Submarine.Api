using System;

namespace Diagnosea.Submarine.Domain.License.Commands.InsertLicense
{
    public class InsertLicenseCommandBuilder
    {
        private Guid _id;
        private string _key;
        private DateTime _created;
        private Guid _userId;

        public InsertLicenseCommandBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public InsertLicenseCommandBuilder WithKey(string key)
        {
            _key = key;
            return this;
        }

        public InsertLicenseCommandBuilder WithCreated(DateTime created)
        {
            _created = created;
            return this;
        }

        public InsertLicenseCommandBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public InsertLicenseCommand Build()
        {
            return new InsertLicenseCommand
            {
                Id = _id,
                Key = _key,
                Created = _created,
                UserId = _userId
            };
        }
    }
}