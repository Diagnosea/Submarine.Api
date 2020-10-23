namespace Abstractions.Exceptions
{
    public static class LicenseExceptionMessages
    {
        private const string Prefix = "License";
        
        public static readonly string InvalidProductName = $"{Prefix}{ExceptionMessages.Separator}InvalidProductName";
    }
}