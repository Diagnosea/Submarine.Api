using System.Collections.Generic;
using Diagnosea.Submarine.Domain.User.Entities;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.User.Builders
{
    public class UserProjectionDefinitionBuilder
    {
        private readonly IList<ProjectionDefinition<UserEntity>> _projectionDefinitions = new List<ProjectionDefinition<UserEntity>>();

        public UserProjectionDefinitionBuilder()
        {
            var userIdProjectionDefinition = new ProjectionDefinitionBuilder<UserEntity>().Include(x => x.Id);
            _projectionDefinitions.Add(userIdProjectionDefinition);
        }

        public UserProjectionDefinitionBuilder WithEmailAddress()
        {
            var userEmailAddressProjectionDefintiion = new ProjectionDefinitionBuilder<UserEntity>().Include(x => x.EmailAddress);
            _projectionDefinitions.Add(userEmailAddressProjectionDefintiion);

            return this;
        }
        
        public ProjectionDefinition<UserEntity> Build()
            => new ProjectionDefinitionBuilder<UserEntity>().Combine(_projectionDefinitions);
    }
}