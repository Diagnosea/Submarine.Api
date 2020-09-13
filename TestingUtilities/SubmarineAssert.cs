using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Diagnosea.Submarine.TestingUtilities
{
    public static class SubmarineAssert
    {
        public static void That(DateTime? result, EqualConstraint constraint)
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, constraint.Within(TimeSpan.FromMilliseconds(1)));
        }
    }
}