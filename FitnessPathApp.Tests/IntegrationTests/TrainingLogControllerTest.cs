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
    public class TrainingLogControllerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GetAll_SuccessStatusCode_GetsAll()
        {
            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync("/TrainingLog/GetAll");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TrainingLog>>(content);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task GetById_SuccessStatusCode_GetsOne()
        {
            var id = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync($"/TrainingLog/Get/{id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeightLog>(content);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_SuccessStatusCode_Creates()
        {
            var log = new TrainingLog()
            {
                Id = Guid.Parse("7ccb13f4-a2ae-4614-a295-1d86e4e67344"),
                Date = new DateTime(2022, 3, 12),
                UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(log),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync($"/TrainingLog/Create", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TrainingLog>(content);

            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Update_EnsureStatusCode_Updates()
        {
            var log = new TrainingLog()
            {
                Id = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8"),
                Date = new DateTime(2022, 4, 12),
                UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(log),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"/TrainingLog/Update", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeightLog>(content);

            var updateItemResponse = await client.GetAsync($"/TrainingLog/Get/{log.Id}");
            var updateResponseContent = await updateItemResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<WeightLog>(updateResponseContent);

            Assert.Equal(updateResult.Date, log.Date);
            Assert.Equal(updateResult.Id, log.Id);
        }

        [Fact]
        public async Task Delete_SuccessStatusCode_Deletes()
        {
            var id = Guid.Parse("cb31d06e-13da-4ba0-a923-5c062399f3a8");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.DeleteAsync($"/TrainingLog/Delete/{id}");

            response.EnsureSuccessStatusCode();

            var deleteItemResponse = await client.GetAsync($"/TrainingLog/Get/{id}");

            Assert.Equal(HttpStatusCode.NotFound, deleteItemResponse.StatusCode);
        }
    }
}
