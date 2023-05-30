using AutoMapper;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;

namespace FitnessPathApp.BusinessLayer
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Exercise, ExerciseDTO>().ReverseMap();
            CreateMap<TrainingLog, TrainingLogDTO>().ReverseMap();
            CreateMap<WeightLog, WeightLogDTO>().ReverseMap();
            CreateMap<FoodItem, FoodItemDTO>().ReverseMap();
            CreateMap<FoodLog, FoodLogDTO>().ReverseMap();
        }
    }
}
