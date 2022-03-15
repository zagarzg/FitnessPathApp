using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class FoodLog : BaseEntity
    {
        public DateTime Date{ get; set; }
        public ICollection<FoodItem> FoodItems { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
