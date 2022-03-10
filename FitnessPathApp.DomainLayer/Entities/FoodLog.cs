using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class FoodLog : BaseEntity
    {
        public DateTime Date{ get; set; }
        public ICollection<FoodItem> FoodItems { get; set; }
    }
}
