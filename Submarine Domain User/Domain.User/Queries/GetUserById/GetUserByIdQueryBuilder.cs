using System;

namespace Diagnosea.Submarine.Domain.User.Queries.GetUserById
{
    public class GetUserByIdQueryBuilder
    {
        private Guid _id;
        
        public GetUserByIdQueryBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public GetUserByIdQuery Build()
        {
            return new GetUserByIdQuery
            {
                Id = _id
            };
        }
    }
}