using FitnessPathApp.DomainLayer.Entities;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FitnessPathApp.Tests.IntegrationTests
{
    public class ExerciseControllerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GetAll_SuccessStatusCode_GetsAll()
        {
            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync("/Exercise/GetAll");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Exercise>>(content);

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task GetById_SuccessStatusCode_GetsOne()
        {
            var id = Guid.Parse("82a61b04-1cda-4045-abb5-0c1596f9aa36");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync($"/Exercise/Get/{id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Exercise>(content);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_SuccessStatusCode_Creates()
        {
            var exercise = new Exercise()
            {
                Id = Guid.Parse("4bd80afd-c63c-4c05-8d4a-42977ba40733"),
                Name = "Incline press",
                Weight = 120,
                Sets = 3,
                Reps = 8,
                TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8")
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(exercise),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync($"/Exercise/Create", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Exercise>(content);

            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Update_EnsureStatusCode_Updates()
        {
            var exercise = new Exercise()
            {
                Id = Guid.Parse("46aa1ca5-4670-4a38-a840-96204dd0b3a2"),
                Name = "Squat",
                Weight = 180,
                Sets = 5,
                Reps = 5,
                TrainingLogId = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8")
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(exercise),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"/Exercise/Update", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Exercise>(content);

            var updateItemResponse = await client.GetAsync($"/Exercise/Get/{exercise.Id}");
            var updateResponseContent = await updateItemResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<Exercise>(updateResponseContent);

            Assert.Equal(updateResult.Weight, exercise.Weight);
            Assert.Equal(updateResult.Id, exercise.Id);
        }

        [Fact]
        public async Task Delete_SuccessStatusCode_Deletes()
        {
            var id = Guid.Parse("82a61b04-1cda-4045-abb5-0c1596f9aa36");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.DeleteAsync($"/Exercise/Delete/{id}");

            response.EnsureSuccessStatusCode();

            var deleteItemResponse = await client.GetAsync($"/Exercise/Get/{id}");

            Assert.Equal(HttpStatusCode.NotFound, deleteItemResponse.StatusCode);
        }
    }
}
