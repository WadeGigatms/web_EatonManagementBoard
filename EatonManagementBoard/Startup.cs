using EatonManagementBoard.Database;
using EatonManagementBoard.Database.Dapper;
using EatonManagementBoard.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace EatonManagementBoard
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

            services.AddControllersWithViews();

            // IHttpClientFactory
            services.AddHttpClient();

            // IMemoryCache
            services.AddMemoryCache();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            // Add services to scoped with database for Dapper
            services.AddScoped<EpcService>(serviceProvider =>
            {
                var msSqlConnection = new MsSqlConnectionRepository(Configuration.GetConnectionString("DefaultConnection"));
                var manager = new ConnectionRepositoryManager(msSqlConnection);
                var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
                var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                return new EpcService(manager, memoryCache, httpClientFactory);
            });
            services.AddScoped<EpcDataService>(serviceProvider =>
            {
                var msSqlConnection = new MsSqlConnectionRepository(Configuration.GetConnectionString("DefaultConnection"));
                var manager = new ConnectionRepositoryManager(msSqlConnection);
                return new EpcDataService(manager);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
