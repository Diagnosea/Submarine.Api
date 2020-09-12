using System;
using System.Collections.Generic;
using System.Net;
using Diagnosea.Submarine.Abstractions.Exceptions;
using Diagnosea.Submarine.Api.Abstractions.Extensions;
using Diagnosea.Submarine.Api.Abstractions.Middleware;
using Diagnosea.Submarine.Api.Settings;
using Diagnosea.Submarine.Domain.Authentication.Extensions;
using Diagnosea.Submarine.Domain.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Diagnosea.Submarine.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        private static IDictionary<Type, HttpStatusCode> _exceptionMapping = new Dictionary<Type, HttpStatusCode>
        {
            { typeof(SubmarineEntityNotFoundException), HttpStatusCode.NotFound }
        };

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var databaseSettings = _configuration.GetSettings<DatabaseSettings>();
            var authenticationSettings = _configuration.GetSettings<AuthenticationSettings>();
            
            services.AddSubmarineAuthenticationSettings(authenticationSettings);
            services.AddSubmarineMediator();
            services.AddSubmarineInstructors();
            services.AddSubmarineControllers();
            services.AddSubmarineSwagger<Startup>();
            services.AddSubmarineAuthentication(authenticationSettings);
            services.AddSubmarineDatabase(builder => builder.WithConnectionString(databaseSettings.ConnectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>(_exceptionMapping);
            app.AddSubmarineSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}