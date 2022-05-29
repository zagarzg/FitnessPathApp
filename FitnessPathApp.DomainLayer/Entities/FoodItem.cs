using System;
using System.Text.Json.Serialization;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class FoodItem : BaseEntity
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Carbs { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }

        [JsonIgnore]
        public FoodLog Log { get; set; }
        public Guid FoodLogId { get; set; }
    }
}
