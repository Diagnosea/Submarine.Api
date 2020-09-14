using MongoDB.Driver;

namespace Diagnosea.Submarine.TestingUtilities.Factories
{
    public class SubmarineWebApplicationFactoryOptions
    {
        public IMongoDatabase Database { get; set; }
    }
}