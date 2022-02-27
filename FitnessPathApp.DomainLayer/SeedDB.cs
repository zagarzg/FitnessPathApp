using FitnessPathApp.DomainLayer.Entities;
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
            builder.SeedWeightLogs();
            builder.SeedExercises();
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
                },
                new WeightLog
                {
                    Id = Guid.Parse("c4714153-23fc-4413-b6dc-7fa230cb8883"),
                    Value = 78,
                    Date = new DateTime(2022, 2, 15),
                },
                new WeightLog
                {
                    Id = Guid.Parse("9d9926f0-ee0b-4cdb-b4bd-c178377d8868"),
                    Value = 77.3,
                    Date = new DateTime(2022, 2, 16),
                },
                new WeightLog
                {
                    Id = Guid.Parse("8514a58d-0edc-46bc-a0c8-bd912b5f5742"),
                    Value = 77.6,
                    Date = new DateTime(2022, 2, 17),
                },
                new WeightLog
                {
                    Id = Guid.Parse("52fce968-8a43-4dbd-ab26-dcf334c149dd"),
                    Value = 77.8,
                    Date = new DateTime(2022, 2, 18),
                }
            };

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
                    Reps = 5
                },
                new Exercise
                {
                    Id = Guid.Parse("46aa1ca5-4670-4a38-a840-96204dd0b3a2"),
                    Name = "Squat",
                    Weight = 140,
                    Sets = 5,
                    Reps = 5
                },
                new Exercise
                {
                    Id = Guid.Parse("853172d8-26ca-4de1-acc0-b28d753c328f"),
                    Name = "Deadlift",
                    Weight = 180,
                    Sets = 3,
                    Reps = 5
                },
                new Exercise
                {
                    Id = Guid.Parse("9d9d6825-98ba-47cf-b137-2f6431a047ca"),
                    Name = "Overhead Press",
                    Weight = 60,
                    Sets = 3,
                    Reps = 8
                },
                new Exercise
                {
                    Id = Guid.Parse("e937cea5-99db-4068-96a2-c75fde51df74"),
                    Name = "Barbell Row",
                    Weight = 70,
                    Sets = 3,
                    Reps = 8
                },
            };

            builder.Entity<Exercise>().HasData(exercises);
        }
    }
}
