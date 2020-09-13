using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Diagnosea.Submarine.Api.Abstractions.DocumentFilters;
using Diagnosea.Submarine.Api.Abstractions.OperationFilters;
using Diagnosea.Submarine.Domain.Authentication;
using Diagnosea.Submarine.Domain.Authentication.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Diagnosea.Submarine.Api.Abstractions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineControllers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddRouting(options => options.LowercaseUrls = true);
            serviceCollection.AddControllers();
            serviceCollection.AddApiVersioning();
        }
        
        public static void AddSubmarineAuthentication(this IServiceCollection serviceCollection, ISubmarineAuthenticationSettings settings)
        {
            serviceCollection.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
                        ValidIssuers = new []{settings.Issuer}
                    };
                });
        }

        public static void AddSubmarineSwagger<T>(this IServiceCollection serviceCollection) where T : class
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
                c.OperationFilter<VersionOperationFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.ExampleFilters();
                c.CustomSchemaIds(x => x.FullName?.ToString());
            });

            serviceCollection.AddSwaggerExamplesFromAssemblyOf<T>();
        }
    }
}
