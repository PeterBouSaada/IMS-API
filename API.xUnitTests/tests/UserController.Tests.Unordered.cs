using API.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;
using System.Text;
using System.Net.Http.Headers;
using API.xUnitTests.Auth;


namespace API.xUnitTests.Tests.UserController
{
    public class UserController_Tests_Unordered : IClassFixture<WebApplicationFactory<Program>>
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
        public UserController_Tests_Unordered(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
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

        #region Unordered Tests
        [Fact]
        public async void UserController_PostRequestToLoginEndpointAndCheckResponseSuccessStatusCode()
        {
            User user = new User();
            user.username = "testUser";
            user.password = "testPassword";

            string userAsString = user.ToJson();

            var content = new StringContent(userAsString, Encoding.UTF8, "application/json");
            var apiResponse = await client.PostAsync("users/Authenticate", content);

            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseValue = await apiResponse.Content.ReadAsStringAsync();
            var deserializedResponseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<String>(responseValue);

            Assert.True(deserializedResponseContent != null);

            tokenResponse? jsonToken = Newtonsoft.Json.JsonConvert.DeserializeObject<tokenResponse>(deserializedResponseContent);

            Assert.True(jsonToken != null);

            Assert.True(jsonToken.token.Length > 0 && jsonToken.token != null);
            output.WriteLine("Successfully logged in using test account.");
        }

        [Fact]
        public async void UserController_GetRequestToUsersGetAllEndpointAndCheckResponseSuccessStatus()
        {
            CheckAndSetToken();

            var apiResponse = await client.GetAsync("users");

            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseValue = await apiResponse.Content.ReadAsStringAsync();

            Assert.True(responseValue != null);

            User[]? users = Newtonsoft.Json.JsonConvert.DeserializeObject<User[]>(responseValue);
            
            Assert.True(users != null && users.Length > 0);
            output.WriteLine("Successfully retrieved all users.");
        }

        [Fact]
        public async void UserController_GetRequestToUsersGetByIdEndpointAndCheckResponseSuccessStatus()
        {
            CheckAndSetToken();

            var id = "63fa98e9f500820aae780272";

            var apiResponse = await client.GetAsync("users/" + id);

            Assert.True(apiResponse.IsSuccessStatusCode);
            var responseValue = await apiResponse.Content.ReadAsStringAsync();

            Assert.True(responseValue != null);

            User? user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseValue);

            Assert.True(user != null);
            Assert.True(user.username == "testUser");

            output.WriteLine("Successfully retrieved user by id.");
            output.WriteLine("user.id = " + user.id);
            output.WriteLine("user.username = " + user.username);
            output.WriteLine("user.password = " + user.password);
            output.WriteLine("user.salt = " + user.salt);
        }

        [Fact]
        public async void UserController_PostRequestToUsersSearchEndpointAndCheckResponseSuccessStatus()
        {
            CheckAndSetToken();

            User requestUser = new User();
            requestUser.username = "testUser";

            var userAsString = requestUser.ToJson();

            var content = new StringContent(userAsString, Encoding.UTF8, "application/json");
            var apiResponse = await client.PostAsync("users/search", content);
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
            output.WriteLine("Successfully retrieved one or more users.");
        }
        #endregion Unordered Tests

        #region Helper Functions
        private void CheckAndSetToken()
        {
            Assert.True(tokenIsValid);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        #endregion Helper Functions
    }
};