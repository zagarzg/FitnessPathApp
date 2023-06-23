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
    public class UserControllerTest : IntegrationTestBase
    {
        [Fact]
        public async Task GetAll_SuccessStatusCode_GetsAll()
        {
            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync("/User/GetAll");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<User>>(content);

            Assert.Single(result);
        }

        [Fact]
        public async Task GetById_SuccessStatusCode_GetsOne()
        {
            var id = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync($"/User/Get/{id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<User>(content);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_SuccessStatusCode_Creates()
        {
            var user = new User
            {
                Id = Guid.Parse("d7751f5b-297a-442d-802d-2280c94de92e"),
                Username = "admintwo",
                Password = BCrypt.Net.BCrypt.HashPassword("admintwo"),
                Email = "admintwo@hotmail.com"
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync($"/User/Create", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<User>(content);

            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact]
        public async Task Update_EnsureStatusCode_Updates()
        {
            var user = new User
            {
                Id = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2"),
                Username = "adminupdate",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                Email = "admin@hotmail.com"
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync($"/User/Update", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<User>(content);

            var updateUserResponse = await client.GetAsync($"/User/Get/{user.Id}");
            var updateResponseContent = await updateUserResponse.Content.ReadAsStringAsync();
            var updateResult = JsonConvert.DeserializeObject<User>(updateResponseContent);

            Assert.Equal(updateResult.Username, user.Username);
            Assert.Equal(updateResult.Id, user.Id);
        }

        [Fact]
        public async Task Delete_SuccessStatusCode_Deletes()
        {
            var id = Guid.Parse("cd6b8714-4806-4fe6-b28f-90185dbfbdd2");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.DeleteAsync($"/User/Delete/{id}");

            response.EnsureSuccessStatusCode();

            var deleteItemResponse = await client.GetAsync($"/User/Get/{id}");

            Assert.Equal(HttpStatusCode.NotFound, deleteItemResponse.StatusCode);
        }
    }
}
