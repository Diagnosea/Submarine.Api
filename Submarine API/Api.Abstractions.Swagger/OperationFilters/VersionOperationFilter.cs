using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.OperationFilters
{
    public class VersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parameter = operation.Parameters.FirstOrDefault(x => x.Name == "version");

            if (parameter != null)
            {
                operation.Parameters.Remove(parameter);
            }
        }
    }
}