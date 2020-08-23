using System;
using System.Text;
using Diagnosea.Submarine.Domain.Authentication.Settings;

namespace Diagnosea.Submarine.Domain.Authentication.Extensions
{
    public static class SubmarineJwtSettingsExtensions
    {
        public static byte[] GetEncodedSecret(this ISubmarineJwtSettings submarineJwtSettings)
        {
            return Encoding.UTF8.GetBytes(submarineJwtSettings.Secret);
        }
        
        public static DateTime GetExpiration(this ISubmarineJwtSettings submarineJwtSettings)
        {
            return DateTime.UtcNow.AddDays(-submarineJwtSettings.ExpirationInDays);
        }
    }
}