using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using AspNetCoreGettingStarted.Features.Security;
using AspNetCoreGettingStarted.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            var connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=AspNetCoreGettingStarted;Integrated Security=SSPI;";

            services.AddDbContextPool<AspNetCoreGettingStartedContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.Configure<AuthConfiguration>(Configuration.GetSection("AuthConfiguration"));

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
