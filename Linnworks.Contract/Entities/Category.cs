using System;
using Newtonsoft.Json;

namespace Linnworks.Contract.Entities
{
    public class Category
    {
        [JsonProperty("CategoryId")]
        public Guid Id { get; set; }
        [JsonProperty("CategoryName")]
        public string Name { get; set; }
    }
}