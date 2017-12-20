using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Linnworks.Client.Core;

namespace Linnworks.Client.Messages
{
    public abstract class BaseMessage : HttpRequestMessage
    {
        protected BaseMessage(string token)
        {
            Method = HttpMethod.Post;
            RequestUri = new Uri($"https://api.linnworks.net/api/{Path}");
            Headers.Authorization = new AuthenticationHeaderValue(token);
            Headers.Connection.Add("keep-alive");
            Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(CustomMediaTypeNames.Application.Json));
            Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(CustomMediaTypeNames.Text.Javascript));
            Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(CustomMediaTypeNames.Anything, 0.01));
            Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
            Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        }
        
        protected abstract string Path { get; }
    }
}