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
        }
    }
}
