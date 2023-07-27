using FitnessPathApp.DomainLayer.Entities;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FitnessPathApp.Tests.IntegrationTests
{
    public class ExerciseChoiceControllerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GetAll_SuccessStatusCode_GetsAll()
        {
            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync("/ExerciseChoice/GetAll");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ExerciseChoice>>(content);

            Assert.Equal(42, result.Count);
        }

        [Fact]
        public async Task GetById_SuccessStatusCode_GetsOne()
        {
            var id = Guid.Parse("3E165F3D-F9FA-4E27-A5B9-05D3B938D8EB");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync($"/ExerciseChoice/Get/{id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ExerciseChoice>(content);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_SuccessStatusCode_Creates()
        {
            var exerciseChoice = new ExerciseChoice()
            {
                Id = Guid.Parse("5f0d3ae0-4405-4e96-b31d-ee0f976154f6"),
                Name = "Test Choice",
                ExerciseType = ExerciseType.Compound,
                ImageUrl = "Test Image",
                IsFavorite = true,
                Exercises = new List<Exercise>()
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(exerciseChoice),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync($"/ExerciseChoice/Create", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ExerciseChoice>(content);

            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Update_EnsureStatusCode_Updates()
        {
            var exerciseChoice = new ExerciseChoice()
            {
                Id = Guid.Parse("3E165F3D-F9FA-4E27-A5B9-05D3B938D8EB"),
                Name = "Test Choice Update",
                ExerciseType = ExerciseType.Compound,
                ImageUrl = "Test Image Update",
                IsFavorite = true,
                Exercises = new List<Exercise>()
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(exerciseChoice),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"/ExerciseChoice/Update", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ExerciseChoice>(content);

            var updateItemResponse = await client.GetAsync($"/ExerciseChoice/Get/{exerciseChoice.Id}");
            var updateResponseContent = await updateItemResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<ExerciseChoice>(updateResponseContent);

            Assert.Equal(updateResult.Name, exerciseChoice.Name);
            Assert.Equal(updateResult.ImageUrl, exerciseChoice.ImageUrl);
        }

        [Fact]
        public async Task Delete_SuccessStatusCode_Deletes()
        {
            var id = Guid.Parse("4F6CFEBB-012B-45DF-A565-AE3939D74D26");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.DeleteAsync($"/ExerciseChoice/Delete/{id}");

            response.EnsureSuccessStatusCode();

            var deleteItemResponse = await client.GetAsync($"/ExerciseChoice/Get/{id}");

            Assert.Equal(HttpStatusCode.NotFound, deleteItemResponse.StatusCode);
        }
    }
}
