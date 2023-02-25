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
using API.xUnitTests.Auth;
using API.Classes.Utility;
using MongoDB.Driver.GeoJsonObjectModel;

namespace API.xUnitTests
{

    [TestCaseOrderer("XUnit.Project.Orderers.PriorityOrderer", "XUnit.Project")]
    public class UserController_Tests : IClassFixture<WebApplicationFactory<Program>>
    {
        #region properties
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ITestOutputHelper output;
        private HttpClient client;
        private string? token = "";
        private bool tokenIsValid = false;
        #endregion properties

        #region Helper Classes
        private class tokenResponse
        {
            public string token;

            public tokenResponse()
            {
                token = "";
            }
        }
        #endregion Helper Classes

        #region Constructor
        public UserController_Tests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            output = testOutputHelper;

            client = _factory.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            token = Authorizer.instance.GetToken();
            
            if(token != null)
            {
                tokenIsValid = true;
            }

        }
        #endregion Constructor

        #region Tests
        [Fact, TestPriority(-1)]
        public async void UserController_PostRequestToLoginEndpointAndCheckResponseSuccessStatusCode()
        {
            User user = new User();
            user.username = "testUser";
            user.password = "testPassword";

            string userAsString = user.ToJson();

            var content = new StringContent(userAsString, Encoding.UTF8, "application/json");
            var apiResponse = await client.PostAsync("API/user/Authenticate", content);

            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseValue = await apiResponse.Content.ReadAsStringAsync();
            var deserializedResponseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<String>(responseValue);

            Assert.True(deserializedResponseContent != null);

            tokenResponse? jsonToken = Newtonsoft.Json.JsonConvert.DeserializeObject<tokenResponse>(deserializedResponseContent);

            Assert.True(jsonToken != null);

            Assert.True(jsonToken.token.Length > 0 && jsonToken.token != null);
        }

        [Fact]
        public async void UserController_GetRequestToUsersGetAllEndpointAndCheckResponseSuccessStatus()
        {
            CheckAndSetToken();

            var apiResponse = await client.GetAsync("API/user");

            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseValue = await apiResponse.Content.ReadAsStringAsync();

            Assert.True(responseValue != null);

            User[]? users = Newtonsoft.Json.JsonConvert.DeserializeObject<User[]>(responseValue);
            
            Assert.True(users != null && users.Length > 0);

        }

        [Fact]
        public async void UserController_GetRequestToUsersGetByIdEndpointAndCheckResponseSuccessStatus()
        {
            CheckAndSetToken();

            var id = "63f6d683e53eb32d4e21c079";

            var apiResponse = await client.GetAsync("API/user/" + id);

            Assert.True(apiResponse.IsSuccessStatusCode);
            var responseValue = await apiResponse.Content.ReadAsStringAsync();

            Assert.True(responseValue != null);

            User? user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseValue);

            Assert.True(user != null);
            Assert.True(user.username == "testUser");
            output.WriteLine("user.id = " + user.id);
            output.WriteLine("user.username = " + user.username);
            output.WriteLine("user.password = " + user.password);
            output.WriteLine("user.salt = " + user.salt);
        }


        // TODO: Expand this test to include multiple usernames.
        [Fact]
        public async void UserController_PostRequestToUsersSearchEndpointAndCheckResponseSuccessStatus()
        {
            CheckAndSetToken();

            User requestUser = new User();
            requestUser.username = "testUser";

            var userAsString = requestUser.ToJson();

            var content = new StringContent(userAsString, Encoding.UTF8, "application/json");
            var apiResponse = await client.PostAsync("API/user/search", content);
            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseValue = await apiResponse.Content.ReadAsStringAsync();
            Assert.True(responseValue != null);

            User[]? users = Newtonsoft.Json.JsonConvert.DeserializeObject<User[]>(responseValue);
            Assert.True(users != null);

            User user = users[0];
            Assert.True(user != null);
            Assert.True(user.id != null);
            Assert.True(user.username != null);
            Assert.True(user.password != null);
            Assert.True(user.salt != null);
        }

        #endregion Tests

        #region Helper Functions
        private void CheckAndSetToken()
        {
            Assert.True(tokenIsValid);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        #endregion Helper Functions
    }
};