using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Diagnosea.IntegrationTestPack.Factories
{
    public class SubmarineWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly SubmarineWebApplicationFactoryOptions _webApplicationFactoryOptions;

        public SubmarineWebApplicationFactory(SubmarineWebApplicationFactoryOptions webApplicationFactoryOptions)
        {
            _webApplicationFactoryOptions = webApplicationFactoryOptions;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters.RequireSignedTokens = false;
                    options.TokenValidationParameters.RequireExpirationTime = false;
                    options.TokenValidationParameters.ValidateActor = false;
                    options.TokenValidationParameters.ValidateTokenReplay = false;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.TokenValidationParameters.ValidateIssuer = false;
                    options.TokenValidationParameters.ValidateIssuerSigningKey = false;
                    options.TokenValidationParameters.ValidateLifetime = false;
                    options.TokenValidationParameters.SignatureValidator = (token, parameters) => new JwtSecurityToken(token);
                });
            });

            builder.ConfigureTestServices(services => services.AddSingleton(_webApplicationFactoryOptions.Database));
        }
    }
}