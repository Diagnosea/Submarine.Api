using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Diagnosea.TestPack
{
    public static class DiagnoseaAssert
    {
        public static void That(DateTime? result, EqualConstraint constraint)
         => Assert.That(result, constraint.Within(TimeSpan.FromSeconds(1)));
    }
}