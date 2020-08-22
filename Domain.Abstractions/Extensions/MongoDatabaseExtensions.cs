using System;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.Abstractions.Extensions
{
    public static class MongoDatabaseExtensions
    {
        private static string EntitySuffix = "Entity";
        
        public static IMongoCollection<TEntity> GetEntityCollection<TEntity>(this IMongoDatabase mongoDatabase) where TEntity : class
        {
            var entityName = typeof(TEntity).Name;
            
            var indexOfEntitySuffix = entityName.IndexOf(EntitySuffix, StringComparison.OrdinalIgnoreCase);
            var collectionName = entityName.Substring(0, indexOfEntitySuffix);
            
            return mongoDatabase.GetCollection<TEntity>(collectionName);
        }
    }
}