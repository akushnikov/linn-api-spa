using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Linnworks.Contract.Entities;

namespace Linnworks.Client.Interfaces
{
    public interface IApiClient
    {
        Task<ICollection<Category>> GetCategoriesAsync();
        Task<Category> CreateCategoryAsync(string name);
        Task DeleteCategoryAsync(Guid id);
        Task UpdateCategoryAsync(Category category);
        Task<CustomScriptResult> ExecuteCustomScriptAsync(string script);
        Task<CustomScriptResult<T>> ExecuteCustomScriptAsync<T>(string script);
        Task<string> GetNewItemNumber();
    }
}