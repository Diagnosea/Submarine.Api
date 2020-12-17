namespace Abstractions.Exceptions
{
    public interface IDiagnoseaException
    {
        int ExceptionCode { get; }
        string TechnicalMessage { get; }
        string UserMessage { get; }
    }
}