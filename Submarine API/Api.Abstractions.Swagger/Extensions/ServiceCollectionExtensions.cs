using System.Collections.Generic;
using Diagnosea.Submarine.Api.Abstractions.Swagger.DocumentFilters;
using Diagnosea.Submarine.Api.Abstractions.Swagger.OperationFilters;
using Diagnosea.Submarine.Domain.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineSwagger<TStartup>(this IServiceCollection serviceCollection) where TStartup : class
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Submarine.API",
                    Description = "The Submarine API"
                });
                c.AddSecurityDefinition(
                    SubmarineSecurityDefinitionNames.Bearer,
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    }
                );
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = SubmarineSecurityDefinitionNames.Bearer,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
                c.DocumentFilter<VersionDocumentFilter>();
                c.DocumentFilter<RemoveSchemasDocumentFilter>();
                c.OperationFilter<VersionOperationFilter>();
                c.ExampleFilters();
                c.CustomSchemaIds(x => x.FullName?.ToString());
            });

            serviceCollection.AddSwaggerExamplesFromAssemblyOf<TStartup>();
        }
    }
}
