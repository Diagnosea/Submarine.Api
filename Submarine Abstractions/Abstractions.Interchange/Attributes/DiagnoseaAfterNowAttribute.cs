using System;
using System.ComponentModel.DataAnnotations;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Abstractions.Interchange.Attributes
{
    public class DiagnoseaAfterNowAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is DateTime givenDate))
            {
                return false;
            }

            return givenDate > DateTime.UtcNow;
        }

        public override string FormatErrorMessage(string name)
            => ErrorMessage ?? InterchangeExceptionMessages.InvalidDateAfterNow;
    }
}