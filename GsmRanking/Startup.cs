using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GsmRanking.Common;
using GsmRanking.Common.Authorization;
using GsmRanking.Common.Enums;
using GsmRanking.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GsmRanking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace GsmRanking
{
    public class Startup
    {
        public const string AuthenticationScheme = "GsmRankingAuthentication";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            // Add framework services.

            //Add EF DbContext
            services.AddMvc();

            var connection = Configuration.GetConnectionString("Database");
            services.AddDbContext<GsmRankingContext>(options => options.UseSqlServer(connection));

            //Dependency injection mapping
            services.AddTransient<INewsService, NewsService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAuthorization(x =>
            {
                x.AddPolicy(Policies.Editor, builder => builder.RequireAssertion(y => 
                    y.User.IsInRole(UserTypeEnum.Editor.ToString()) || y.User.IsInRole(UserTypeEnum.Admin.ToString()))
                );
                x.AddPolicy(Policies.Admin, builder => builder.RequireAssertion(y =>
                    y.User.IsInRole(UserTypeEnum.Admin.ToString()))
                );
                x.DefaultPolicy = new AuthorizationPolicyBuilder(AuthenticationScheme)
                    .AddRequirements(new AssertionRequirement(y=>y.User.IsLoggedIn()))
                    .Build();
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.CookieHttpOnly = true;
            });
            services.AddAutoMapper();
            services.AddAntiforgery();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = AuthenticationScheme,
                LoginPath = new PathString("/Account/Unauthorized/"),
                AccessDeniedPath = new PathString("/Account/Forbidden/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                ReturnUrlParameter = "returnUrlParameter",
                CookieSecure = CookieSecurePolicy.SameAsRequest
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
