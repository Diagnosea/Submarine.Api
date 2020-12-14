using System;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Domain.Tank.Queries.GetTankByUserId
{
    public class GetTankByUserIdQueryBuilder
    {
        private Guid? _userId { get; set; }

        public GetTankByUserIdQueryBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public GetTankByUserIdQuery Build()
        {
            if (!_userId.HasValue)
            {
                throw new SubmarineMappingException(
                    $"Failed to Map UserId to {nameof(GetTankByUserIdQuery)}",
                    MappingExceptionMessages.Failed);
            }
            
            return new GetTankByUserIdQuery
            {
                UserId = _userId.Value
            };
        }
    }
}