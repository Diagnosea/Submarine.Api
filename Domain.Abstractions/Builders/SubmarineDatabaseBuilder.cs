using System;
using System.Reflection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Diagnosea.Submarine.Domain.Abstractions.Builders
{
    public class SubmarineDatabaseBuilder
    {
        private MongoUrl? _mongoUrl;
        private MongoClientSettings? _mongoClientSettings;
        private readonly ConventionPack _conventionPack;

        internal SubmarineDatabaseBuilder()
        {
            _conventionPack = new ConventionPack();
        }

        public SubmarineDatabaseBuilder WithConnectionString(string connectionString)
        {
            _mongoUrl = MongoUrl.Create(connectionString);
            _mongoClientSettings = MongoClientSettings.FromUrl(_mongoUrl);
            
            return this;
        }

        public SubmarineDatabaseBuilder WithConvention(IConvention convention)
        {
            _conventionPack.Add(convention);
            return this;
        }

        internal IMongoDatabase Build()
        {
            var client = new MongoClient(_mongoClientSettings);
            var callingAssemblyName = Assembly.GetCallingAssembly().GetName();
            
            ConventionRegistry.Register(callingAssemblyName.FullName, _conventionPack, x => true);

            if (_mongoUrl is null)
                throw new ArgumentException("Cannot Build Database Without URL");
            
            return client.GetDatabase(_mongoUrl.DatabaseName);
        }
    }
}