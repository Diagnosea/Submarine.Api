using Diagnosea.Submarine.Abstractions.Enums.Supply;

namespace Diagnosea.Submarine.Domain.Supply.Entities.FiltrationSupply
{
    public class FiltrationSupplyEntity : SupplyEntity
    {
        public FiltrationSupplyType Type { get; set; }
    }
}