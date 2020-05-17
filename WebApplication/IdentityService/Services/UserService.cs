using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using DnsClient;
using IdentityService.Dtos;
using Microsoft.Extensions.Options;
using Resilience;
using Newtonsoft.Json;

namespace IdentityService.Services
{
    public class UserService : IUserService
    {
        private IHttpClient _httpClient;
        //private readonly string userServiceUrl = "http://localhost:6001";
        private  string userServiceUrl;

        public UserService(IHttpClient httpClient, IOptions<ServiceDisvoveryOptions> options,IDnsQuery dns)
        {
            _httpClient = httpClient;
            var address = dns.ResolveService("service.consul", options.Value.ServiceName);
            var addressList = address.First().AddressList;
            var host = addressList.Any()? addressList.First().ToString():address.First().HostName;
            var port = address.First().Port;
            userServiceUrl = $"http://{host}:{port}";

        }


        public async Task<UserIdentityInfo> CheckOrCreateByPhone(string phone)
        {
            Dictionary<string, string> postForm = new Dictionary<string, string> { { "phone", phone } };

            //var content = new FormUrlEncodedContent(postForm);
            try
            {
                var response = await _httpClient.PostAsync(userServiceUrl + @"/api/Check/GetOrCreateUserByPhone", postForm);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseResult = await response.Content.ReadAsStringAsync();
                    var userIderntityInfo = JsonConvert.DeserializeObject<UserIdentityInfo>(responseResult);
                    return userIderntityInfo;
                }
            }
            catch (Exception)
            {
                var a = "";
                throw;
            }
           

           
            return null;
        }

        public async Task<UserIdentityInfo> CheckByPassword(string name,string password)
        {
            Dictionary<string, string> postForm = new Dictionary<string, string> { { "code", name },{ "password", password } };

            //var content = new FormUrlEncodedContent(postForm);
            try
            {
                var response = await _httpClient.PostAsync(userServiceUrl + @"/api/Check/GetUserByCodeAndPassword", postForm);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseResult = await response.Content.ReadAsStringAsync();
                    var userIderntityInfo = JsonConvert.DeserializeObject<UserIdentityInfo>(responseResult);
                    return userIderntityInfo;
                }
            }
            catch (Exception)
            {
                var a = "";
                throw;
            }



            return null;
        }
    }
}
