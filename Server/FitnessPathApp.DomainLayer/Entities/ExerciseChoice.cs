using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FitnessPathApp.DomainLayer.Entities
{
    public enum ExerciseType
    {
        Compound,
        Accessory
    }
    public class ExerciseChoice : BaseEntity
    {
        public string Name { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsFavorite { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }

    }
}
