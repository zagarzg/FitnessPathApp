using System;
using System.Text.Json.Serialization;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class Exercise : BaseEntity
    {
        public double Weight { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        [JsonIgnore]
        public TrainingLog Log { get; set; }
        public Guid TrainingLogId { get; set; }
        public virtual ExerciseChoice ExerciseChoice { get; set; }
        public Guid ExerciseChoiceId { get; set; }
    }
}
