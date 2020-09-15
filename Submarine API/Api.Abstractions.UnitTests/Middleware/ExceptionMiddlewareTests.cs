using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Responses;
using Diagnosea.Submarine.Api.Abstractions.Middleware;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;

namespace Diagnosea.Submarine.Api.Abstractions.UnitTests.Middleware
{
    [TestFixture]
    public class ExceptionMiddlewareTests
    {
        private IDictionary<Type, HttpStatusCode> _exceptionMapping;
        private JsonSerializerOptions _jsonSerializerOptions;

        [SetUp]
        public void SetUp()
        {
            _exceptionMapping = new Dictionary<Type, HttpStatusCode>
            {
                {typeof(Exception), HttpStatusCode.BadRequest},
                {typeof(SubmarineTestException), HttpStatusCode.Conflict}
            };

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public class InvokeAsync : ExceptionMiddlewareTests
        {
            [Test]
            public async Task GivenRequestThrowsException_SetsErrorResponse()
            {
                // Arrange
                var exception = new KeyNotFoundException("Not a Submarine Exception");
                Task CurrentRequest(HttpContext c) => throw exception;
                var classUnderTest = new ExceptionMiddleware(CurrentRequest, _exceptionMapping);
                var context = GetDefaultHttpContext();
                
                // Act
                await classUnderTest.InvokeAsync(context);
                
                // Assert
                var message = await GetExceptionResponse(context);

                Assert.Multiple(() =>
                {
                    Assert.That(message.ExceptionCode, Is.EqualTo((int)SubmarineExceptionCode.Unknown));
                    Assert.That(message.TechnicalMessage, Is.EqualTo(exception.Message));
                });
            }

            [Test]
            public async Task GivenRequestThrowsCustomException_SetsErrorResponse()
            {
                // Arrange
                var exception = new SubmarineTestException(SubmarineExceptionCode.EntityNotFound, "TM", "UM");
                Task CurrentRequest(HttpContext c) => throw exception;
                var classUnderTest = new ExceptionMiddleware(CurrentRequest, _exceptionMapping);
                var context = GetDefaultHttpContext();
                
                // Act
                await classUnderTest.InvokeAsync(context);
                
                // Assert
                var message = await GetExceptionResponse(context);

                Assert.Multiple(() =>
                {
                    var statusCode = _exceptionMapping[typeof(SubmarineTestException)];
                    Assert.That(context.Response.StatusCode, Is.EqualTo((int)statusCode));
                    
                    Assert.That(message.ExceptionCode, Is.EqualTo(exception.ExceptionCode));
                    Assert.That(message.TechnicalMessage, Is.EqualTo(exception.TechnicalMessage));
                    Assert.That(message.UserMessage, Is.EqualTo(exception.UserMessage));
                });
            }

            [Test]
            public async Task GivenRequestThrowsCustomExceptionWithoutUSerMessage_SetsErrorResponse()
            {
                // Arrange
                var exception = new SubmarineTestException(SubmarineExceptionCode.EntityNotFound, "TM");
                Task CurrentRequest(HttpContext c) => throw exception;
                var classUnderTest = new ExceptionMiddleware(CurrentRequest, _exceptionMapping);
                var context = GetDefaultHttpContext();
                
                // Act
                await classUnderTest.InvokeAsync(context);
                
                // Assert
                var message = await GetExceptionResponse(context);
                
                Assert.Multiple(() =>
                {
                    var statusCode = _exceptionMapping[typeof(SubmarineTestException)];
                    Assert.That(context.Response.StatusCode, Is.EqualTo((int)statusCode));
                    
                    Assert.That(message.ExceptionCode, Is.EqualTo(exception.ExceptionCode));
                    Assert.That(message.TechnicalMessage, Is.EqualTo(exception.TechnicalMessage));
                    Assert.That(message.UserMessage, Is.Null);
                });
            }
            
            private async Task<ExceptionResponse> GetExceptionResponse(DefaultHttpContext context)
            {
                context.Response.Body.Seek(default(int), SeekOrigin.Begin);
                return await JsonSerializer.DeserializeAsync<ExceptionResponse>(context.Response.Body, _jsonSerializerOptions);
            }
        }

        private static DefaultHttpContext GetDefaultHttpContext()
        {
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            return context;
        }

        private class SubmarineTestException : SubmarineException, ISubmarineException
        {
            public SubmarineTestException(SubmarineExceptionCode exceptionCode, string technicalMessage) 
                : base(exceptionCode, technicalMessage)
            {
            }

            public SubmarineTestException(SubmarineExceptionCode exceptionCode, string technicalMessage, string userMessage) 
                : base(exceptionCode, technicalMessage, userMessage)
            {
            }
        }
    }
}