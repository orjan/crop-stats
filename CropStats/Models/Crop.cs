namespace CropStats.Models
{
    public class Crop
    {
        public int Id { get; set; }
        public int FarmerId { get; set; }
        public int CropTypeId { get; set; }
        public int Year { get; set; }

        public int Hectare { get; set; }
        public int YieldPerHectare { get; set; }
        public bool Estimated { get; set; }
    }
}