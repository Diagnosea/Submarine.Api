using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Diagnosea.Submarine.Domain.Authentication.Builders
{
    public class SecurityTokenDescriptorBuilder
    {
        private readonly IList<Claim> _claims = new List<Claim>();
        private DateTime _expiration;
        private SymmetricSecurityKey _symmetricSecurityKey;

        public SecurityTokenDescriptorBuilder WithClaim(string type, string value)
        {
            _claims.Add(new Claim(type, value));
            return this;
        }
        
        public SecurityTokenDescriptorBuilder WithExpiration(DateTime expiration)
        {
            _expiration = expiration;
            return this;
        }
        
        public SecurityTokenDescriptorBuilder WithSymmetricSecurityKey(byte[] encodedSecret)
        {
            _symmetricSecurityKey = new SymmetricSecurityKey(encodedSecret);
            return this;
        }

        public SecurityTokenDescriptor Build()
        {
            var claimsIdentity = new ClaimsIdentity(_claims);
            var signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            return new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = _expiration,
                SigningCredentials = signingCredentials
            };
        }
    }
}