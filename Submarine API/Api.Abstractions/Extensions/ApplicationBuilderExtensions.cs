using System;
using System.Collections.Generic;
using System.Net;
using Diagnosea.Submarine.Abstractions.Exceptions;
using Diagnosea.Submarine.Api.Abstractions.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Diagnosea.Submarine.Api.Abstractions.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static IDictionary<Type, HttpStatusCode> _exceptionMapping = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(SubmarineEntityNotFoundException), HttpStatusCode.NotFound }
        };
        
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
                c.DocumentTitle = "Diagnosea Submarine API";
                
                // // TODO: Store in settings as List<string>.
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Submarine API V1");
            });
        }

        public static void AddSubmarineExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>(_exceptionMapping);
        }
    }
}