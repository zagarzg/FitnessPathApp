﻿using FitnessPathApp.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessPathApp.DomainLayer
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedUsers();
            builder.SeedWeightLogs();
            builder.SeedExercises();
            builder.SeedTrainingLogs();
            builder.SeedFoodItems();
            builder.SeedFoodLogs();
        }

        private static void SeedUsers(this ModelBuilder builder)
        {
            var users = new List<User>
            {
                new User
                {
                    Id = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"),
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Email = "admin@hotmail.com"
                }
            };

            builder.Entity<User>().HasData(users);
        }

        private static void SeedWeightLogs(this ModelBuilder builder)
        {

            var logs = new List<WeightLog>
            {
                new WeightLog
                {
                    Id = Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd559"),
                    Value = 77.5,
                    Date = new DateTime(2022, 2, 14),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                },
                new WeightLog
                {
                    Id = Guid.Parse("c4714153-23fc-4413-b6dc-7fa230cb8883"),
                    Value = 78,
                    Date = new DateTime(2022, 2, 15),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                },
                new WeightLog
                {
                    Id = Guid.Parse("9d9926f0-ee0b-4cdb-b4bd-c178377d8868"),
                    Value = 77.3,
                    Date = new DateTime(2022, 2, 16),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                },
                new WeightLog
                {
                    Id = Guid.Parse("8514a58d-0edc-46bc-a0c8-bd912b5f5742"),
                    Value = 77.6,
                    Date = new DateTime(2022, 2, 17),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                },
                new WeightLog
                {
                    Id = Guid.Parse("52fce968-8a43-4dbd-ab26-dcf334c149dd"),
                    Value = 77.8,
                    Date = new DateTime(2022, 2, 18),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                }
            };

            builder.Entity<WeightLog>().HasOne(l => l.User).WithMany(u => u.WeightLogs).HasForeignKey(l => l.UserId).IsRequired(false);
            builder.Entity<WeightLog>().HasData(logs);
        }

        private static void SeedExercises(this ModelBuilder builder)
        {

            var exercises = new List<Exercise>
            {
                new Exercise
                {
                    Id = Guid.Parse("82a61b04-1cda-4045-abb5-0c1596f9aa36"),
                    Name = "Bench Press",
                    Weight = 100,
                    Sets = 5,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8")
                },
                new Exercise
                {
                    Id = Guid.Parse("46aa1ca5-4670-4a38-a840-96204dd0b3a2"),
                    Name = "Squat",
                    Weight = 140,
                    Sets = 5,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8")
                },
                new Exercise
                {
                    Id = Guid.Parse("853172d8-26ca-4de1-acc0-b28d753c328f"),
                    Name = "Deadlift",
                    Weight = 180,
                    Sets = 3,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8")
                },
                new Exercise
                {
                    Id = Guid.Parse("9d9d6825-98ba-47cf-b137-2f6431a047ca"),
                    Name = "Overhead Press",
                    Weight = 60,
                    Sets = 3,
                    Reps = 8,
                    TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8")
                },
                new Exercise
                {
                    Id = Guid.Parse("e937cea5-99db-4068-96a2-c75fde51df74"),
                    Name = "Barbell Row",
                    Weight = 70,
                    Sets = 3,
                    Reps = 8,
                    TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8")
                },
            };

            builder.Entity<Exercise>().HasOne(e => e.Log).WithMany(l => l.Exercises).HasForeignKey(e => e.TrainingLogId).IsRequired(false);
            builder.Entity<Exercise>().HasData(exercises);
        }

        private static void SeedTrainingLogs(this ModelBuilder builder)
        {

            var logs = new List<TrainingLog>
            {
                new TrainingLog
                {
                    Id = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                    Date = new DateTime(2022, 2, 28),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                },
            };
            builder.Entity<TrainingLog>().HasOne(l => l.User).WithMany(u => u.TrainingLogs).HasForeignKey(l => l.UserId).IsRequired(false);
            builder.Entity<TrainingLog>().HasData(logs);
        }

        private static void SeedFoodItems(this ModelBuilder builder)
        {

            var items = new List<FoodItem>
            {
                new FoodItem
                {
                    Id = Guid.Parse("aac86f05-0ed4-43a1-876f-6ba34f605661"),
                    Name = "Tuna",
                    Calories = 170,
                    Carbs = 0,
                    Protein = 26,
                    Fat = 7,
                    FoodLogId = Guid.Parse("da789a67-5481-4ac9-b338-0a9b3c069a1f")
                },
                new FoodItem
                {
                    Id = Guid.Parse("fe8e839e-c834-4b88-ab3c-e8fe0610a3f1"),
                    Name = "Pasta",
                    Calories = 356,
                    Carbs = 72,
                    Protein = 12,
                    Fat = 2,
                    FoodLogId = Guid.Parse("da789a67-5481-4ac9-b338-0a9b3c069a1f")
                },
                new FoodItem
                {
                    Id = Guid.Parse("81b43909-4490-42fe-af1d-f0e952cf727a"),
                    Name = "Zbregov Protein",
                    Calories = 325,
                    Carbs = 25,
                    Protein = 50,
                    Fat = 3,
                    FoodLogId = Guid.Parse("da789a67-5481-4ac9-b338-0a9b3c069a1f")
                },
            };

            builder.Entity<FoodItem>().HasOne(i => i.Log).WithMany(l => l.FoodItems).HasForeignKey(i => i.FoodLogId).IsRequired(false);
            builder.Entity<FoodItem>().HasData(items);
        }

        private static void SeedFoodLogs(this ModelBuilder builder)
        {

            var logs = new List<FoodLog>
            {
                new FoodLog
                {
                    Id = Guid.Parse("da789a67-5481-4ac9-b338-0a9b3c069a1f"),
                    Date = new DateTime(2022, 2, 28),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                },
            };
            builder.Entity<FoodLog>().HasOne(l => l.User).WithMany(u => u.FoodLogs).HasForeignKey(l => l.UserId).IsRequired(false);
            builder.Entity<FoodLog>().HasData(logs);
        }
    }
}
