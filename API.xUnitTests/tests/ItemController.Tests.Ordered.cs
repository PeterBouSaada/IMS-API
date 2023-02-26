using API.Models;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;
using System.Text;
using System.Net.Http.Headers;
using API.xUnitTests.Attributes;
using API.xUnitTests.Auth;


namespace API.xUnitTests.Tests.ItemController
{
    public class ItemControllerFixture
    {
        public bool addItemRan = false;
        public bool updateItemRan = false;
        public bool deleteItemRan = false;

        public string? newItemId = null;
    };

    [TestCaseOrderer("API.xUnitTests.Orderers.PriorityOrderer", "API.xUnitTests")]
    public class ItemController_Tests_Ordered : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<ItemControllerFixture>
    {
        #region properties
        private readonly WebApplicationFactory<Program> _factory;
        private readonly ItemControllerFixture _fixture;
        private readonly ITestOutputHelper output;
        private HttpClient client;
        private string? token = "";
        private bool tokenIsValid = false;
        #endregion properties

        #region Constructor
        public ItemController_Tests_Ordered(WebApplicationFactory<Program> factory, ItemControllerFixture fixture, ITestOutputHelper testOutputHelper)
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
        public async Task ItemController_Test1_PostRequestToItemsAddEndpointAndCheckResponseStatus()
        {
            CheckAndSetToken();

            _fixture.addItemRan = true;
            Assert.False(_fixture.updateItemRan);
            Assert.False(_fixture.deleteItemRan);

            Item requestItem = new Item();
            requestItem.partNumber = "TestAddedItem_ForTestingOnly";

            var ItemAsString = requestItem.ToJson();

            var content = new StringContent(ItemAsString, Encoding.UTF8, "application/json");
            var apiResponse = await client.PostAsync("items/add", content);
            Assert.True(apiResponse.IsSuccessStatusCode);

            var responseValue = await apiResponse.Content.ReadAsStringAsync();
            Assert.True(responseValue != null);

            Item? Item = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(responseValue);
            output.WriteLine("Successfully created item:");
            printItem(Item);

            Assert.True(Item.id != null);
            _fixture.newItemId = Item.id;
            Assert.True(_fixture.newItemId != null);

        }

        [Fact, TestPriority(2)]
        public async Task ItemController_Test2_PutRequestToItemsUpdateEndpointAndCheckResponseStatus()
        {

            CheckAndSetToken();

            _fixture.updateItemRan = true;
            Assert.True(_fixture.addItemRan);
            Assert.False(_fixture.deleteItemRan);

            var Itemname = "TestAddedItem_ForTestingOnly_Updated";
            var ItemAsString = "{ \"id\": \"" + _fixture.newItemId + "\", \"part_number\": \"" + Itemname + "\" }";
            var content = new StringContent(ItemAsString, Encoding.UTF8, "application/json");

            var apiResponse = await client.PutAsync("items/" + _fixture.newItemId, content);
            var responseValue = await apiResponse.Content.ReadAsStringAsync();

            output.WriteLine(apiResponse.ToString());
            output.WriteLine(responseValue);
            
            Assert.True(apiResponse.IsSuccessStatusCode);

            Assert.True(responseValue != null);

            Item? Item = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(responseValue);
            output.WriteLine("Successfully updated Item:");
            printItem(Item);

        }

        [Fact, TestPriority(3)]
        public async Task ItemController_Test3_DeleteRequestToItemsDeleteEndpointAndCheckResponseStatus()
        {
            CheckAndSetToken();

            _fixture.deleteItemRan = true;
            Assert.True(_fixture.addItemRan);
            Assert.True(_fixture.updateItemRan);

            var apiResponse = await client.DeleteAsync("items/" + _fixture.newItemId);
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

        private void printItem(Item? Item)
        {

            Assert.True(Item.id != null);
            Assert.True(Item.partNumber != null);

            output.WriteLine("item.id = " + Item.id);
            output.WriteLine("item.Itemname = " + Item.partNumber);
        }
        #endregion Helper Functions
    }
};