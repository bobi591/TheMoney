using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Okta.AspNetCore;
using TheMoney.Datastore.Databases.MongoDB.Application;
using TheMoney.Shared.Entities;
using TheMoney.Shared.UXServices;
using TheMoney.Shared.UXServices.Messages;

namespace TheMoney
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

            //Configure the database IRepository singleton
            ConfigureRepositoryService(ref services);

            //Configure the OAuth services
            ConfigureOAuthService(ref services);

            //Configure the Frontend Translation Service
            ConfigureTranslationService(ref services);

            //Adds custom paths for shared or module specific views
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("/Modules/{1}/Views/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Shared/Views/{0}" + RazorViewEngine.ViewExtension);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                HttpOnly = HttpOnlyPolicy.None,
                MinimumSameSitePolicy = SameSiteMode.None,
                Secure = CookieSecurePolicy.Always
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        //This method is used to configure OAuth 2.0
        private void ConfigureOAuthService(ref IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/User/SignIn");
            })
            .AddOktaMvc(new OktaMvcOptions
            {
                OktaDomain = Configuration.GetValue<string>("Okta:OktaDomain"),
                ClientId = Configuration.GetValue<string>("Okta:ClientId"),
                ClientSecret = Configuration.GetValue<string>("Okta:ClientSecret"),
                CallbackPath = "/Home",
                Scope = new List<string> { "openid", "profile", "email" },
            });
        }

        //This method is used to configure the IRepository dependency injection, based on the DatabaseProvider setting in the appsettings.json
        private void ConfigureRepositoryService(ref IServiceCollection services)
        {
            IConfigurationSection databaseProviderSetting = Configuration.GetChildren().Where(x => x.Key == "DatabaseProvider").FirstOrDefault();

            if(databaseProviderSetting != null)
            {
                if(databaseProviderSetting.Value == "mongodb")
                {
                    //Retrieve the Mongo database settings
                    MongoDBSettings currentMongoDbSettings = new MongoDBSettings();
                    Configuration.Bind("MongoDBSettings", currentMongoDbSettings);
                    services.AddSingleton<IRepository>(new MongoDBRepository(currentMongoDbSettings));
                }
            }
        }

        //This method is used to configure the frontend alert service
        private void ConfigureTranslationService(ref IServiceCollection services)
        {
            string language = "bg";
            ResourceManager translationsResourceManager = new ResourceManager(language + "-messages", Assembly.GetEntryAssembly());

            IMessagesResource messagesResource = new MessagesResource(translationsResourceManager);

            services.AddSingleton<ITranslationService>(new TranslationService(messagesResource));
        }
    }
}
