using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Domain.User.Enums;
using MediatR;

namespace Diagnosea.Submarine.Domain.User.Commands.InsertUser
{
    /// <summary>
    /// Insert a user with their basic information for authentication and permissions.
    /// </summary>
    public class InsertUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public IList<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}