using System;

namespace Abstractions.Exceptions
{
    public class DiagnoseaException : Exception
    {
        public int ExceptionCode { get; }
        public string TechnicalMessage { get; }
        public string UserMessage { get; }
        
        public override string Message => !string.IsNullOrEmpty(UserMessage) ? UserMessage : TechnicalMessage;

        public DiagnoseaException(ExceptionCode exceptionCode, string technicalMessage)
        {
            ExceptionCode = (int)exceptionCode;
            TechnicalMessage = technicalMessage;
        }
        
        public DiagnoseaException(ExceptionCode exceptionCode, string technicalMessage, string userMessage)
        {
            ExceptionCode = (int) exceptionCode;
            TechnicalMessage = technicalMessage;
            UserMessage = userMessage;
        }
    }
}