using System;
using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Livestock;
using Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedRequirement;
using Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed.LivestockBreedTolerance;

namespace Diagnosea.Submarine.Domain.Livestock.Entities.LivestockBreed
{
    public class LivestockBreedEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LivestockBreedType Type { get; set; }
        
        /// <summary>
        /// Can be used to derive minimum expected values.
        /// </summary>
        public IList<LivestockBreedRequirementEntity> Requirements { get; set; }
        
        /// <summary>
        /// Can be used to derive maximum expected values.
        /// </summary>
        public IList<LivestockBreedToleranceEntity> Tolerances { get; set; }
    }


    //
    // public class LivestockBreedWaterConditionRequirementEntity : LivestockBreedRequirementEntity
    // {
    //     public WaterCondition WaterCondition { get; set; }
    // }
    //
    // public class LivestockBreedNutritionalRequirementEntity : LivestockBreedRequirementEntity
    // {
    //     public Nutrition Nutrition { get; set; }
    // }
    //
    // public enum LivestockBreedRequirementType
    // {
    //     WaterCondition,
    //     Nutritional
    // }
    //
    // public enum WaterCondition
    // {
    //     Alkalinity
    // }
    //
    // public enum Nutrition
    // {
    //     Iron
    // }
    //
    // public class LivestockEntity
    // {
    //     public Guid Id { get; set; }
    //     public DateTime? Birthday { get; set; }
    //     
    //     public bool Acclimatised { get; set; }
    //
    //     
    //     // Brred is something that will stay static but needs to be  used by multiple livestock instances
    //     // to reduce duplication
    //     public Guid LivestockBreedId { get; set; }
    //     
    //     // Latest update to medical history's status can be used to derive information for the Tank overview.
    //     public IList<LivestockMedicalEntity> Medical { get; set; }
    // }
    //
    // public class LivestockMedicalEntity
    // {
    //     public DateTime Created { get; set; }
    //     public LivestockMedicalStatus Status { get; set; }
    //     public IList<Ailment> Ailments { get; set; }
    // }
    //
    // public enum LivestockMedicalStatus
    // {
    //     Healthy
    // }
    //
    // public enum Ailment
    // {
    //     Finrot
    // }
    //
    // public class LivestockFeedEntity
    // {
    //     public DateTime Fed { get; set; }
    //     
    //     // What item would be used to feed the fish?
    //     public Guid InventoryId { get; set; }
    // }
}