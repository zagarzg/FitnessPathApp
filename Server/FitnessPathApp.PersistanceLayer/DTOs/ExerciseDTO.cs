﻿using System;

namespace FitnessPathApp.PersistanceLayer.DTOs
{
    public class ExerciseDTO
    {
        public Guid Id { get; set; }
        public Guid ExerciseChoiceId { get; set; }
        public ExerciseChoiceDTO ExerciseChoice { get; set; }
        public double Weight { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}
