using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Diagnosea.TestPack
{
    public static class DiagnoseaAssert
    {
        public static void That(DateTime? result, EqualConstraint constraint) => Assert.That(result, constraint.Within(1).Seconds);

        /// <summary>
        /// Test an error exists under a field within model state directly. E.g. object.field
        /// </summary>
        public static void Contains(IDictionary<string, string[]> dictionary, string key, string value)
        {
            var correctedKey = CorrectKeyFieldName(key);
            
            ContainsKeyedValue(dictionary, correctedKey, value);
        }

        /// <summary>
        /// Test an error exists under a nested array field within model state. E.g. object[1].field
        /// </summary>
        public static void Contains(IDictionary<string, string[]> dictionary, string keyNestingObjectName, int keyNestingIndex, string keyNestingFieldName, string value)
        {
            var correctedKeyNestingFieldName = CorrectKeyFieldName(keyNestingObjectName);
            var correctedKey = $"{correctedKeyNestingFieldName}[{keyNestingIndex}].{keyNestingFieldName}";

            ContainsKeyedValue(dictionary, correctedKey, value);
        }

        private static string CorrectKeyFieldName(string keyNestingFieldName)
            => char.ToLowerInvariant(keyNestingFieldName[0]) + keyNestingFieldName.Substring(1);

        private static void ContainsKeyedValue(IDictionary<string, string[]> dictionary, string key, string value)
        {
            Assert.That(dictionary.ContainsKey(key));

            var nestedValues = dictionary[key];
            CollectionAssert.IsNotEmpty(nestedValues);

            var nestedValue = nestedValues.FirstOrDefault(nv => nv == value);
            Assert.That(nestedValue, Is.Not.Null);
        }
    }
}