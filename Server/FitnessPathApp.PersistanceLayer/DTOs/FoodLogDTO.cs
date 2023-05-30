using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPathApp.PersistanceLayer.DTOs
{
    public class FoodLogDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<FoodItemDTO> FoodItems { get; set; }
    }
}
