﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Ocelot.Provider.Consul;



namespace API.Gateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //var authenticationProviderKey = "TestKey";
            //Action<IdentityServerAuthenticationOptions> options = o =>
            //{
            //    o.Authority = "http://127.0.0.1:6002";
            //    o.ApiName = "gateway_api";
            //    o.SupportedTokens = SupportedTokens.Both;
            //    o.RequireHttpsMetadata = false;
            //    o.ApiSecret = "secret";
            //};

            //services.AddAuthentication()
            //    .AddIdentityServerAuthentication(authenticationProviderKey,options);

            services.AddOcelot().AddConsul().AddCacheManager(x =>
                {
                    x.WithDictionaryHandle();//默认字典存储
                });
            //services.AddOcelot().AddConsul();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOcelot();
        }
    }
}
