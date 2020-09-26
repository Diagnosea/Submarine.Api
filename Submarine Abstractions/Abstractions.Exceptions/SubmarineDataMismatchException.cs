namespace Abstractions.Exceptions
{
    public class SubmarineDataMismatchException : SubmarineException, ISubmarineException
    {
        public SubmarineDataMismatchException(string technicalMessage, string userMessage) 
            : base(SubmarineExceptionCode.DataMismatchException, technicalMessage, userMessage)
        {
        }
    }
}