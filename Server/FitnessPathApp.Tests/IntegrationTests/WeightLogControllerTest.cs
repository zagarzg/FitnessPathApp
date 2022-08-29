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
    public class WeightLogControllerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GetAll_SuccessStatusCode_GetsAll()
        {
            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync("/WeightLog/GetAll");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<WeightLog>>(content);

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task GetById_SuccessStatusCode_GetsOne()
        {
            var id = Guid.Parse("c4714153-23fc-4413-b6dc-7fa230cb8883");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync($"/WeightLog/Get/{id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeightLog>(content);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_SuccessStatusCode_Creates()
        {
            var log = new WeightLog()
            {
                Id = Guid.Parse("eef0a682-ad12-40c6-a2e8-2900c1901185"),
                Value = 80,
                Date = new DateTime(2022, 1, 14),
                UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(log),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync($"/WeightLog/Create", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeightLog>(content);

            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Update_EnsureStatusCode_Updates()
        {
            var log = new WeightLog()
            {
                Id = Guid.Parse("c4714153-23fc-4413-b6dc-7fa230cb8883"),
                Value = 73,
                Date = new DateTime(2022, 1, 14),
                UserId = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2")
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(log),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"/WeightLog/Update", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeightLog>(content);

            var updateItemResponse = await client.GetAsync($"/WeightLog/Get/{log.Id}");
            var updateResponseContent = await updateItemResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<WeightLog>(updateResponseContent);

            Assert.Equal(updateResult.Value, log.Value);
            Assert.Equal(updateResult.Id, log.Id);
        }

        [Fact]
        public async Task Delete_SuccessStatusCode_Deletes()
        {
            var id = Guid.Parse("c4714153-23fc-4413-b6dc-7fa230cb8883");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.DeleteAsync($"/WeightLog/Delete/{id}");

            response.EnsureSuccessStatusCode();

            var deleteItemResponse = await client.GetAsync($"/WeightLog/Get/{id}");

            Assert.Equal(HttpStatusCode.NotFound, deleteItemResponse.StatusCode);
        }
    }
}
