using System.Collections.Generic;

namespace Diagnosea.Submarine.Abstractions.Interchange.Responses
{
    /// <summary>
    /// This response resembles .NET Core 3.1's Model State.
    /// </summary>
    public class ValidationResponse
    {
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}