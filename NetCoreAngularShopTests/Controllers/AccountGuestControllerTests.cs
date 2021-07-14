using Infrastructure.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngularCoreShop.Controllers;
using AngularCoreShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Assert = Xunit.Assert;

namespace AngularCoreShop.Controllers.Tests
{
    [Collection("WebHost collection")]
    public class AccountGuestControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public AccountGuestControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

       // public AccountGuestControllerTests(AuthServerWebApplicationFactory<TestStartup, AppIdentityDbContext> factory, WebHostFixture webHostFixture)
        private IList<SignInRequestModel> requests = new List<SignInRequestModel> {
            new SignInRequestModel(){FirstName="Kamil",LastName="Nowak",Email="another@gmail.com",Password="SomePassword",Role="Customer" },
            new SignInRequestModel(){FirstName="Magda",LastName="Sonak",Email="another12@gmail.com",Password="SomePassword",Role="Employee" }};
        [Fact]
        public async Task CanCreateAccount()
        {
         
            var client = _factory.CreateClient();
            var httpResponse = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/accountguest")
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requests[0]), Encoding.UTF8, "application/json")
            });
          //  httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<SignInResponseModel>(stringResponse);
            Assert.Equal(requests[0].FirstName, response.FirstName);
            Assert.Equal(requests[0].Email, response.Email);
            Assert.Equal(requests[0].Role, response.Role);
            Assert.Equal(requests[0].LastName, response.LastName);
          //  Assert.Equal(requests[0].LastName, response.Email);
            Assert.True(Guid.TryParse(response.Id, out _));
        }
    }
}