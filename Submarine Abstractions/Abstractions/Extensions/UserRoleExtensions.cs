using System.Collections.Generic;
using System.Linq;
using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Abstractions.Extensions
{
    public static class UserRoleExtensions
    {
        public static IList<string> AsStrings(this IEnumerable<UserRole> roles) 
            => roles.Select(x => x.ToString()).ToList();
    }
}