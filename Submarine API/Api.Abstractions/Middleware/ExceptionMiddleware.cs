using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Responses;
using Microsoft.AspNetCore.Http;

namespace Diagnosea.Submarine.Api.Abstractions.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDictionary<Type, HttpStatusCode> _statusCodes;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ExceptionMiddleware(RequestDelegate next, IDictionary<Type, HttpStatusCode> statusCodes)
        {
            _next = next;
            _statusCodes = statusCodes;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
                
                // Reset stream.
                context.Response.Body.Seek(0, SeekOrigin.Begin);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var exceptionType = exception.GetType();
            var statusCode = _statusCodes.ContainsKey(exceptionType) ? _statusCodes[exceptionType] : HttpStatusCode.InternalServerError;
            
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int) statusCode;

            if (exception is ISubmarineException submarineException)
            {
                var response = new ExceptionResponse(submarineException);
                var serialization = JsonSerializer.Serialize(response, _jsonSerializerOptions);
                return context.Response.WriteAsync(serialization);
            }
            
            var fallbackResponse = new ExceptionResponse
            {
                ExceptionCode = (int) SubmarineExceptionCode.Unknown,
                TechnicalMessage = exception.Message
            };

            var fallbackSerialization = JsonSerializer.Serialize(fallbackResponse, _jsonSerializerOptions);
            return context.Response.WriteAsync(fallbackSerialization);
        }
    }
}