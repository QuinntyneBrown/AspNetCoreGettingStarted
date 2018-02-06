using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using AspNetCoreGettingStarted.Features.Security;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http;

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
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            AspNetCoreGettingStarted.Configuration.Options.LoadConfigurationOptions(services, Configuration);
            
            services.AddDbContextPool<AspNetCoreGettingStartedContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
            });
      
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "AspNetCoreGettingStarted",
                    Version = "v1",
                    Description = ".NET Core REST API",
                });
                options.CustomSchemaIds(x => x.FullName);
            });
            
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler
            {
                InboundClaimTypeMap = new Dictionary<string, string>()
            };
        
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.SecurityTokenValidators.Clear();
                    options.SecurityTokenValidators.Add(jwtSecurityTokenHandler);
                    options.TokenValidationParameters = GetTokenValidationParameters();
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if ((context.Request.Path.Value.StartsWith("/events") || (context.Request.Path.Value.StartsWith("/api/digitalassets/serve")))
                                && context.Request.Query.TryGetValue("token", out StringValues token)
                            )
                            {
                                context.Token = token;
                            }

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            var timeoutException = context.Exception;
                            return Task.CompletedTask;
                        }                        
                    };
                });
           
            services.AddMediatR(typeof(Startup));

            services.AddScoped<IMediator, AspNetCoreGettingStartedMediator>();

            services.AddSingleton<IEncryptionService, EncryptionService>();

            services.AddMemoryCache();

            services.AddTransient<ICache, MemoryCache>();

            services.AddSignalR();
            AddDataStores(services);
            services.AddMvc();

        }

        private TokenValidationParameters GetTokenValidationParameters()
        {            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Authentication:JwtKey"])),
                ValidateIssuer = true,
                ValidIssuer = Configuration["Authentication:JwtIssuer"],
                ValidateAudience = true,
                ValidAudience = Configuration["Authentication:JwtAudience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                NameClaimType = JwtRegisteredClaimNames.UniqueName
            };

            return tokenValidationParameters;
        }

        public virtual void AddDataStores(IServiceCollection services) {
            services.AddDataStores();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IEncryptionService encryptionService, AspNetCoreGettingStartedContext context)
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

            app.UseSwagger(); 

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreGettingStarted API V1");
                
            });

            
            if (Configuration["SeedData:Reload"] == "True")
            {
                context.Database.EnsureDeleted();
                DbInitializer.Initialize(context,encryptionService).Wait();
            }            
        }
    }
}
