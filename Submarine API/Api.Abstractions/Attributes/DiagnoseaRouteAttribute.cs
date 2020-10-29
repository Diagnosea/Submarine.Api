using Microsoft.AspNetCore.Mvc;

namespace Diagnosea.Submarine.Api.Abstractions.Attributes
{
    public class DiagnoseaRouteAttribute : RouteAttribute
    {
        private const string RouteVersionParameter = "v{version:apiVersion}";

        public DiagnoseaRouteAttribute(string template)
            : base($"{RouteVersionParameter}/{template}")
        {
        }
    }
}