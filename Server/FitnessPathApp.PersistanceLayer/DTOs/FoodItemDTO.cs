using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPathApp.PersistanceLayer.DTOs
{
    public class FoodItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Carbs { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
    }
}
