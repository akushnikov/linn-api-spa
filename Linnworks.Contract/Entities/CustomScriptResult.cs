using System.Collections.Generic;

namespace Linnworks.Contract.Entities
{
    public class CustomScriptResult<T>
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public long TotalResults { get; set; }
        
        public IList<CustomScriptColumn> Columns { get; set; }
        public IList<T> Results { get; set; }
    }
    
    public class CustomScriptResult : CustomScriptResult<Dictionary<string, object>>
    {
        
    }
}