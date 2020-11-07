namespace Diagnosea.Submarine.Domain.Livestock.Entities.Livestock.FishLivestock
{
    public class FishLivestockEntity : LivestockEntity
    {
        /// <summary>
        /// Fish is swimming actively throughout the entire tank, not just hanging out or
        /// laying at the bottom; floating near the top or hiding behind plants and ornaments.
        /// </summary>
        public bool SwimmingActively { get; set; }
        
        /// <summary>
        /// Fish is eating regularly and swims to the surface quickly at feeding time.
        /// </summary>
        public bool EatingRegularly { get; set; }
        
        /// <summary>
        /// Fish's gills are expanding regularly - but not rapidly - to take in water and oxygen.
        /// </summary>
        public bool GillsExpanding { get; set; }
        
        /// <summary>
        /// Fish's scales are vibrant and brightly colored.
        /// </summary>
        public bool BrightlyColored { get; set; }
        
        /// <summary>
        /// Fish is swimming in clear, clean and odorless water.
        /// </summary>
        public bool PrefersClearWater { get; set; }
    }
}