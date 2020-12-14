namespace Abstractions.Exceptions
{
    public class TankExceptionMessages
    {
        private const string Prefix = "Tank";
        
        public static readonly string NoTanksWithUserId = $"{Prefix}{ExceptionMessages.Separator}{nameof(NoTanksWithUserId)}";
    }
}