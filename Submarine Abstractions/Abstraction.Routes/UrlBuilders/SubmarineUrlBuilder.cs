using System.Collections.Generic;

namespace Diagnosea.Submarine.Abstraction.Routes.UrlBuilders
{
    public abstract class SubmarineUrlBuilder
    {
        public readonly IList<string> _parts = new List<string>();

        protected SubmarineUrlBuilder(string version)
        {
            _parts.Add(version);
        }

        protected string GetConcatenatedUrlParts() => string.Join("/", _parts);
    }
}