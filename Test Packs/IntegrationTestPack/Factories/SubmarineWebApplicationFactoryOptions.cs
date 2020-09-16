using MongoDB.Driver;

namespace Diagnosea.IntegrationTestPack.Factories
{
    public class SubmarineWebApplicationFactoryOptions
    {
        public IMongoDatabase Database { get; set; }
    }
}