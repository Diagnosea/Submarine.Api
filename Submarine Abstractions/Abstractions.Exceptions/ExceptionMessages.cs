namespace Abstractions.Exceptions
{
    public static class ExceptionMessages
    {
        public const string Separator = ":";
        
        public class Mapping
        {
            private const string Prefix = "Mapping";

            public static readonly string Failed = $"{Prefix}{Separator}Failed";
        }
        
        public static class Interchange
        {
            public const string Required = "RequestValidation:Required";
            public const string InvalidEmailAddress = "RequestValidation:InvalidEmailAddress";
            public const string InvalidDateAfterNow = "RequestValidation:InvalidDateAfterNow";
            public const string InvalidStringLength = "RequestValidation:InvalidStringLength";
        }
        
        public static class Authentication
        {
            private const string Prefix = "Authentication";
            
            public static readonly string PasswordIsIncorrect = $"{Prefix}{Separator}InvalidPassword";
        }
        
        public static class User
        {
            private const string Prefix = "User";

            public static readonly string UserNotFound = $"{Prefix}{Separator}{nameof(UserNotFound)}";
            public static readonly string UserExistsWithEmail = $"{Prefix}{Separator}{nameof(UserExistsWithEmail)}";
        }
        
        public static class License
        {
            private const string Prefix = "License";
        
            public static readonly string NoLicenseWithId = $"{Prefix}{Separator}{nameof(NoLicenseWithId)}";
            public static readonly string NoLicenseWithUserId = $"{Prefix}{Separator}{nameof(NoLicenseWithUserId)}";
        }

        public static class Tank
        {
            private const string Prefix = "Tank";
        
            public static readonly string NoTanksWithUserId = $"{Prefix}{Separator}{nameof(NoTanksWithUserId)}";
        }
    }
}