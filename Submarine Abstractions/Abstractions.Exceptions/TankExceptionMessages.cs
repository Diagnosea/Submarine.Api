namespace Abstractions.Exceptions
{
    public class TankExceptionMessages
    {
        private const string Prefix = "Tank";
        
        public static readonly string NoTankWithUserId = $"{Prefix}{ExceptionMessages.Separator}{nameof(NoTankWithUserId)}";
    }
}