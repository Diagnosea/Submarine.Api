namespace Abstractions.Exceptions
{
    public class SubmarineMappingException : SubmarineException
    {
        public SubmarineMappingException(string technicalMessage, string userMessage) 
            : base(SubmarineExceptionCode.MappingException, technicalMessage, userMessage)
        {
        }
    }
}