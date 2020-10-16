using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Abstractions.Interchange.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Diagnosea.Submarine.Api.Abstractions.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IDictionary<Type, HttpStatusCode> _statusCodes;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ExceptionMiddleware(RequestDelegate requestDelegate, IDictionary<Type, HttpStatusCode> statusCodes)
        {
            _requestDelegate = requestDelegate;
            _statusCodes = statusCodes;

            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()    
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
                
                var serialization = JsonConvert.SerializeObject(response, _jsonSerializerSettings);
                return context.Response.WriteAsync(serialization);
            }
            
            var fallbackResponse = new ExceptionResponse
            {
                ExceptionCode = (int) SubmarineExceptionCode.Unknown,
                TechnicalMessage = exception.Message
            };

            var fallbackSerialization = JsonConvert.SerializeObject(fallbackResponse, _jsonSerializerSettings);
            return context.Response.WriteAsync(fallbackSerialization);
        }
    }
}