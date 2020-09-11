using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Diagnosea.Submarine.Api.Abstractions.DocumentFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using NUnit.Framework;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Diagnosea.Submarine.Api.Abstractions.UnitTests.DocumentFilters
{
    [TestFixture]
    public class VersionDocumentFilterTests
    {
        private VersionDocumentFilter _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new VersionDocumentFilter();
        }
        
        public class Apply : VersionDocumentFilterTests
        {
            [Test]
            public void GivenSwaggerDocumentWithoutApiVersions_ThrowsException()
            {
                // Arrange
                const string path = "/v{version}/book";

                var document = GetOpenApiDocument(path);
                
                var apiVersionModel = new ApiVersionModel(
                    new List<ApiVersion>(),
                    new List<ApiVersion>(),
                    new List<ApiVersion>(),
                    new List<ApiVersion>(),
                    new List<ApiVersion>());
                
                var documentFilterContext = GetDocumentFilterContext(apiVersionModel);
                
                // Act & Assert
                Assert.Multiple(() =>
                {
                    var exception = Assert.Throws<ArgumentException>(() => _classUnderTest.Apply(document, documentFilterContext));

                    Assert.That(exception.Message, Is.Not.Null);
                });
            }
            
            [Test]
            public void GivenSwaggerDocumentWithDeclaredApiVersions_CorrectsVersion()
            {
                // Arrange
                const string path = "/v{version}/book";

                var document = GetOpenApiDocument(path);
                
                var apiVersionModel = new ApiVersionModel(
                    GetApiVersions(),
                    new List<ApiVersion>(),
                    new List<ApiVersion>(),
                    new List<ApiVersion>(),
                    new List<ApiVersion>());
                
                var documentFilterContext = GetDocumentFilterContext(apiVersionModel);
                
                // Act
                _classUnderTest.Apply(document, documentFilterContext);
                
                // Assert
                var indexedPath = document.Paths.FirstOrDefault();
                
                Assert.That(indexedPath.Key, Is.EqualTo("/v1.0/book"));
            }

            [Test]
            public void GivenSwaggerDocumentWithImplementedApiVersions_CorrectsVersion()
            {
                // Arrange
                const string path = "/v{version}/book";

                var document = GetOpenApiDocument(path);
                
                var apiVersionModel = new ApiVersionModel(
                    new List<ApiVersion>(), 
                    GetApiVersions(),
                    new List<ApiVersion>(),
                    new List<ApiVersion>(),
                    new List<ApiVersion>());
                
                var documentFilterContext = GetDocumentFilterContext(apiVersionModel);
                
                // Act
                _classUnderTest.Apply(document, documentFilterContext);
                
                // Assert
                var indexedPath = document.Paths.FirstOrDefault();
                
                Assert.That(indexedPath.Key, Is.EqualTo("/v1.0/book"));
            }
            
            private static OpenApiDocument GetOpenApiDocument(string path)
            {
                return new OpenApiDocument
                {
                    Paths = new OpenApiPaths
                    {
                        {path, new OpenApiPathItem()}
                    }
                };
            }

            private static DocumentFilterContext GetDocumentFilterContext(ApiVersionModel versionModel)
            {
                var actionDescriptor = new ActionDescriptor();
                actionDescriptor.SetProperty(versionModel);

                var apiDescription = new ApiDescription
                {
                    ActionDescriptor = actionDescriptor
                };
                
                var apiDescriptions = new List<ApiDescription>
                {
                    apiDescription
                };

                var schemaGeneratorOptions = new SchemaGeneratorOptions();
                
                var jsonSerializerOptions = new JsonSerializerOptions();
                var jsonSerializerDataContractResolver = new JsonSerializerDataContractResolver(jsonSerializerOptions);
                
                var schemaGenerator = new SchemaGenerator(schemaGeneratorOptions, jsonSerializerDataContractResolver);

                var schemaRepository = new SchemaRepository();
                
                return new DocumentFilterContext(apiDescriptions, schemaGenerator, schemaRepository);
            }

            private static IEnumerable<ApiVersion> GetApiVersions()
            {
                var version = new ApiVersion(1, 0);
                
                return new List<ApiVersion>
                {
                    version
                };
            }
        }
    }
}