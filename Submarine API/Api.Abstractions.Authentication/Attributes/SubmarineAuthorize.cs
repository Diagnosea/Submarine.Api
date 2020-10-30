using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Abstractions.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Diagnosea.Submarine.Api.Abstractions.Authentication.Attributes
{
    public class SubmarineAuthorize : AuthorizeAttribute
    {
        public SubmarineAuthorize()
        {
        }
        
        public SubmarineAuthorize(params UserRole[] roles)
        {
            var givenRoles = roles.AsStrings();
            givenRoles.Add(UserRole.Administrator.ToString());
            
            Roles = string.Join(", ", givenRoles);
        }
    }
}