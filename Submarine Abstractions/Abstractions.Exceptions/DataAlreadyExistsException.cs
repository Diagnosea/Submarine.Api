namespace Abstractions.Exceptions
{
    public class DataAlreadyExistsException : DiagnoseaException, IDiagnoseaException
    {
        public DataAlreadyExistsException(string technicalMessage, string userMessage) 
            : base(Exceptions.ExceptionCode.DataAlreadyExists, technicalMessage, userMessage)
        {
        }
    }
}