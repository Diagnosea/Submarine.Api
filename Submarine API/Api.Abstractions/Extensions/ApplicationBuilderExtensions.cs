using System;
using System.Collections.Generic;
using System.Net;
using Abstractions.Exceptions;
using Diagnosea.Submarine.Api.Abstractions.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Diagnosea.Submarine.Api.Abstractions.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static readonly IDictionary<Type, HttpStatusCode> _exceptionMapping = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(EntityNotFoundException), HttpStatusCode.NotFound },
            { typeof(DataAlreadyExistsException), HttpStatusCode.Conflict },
            { typeof(DataMismatchException), HttpStatusCode.Conflict }
        };

        public static void AddSubmarineExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>(_exceptionMapping);
        }
    }
}