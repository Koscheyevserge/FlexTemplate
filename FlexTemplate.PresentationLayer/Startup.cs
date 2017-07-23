using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.BlobAccessLayer;
using FlexTemplate.DataAccessLayer;
using FlexTemplate.DataAccessLayer.Entities;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FlexTemplate.PresentationLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using React.AspNet;

namespace FlexTemplate.PresentationLayer
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; set; }
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                //builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsEnvironment("Development"))
            {
                services.AddEntityFrameworkSqlServer()
                    .AddDbContext<FlexTemplateContext>(options => options
                        .UseSqlServer(Configuration.GetConnectionString("DatabaseDevelopment")));
                services.Configure<StorageAccountOptions>(Configuration.GetSection("TestStorageAccount"));
            }
            if (Environment.IsEnvironment("Test"))
            {
                services.AddEntityFrameworkSqlServer()
                    .AddDbContext<FlexTemplateContext>(options => options
                        .UseSqlServer(Configuration.GetConnectionString("DatabaseTest")));
                services.Configure<StorageAccountOptions>(Configuration.GetSection("TestStorageAccount"));
            }
            if (Environment.IsEnvironment("Production"))
            {
                services.AddEntityFrameworkSqlServer()
                    .AddDbContext<FlexTemplateContext>(options => options
                        .UseSqlServer(Configuration.GetConnectionString("DatabaseProduction")));
                services.Configure<StorageAccountOptions>(Configuration.GetSection("StorageAccount"));
            }
            services.AddIdentity<User, IdentityRole>(o => 
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                o.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                o.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";
            })
            .AddEntityFrameworkStores<FlexTemplateContext>()
            .AddDefaultTokenProviders();
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            services.AddMvc().AddRazorOptions(razorOptions =>
            {
                razorOptions.ViewLocationExpanders.Add(new FlexViewLocator());
            });
            services.AddReact();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<BusinessLogicLayer.Services.ControllerServices>();
            services.AddTransient<BusinessLogicLayer.Services.ComponentsServices>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<DataAccessLayer.Services.Services>();
            services.AddTransient<BlobAccessLayer.Services.ImagesServices>();
            services.AddScoped<FlexContext, FlexTemplateContext>();
            services.AddMemoryCache();
        }
        
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (Environment.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            if (Environment.IsEnvironment("Test"))
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            if (Environment.IsEnvironment("Production"))
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseReact(config => { });

            app.UseStaticFiles();
            
            app.UseMiddleware<FlexContextInitializer>();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
