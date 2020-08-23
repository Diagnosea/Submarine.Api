using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Diagnosea.Submarine.Domain.Authentication.Queries
{
    public class GenerateJwtQueryHandler : IRequestHandler<GenerateJwtQuery, string>
    {
        public Task<string> Handle(GenerateJwtQuery request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(request.PrivateSigningKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = request.Audience,
                Expires = DateTime.Now.AddMinutes(20),
                Issuer = request.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}