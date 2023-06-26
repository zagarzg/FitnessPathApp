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
            builder.SeedUsers();
            builder.SeedWeightLogs();
            builder.SeedExerciseChoices();
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
                    Date = new DateTime(2023, 6, 5),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                },
                new WeightLog
                {
                    Id = Guid.Parse("c4714153-23fc-4413-b6dc-7fa230cb8883"),
                    Value = 78,
                    Date = new DateTime(2023, 6, 6),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                },
                new WeightLog
                {
                    Id = Guid.Parse("9d9926f0-ee0b-4cdb-b4bd-c178377d8868"),
                    Value = 77.3,
                    Date = new DateTime(2023, 6, 7),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                },
                new WeightLog
                {
                    Id = Guid.Parse("8514a58d-0edc-46bc-a0c8-bd912b5f5742"),
                    Value = 77.6,
                    Date = new DateTime(2023, 6, 8),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                },
                new WeightLog
                {
                    Id = Guid.Parse("52fce968-8a43-4dbd-ab26-dcf334c149dd"),
                    Value = 77.8,
                    Date = new DateTime(2023, 6, 9),
                    UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
                }
            };

            builder.Entity<WeightLog>().HasOne(l => l.User).WithMany(u => u.WeightLogs).OnDelete(DeleteBehavior.Cascade).HasForeignKey(l => l.UserId).IsRequired(false);
            builder.Entity<WeightLog>().HasData(logs);
        }

        private static void SeedExerciseChoices(this ModelBuilder builder)
        {
            var exerciseChoices = new List<ExerciseChoice>
            {
                new ExerciseChoice
                {
                    Id = Guid.Parse("3e165f3d-f9fa-4e27-a5b9-05d3b938d8eb"),
                    Name = "Bench Press",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/04/barbell-bench-press.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                new ExerciseChoice
                {
                    Id = Guid.Parse("bde86a6d-9a37-420d-93ca-60d7b02dc7c4"),
                    Name = "Squat",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/barbell-full-squat.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                 new ExerciseChoice
                {
                    Id = Guid.Parse("c1769344-dd80-41fe-bbfc-a16df832929b"),
                    Name = "Deadlift",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/02/barbell-deadlift-movement.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                  new ExerciseChoice
                {
                    Id = Guid.Parse("c328e92b-33ff-4863-9397-d53424ca3f5c"),
                    Name = "Overhead Press",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/04/military-press.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                  new ExerciseChoice
                {
                    Id = Guid.Parse("0b9a334e-0849-4a54-91bd-e6789bcab11c"),
                    Name = "Barbell Row",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/08/barbell-bent-over-row.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                    new ExerciseChoice
                {
                    Id = Guid.Parse("e86748e9-9203-41a2-bdf3-12c9a0b24560"),
                    Name = "Romanian Deadlift",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/06/barbell-romanian-deadlift-movement.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                         new ExerciseChoice
                {
                    Id = Guid.Parse("032c5827-77d7-4f38-a205-2aea3a215005"),
                    Name = "Dumbell Romanian Deadlift",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2021/11/dumbbell-romanian-deadlift.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice {
                    Id = Guid.Parse("5020ad9b-2a1f-4634-99a6-f89b65b46ab4"),
                    Name = "Stiff Leg Deadlift",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/11/straight-leg-deadlift.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("8ef4878b-2aa6-4319-bd84-abda5148fba2"),
                    Name = "Pullup",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/11/pull-up.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("7b548a27-ecdf-4f15-b485-8f57e456d3f5"),
                    Name = "Chinup",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/09/chin-ups.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("dd5a8953-4e8b-48c1-973a-741faaece2f7"),
                    Name = "Neutral Grip Pullup",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2023/03/neutral-grip-pull-ups-shoulder-width.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("0c12a6c5-d47e-431c-91b0-8e8d9a6fa83a"),
                    Name = "Cable Row",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/08/cable-seated-row.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("2e1c74ce-d121-4832-8530-d045b717c4e7"),
                    Name = "Lat Pulldown",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/08/wide-grip-lat-pulldown.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("f856276a-e2ab-4158-bf50-0075197b7a16"),
                    Name = "One Arm Dumbell Row",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2021/10/single-arm-dumbbell-row.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("1cea4155-1b12-4fec-9d31-1c91c3f6fb60"),
                    Name = "Dumbell Shoulder Press",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/10/dumbbell-shoulder-press.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("a9bfc5a4-8666-47f0-ab14-929345675627"),
                    Name = "Incline Dumbell Curl",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/10/incline-dumbbell-curl.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("dcc607b0-0573-4ba5-b00f-7db3525a8e04"),
                    Name = "Dumbell Bicep Curl",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/dumbbell-bicep-curl.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("23af1cfe-de80-4a25-98f3-9de67f64cf36"),
                    Name = "Ez Preacher Curl",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/ez-bar-preacher-curl.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("f8e9dcb1-3269-4b40-8a91-b5cc6c18211e"),
                    Name = "Dumbell Hammer Curl",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/04/dumbbell-hammer-curl.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("44eb78e8-1353-41e2-ba3d-0b93c4c66af5"),
                    Name = "Dumbell Concentration Curl",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/dumbbell-concentration-curl.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                         new ExerciseChoice{
                Id = Guid.Parse("9e78421c-e5f8-4040-a0fd-8a12454b03bc"),
                    Name = "Ez Bar Curl",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2023/02/ez-bar-bicep-curl.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("828ac5f9-1936-4194-a462-e83163f12162"),
                    Name = "Barbell Curl",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/05/barbell-curl.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                     new ExerciseChoice{
                Id = Guid.Parse("c1258b52-4e63-4aa7-a075-a16f4a460357"),
                    Name = "Dumbell Chest Fly",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/08/dumbbell-chest-fly-muscles.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("82f28e0d-b7cf-4c9a-98b9-9b757b31212c"),
                    Name = "Dumbell Chest Press",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/09/dumbbell-chest-press.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("be5d2cee-e898-4b3a-8386-ea39fda3622e"),
                    Name = "Dumbell Incline Press",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/09/dumbbell-incline-chest-press.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("4e24fd67-6d30-433b-a604-d01ad90ef54e"),
                    Name = "Incline Bench Press",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/05/incline-barbell-bench-press.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("5c9d00b3-8095-4a75-8d83-98d5132d4db3"),
                    Name = "Close Grip Bench Press",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/01/close-grip-bench-press-movement.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("4e47994a-3b58-47b6-b4ac-424a6686cd36"),
                    Name = "Skullcrusher",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/08/flat-bench-skull-crusher.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("4f6cfebb-012b-45df-a565-ae3939d74d26"),
                    Name = "Diamond Pushup",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2021/08/diamond-push-up.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("e006abf7-da50-4530-a93d-8683fed79d43"),
                    Name = "Pushup",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/11/push-up.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("a847047f-4af5-444e-a81e-ebacd4097aae"),
                    Name = "Bulgarian Split Squat",
                    ImageUrl = "https://fitnessprogramer.com/wp-content/uploads/2021/05/Dumbbell-Bulgarian-Split-Squat.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("60669c8a-df74-4e95-a9ce-c9b7e0be40ab"),
                    Name = "Lunges",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/01/dumbbell-lunges.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("d6350d47-6274-47be-a43c-ecdf29cf7390"),
                    Name = "Triceps Overhead Extension",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/tricep-overhead-extensions.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("c3449838-8c95-400a-823f-f06e49618269"),
                    Name = "Cable Triceps Pushdown",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2021/10/cable-tricep-pushdown.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("1b5889fa-484c-4a55-8df7-7b38415762c2"),
                    Name = "Dumbell Lateral Raise",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/03/dumbbell-lateral-raise.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("2607d5fe-bcb6-48e7-a2ed-6ff0693f2361"),
                    Name = "Hipthrust",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/09/barbell-hip-thrust.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("dd4d5068-0311-4189-b7a4-237bf43306fb"),
                    Name = "Barbell Reverse Curl",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/10/barbell-reverse-curl.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("46102532-02a6-4e62-b35b-bcea221b77c1"),
                    Name = "Plank",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/01/plank-movement.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("18522a6b-c3c0-454e-bc25-d145fb69d0ad"),
                    Name = "Decline Situp",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/05/decline-sit-up.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("a34f4863-35ee-4baf-86c8-d2a678bc1776"),
                    Name = "Hanging Leg Raises",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/06/hanging-leg-raise-movement.gif",
                    ExerciseType = ExerciseType.Accessory,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("1c4e0b57-e66a-4604-960b-122c396b1574"),
                    Name = "Weighted Dips",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/04/weighted-dips.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
                      new ExerciseChoice{
                Id = Guid.Parse("846df523-dc1d-4206-917c-7b289e156ccc"),
                    Name = "Dips",
                    ImageUrl = "https://www.inspireusafoundation.org/wp-content/uploads/2022/01/chest-dip-movement.gif",
                    ExerciseType = ExerciseType.Compound,
                    IsFavorite = true,
                },
        };

            builder.Entity<ExerciseChoice>().HasMany(e => e.Exercises).WithOne(l => l.ExerciseChoice).HasForeignKey(e => e.ExerciseChoiceId).IsRequired(false); ;
            builder.Entity<ExerciseChoice>().HasData(exerciseChoices);
        }

        private static void SeedExercises(this ModelBuilder builder)
        {

            var exercises = new List<Exercise>
            {
                new Exercise
                {
                    Id = Guid.Parse("82a61b04-1cda-4045-abb5-0c1596f9aa36"),
                    Weight = 100,
                    Sets = 5,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                    ExerciseChoiceId = Guid.Parse("3e165f3d-f9fa-4e27-a5b9-05d3b938d8eb"),
                },
                new Exercise
                {
                    Id = Guid.Parse("46aa1ca5-4670-4a38-a840-96204dd0b3a2"),
                    Weight = 140,
                    Sets = 5,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                    ExerciseChoiceId = Guid.Parse("bde86a6d-9a37-420d-93ca-60d7b02dc7c4")
                },
                new Exercise
                {
                    Id = Guid.Parse("853172d8-26ca-4de1-acc0-b28d753c328f"),
                    Weight = 180,
                    Sets = 3,
                    Reps = 5,
                    TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                    ExerciseChoiceId = Guid.Parse("c1769344-dd80-41fe-bbfc-a16df832929b")
                },
                new Exercise
                {
                    Id = Guid.Parse("9d9d6825-98ba-47cf-b137-2f6431a047ca"),
                    Weight = 60,
                    Sets = 3,
                    Reps = 8,
                    TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                    ExerciseChoiceId = Guid.Parse("c328e92b-33ff-4863-9397-d53424ca3f5c"),
                },
                new Exercise
                {
                    Id = Guid.Parse("e937cea5-99db-4068-96a2-c75fde51df74"),
                    Weight = 70,
                    Sets = 3,
                    Reps = 8,
                    TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                    ExerciseChoiceId = Guid.Parse("c328e92b-33ff-4863-9397-d53424ca3f5c"),
                },
            };

            builder.Entity<Exercise>().HasOne(e => e.Log).WithMany(l => l.Exercises).OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => e.TrainingLogId).IsRequired(false);
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
            builder.Entity<TrainingLog>().HasOne(l => l.User).WithMany(u => u.TrainingLogs).OnDelete(DeleteBehavior.Cascade).HasForeignKey(l => l.UserId).IsRequired(false);
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

            builder.Entity<FoodItem>().HasOne(i => i.Log).WithMany(l => l.FoodItems).OnDelete(DeleteBehavior.Cascade).HasForeignKey(i => i.FoodLogId).IsRequired(false);
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
            builder.Entity<FoodLog>().HasOne(l => l.User).WithMany(u => u.FoodLogs).OnDelete(DeleteBehavior.Cascade).HasForeignKey(l => l.UserId).IsRequired(false);
            builder.Entity<FoodLog>().HasData(logs);
        }
    }
}
