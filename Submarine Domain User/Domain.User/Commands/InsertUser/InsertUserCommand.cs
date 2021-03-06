﻿using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums;
using MediatR;

namespace Diagnosea.Submarine.Domain.User.Commands.InsertUser
{
    /// <summary>
    /// Insert a user with authentication and permissions details.
    /// </summary>
    public class InsertUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FriendlyName { get; set; }
        public IList<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}