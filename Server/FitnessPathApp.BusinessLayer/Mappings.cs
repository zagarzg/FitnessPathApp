using AutoMapper;
using FitnessPathApp.DomainLayer.Entities;
using FitnessPathApp.PersistanceLayer.DTOs;
using System.Collections.Generic;

namespace FitnessPathApp.BusinessLayer
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Exercise, ExerciseDTO>().ReverseMap();
            CreateMap<ExerciseChoice, ExerciseChoiceDTO>().ReverseMap();
            CreateMap<TrainingLog, TrainingLogDTO>().ReverseMap();
            CreateMap<WeightLog, WeightLogDTO>().ReverseMap();
            CreateMap<FoodItem, FoodItemDTO>().ReverseMap();
            CreateMap<FoodLog, FoodLogDTO>().ReverseMap();
            CreateMap<byte[], string>().ConvertUsing<Base64Converter>();
            CreateMap<string, byte[]>().ConvertUsing<Base64Converter>();
        }

        private class Base64Converter : ITypeConverter<string, byte[]>, ITypeConverter<byte[], string>
        {
            public byte[] Convert(string source, byte[] destination, ResolutionContext context)
                => System.Convert.FromBase64String(source);

            public string Convert(byte[] source, string destination, ResolutionContext context)
                => System.Convert.ToBase64String(source);
        }
    }
}
