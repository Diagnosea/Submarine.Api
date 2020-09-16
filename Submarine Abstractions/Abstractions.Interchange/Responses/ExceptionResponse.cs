namespace Diagnosea.Submarine.Abstractions.Interchange.Responses
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
    }
}