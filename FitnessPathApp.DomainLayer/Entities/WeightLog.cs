using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class WeightLog : BaseEntity
    {
        public double Value { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
