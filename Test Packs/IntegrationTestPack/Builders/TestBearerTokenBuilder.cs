using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Diagnosea.IntegrationTestPack.Builders
{
    public class TestBearerTokenBuilder
    {
        private readonly IList<Claim> _claims = new List<Claim>();

        public TestBearerTokenBuilder WithRole(UserRole role)
        {
            var claim = new Claim(SubmarineRegisteredClaimNames.Roles, role.ToString());
            _claims.Add(claim);

            return this;
        }
        
        public string Build()
        {
            var identity = new ClaimsIdentity(_claims);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity
            };

            var securityTokenHandler = new JwtSecurityTokenHandler();

            var token = securityTokenHandler.CreateToken(securityTokenDescriptor);

            var encodedAccessToken = securityTokenHandler.WriteToken(token);

            return encodedAccessToken;
        }
    }
}