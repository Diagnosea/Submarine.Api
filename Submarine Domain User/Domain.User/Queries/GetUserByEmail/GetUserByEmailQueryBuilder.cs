namespace Diagnosea.Submarine.Domain.User.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryBuilder
    {
        private string _emailAddress;

        public GetUserByEmailQueryBuilder WithEmailAddress(string emailAdddress)
        {
            _emailAddress = emailAdddress;
            return this;
        }
        
        public GetUserByEmailQuery Build()
        {
            return new GetUserByEmailQuery
            {
                EmailAddress = _emailAddress
            };
        }
    }
}