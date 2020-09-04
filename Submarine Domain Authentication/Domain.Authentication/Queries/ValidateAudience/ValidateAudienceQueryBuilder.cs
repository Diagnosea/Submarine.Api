namespace Diagnosea.Submarine.Domain.Authentication.Queries.ValidateAudience
{
    public class ValidateAudienceQueryBuilder
    {
        private string _audienceId;

        public ValidateAudienceQueryBuilder WithAudienceId(string audienceId)
        {
            _audienceId = audienceId;
            return this;
        }

        public ValidateAudienceQuery Build()
        {
            return new ValidateAudienceQuery
            {
                AudienceId = _audienceId
            };
        }
    }
}