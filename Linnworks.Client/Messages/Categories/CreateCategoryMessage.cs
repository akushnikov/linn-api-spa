using System.Collections.Generic;
using System.Net.Http;

namespace Linnworks.Client.Messages.Categories
{
    public class CreateCategoryMessage : BaseMessage
    {
        public CreateCategoryMessage(string token, string categoryName) 
            : base(token)
        {
            var parameters = new Dictionary<string, string>
            {
                {nameof(categoryName), categoryName}
            };
            Content = new FormUrlEncodedContent(parameters);
        }

        protected override string Path => "Inventory/CreateCategory";
    }
}