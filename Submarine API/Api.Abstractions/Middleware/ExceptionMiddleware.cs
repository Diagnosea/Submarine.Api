using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Interchange.Responses;
using Microsoft.AspNetCore.Http;

namespace Diagnosea.Submarine.Api.Abstractions.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IDictionary<Type, HttpStatusCode> _statusCodes;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ExceptionMiddleware(RequestDelegate requestDelegate, IDictionary<Type, HttpStatusCode> statusCodes)
        {
            _requestDelegate = requestDelegate;
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
                await _requestDelegate(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
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
                var response = new ExceptionResponse
                {
                    ExceptionCode = submarineException.ExceptionCode,
                    TechnicalMessage = submarineException.TechnicalMessage,
                    UserMessage = submarineException.UserMessage
                };
                
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