namespace Abstractions.Exceptions
{
    public class EntityNotFoundException : DiagnoseaException, IDiagnoseaException
    {
        public EntityNotFoundException(string technicalMessage, string userMessage)
            : base(Exceptions.ExceptionCode.EntityNotFound, technicalMessage, userMessage)
        {
        }
    }
}