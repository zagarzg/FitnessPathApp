using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class WeightLog : BaseEntity
    {
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
