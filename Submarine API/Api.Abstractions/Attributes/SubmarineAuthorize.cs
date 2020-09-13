using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.User.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Diagnosea.Submarine.Api.Abstractions.Attributes
{
    public class SubmarineAuthorize : AuthorizeAttribute
    {
        public SubmarineAuthorize()
        {
        }
        
        public SubmarineAuthorize(params UserRole[] roles)
        {
            var givenRoles = roles.AsStrings();
            Roles = string.Join(", ", givenRoles);
        }
    }
}