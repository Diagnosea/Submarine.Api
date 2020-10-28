using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.User.Commands.InsertUser
{
    public class InsertUserCommandBuilder
    {
        private Guid _id;
        private string _emailAddress;
        private string _password;
        private string _userName;
        private string _friendlyName;
        private readonly IList<UserRole> _roles = new List<UserRole>();

        public InsertUserCommandBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }
        
        public InsertUserCommandBuilder WithEmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public InsertUserCommandBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public InsertUserCommandBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }
        
        public InsertUserCommandBuilder WithFriendlyName(string friendlyName)
        {
            _friendlyName = friendlyName;
            return this;
        }

        public InsertUserCommandBuilder WithRole(UserRole role)
        {
            _roles.Add(role);
            return this;
        }

        public InsertUserCommand Build()
        {
            return new InsertUserCommand
            {
                Id = _id,
                EmailAddress = _emailAddress,
                Password = _password,
                UserName = _userName,
                FriendlyName = _friendlyName,
                Roles = _roles
            };
        }
        
    }
}