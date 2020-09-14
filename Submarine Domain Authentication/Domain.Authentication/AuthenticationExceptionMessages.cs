using Diagnosea.Submarine.Domain.Abstractions;

namespace Diagnosea.Submarine.Domain.Authentication
{
    public static class AuthenticationExceptionMessages
    {
        private const string Prefix = "Authentication";

        public static readonly string NoLicensesUnderUserWithId = $"{Prefix}{ExceptionMessages.Separator}InvalidProductKey";
        public static readonly string NoProductWithNameRequested = $"{Prefix}{ExceptionMessages.Separator}NoProductWithNameRequested";
        public static readonly string InvalidProductKeyForProduct = $"{Prefix}{ExceptionMessages.Separator}InvalidProductKeyForProduct";
        public static readonly string PasswordIsIncorrect = $"{Prefix}{ExceptionMessages.Separator}InvalidPassword";
    }
}