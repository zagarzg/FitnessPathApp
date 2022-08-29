using System.Collections.Generic;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<WeightLog> WeightLogs { get; set; }
        public ICollection<TrainingLog> TrainingLogs { get; set; }
        public ICollection<FoodLog> FoodLogs { get; set; }

    }
}
