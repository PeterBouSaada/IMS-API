using API.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;
using System.Text;
using System.Net.Http.Headers;
using API.xUnitTests.Auth;


namespace API.xUnitTests.Tests.ItemController
{
    public class ItemController_Tests_Unordered : IClassFixture<WebApplicationFactory<Program>>
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
        public ItemController_Tests_Unordered(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
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

        #region Unordered Tests
        [Fact]
        public async Task ItemController_GetRequestToItemsGetAllEndpointAndCheckResponseSuccessStatus()
        {
            CheckAndSetToken();

            var apiResponse = await client.GetAsync("items");

            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseValue = await apiResponse.Content.ReadAsStringAsync();

            Assert.True(responseValue != null);

            Item[]? Items = Newtonsoft.Json.JsonConvert.DeserializeObject<Item[]>(responseValue);

            Assert.True(Items != null && Items.Length > 0);
            output.WriteLine("Successfully retrieved all items.");
        }

        [Fact]
        public async Task ItemController_GetRequestToItemsGetByIdEndpointAndCheckResponseSuccessStatus()
        {
            CheckAndSetToken();

            var id = "63fbce03a0f6b09d290d7745";

            var apiResponse = await client.GetAsync("items/" + id);

            Assert.True(apiResponse.IsSuccessStatusCode);
            var responseValue = await apiResponse.Content.ReadAsStringAsync();

            Assert.True(responseValue != null);

            Item? Item = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(responseValue);

            Assert.True(Item != null);
            Assert.True(Item.partNumber == "testPartNumber");

            output.WriteLine("Successfully retrieved item by id.");
            output.WriteLine("item.id = " + Item.id);
            output.WriteLine("item.Itemname = " + Item.partNumber);
        }

        [Fact]
        public async Task ItemController_PostRequestToItemsSearchEndpointAndCheckResponseSuccessStatus()
        {
            CheckAndSetToken();

            Item requestItem = new Item();
            requestItem.partNumber = "testPartNumber";

            var ItemAsString = requestItem.ToJson();

            var content = new StringContent(ItemAsString, Encoding.UTF8, "application/json");
            var apiResponse = await client.PostAsync("items/search", content);
            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseValue = await apiResponse.Content.ReadAsStringAsync();
            Assert.True(responseValue != null);

            Item[]? Items = Newtonsoft.Json.JsonConvert.DeserializeObject<Item[]>(responseValue);
            Assert.True(Items != null);

            Item Item = Items[0];
            Assert.True(Item != null);
            Assert.True(Item.id != null);
            Assert.True(Item.partNumber != null);

            output.WriteLine("Successfully retrieved one or more items.");
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