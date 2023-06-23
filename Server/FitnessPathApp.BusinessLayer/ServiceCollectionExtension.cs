using AutoMapper;
using FitnessPathApp.BusinessLayer.Implementations;
using FitnessPathApp.BusinessLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessPathApp.BusinessLayer
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IWeightLogService, WeightLogService>();
            services.AddTransient<IChartService, ChartService>();
            services.AddTransient<IExerciseService, ExerciseService>();
            services.AddTransient<IExerciseChoiceService, ExerciseChoiceService>();
            services.AddTransient<ITrainingLogService, TrainingLogService>();
            services.AddTransient<IFoodItemService, FoodItemService>();
            services.AddTransient<IFoodLogService, FoodLogService>();
            services.AddTransient<IUserService, UserService>();
        }

        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var config = new MapperConfiguration(c =>
                {
                    c.AddProfile<Mappings>();
                });

                return config.CreateMapper();
            });
        }
    }
}
