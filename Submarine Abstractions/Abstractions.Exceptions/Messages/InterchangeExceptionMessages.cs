namespace Abstractions.Exceptions.Messages
{
    public static class InterchangeExceptionMessages
    {
        private const string Prefix = "RequestValidation";

        public static readonly string Required = $"{Prefix}|{ExceptionMessages.Separator}Required";
        public static readonly string InvalidEmailAddress = $"{Prefix}{ExceptionMessages.Separator}InvalidEmailAddress";
    }
}