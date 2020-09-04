using Diagnosea.Submarine.Domain.Abstractions;

namespace Diagnosea.Submarine.Domain.User
{
    public static class UserExceptionMessages
    {
        private const string Prefix = "User";

        public static readonly string UserNotFound = $"{Prefix}{ExceptionMessages.Separator}UserNotFound";
    }
}