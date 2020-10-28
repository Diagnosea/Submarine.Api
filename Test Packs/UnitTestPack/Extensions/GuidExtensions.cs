using System;

namespace Diagnosea.Submarine.UnitTestPack.Extensions
{
    public static class GuidExtensions
    {
        public static string ToBase64String(this Guid guid)
            =>  Convert.ToBase64String(guid.ToByteArray());
    }
}