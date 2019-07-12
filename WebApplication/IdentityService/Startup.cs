﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4;
using IdentityService.Services;
using System.Net.Http;

namespace IdentityService
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer()
                .AddExtensionGrantValidator<Authentication.SmsAuthCodeValidator>() //添加IExtensionGrantValidator实现以用于扩展授权。
                .AddDeveloperSigningCredential()
                 .AddInMemoryIdentityResources(Config.GetIdentityResources())
                 .AddInMemoryApiResources(Config.GetApis())
                 .AddInMemoryClients(Config.GetClients());

            services.AddSingleton(new HttpClient());
            services.AddScoped<IValidService, ValidService>();
            services.AddScoped<IUserService, UserService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseMvc();
        }
    }
}
