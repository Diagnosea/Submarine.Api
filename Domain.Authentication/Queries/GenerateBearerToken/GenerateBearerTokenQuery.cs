using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Domain.User.Enums;
using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken
{
    public class GenerateBearerTokenQuery : IRequest<string>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Audience { get; set; }
        public IEnumerable<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}