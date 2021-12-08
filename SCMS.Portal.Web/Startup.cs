//-----------------------------------------------------------------------
//Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
//-----------------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RESTFulSense.Clients;
using SCMS.Portal.Web.Brokers.Apis;
using SCMS.Portal.Web.Brokers.DateTimes;
using SCMS.Portal.Web.Brokers.Loggings;
using SCMS.Portal.Web.Brokers.Navigations;
using SCMS.Portal.Web.Models.Configurations;
using SCMS.Portal.Web.Services.Foundations.Students;
using SCMS.Portal.Web.Services.Foundations.Users;
using SCMS.Portal.Web.Services.Views.StudentViews;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

namespace SCMS.Portal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSyncfusionBlazor();
            AddHttpClient(services);
            AddRootDirectory(services);
            AddBrokers(services);
            AddServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            AddSyncfusionLicense();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void AddSyncfusionLicense()
        {
            SyncfusionLicenseProvider.RegisterLicense(
                Environment.GetEnvironmentVariable("SYNCFUSION_LICENSE_KEY"));
        }

        private IHttpClientBuilder AddHttpClient(IServiceCollection services)
        {
            return services.AddHttpClient<IRESTFulApiFactoryClient, RESTFulApiFactoryClient>(client =>
            {
                LocalConfigurations localConfigurations = Configuration.Get<LocalConfigurations>();
                string apiUrl = localConfigurations.ApiConfigurations.Url;
                client.BaseAddress = new Uri(apiUrl);
            });
        }

        private static void AddRootDirectory(IServiceCollection services) =>
            services.AddRazorPages(options =>
            options.RootDirectory = "/Views/Pages");

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddScoped<IApiBroker, ApiBroker>();
            services.AddScoped<ILogger, Logger<LoggingBroker>>();
            services.AddScoped<ILoggingBroker, LoggingBroker>();
            services.AddScoped<IDateTimeBroker, DateTimeBroker>();
            services.AddScoped<INavigationBroker, NavigationBroker>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentViewService, StudentViewService>();
        }
    }
}
