using System.Collections.Generic;
using System.Linq;
using Diagnosea.Submarine.Domain.User.Enums;

namespace Diagnosea.Submarine.Domain.User.Extensions
{
    public static class UserRoleExtensions
    {
        public static IList<string> ToPlainText(this IList<UserRole> roles)
        {
            return roles.Select(x => x.ToString()).ToList();
        }
    }
}