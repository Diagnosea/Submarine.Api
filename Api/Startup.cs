using Diagnosea.Submarine.Api.Abstractions.Extensions;
using Diagnosea.Submarine.Api.Options;
using Diagnosea.Submarine.Domain.Instructors.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Diagnosea.Submarine.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationOptions>(Configuration);

            services.AddSubmarineMediator();

            services.AddControllers();

            services.AddSubmarineSwagger<Startup>();

            var applicationOptions = Configuration.Get<ApplicationOptions>();
            services.AddSubmarineAuthentication(applicationOptions);

            services.AddSubmarineDatabase(builder => builder.WithConnectionString(applicationOptions.SubmarineConnectionString));
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

            var pathBase = Configuration.GetValue<string>("PathBase");
            app.AddSwagger(pathBase);
            app.UsePathBase(pathBase);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}