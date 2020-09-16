using System.ComponentModel.DataAnnotations;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Abstractions.Interchange.Attributes
{
    public class SubmarineEmailAddressAttribute : DataTypeAttribute
    {
        public SubmarineEmailAddressAttribute() : base(DataType.EmailAddress)
        {
        }

        public override string FormatErrorMessage(string name)
            => ErrorMessage ?? InterchangeExceptionMessages.InvalidEmailAddress;
    }
}