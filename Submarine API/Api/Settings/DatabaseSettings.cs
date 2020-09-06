using Diagnosea.Submarine.Domain.Settings;

namespace Diagnosea.Submarine.Api.Settings
{
    public class DatabaseSettings : ISubmarineDatabaseSettings
    {
        public string ConnectionString { get; set; }
    }
}