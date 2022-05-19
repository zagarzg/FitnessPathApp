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
    public class FoodItemControllerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GetAll_SuccessStatusCode_GetsAll()
        {
            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync("/FoodItem/GetAll");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<FoodItem>>(content);

            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetById_SuccessStatusCode_GetsOne()
        {
            var id = Guid.Parse("aac86f05-0ed4-43a1-876f-6ba34f605661");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync($"/FoodItem/Get/{id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FoodItem>(content);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_SuccessStatusCode_Creates()
        {
            var item = new FoodItem()
            {
                Id = Guid.Parse("5f0d3ae0-4405-4e96-b31d-ee0f976154f6"),
                Name = "Hamburger",
                Calories = 550,
                Carbs = 70,
                Protein = 30,
                Fat = 30,
                FoodLogId = Guid.Parse("da789a67-5481-4ac9-b338-0a9b3c069a1f")
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(item),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync($"/FoodItem/Create", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FoodItem>(content);

            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Update_EnsureStatusCode_Updates()
        {
            var item = new FoodItem()
            {
                Id = Guid.Parse("aac86f05-0ed4-43a1-876f-6ba34f605661"),
                Name = "Tuna",
                Calories = 170,
                Carbs = 1,
                Protein = 34,
                Fat = 7,
                FoodLogId = Guid.Parse("da789a67-5481-4ac9-b338-0a9b3c069a1f")
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(item),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"/FoodItem/Update", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FoodItem>(content);

            var updateItemResponse = await client.GetAsync($"/FoodItem/Get/{item.Id}");
            var updateResponseContent = await updateItemResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<FoodItem>(updateResponseContent);

            Assert.Equal(updateResult.Protein, item.Protein);
            Assert.Equal(updateResult.Id, item.Id);
        }

        [Fact]
        public async Task Delete_SuccessStatusCode_Deletes()
        {
            var id = Guid.Parse("aac86f05-0ed4-43a1-876f-6ba34f605661");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.DeleteAsync($"/FoodItem/Delete/{id}");

            response.EnsureSuccessStatusCode();

            var deleteItemResponse = await client.GetAsync($"/FoodItem/Get/{id}");

            Assert.Equal(HttpStatusCode.NotFound, deleteItemResponse.StatusCode);
        }
    }
}
