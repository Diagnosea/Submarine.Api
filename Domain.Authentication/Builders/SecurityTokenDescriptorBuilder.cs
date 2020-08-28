using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Diagnosea.Submarine.Domain.Authentication.Builders
{
    public class SecurityTokenDescriptorBuilder
    {
        private readonly IList<Claim> _claims = new List<Claim>();
        private DateTime _expires;
        private byte[] _key;

        public SecurityTokenDescriptorBuilder WithClaim(string type, string value)
        {
            _claims.Add(new Claim(type, value));
            return this;
        }
        
        public SecurityTokenDescriptorBuilder WithExpires(DateTime expiration)
        {
            _expires = expiration;
            return this;
        }

        public SecurityTokenDescriptorBuilder WithKey(byte[] key)
        {
            _key = key;
            return this;
        }

        public SecurityTokenDescriptor Build()
        {
            var claimsIdentity = new ClaimsIdentity(_claims);
            var symmetricSecurityKey = new SymmetricSecurityKey(_key);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            
            return new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = _expires,
                SigningCredentials = signingCredentials
            };
        }
    }
}