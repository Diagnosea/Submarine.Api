using Diagnosea.Submarine.Abstractions.Exceptions;

namespace Diagnosea.Submarine.Abstractions.Responses
{
    public class ExceptionResponse
    {
        /// <summary>
        /// Code of the exception that is searchable in wikis.
        /// </summary>
        public int? ExceptionCode { get; set;  }
        
        /// <summary>
        /// Message developer can use to look into issue.
        /// </summary>
        public string TechnicalMessage { get; set; }
        
        /// <summary>
        /// Localised message to help understand issue.
        /// </summary>
        public string UserMessage { get; set;  }

        public ExceptionResponse()
        {
        }
        
        public ExceptionResponse(int exceptionCode, string technicalMessage, string userMessage)
        {
            ExceptionCode = exceptionCode;
            TechnicalMessage = technicalMessage;
            UserMessage = userMessage;
        }

        public ExceptionResponse(ISubmarineException exception) : this(exception.ExceptionCode, exception.TechnicalMessage, exception.UserMessage)
        {
        }
    }
}