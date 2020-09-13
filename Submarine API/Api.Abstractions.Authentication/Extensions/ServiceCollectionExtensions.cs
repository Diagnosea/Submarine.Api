using System.Text;
using Diagnosea.Submarine.Domain.Authentication.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Diagnosea.Submarine.Api.Abstractions.Authentication.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSubmarineAuthentication(this IServiceCollection serviceCollection, ISubmarineAuthenticationSettings settings)
        {
            serviceCollection.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
                        ValidIssuers = new []{settings.Issuer}
                    };
                });
        }
    }
}
