using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.User.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FriendlyName { get; set; }
        public IList<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}