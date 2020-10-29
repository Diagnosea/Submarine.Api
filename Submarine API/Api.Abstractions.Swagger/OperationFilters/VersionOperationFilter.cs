using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.OperationFilters
{
    public class VersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.FirstOrDefault(x => x.Name == "version");

            if (versionParameter != null)
            {
                operation.Parameters.Remove(versionParameter);
            }

            var groupVersionParameter = operation.Parameters.FirstOrDefault(x => x.Name == "GroupVersion");

            if (groupVersionParameter != null)
            {
                operation.Parameters.Remove(groupVersionParameter);
            }
            
            var majorVersionParameter = operation.Parameters.FirstOrDefault(x => x.Name == "MajorVersion");

            if (majorVersionParameter != null)
            {
                operation.Parameters.Remove(majorVersionParameter);
            }
            
            var minorVersionParameter = operation.Parameters.FirstOrDefault(x => x.Name == "MinorVersion");

            if (minorVersionParameter != null)
            {
                operation.Parameters.Remove(minorVersionParameter);
            }
            
            var statusParameter = operation.Parameters.FirstOrDefault(x => x.Name == "Status");

            if (statusParameter != null)
            {
                operation.Parameters.Remove(statusParameter);
            }
        }
    }
}