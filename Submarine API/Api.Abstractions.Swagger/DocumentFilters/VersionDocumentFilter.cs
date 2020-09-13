using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.DocumentFilters
{
    public class VersionDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var updatedApiPaths = new OpenApiPaths();
            var routeVersion = GetControllerVersion(context.ApiDescriptions);
            
            foreach (var documentedPath in swaggerDoc.Paths)
            {
                var correctedPath = documentedPath.Key.Replace("{version}", routeVersion);
                updatedApiPaths.Add(correctedPath, documentedPath.Value);
            }

            swaggerDoc.Paths = updatedApiPaths;
        }

        private static string GetControllerVersion(IEnumerable<ApiDescription> apiDescriptions)
        {
            // Get the version of the first controller action you can find.
            var actionDescriptor = apiDescriptions.FirstOrDefault()?.ActionDescriptor;
            var apiVersionModel = actionDescriptor?.GetApiVersionModel();
            
            // If there are any versions specifically declared in an attribute against this action.
            if (apiVersionModel != null && apiVersionModel.DeclaredApiVersions.Any())
            {
                var declaredApiVersion = apiVersionModel.DeclaredApiVersions.First();
                return declaredApiVersion.ToString();
            }

            // If there are any versions implemented by this action.
            if (apiVersionModel != null && apiVersionModel.ImplementedApiVersions.Any())
            {
                var implementedVersion = apiVersionModel.ImplementedApiVersions.First();
                return implementedVersion.ToString();
            }

            throw new ArgumentException($"No Version Found for {actionDescriptor.DisplayName}");
        }
    }
}