using FitnessPathApp.DomainLayer.Entities;
using System;

namespace FitnessPathApp.PersistanceLayer.DTOs
{
    public class ExerciseChoiceDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsFavorite { get; set; }
    }
}
