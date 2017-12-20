using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Linnworks.Client.Core;
using Linnworks.Client.Exceptions;
using Linnworks.Client.Messages.Categories;
using Newtonsoft.Json;
using Xunit;

namespace Linnworks.Tests
{
    public class ApiClientTest
    {
        public ApiClientTest()
        {
            Token = "ec184fa2-1bc5-4c87-a82d-705fb0ee692f";
        }
        
        private class CategoryResponse
        {
            [JsonProperty("CategoryId")]
            public Guid Id { get; set; }
            [JsonProperty("StockItemCount")]
            public int Count { get; set; }
        }

        private string Token { get; }
        
        [Fact]
        public async Task Test1()
        {
            var client = new HttpClient();
            var res = await client.SendAsync(new GetCategoriesMessage(Token));
            Assert.NotNull(res);
        }

        [Fact]
        public async Task Test2()
        {
            var client = new LinnworksClient(new HttpClient(), Token);
            var categories = await client.GetCategoriesAsync();
            Assert.NotEmpty(categories);
        }
        
        [Fact]
        public async Task Test3()
        {
            var client = new LinnworksClient(new HttpClient(), Token);
            var category = await client.CreateCategoryAsync(Guid.NewGuid().ToString());
            Assert.NotNull(category);
        }

        [Fact]
        public async Task Test4()
        {
            var client = new LinnworksClient(new HttpClient(), Token);
            var script = @"SELECT p.CategoryId, Count(*) as StockItemCount
FROM StockItem AS S
INNER JOIN ProductCategories AS P ON P.CategoryId = S.CategoryId
GROUP BY P.CategoryId";
            var result = await client.ExecuteCustomScriptAsync<CategoryResponse>(script);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Test5()
        {
            var client = new LinnworksClient(new HttpClient(), Token);
            var number = await client.GetNewItemNumber();
            Assert.NotEmpty(number);
        }

        [Fact]
        public async Task Test7()
        {
            var client = new LinnworksClient(new HttpClient(), Token);
            
            var category = await client.CreateCategoryAsync("Test category");
            Assert.NotNull(category);
            
            category.Name = "Updated test";
            var updateException = await Record.ExceptionAsync(async () => await client.UpdateCategoryAsync(category));
            Assert.Null(updateException);

            var deleteResult = await Record.ExceptionAsync(async () => await client.DeleteCategoryAsync(category.Id));
            Assert.Null(deleteResult);

            var categories = await client.GetCategoriesAsync();
            Assert.NotEmpty(categories);

            var deleted = categories.SingleOrDefault(item => item.Name == category.Name);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task Test8()
        {
            var client = new LinnworksClient(new HttpClient(), Guid.NewGuid().ToString());
            var script = @"SELECT 1";
            var ex = await Record.ExceptionAsync(async () => await client.ExecuteCustomScriptAsync(script));
            Assert.IsAssignableFrom<ApiException>(ex);
        }
    }
}