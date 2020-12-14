using System;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Domain.Tank.Queries.GetTanksByUserId
{
    public class GetTanksByUserIdQueryBuilder
    {
        private Guid? _userId { get; set; }

        public GetTanksByUserIdQueryBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public GetTanksByUserIdQuery Build()
        {
            if (!_userId.HasValue)
            {
                throw new SubmarineMappingException(
                    $"Failed to Map UserId to {nameof(GetTanksByUserIdQuery)}",
                    MappingExceptionMessages.Failed);
            }
            
            return new GetTanksByUserIdQuery
            {
                UserId = _userId.Value
            };
        }
    }
}