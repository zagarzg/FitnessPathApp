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
    public class FoodLogControllerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GetAll_SuccessStatusCode_GetsAll()
        {
            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync("/FoodLog/GetAll");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<FoodLog>>(content);

            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task GetById_SuccessStatusCode_GetsOne()
        {
            var id = Guid.Parse("da789a67-5481-4ac9-b338-0a9b3c069a1f");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync($"/FoodLog/Get/{id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FoodLog>(content);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_SuccessStatusCode_Creates()
        {
            var log = new FoodLog()
            {
                Id = Guid.Parse("c2bfa824-6882-4c88-9840-1acc0c4be0d9"),
                Date = new DateTime(2022, 2, 25),
                UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(log),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync($"/FoodLog/Create", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FoodLog>(content);

            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Update_EnsureStatusCode_Updates()
        {
            var log = new FoodLog()
            {
                Id = Guid.Parse("da789a67-5481-4ac9-b338-0a9b3c069a1f"),
                Date = new DateTime(2022, 4, 14),
                UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(log),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"/FoodLog/Update", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FoodLog>(content);

            var updateItemResponse = await client.GetAsync($"/FoodLog/Get/{log.Id}");
            var updateResponseContent = await updateItemResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<FoodLog>(updateResponseContent);

            Assert.Equal(updateResult.Date, log.Date);
            Assert.Equal(updateResult.Id, log.Id);
        }

        [Fact]
        public async Task Delete_SuccessStatusCode_Deletes()
        {
            var id = Guid.Parse("da789a67-5481-4ac9-b338-0a9b3c069a1f");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.DeleteAsync($"/FoodLog/Delete/{id}");

            response.EnsureSuccessStatusCode();

            var deleteItemResponse = await client.GetAsync($"/FoodLog/Get/{id}");

            Assert.Equal(HttpStatusCode.NotFound, deleteItemResponse.StatusCode);
        }
    }
}
