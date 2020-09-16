using System.ComponentModel.DataAnnotations;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Abstractions.Interchange.Attributes
{
    public class SubmarineRequiredAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
            => ErrorMessage ?? InterchangeExceptionMessages.Required;
    }
}