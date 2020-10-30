namespace Abstractions.Exceptions
{
    public static class LicenseExceptionMessages
    {
        private const string Prefix = "License";
        
        public static readonly string NoLicenseWithId = $"{Prefix}{ExceptionMessages.Separator}{nameof(NoLicenseWithId)}";
        public static readonly string NoLicenseWithUserId = $"{Prefix}{ExceptionMessages.Separator}{nameof(NoLicenseWithUserId)}";
    }
}