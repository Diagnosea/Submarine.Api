using System.ComponentModel.DataAnnotations;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Abstractions.Interchange.Attributes
{
    public class DiagnoseaStringLengthAttribute : StringLengthAttribute
    {
        public DiagnoseaStringLengthAttribute(int maximumLength = 1) : base(maximumLength)
        {
        }

        public override string FormatErrorMessage(string name)
            => ErrorMessage ?? $"{ExceptionMessages.Interchange.InvalidStringLength}|{MinimumLength}|{MaximumLength}";
    }
}