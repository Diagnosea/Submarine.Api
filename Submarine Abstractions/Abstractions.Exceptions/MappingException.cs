namespace Abstractions.Exceptions
{
    public class MappingException : DiagnoseaException
    {
        public MappingException(string technicalMessage, string userMessage) 
            : base(Exceptions.ExceptionCode.MappingException, technicalMessage, userMessage)
        {
        }
    }
}