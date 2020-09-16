using System.ComponentModel.DataAnnotations;
using Abstractions.Exceptions.Messages;

namespace Diagnosea.Submarine.Abstractions.Interchange.Attributes
{
    public class SubmarineRequiredAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
            => ErrorMessage ?? InterchangeExceptionMessages.Required;
    }
}