using System.Collections.Generic;
using Diagnosea.Submarine.Api.Abstractions.Swagger.DocumentFilters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Moq;
using NUnit.Framework;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.UnitTests.DocumentFilters
{
    [TestFixture]
    public class RemoveSchemasDocumentFilterTests
    {
        private RemoveSchemasDocumentFilter _classUnderTest;

        [OneTimeSetUp] 
        public void OneTimeSetUp()
        {
            _classUnderTest = new RemoveSchemasDocumentFilter();
        }
        
        public class Apply : RemoveSchemasDocumentFilterTests
        {
            [Test]
            public void GivenSwaggerDoc_RemovesAllSchemas()
            {
                // Arrange
                var document = GetOpenApiDocument();
                var documentFilterContext = GetDocumentFilterContext();
                
                // Act
                _classUnderTest.Apply(document, documentFilterContext);
                
                // Assert
                CollectionAssert.IsEmpty(document.Components.Schemas);
            }
            
            private static OpenApiDocument GetOpenApiDocument()
            {
                return new OpenApiDocument
                {
                    Components = new OpenApiComponents
                    {
                        Schemas = new Dictionary<string, OpenApiSchema>
                        {
                            { "schema", new OpenApiSchema()}
                        }
                    }
                };
            }
            
            private static DocumentFilterContext GetDocumentFilterContext()
            {
                return new DocumentFilterContext(
                    It.IsAny<ApiDescription[]>(), 
                    It.IsAny<SchemaGenerator>(), 
                    It.IsAny<SchemaRepository>());
            }
        }
    }
}