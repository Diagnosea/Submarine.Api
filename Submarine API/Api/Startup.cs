using Diagnosea.Submarine.Api.Abstractions.Authentication.Extensions;
using Diagnosea.Submarine.Api.Abstractions.Extensions;
using Diagnosea.Submarine.Api.Abstractions.Swagger.Extensions;
using Diagnosea.Submarine.Api.Settings;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.Authentication.Extensions;
using Diagnosea.Submarine.Domain.Extensions;
using Diagnosea.Submarine.Domain.License;
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

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var databaseSettings = _configuration.GetSettings<DatabaseSettings>();
            var authenticationSettings = _configuration.GetSettings<AuthenticationSettings>();
            var licenseSettings = _configuration.GetSettings<LicenseSettings>();

            // Add Settings
            services.AddSubmarineAuthenticationSettings(authenticationSettings);
            services.AddSingleton<ILicenseSettings>(licenseSettings);

            // Dependencies
            services.AddSubmarineMediator();
            services.AddSubmarineInstructors();
            services.AddSubmarineControllers();
            services.AddSubmarineSwagger("Api.Abstractions.xml");
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
            app.AddSubmarineExceptionMiddleware();
            app.AddSubmarineSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}