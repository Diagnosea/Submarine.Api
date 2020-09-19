using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddSubmarineSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer
                        {
                            Url = $"https://{httpReq.Host.Value}"
                        }
                    };
                });
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.DefaultModelsExpandDepth(-1);
                c.DocumentTitle = "Diagnosea Submarine API";
                
                // // TODO: Store in settings as List<string>.
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Submarine API V1");
            });
        }
    }
}