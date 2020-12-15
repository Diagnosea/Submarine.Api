using System;
using Abstractions.Exceptions;

namespace Diagnosea.Submarine.Domain.Tank.Queries.GetTanksByUserId
{
    public class GetTanksByUserIdQueryBuilder
    {
        private Guid? UserId { get; set; }

        public GetTanksByUserIdQueryBuilder WithUserId(Guid userId)
        {
            UserId = userId;
            return this;
        }

        public GetTanksByUserIdQuery Build()
        {
            if (!UserId.HasValue)
            {
                throw new SubmarineMappingException(
                    $"Failed to Map UserId to {nameof(GetTanksByUserIdQuery)}",
                    MappingExceptionMessages.Failed);
            }
            
            return new GetTanksByUserIdQuery
            {
                UserId = UserId.Value
            };
        }
    }
}