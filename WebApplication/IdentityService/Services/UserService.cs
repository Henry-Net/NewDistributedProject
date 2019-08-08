using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using DnsClient;
using IdentityService.Dtos;
using Microsoft.Extensions.Options;

namespace IdentityService.Services
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        //private readonly string userServiceUrl = "http://localhost:6001";
        private  string userServiceUrl;

        public UserService(HttpClient httpClient, IOptions<ServiceDisvoveryOptions> options,IDnsQuery dns)
        {
            _httpClient = httpClient;
            var address = dns.ResolveService("service.consul", options.Value.ServiceName);
            var addressList = address.First().AddressList;
            var host = addressList.Any()? addressList.First().ToString():address.First().HostName;
            var port = address.First().Port;
            userServiceUrl = $"http://{host}:{port}";

        }


        public async Task<int> CheckOrCreate(string phone)
        {
            int userId = 0;
            Dictionary<string, string> postForm = new Dictionary<string, string> { { "phone", phone } };

            var content = new FormUrlEncodedContent(postForm);
            var response = await _httpClient.PostAsync(userServiceUrl + @"/api/Values/GetOrCreat", content);

            if (response.StatusCode==HttpStatusCode.OK)
            {
                var responseResult = await response.Content.ReadAsStringAsync();
                int.TryParse(responseResult, out userId);
                return userId;
            }
            return userId;
        }
    }
}
