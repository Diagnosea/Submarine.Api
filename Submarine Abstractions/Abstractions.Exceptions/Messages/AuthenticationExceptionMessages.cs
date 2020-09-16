namespace Abstractions.Exceptions.Messages
{
    public static class AuthenticationExceptionMessages
    {
        private const string Prefix = "Authentication";

        public static readonly string NoLicensesUnderUserWithId = $"{Prefix}{ExceptionMessages.Separator}InvalidProductKey";
        public static readonly string PasswordIsIncorrect = $"{Prefix}{ExceptionMessages.Separator}InvalidPassword";
    }
}