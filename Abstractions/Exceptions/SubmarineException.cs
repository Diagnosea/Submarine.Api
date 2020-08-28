using System;

namespace Diagnosea.Submarine.Abstractions.Exceptions
{
    public class SubmarineException : Exception
    {
        public int ErrorCode { get; set; }
        public string TechnicalMessage { get; set; }
        public string UserMessage { get; set; }
        
        public override string Message => !string.IsNullOrEmpty(UserMessage) ? UserMessage : TechnicalMessage;
        
        public SubmarineException(SubmarineExceptionCode code, string technicalMessage, string userMessage)
        {
            ErrorCode = (int) code;
            TechnicalMessage = technicalMessage;
            UserMessage = userMessage;
        }
    }
}