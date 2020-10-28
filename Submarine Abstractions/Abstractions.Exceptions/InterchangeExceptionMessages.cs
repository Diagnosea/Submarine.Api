namespace Abstractions.Exceptions
{
    public static class InterchangeExceptionMessages
    {
        public const string Required = "RequestValidation:Required";
        public const string InvalidEmailAddress = "RequestValidation:InvalidEmailAddress";
        public const string InvalidDateAfterNow = "RequestValidation:InvalidDateBeforeNow";
        public const string InvalidStringLength = "RequestValidation:InvalidStringLength";
    }
}