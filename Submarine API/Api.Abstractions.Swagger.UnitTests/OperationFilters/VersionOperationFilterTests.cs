using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Diagnosea.Submarine.Api.Abstractions.Swagger.OperationFilters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Moq;
using NUnit.Framework;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Diagnosea.Submarine.Api.Abstractions.Swagger.UnitTests.OperationFilters
{
    [TestFixture]
    public class VersionOperationFilterTests
    {
        private VersionOperationFilter _classUnderTest;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _classUnderTest = new VersionOperationFilter();
        }

        public class Apply : VersionOperationFilterTests
        {
            [Test]
            public void GivenNoVersionParameter_DoesNotTouchParameters()
            {
                var operation = new OpenApiOperation
                {
                    Parameters = new List<OpenApiParameter>
                    {
                        new OpenApiParameter
                        {
                            Name = "not version"
                        },
                        new OpenApiParameter
                        {
                            Name = "also not version"
                        }
                    }
                };

                var context = GetOperationFilterContext();
                
                // aCT
                _classUnderTest.Apply(operation, context);
                
                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(operation.Parameters.Any(x => x.Name == "not version"));
                    Assert.That(operation.Parameters.Any(x => x.Name == "also not version"));
                });
            }
            
            [Test]
            public void GivenVersionParameter_RemovesVersionParameter()
            {
                // Assert
                var operation = new OpenApiOperation
                {
                    Parameters = new List<OpenApiParameter>
                    {
                        new OpenApiParameter
                        {
                            Name = "version"
                        }
                    }
                };

                var context = GetOperationFilterContext();
                
                // Act
                _classUnderTest.Apply(operation, context);
                
                // Assert
                Assert.That(operation.Parameters.All(x => x.Name != "version"));
            }
            
            private static OperationFilterContext GetOperationFilterContext()
            {
                return new OperationFilterContext(
                    It.IsAny<ApiDescription>(), 
                    It.IsAny<SchemaGenerator>(), 
                    It.IsAny<SchemaRepository>(), 
                    It.IsAny<MethodInfo>());
            }
        }
    }
}