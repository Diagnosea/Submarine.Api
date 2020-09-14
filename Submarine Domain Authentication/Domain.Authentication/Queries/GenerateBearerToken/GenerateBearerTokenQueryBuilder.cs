using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.Authentication.Queries.GenerateBearerToken
{
    public class GenerateBearerTokenQueryBuilder
    {
        private string _subject;
        private string _name;
        private string _audienceId;
        private IEnumerable<string> _roles = new List<string>();
        private IEnumerable<string> _products = new List<string>();

        public GenerateBearerTokenQueryBuilder WithSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public GenerateBearerTokenQueryBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public GenerateBearerTokenQueryBuilder WithAudience(string audienceId)
        {
            _audienceId = audienceId;
            return this;
        }

        public GenerateBearerTokenQueryBuilder WithRoles(IEnumerable<string> roles)
        {
            _roles = roles;
            return this;
        }

        public GenerateBearerTokenQueryBuilder WithProducts(IEnumerable<string> products)
        {
            _products = products;
            return this;
        }
        
        public GenerateBearerTokenQuery Build()
        {
            return new GenerateBearerTokenQuery
            {
                Subject = _subject,
                Name = _name,
                Audience = _audienceId,
                Roles = _roles,
                Products = _products
            };
        }
    }
}