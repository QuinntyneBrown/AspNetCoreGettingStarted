using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using AspNetCoreGettingStarted.Features.Security;
using AspNetCoreGettingStarted.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace AspNetCoreGettingStarted
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("AspNetCoreGettingStartedContext");

            services.Configure<AuthConfiguration>(Configuration.GetSection("AuthConfiguration"));

            services.AddDbContextPool<AspNetCoreGettingStartedContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = GetTokenValidationParameters();
                });

            services.AddCors(options =>
            options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));
            
            services.AddMediatR(typeof(Startup));
            
            services.AddMemoryCache();
            services.AddTransient<ICache, MemoryCache>();

            services.AddSignalR();
            AddDataStores(services);
            services.AddMvc();
        }

        private TokenValidationParameters GetTokenValidationParameters()
        {
            AuthConfiguration authConfiguration = Configuration.GetSection("AuthConfiguration").Get<AuthConfiguration>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authConfiguration.JwtKey)),
                ValidateIssuer = true,
                ValidIssuer = authConfiguration.JwtIssuer,
                ValidateAudience = true,
                ValidAudience = authConfiguration.JwtAudience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            return tokenValidationParameters;
        }

        public virtual void AddDataStores(IServiceCollection services) {
            services.AddDataStores();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            app.UseMvc();

            app.UseSignalR(routes =>
            {
                routes.MapHub<EventHub>("events");
            });
        }
    }
}
