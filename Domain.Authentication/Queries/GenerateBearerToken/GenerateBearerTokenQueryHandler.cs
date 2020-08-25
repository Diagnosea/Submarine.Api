using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Authentication.Builders;
using Diagnosea.Submarine.Domain.Authentication.Extensions;
using Diagnosea.Submarine.Domain.Authentication.Settings;
using MediatR;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken
{
    public class GenerateBearerTokenQueryHandler : IRequestHandler<GenerateBearerTokenQuery, string>
    {
        private readonly ISubmarineJwtSettings _submarineJwtSettings;

        public GenerateBearerTokenQueryHandler(ISubmarineJwtSettings submarineJwtSettings)
        {
            _submarineJwtSettings = submarineJwtSettings;
        }
        
        public Task<string> Handle(GenerateBearerTokenQuery request, CancellationToken cancellationToken)
        {
            var encodedSecret = _submarineJwtSettings.GetEncodedSecret();
            var expiration = _submarineJwtSettings.GetExpiration();

            var securityTokenDescriptorBuilder = new SecurityTokenDescriptorBuilder()
                .WithSymmetricSecurityKey(encodedSecret)
                .WithExpiration(expiration)
                .WithClaim(AuthenticationConstants.ClaimTypes.Subject, request.Id.ToString())
                .WithClaim(AuthenticationConstants.ClaimTypes.Name, request.Name)
                .WithClaim(AuthenticationConstants.ClaimTypes.Issuer, _submarineJwtSettings.Issuer)
                .WithClaim(AuthenticationConstants.ClaimTypes.IssuedAt, DateTime.UtcNow.ToString())
                .WithClaim(AuthenticationConstants.ClaimTypes.Expiration, expiration.ToString());

            if (_submarineJwtSettings.Audiences.All(x => x != request.Audience))
            {
                throw new ArgumentException($"Invalid Audience: '{request.Audience}'");
            }
            
            securityTokenDescriptorBuilder.WithClaim(AuthenticationConstants.ClaimTypes.Audience, request.Audience);

            foreach (var role in request.Roles)
            {
                securityTokenDescriptorBuilder.WithClaim(AuthenticationConstants.ClaimTypes.Role, role.ToString());
            }

            var securityTokenDescriptor = securityTokenDescriptorBuilder.Build();
            var jwetSecurityTokenHander = new JwtSecurityTokenHandler();

            var securityToken = jwetSecurityTokenHander.CreateToken(securityTokenDescriptor);
            var serializedToken = jwetSecurityTokenHander.WriteToken(securityToken);

            return Task.FromResult(serializedToken);
        }
    }
}