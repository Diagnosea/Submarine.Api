using System;
using System.Linq;
using System.Security.Claims;

namespace Diagnosea.Submarine.Api.Abstractions.Authentication.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
        {
            var subjectClaim = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            return subjectClaim != null ? Guid.Parse(subjectClaim.Value) : Guid.Empty;
        }
    }
}