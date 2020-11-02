using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Supply.Entities.Supply
{
    public class MedicalSupplyEntity : SupplyEntity
    {
        public Ailment Ailment { get; set; }
    }
}