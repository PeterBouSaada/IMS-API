using API.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;
using System.Text;
using System.Net.Http.Headers;
using API.xUnitTests.Attributes;
using API.xUnitTests.Auth;


namespace API.xUnitTests.Tests.UserController
{
    public class UserControllerFixture 
    {
        public bool addUserRan = false;
        public bool updateUserRan = false;
        public bool deleteUserRan = false;

        public string? newUserId = null;
    };

    [TestCaseOrderer("API.xUnitTests.Orderers.PriorityOrderer", "API.xUnitTests")]
    public class UserController_Tests_Ordered : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<UserControllerFixture>
    {
        #region properties
        private readonly WebApplicationFactory<Program> _factory;
        private readonly UserControllerFixture _fixture;
        private readonly ITestOutputHelper output;
        private HttpClient client;
        private string? token = "";
        private bool tokenIsValid = false;
        #endregion properties

        #region Constructor
        public UserController_Tests_Ordered(WebApplicationFactory<Program> factory, UserControllerFixture fixture, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _fixture = fixture;

            output = testOutputHelper;

            client = _factory.CreateClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            token = Authorizer.instance.GetToken();

            if (token != null)
            {
                tokenIsValid = true;
            }

        }
        #endregion Constructor

        #region Ordered Tests
        [Fact, TestPriority(1)]
        public async Task UserController_Test1_PostRequestToUsersAddEndpointAndCheckResponseStatus()
        {
            CheckAndSetToken();

            _fixture.addUserRan = true;
            Assert.False(_fixture.updateUserRan);
            Assert.False(_fixture.deleteUserRan);

            User requestUser = new User();
            requestUser.username = "TestAddedUser_ForTestingOnly";
            requestUser.password = "TestAddedPassword_ForTestingOnly";

            var userAsString = requestUser.ToJson();

            var content = new StringContent(userAsString, Encoding.UTF8, "application/json");
            var apiResponse = await client.PostAsync("users/add", content);
            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseValue = await apiResponse.Content.ReadAsStringAsync();
            Assert.True(responseValue != null);

            User? user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseValue);
            output.WriteLine("Successfully created user:");
            printUser(user);

            Assert.True(user != null);

            Assert.True(user.id != null);
            _fixture.newUserId = user.id;
            Assert.True(_fixture.newUserId != null);

        }

        [Fact, TestPriority(2)]
        public async Task UserController_Test2_PutRequestToUsersUpdateEndpointAndCheckResponseStatus()
        {

            CheckAndSetToken();

            _fixture.updateUserRan = true;
            Assert.True(_fixture.addUserRan);
            Assert.False(_fixture.deleteUserRan);

            var username = "TestAddedUser_ForTestingOnly_Updated";
            var userAsString = "{ \"id\": \"" + _fixture.newUserId + "\", \"username\": \"" + username + "\" }";
            var content = new StringContent(userAsString, Encoding.UTF8, "application/json");
            
            var apiResponse = await client.PutAsync("users/" + _fixture.newUserId, content);
            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseValue = await apiResponse.Content.ReadAsStringAsync();
            Assert.True(responseValue != null);

            User? user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseValue);
            output.WriteLine("Successfully updated user:");
            printUser(user);

        }

        [Fact, TestPriority(3)]
        public async Task UserController_Test3_DeleteRequestToUsersDeleteEndpointAndCheckResponseStatus()
        {
            CheckAndSetToken();

            _fixture.deleteUserRan = true;
            Assert.True(_fixture.addUserRan);
            Assert.True(_fixture.updateUserRan);

            var apiResponse = await client.DeleteAsync("users/" + _fixture.newUserId);
            Assert.True(apiResponse.IsSuccessStatusCode);
            output.WriteLine("Successfully Deleted.");
        }

        #endregion Ordered Tests

        #region Helper Functions
        private void CheckAndSetToken()
        {
            Assert.True(tokenIsValid);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        private void printUser(User? user)
        {
            Assert.True(user != null);
            Assert.True(user.id != null);
            Assert.True(user.username != null);
            Assert.True(user.password != null);
            Assert.True(user.salt != null);

            output.WriteLine("user.id = " + user.id);
            output.WriteLine("user.username = " + user.username);
            output.WriteLine("user.password = " + user.password);
            output.WriteLine("user.salt = " + user.salt);
        }
        #endregion Helper Functions
    }
};