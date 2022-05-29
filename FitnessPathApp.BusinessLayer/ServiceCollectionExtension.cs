﻿using FitnessPathApp.BusinessLayer.Implementations;
using FitnessPathApp.BusinessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessPathApp.BusinessLayer
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IWeightLogService, WeightLogService>();
            services.AddTransient<IExerciseService, ExerciseService>();
            services.AddTransient<ITrainingLogService, TrainingLogService>();
            services.AddTransient<IFoodItemService, FoodItemService>();
            services.AddTransient<IFoodLogService, FoodLogService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
