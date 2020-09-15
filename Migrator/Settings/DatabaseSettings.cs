using Diagnosea.Submarine.Domain.Abstractions.Settings;

namespace Diagnosea.Migrator.Settings
{
    public class DatabaseSettings : ISubmarineDatabaseSettings
    {
        public string ConnectionString { get; set; }
    }
}