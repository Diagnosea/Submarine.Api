using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Diagnosea.Submarine.Api.Abstractions.Extensions
{
    public static class AppExtensions
    {
        public static void AddSwagger(this IApplicationBuilder app, string pathBase)
        {
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer
                        {
                            Url = $"https://{httpReq.Host.Value}{pathBase}"
                        }
                    };
                });
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint($"{pathBase}/swagger/v1/swagger.json", "Submarine.API"); });
        }
    }
}