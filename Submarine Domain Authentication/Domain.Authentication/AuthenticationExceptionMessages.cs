using Diagnosea.Submarine.Domain.Abstractions;

namespace Diagnosea.Submarine.Domain.Authentication
{
    public static class AuthenticationExceptionMessages
    {
        private const string Prefix = "Authentication";

        public static readonly string InvalidAudience = $"{Prefix}{ExceptionMessages.Separator}InvalidAudience";
        public static readonly string InvalidPassword = $"{Prefix}{ExceptionMessages.Separator}InvalidPassword";
    }
}