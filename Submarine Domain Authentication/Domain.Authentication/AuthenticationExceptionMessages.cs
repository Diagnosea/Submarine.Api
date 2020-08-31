namespace Diagnosea.Submarine.Domain.Authentication
{
    public static class AuthenticationExceptionMessages
    {
        private const string Prefix = "Authentication";

        public static readonly string InvalidAudience = $"{Prefix}:InvalidAudience";
        public static readonly string InvalidPassword = $"{Prefix}:InvalidPassword";
    }
}