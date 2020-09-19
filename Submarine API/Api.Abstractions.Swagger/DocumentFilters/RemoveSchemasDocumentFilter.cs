using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.DocumentFilters
{
    public class RemoveSchemasDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            while (swaggerDoc.Components.Schemas.Any())
            {
                swaggerDoc.Components.Schemas.Remove(swaggerDoc.Components.Schemas.First());
            }
        }
    }
}