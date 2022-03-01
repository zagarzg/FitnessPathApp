using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.DomainLayer.Entities
{
    public class TrainingLog : BaseEntity
    {
        public DateTime Date { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
