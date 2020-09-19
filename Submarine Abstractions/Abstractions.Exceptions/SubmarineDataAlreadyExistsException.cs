namespace Abstractions.Exceptions
{
    public class SubmarineDataAlreadyExistsException : SubmarineException, ISubmarineException
    {
        public SubmarineDataAlreadyExistsException(string technicalMessage, string userMessage) 
            : base(SubmarineExceptionCode.DataAlreadyExists, technicalMessage, userMessage)
        {
        }
    }
}