using System.Collections.Generic;
using System.Net.Http;
using Linnworks.Contract.Entities;
using Newtonsoft.Json;

namespace Linnworks.Client.Messages.Categories
{
    public class UpdateCategoryMessage : BaseMessage
    {
        public UpdateCategoryMessage(string token, Category category)
            : base(token)
        {
            var parameters = new Dictionary<string, string>
            {
                {nameof(category), JsonConvert.SerializeObject(category)}
            };
            Content = new FormUrlEncodedContent(parameters);
        }

        protected override string Path => "Inventory/UpdateCategory";
    }
}