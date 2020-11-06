using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Supply;

namespace Diagnosea.Submarine.Domain.Supply.Entities.MedicalSupply
{
    public class MedicalSupplyEntity : SupplyEntity
    {
        public IList<MedicalSupplyTreatmentEntity> Treats { get; set; }
        
        public MedicalSupplyEntity()
        {
            Category = SupplyCategory.Medical;
        }
    }
}