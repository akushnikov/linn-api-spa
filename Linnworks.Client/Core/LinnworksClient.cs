using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Linnworks.Client.Exceptions;
using Linnworks.Client.Interfaces;
using Linnworks.Client.Messages.Categories;
using Linnworks.Client.Messages.CustomScript;
using Linnworks.Client.Messages.Inventory;
using Linnworks.Contract.Entities;

namespace Linnworks.Client.Core
{
    public class LinnworksClient : IApiClient
    {
        public LinnworksClient(HttpClient client, string token)
        {
            HttpClient = client;
            Token = token;
        }
        
        private HttpClient HttpClient { get; }
        private string Token { get; }

        private async Task<HttpResponseMessage> Execute(HttpRequestMessage message)
        {
            var response = await HttpClient.SendAsync(message);
            if (response.IsSuccessStatusCode) return response;
            
            var error = await response.Content.ReadAsAsync<ErrorMessage>();
            throw new ApiException(error);
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            var message = new GetCategoriesMessage(Token);
            var response = await Execute(message);
            var result = await response.Content.ReadAsAsync<List<Category>>();
            return result;
        }

        public async Task<Category> CreateCategoryAsync(string name)
        {
            var message = new CreateCategoryMessage(Token, name);
            var response = await Execute(message);
            var result = await response.Content.ReadAsAsync<Category>();
            return result;
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var message = new DeleteCategoryMessage(Token, id);
            await Execute(message);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var message = new UpdateCategoryMessage(Token, category);
            await Execute(message);
        }

        public async Task<CustomScriptResult> ExecuteCustomScriptAsync(string script)
        {
            var message = new ExecuteCustomScriptMessage(Token, script);
            var response = await Execute(message);
            var result = await response.Content.ReadAsAsync<CustomScriptResult>();
            return result;
        }
        
        public async Task<CustomScriptResult<T>> ExecuteCustomScriptAsync<T>(string script)
        {
            var message = new ExecuteCustomScriptMessage(Token, script);
            var response = await Execute(message);
            var result = await response.Content.ReadAsAsync<CustomScriptResult<T>>();
            if (result.IsError)
                throw new ApiException(result.ErrorMessage);
            return result;
        }

        public async Task<string> GetNewItemNumber()
        {
            var message = new GetNewItemNumberMessage(Token);
            var response = await Execute(message);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}