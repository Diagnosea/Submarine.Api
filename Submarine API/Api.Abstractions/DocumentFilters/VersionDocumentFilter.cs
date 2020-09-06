using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Diagnosea.Submarine.Api.Abstractions.DocumentFilters
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
            var versionModel = actionDescriptor?.GetApiVersionModel();
            
            // If there are any versions specifically declared in an attribute against this action.
            if (versionModel != null && versionModel.DeclaredApiVersions.Any())
            {
                var declaredVersion = versionModel.DeclaredApiVersions.FirstOrDefault();

                return declaredVersion.ToString();
            }

            // If there are any versions implemented by this action.
            if (versionModel != null && versionModel.ImplementedApiVersions.Any())
            {
                var implementedVersion = versionModel.ImplementedApiVersions.OrderByDescending(v => v).FirstOrDefault();

                return implementedVersion.ToString();
            }

            throw new ArgumentException($"No Version Found for {actionDescriptor.DisplayName}");
        }
    }
}