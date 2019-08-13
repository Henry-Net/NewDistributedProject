using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Wrap;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Resilience
{
    public class ResilienceHttpClient : IHttpClient
    {
        private HttpClient _httpClient;
        //根据URL origin去创建policy
        private readonly Func<string, IEnumerable<AsyncPolicy>> _policCreator;
        //把policy打包成组合 policy wraper 进行本地缓存
        private readonly ConcurrentDictionary<string, AsyncPolicyWrap> _policyWrapper;

        private ILogger<ResilienceHttpClient> _logger;
        private IHttpContextAccessor _httpContextAccessor;

        public ResilienceHttpClient(Func<string, IEnumerable<AsyncPolicy>> policCreator, ILogger<ResilienceHttpClient> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _policCreator = policCreator;
            _policyWrapper = new ConcurrentDictionary<string, AsyncPolicyWrap>();
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 传入对象 以json形式post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="item"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync<T>(string url, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostAsync<T>(HttpMethod.Post, url, item, authorizationToken, requestId, authorizationMethod);
        }
        /// <summary>
        /// 传入Dictionary 以form形式post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> form, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostAsync(HttpMethod.Post, url, form, authorizationToken, requestId, authorizationMethod);
        }


        private Task<HttpResponseMessage> DoPostAsync<T>(HttpMethod method, string url, T item, string authorizationToken=null, string requestId = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Method value must be either post or put", nameof(method));
            }
            var origin = GetOriginFromUrl(url);

            return HttpInvoker(origin, async () =>
            {
                var requestMasssge = new HttpRequestMessage(method, url);
                SetAuthorizationHeader(requestMasssge);

                requestMasssge.Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                if (authorizationToken != null)
                {
                    requestMasssge.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }
                if (requestId != null)
                {
                    requestMasssge.Headers.Add("x-requestid", requestId);
                }
                var response = await _httpClient.SendAsync(requestMasssge);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException();
                }
                return response;
            });
        }

        private Task<HttpResponseMessage> DoPostAsync(HttpMethod method, string url, Dictionary<string, string> form, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Method value must be either post or put", nameof(method));
            }
            var origin = GetOriginFromUrl(url);

            AsyncPolicy a = Policy.Handle<HttpRequestException>().RetryAsync();

            return HttpInvoker(origin, async () =>
            {
                var requestMasssge = new HttpRequestMessage(method, url);
                SetAuthorizationHeader(requestMasssge);

                requestMasssge.Content = new FormUrlEncodedContent(form);

                if (authorizationToken != null)
                {
                    requestMasssge.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
                }
                if (requestId != null)
                {
                    requestMasssge.Headers.Add("x-requestid", requestId);
                }
                var response = await _httpClient.SendAsync(requestMasssge);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException();
                }
                return response;
            });
        }


        private async Task<T> HttpInvoker<T>(string origin, Func<Task<T>> action)
        {
            var normalizeOrigin = NormalizeOrigin(origin);

            if (!_policyWrapper.TryGetValue(normalizeOrigin, out AsyncPolicyWrap policyWrap))
            {
                policyWrap = Policy.WrapAsync(_policCreator(normalizeOrigin).ToArray());
                _policyWrapper.TryAdd(normalizeOrigin, policyWrap);
            }
            return await policyWrap.ExecuteAsync(action);
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
