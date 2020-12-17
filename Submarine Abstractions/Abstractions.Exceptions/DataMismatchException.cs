namespace Abstractions.Exceptions
{
    public class DataMismatchException : DiagnoseaException, IDiagnoseaException
    {
        public DataMismatchException(string technicalMessage, string userMessage) 
            : base(Exceptions.ExceptionCode.DataMismatchException, technicalMessage, userMessage)
        {
        }
    }
}