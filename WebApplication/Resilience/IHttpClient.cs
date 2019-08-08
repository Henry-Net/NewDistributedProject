﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Resilience
{
    public interface IHttpClient
    {
         Task<HttpResponseMessage> DoPostAsync<T>(HttpMethod method , string url, T item, string authorizationToken, string requestId = null, string authorizationMethod = "Bearer");
    }
}