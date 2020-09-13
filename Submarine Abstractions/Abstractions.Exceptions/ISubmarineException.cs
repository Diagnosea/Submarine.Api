namespace Diagnosea.Submarine.Abstractions.Exceptions
{
    public interface ISubmarineException
    {
        int ExceptionCode { get; }
        string TechnicalMessage { get; }
        string UserMessage { get; }
    }
}