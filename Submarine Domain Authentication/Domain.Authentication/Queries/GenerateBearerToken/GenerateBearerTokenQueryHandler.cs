using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Authentication.Builders;
using Diagnosea.Submarine.Domain.Authentication.Settings;
using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken
{
    public class GenerateBearerTokenQueryHandler : IRequestHandler<GenerateBearerTokenQuery, string>
    {
        private readonly ISubmarineAuthenticationSettings _submarineAuthenticationSettings;

        public GenerateBearerTokenQueryHandler(ISubmarineAuthenticationSettings submarineAuthenticationSettings)
        {
            _submarineAuthenticationSettings = submarineAuthenticationSettings;
        }
        
        public Task<string> Handle(GenerateBearerTokenQuery request, CancellationToken cancellationToken)
        {
            var key = Encoding.UTF8.GetBytes(_submarineAuthenticationSettings.Secret);
            var expiration = DateTime.UtcNow.AddDays(_submarineAuthenticationSettings.ExpirationInDays);

            var securityTokenDescriptorBuilder = new SecurityTokenDescriptorBuilder()
                .WithKey(key)
                .WithExpires(expiration)
                .WithClaim(JwtRegisteredClaimNames.Sub, request.Subject.ToString())
                .WithClaim(SubmarineRegisteredClaimNames.Name, request.Name)
                .WithClaim(JwtRegisteredClaimNames.Iss, _submarineAuthenticationSettings.Issuer)
                .WithClaim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture))
                .WithClaim(JwtRegisteredClaimNames.Iat, expiration.ToString(CultureInfo.InvariantCulture))
                .WithClaim(JwtRegisteredClaimNames.Aud, request.AudienceId);

            foreach (var role in request.Roles)
            {
                securityTokenDescriptorBuilder.WithClaim(SubmarineRegisteredClaimNames.Role, role.ToString());
            }

            var securityTokenDescriptor = securityTokenDescriptorBuilder.Build();
            var jwetSecurityTokenHander = new JwtSecurityTokenHandler();

            var securityToken = jwetSecurityTokenHander.CreateToken(securityTokenDescriptor);
            var serializedToken = jwetSecurityTokenHander.WriteToken(securityToken);

            return Task.FromResult(serializedToken);
        }
    }
}