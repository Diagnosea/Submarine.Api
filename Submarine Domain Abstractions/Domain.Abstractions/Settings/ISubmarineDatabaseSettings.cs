namespace Diagnosea.Submarine.Domain.Abstractions.Settings
{
    public interface ISubmarineDatabaseSettings
    {
        string ConnectionString { get; set; }
    }
}