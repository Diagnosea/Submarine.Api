namespace Diagnosea.Submarine.Abstractions.Exceptions
{
    public class SubmarineArgumentException : SubmarineException, ISubmarineException
    {
        public SubmarineArgumentException(string technicalMessage, string userMessage) : base(
            SubmarineExceptionCode.ArgumentException, technicalMessage, userMessage)
        {
        }
    }
}