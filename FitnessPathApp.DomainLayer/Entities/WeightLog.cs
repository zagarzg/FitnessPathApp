using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class WeightLog : BaseEntity
    {
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
