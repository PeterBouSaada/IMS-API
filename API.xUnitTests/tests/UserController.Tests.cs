using API.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;
using System.Text;
using System.Web.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using API.xUnitTests.Attributes;

namespace API.xUnitTests
{

    [TestCaseOrderer("XUnit.Project.Orderers.PriorityOrderer", "XUnit.Project")]
    public class UserController_Tests : IClassFixture<WebApplicationFactory<Program>>
    {

        private class tokenResponse
        {
            public string token;

            public tokenResponse()
            {
                token = "";
            }
        }

        private readonly WebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper output;
        private HttpClient client;

        private static bool AuthHeaderSet = false;
        private static string token = "";

        public UserController_Tests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            output = testOutputHelper;

            client = _factory.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact, TestPriority(-1)]
        public async void UserController_Test1_PostRequestToLoginEndpointAndCheckResponseSuccessStatusCode()
        {
            User user = new User();
            user.username = "testUser";
            user.password = "testPassword";

            string userAsString = user.ToJson();

            var content = new StringContent(userAsString, Encoding.UTF8, "application/json");

            var apiResponse = await client.PostAsync("API/user/Authenticate", content);
            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseContent = await apiResponse.Content.ReadAsStringAsync();

            var deserializedResponseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<String>(responseContent);

            Assert.True(deserializedResponseContent != null);
            
            tokenResponse? jsonToken = Newtonsoft.Json.JsonConvert.DeserializeObject<tokenResponse>(deserializedResponseContent);
            
            Assert.True(jsonToken != null);
            Assert.True(jsonToken.token.Length > 0 && jsonToken.token != null);
            
            token = jsonToken.token;
            AuthHeaderSet = true;
        }

        [Fact]
        public async void UserController_Test2_GetRequestToUsersGetAllEndpointAndCheckResponseSuccessStatus()
        {
            Assert.True(AuthHeaderSet);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var apiResponse = await client.GetAsync("API/user");

            Assert.True(apiResponse.IsSuccessStatusCode);

            var content = await apiResponse.Content.ReadAsStringAsync();

            Assert.True(content != null);

            User[]? users = Newtonsoft.Json.JsonConvert.DeserializeObject<User[]>(content);
            
            Assert.True(users != null && users.Length > 0);

        }

    }
};