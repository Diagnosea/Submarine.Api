using Diagnosea.Submarine.Abstractions.Enums;

namespace Diagnosea.Submarine.Domain.Supply.Entities.MedicalSupply
{
    public class MedicalSupplyTreatmentEntity
    {
        public Ailment Treating { get; set; }
        public int Dosage { get; set; }
        public Occurence Occurence { get; set; }
        public int Period { get; set; }
    }
}