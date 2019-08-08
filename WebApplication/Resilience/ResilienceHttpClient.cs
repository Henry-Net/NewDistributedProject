using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Polly;
using Polly.Wrap;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;

namespace Resilience
{
    public class ResilienceHttpClient : IHttpClient
    {
        private HttpClient _httpClient;
        //根据URL origin去创建policy
        private readonly Func<string, IEnumerable<Policy>> _policCreator;
        //把policy打包成组合 policy wraper 进行本地缓存
        private readonly ConcurrentDictionary<string, PolicyWrap> _policyWrapper;

        private ILogger<ResilienceHttpClient> _logger;
        private IHttpContextAccessor _httpContextAccessor;

        public ResilienceHttpClient(Func<string, IEnumerable<Policy>> policCreator, ILogger<ResilienceHttpClient> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _policCreator = policCreator;
            _policyWrapper = new ConcurrentDictionary<string, PolicyWrap>();
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<HttpResponseMessage> DoPostAsync<T>( HttpMethod method,string url, T item, string authorizationToken, string requestId = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Method value must be either post or put",nameof(method));
            }
            var origin = GetOriginFromUrl(url);

            return HttpInvoker(origin,async () => 
            {
                var requestMasssge = new HttpRequestMessage(method, url);
                SetAuthorizationHeader(requestMasssge);

                requestMasssge.Content = new StringContent(JsonConvert.SerializeObject(item),Encoding.UTF8,"application/json");

                if (authorizationToken!=null)
                {
                    requestMasssge.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }
                if (requestId!=null)
                {
                    requestMasssge.Headers.Add("x-requestid", requestId);
                }
                var response = await _httpClient.SendAsync(requestMasssge);

                if (response.StatusCode== HttpStatusCode.InternalServerError)
                {
                    throw new HttpRequestException();
                }
                return response;
            });
        }



        private async Task<T> HttpInvoker<T>(string origin, Func<Task<T>> action)
        {
            var normalizeOrigin = NormalizeOrigin(origin);

            if (!_policyWrapper.TryGetValue(normalizeOrigin, out PolicyWrap policyWrap))
            {
                policyWrap = Policy.Wrap(_policCreator(normalizeOrigin).ToArray());
                _policyWrapper.TryAdd(normalizeOrigin, policyWrap);
            }
            return await policyWrap.Execute(action);
        }


        private static string NormalizeOrigin(string origin)
        {
            return origin.Trim()?.ToLower();
        }
        
        private static string GetOriginFromUrl(string uri)
        {
            var url = new Uri(uri);
            var origin = $"{url.Scheme}://{url.Host}:{url.Port}";
            return origin;
        }

        private void SetAuthorizationHeader(HttpRequestMessage requestMessage)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrWhiteSpace(authorizationHeader))
            {
                requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
        }


    }
}
