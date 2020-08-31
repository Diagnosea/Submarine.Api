namespace Diagnosea.Submarine.Abstractions.Exceptions
{
    public class SubmarineEntityNotFoundException : SubmarineException, ISubmarineException
    {
        public SubmarineEntityNotFoundException(string technicalMessage, string userMessage)
            : base(SubmarineExceptionCode.EntityNotFound, technicalMessage, userMessage)
        {
        }
    }
}