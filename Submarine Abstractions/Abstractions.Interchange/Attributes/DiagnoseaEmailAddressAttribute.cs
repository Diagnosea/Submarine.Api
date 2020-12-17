using System.ComponentModel.DataAnnotations;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Abstractions.Interchange.Attributes
{
    public class DiagnoseaEmailAddressAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
         => new EmailAddressAttribute().IsValid(value);

        public override string FormatErrorMessage(string name)
            => ErrorMessage ?? ExceptionMessages.Interchange.InvalidEmailAddress;
    }
}