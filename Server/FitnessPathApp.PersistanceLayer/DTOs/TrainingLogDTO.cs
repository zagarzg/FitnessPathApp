using System;
using System.Collections.Generic;

namespace FitnessPathApp.PersistanceLayer.DTOs
{
    public class TrainingLogDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<ExerciseDTO> Exercises { get; set; }
    }
}
