using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace IdentityService.Services
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        private readonly string userServiceUrl = "http://localhost:6001";

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
