using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class TrainingLog : BaseEntity
    {
        public DateTime Date { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
