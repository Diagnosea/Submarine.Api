using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Diagnosea.Submarine.Domain.User.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FriendlyName { get; set; }
        
        [BsonRepresentation(BsonType.String)]
        public IList<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}