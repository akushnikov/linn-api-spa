using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Linnworks.Client.Messages.Categories
{
    public class DeleteCategoryMessage : BaseMessage
    {
        public DeleteCategoryMessage(string token, Guid categoryId) 
            : base(token)
        {
            var parameters = new Dictionary<string, string>
            {
                {nameof(categoryId), categoryId.ToString()}
            };
            Content = new FormUrlEncodedContent(parameters);
        }

        protected override string Path => "Inventory/DeleteCategoryById";
    }
}