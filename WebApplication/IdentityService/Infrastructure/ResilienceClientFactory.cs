using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Resilience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure
{
    public class ResilienceClientFactory
    {

        private ILogger<ResilienceHttpClient> _logger;
        private IHttpContextAccessor _httpContextAccessor;

        private int _retryCount;

        private int _exceptionBeforeBreakingCount;

        public ResilienceClientFactory(ILogger<ResilienceHttpClient> logger, IHttpContextAccessor httpContextAccessor,
            int retryCount, int exceptionBeforeBreakingCount)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _retryCount = retryCount;
            _exceptionBeforeBreakingCount = exceptionBeforeBreakingCount;
        }

        public ResilienceHttpClient GetResilienceHttpClient() =>
            new ResilienceHttpClient(origin=>CreatePolicy(origin),_logger,_httpContextAccessor);

        private AsyncPolicy[] CreatePolicy(string origin)
        {
            return new AsyncPolicy[] {
                Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(_retryCount,
                retryAttempt=>TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)),
                (exception,timespan,retryCount,context)=>
                {
                    var msg = $"第{retryCount}次重试"+
                        $"of{context.PolicyKey} "+
                        $"due to {exception} .";
                    _logger.LogWarning(msg);
                    //_logger.LogDebug(msg);
                }
                ),
                 Policy.Handle<HttpRequestException>()
                 .CircuitBreakerAsync(_exceptionBeforeBreakingCount,
                 TimeSpan.FromMinutes(1),
                 (exception,durntion)=>
                 {
                     _logger.LogWarning("熔断器打开");
                 },()=>{
                     _logger.LogWarning("熔断器关闭");
                 }
                 )
            };

        }



    }
}
