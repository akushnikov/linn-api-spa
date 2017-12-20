using System;
using Newtonsoft.Json;

namespace Linnworks.Contract.Entities
{
    public class StockItemCount
    {
        public Guid CategoryId { get; set; }
        [JsonProperty("StockItemCount")]
        public int Count { get; set; }
    }
}