using Abstractions.Exceptions.Messages;

namespace Abstractions.Exceptions
{
    public static class UserExceptionMessages
    {
        private const string Prefix = "User";

        public static readonly string UserNotFound = $"{Prefix}{ExceptionMessages.Separator}{nameof(UserNotFound)}";
        public static readonly string UserExistsWithEmail = $"{Prefix}{ExceptionMessages.Separator}{nameof(UserExistsWithEmail)}";
    }
}