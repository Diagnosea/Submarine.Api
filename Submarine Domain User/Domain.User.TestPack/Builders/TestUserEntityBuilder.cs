using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.User.Entities;

namespace Diagnosea.Submarine.Domain.User.TestPack.Builders
{
    public class TestUserEntityBuilder
    {
        private Guid? _id;
        private string _emailAddress;
        private string _password;
        private readonly IList<UserRole> _roles = new List<UserRole>();

        public TestUserEntityBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public TestUserEntityBuilder WithEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public TestUserEntityBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public TestUserEntityBuilder WithRole(UserRole role)
        {
            _roles.Add(role);
            return this;
        }
        
        public UserEntity Build()
        {
            return new UserEntity
            {
                Id = _id ?? Guid.NewGuid(),
                EmailAddress = _emailAddress ?? "This is an email address",
                Password = _password ?? "This is a password",
                Roles = _roles
            };
        }
    }
}