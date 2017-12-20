using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Linnworks.API.Base;
using Linnworks.Client.Interfaces;
using Linnworks.Contract.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Linnworks.API.Controllers
{
    public class CategoryController : ApiController
    {
        public CategoryController(ILogger<CategoryController> logger, IApiClient client)
            : base(logger)
        {
            ApiClient = client;
        }

        private const string CustomQuery = @"SELECT p.CategoryId, Count(*) as StockItemCount
FROM StockItem AS S
INNER JOIN ProductCategories AS P ON P.CategoryId = S.CategoryId
GROUP BY P.CategoryId";

        private IApiClient ApiClient { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categoryTask = ApiClient.GetCategoriesAsync();
            var customQueryTask = ApiClient.ExecuteCustomScriptAsync<StockItemCount>(CustomQuery);

            await Task.WhenAll(categoryTask, customQueryTask);

            var (categories, custom) = (categoryTask.Result, customQueryTask.Result);

            var entities = (from cat in categories
                            join cust in custom.Results
                                on cat.Id equals cust.CategoryId
                                into customSet
                            from cust in customSet.DefaultIfEmpty()
                            select new JoinedCategory
                            {
                                Id = cat.Id,
                                Name = cat.Name,
                                Count = cust?.Count ?? 0
                            }).ToList();

            return Ok(new
            {
                success = true,
                data = entities
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entities = await ApiClient.GetCategoriesAsync();
            var entity = entities.SingleOrDefault(item => item.Id == id);
            return Ok(new
            {
                success = true,
                data = entity
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name)
        {
            var category = await ApiClient.CreateCategoryAsync(name);
            return Ok(new
            {
                success = true,
                data = category
            });
        }

        [HttpPut]
        public async Task<IActionResult> Put(Category category)
        {
            await ApiClient.UpdateCategoryAsync(category);
            return Ok(new {success = true});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await ApiClient.DeleteCategoryAsync(id);
            return Ok(new {success = true});
        }
    }
}