using System;
using System.Text.Json.Serialization;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        [JsonIgnore]
        public TrainingLog Log { get; set; }
        public Guid TrainingLogId { get; set; }
    }
}
