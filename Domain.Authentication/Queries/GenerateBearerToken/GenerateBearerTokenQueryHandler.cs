using System.IdentityModel.Tokens.Jwt;
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
                .WithClaim(AuthenticationConstants.ClaimTypes.UserId, request.Id.ToString())
                .WithClaim(AuthenticationConstants.ClaimTypes.UserName, request.Name)
                .WithClaim(AuthenticationConstants.ClaimTypes.Expiration, expiration.ToString());

            foreach (var role in request.Roles)
            {
                securityTokenDescriptorBuilder.WithClaim(AuthenticationConstants.ClaimTypes.Roles, role.ToString());
            }

            var securityTokenDescriptor = securityTokenDescriptorBuilder.Build();
            var jwetSecurityTokenHander = new JwtSecurityTokenHandler();

            var securityToken = jwetSecurityTokenHander.CreateToken(securityTokenDescriptor);
            var serializedToken = jwetSecurityTokenHander.WriteToken(securityToken);

            return Task.FromResult(serializedToken);
        }
    }
}