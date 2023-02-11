using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPathApp.PersistanceLayer.DTOs
{
    public class WeightLogDTO
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        
    }
}
