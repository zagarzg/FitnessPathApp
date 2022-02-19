using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class WeightLog : BaseEntity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
