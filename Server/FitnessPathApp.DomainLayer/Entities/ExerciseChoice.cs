using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FitnessPathApp.DomainLayer.Entities
{
    public enum ExerciseType
    {
        Compound = 1,
        Accessory = 2
    }
    public class ExerciseChoice : BaseEntity
    {
        public string Name { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavorite { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }

    }
}
