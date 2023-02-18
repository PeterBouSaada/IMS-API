using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Classes;
using API.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using API.Classes.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            string key = Configuration.GetSection("JWTTokenData").GetSection("Private_Key").Value;
            services.AddControllers().AddNewtonsoftJson();

            services.AddCors(
                b => b.AddPolicy("policy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            ));

            _ = services.AddAuthentication(x =>
              {
                  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              }).AddJwtBearer(x =>
              {
                  x.RequireHttpsMetadata = false;
                  x.SaveToken = true;
                  x.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });

            services.AddMemoryCache();
            // register Cache as a generic singleton so we dont have to do it once for each model we want to cache.
            services.AddSingleton(typeof(ICache<>), typeof(Cache<>));
            services.AddSingleton<IItemService, ItemService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IJWTAuthenticationService, JWTAuthenticationService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseCors("policy");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
