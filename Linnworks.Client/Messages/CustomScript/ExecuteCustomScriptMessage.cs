using System.Collections.Generic;
using System.Net.Http;

namespace Linnworks.Client.Messages.CustomScript
{
    public class ExecuteCustomScriptMessage : BaseMessage
    {
        public ExecuteCustomScriptMessage(string token, string script) 
            : base(token)
        {
            var parameters = new Dictionary<string, string>
            {
                {nameof(script), script}
            };
            Content = new FormUrlEncodedContent(parameters);
        }

        protected override string Path => "Dashboards/ExecuteCustomScriptQuery";
    }
}