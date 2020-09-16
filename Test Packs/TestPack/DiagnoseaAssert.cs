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
        public static void Contains(IDictionary<string, string[]> dictionary, string key, string value)
        {
            Assert.That(dictionary.ContainsKey(key));

            var nestedValues = dictionary[key];
            CollectionAssert.IsNotEmpty(nestedValues);

            var nestedValue = nestedValues.FirstOrDefault(nv => nv == value);
            Assert.That(nestedValue, Is.Not.Null);
        }
    }
}