using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreAngularShop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetCoreAngularShop.Controllers.Tests
{
    [TestClass()]
    public class AccountGuestControllerTests
    {
        [TestMethod()]
        public void PostTest()
        {
Assert.Fail();
        }
        [Fact]
        public async Task CanCreateAccount()
        {
            var httpResponse = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "/api/accounts")
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(_signupRequests[0]), Encoding.UTF8, "application/json")
            });

            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<SignupResponse>(stringResponse);
            Assert.Equal(_signupRequests[0].FullName, response.FullName);
            Assert.Equal(_signupRequests[0].Email, response.Email);
            Assert.Equal(_signupRequests[0].Role, response.Role);
            Assert.True(Guid.TryParse(response.Id, out _));
        }
    }
}