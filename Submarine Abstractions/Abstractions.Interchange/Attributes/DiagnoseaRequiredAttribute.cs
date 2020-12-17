using System;
using System.ComponentModel.DataAnnotations;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Abstractions.Interchange.Attributes
{
    public class DiagnoseaRequiredAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is Guid guidValue)
            {
                return base.IsValid(value) && guidValue != Guid.Empty;
            }
            
            return base.IsValid(value);
        }

        public override string FormatErrorMessage(string name)
            => ErrorMessage ?? ExceptionMessages.Interchange.Required;
    }
}