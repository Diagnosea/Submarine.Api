using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Diagnosea.TestPack
{
    public static class DiagnoseaAssert
    {
        public static void That(DateTime? result, EqualConstraint constraint) => Assert.That(result, constraint.Within(TimeSpan.FromSeconds(1)));

        /// <summary>
        /// Test an error exists under a field within model state.
        /// </summary>
        public static void ForNestedValue(IDictionary<string, string[]> modelStateErrors, string modelFieldName, string errorMessage)
        {
            Assert.That(modelStateErrors.ContainsKey(modelFieldName));

            var modelStateFieldErrors = modelStateErrors[modelFieldName];
            CollectionAssert.IsNotEmpty(modelStateFieldErrors);

            var modelStateFieldError = modelStateFieldErrors.FirstOrDefault(error => error == errorMessage);
            Assert.NotNull(modelStateFieldError);
        }
    }
}