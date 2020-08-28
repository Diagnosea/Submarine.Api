namespace Diagnosea.Submarine.Abstractions.Exceptions
{
    public interface ISubmarineException
    {
        int ErrorCode { get; set; }
        string TechnicalMessage { get; set; }
        string UserMessage { get; set; }
    }
}