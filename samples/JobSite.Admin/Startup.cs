using Finbuckle.MultiTenant;
using JobSite.Core.Data;
using JobSite.Core.Data.Configuration;
using JobSite.Core.Data.MainContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;

namespace JobSite.Admin
{
    public class Startup
    {
        public static string ConnectionString { get; private set; }
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SiteContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), opt =>
               {
                   opt.MigrationsAssembly("JobSite.Core");
               }));

            services.AddMvc();
            services.AddDbContext<SiteContext>();

            services.AddDbContext<JobContext>();
            services.AddIdentity<MultiTenantIdentityUser, MultiTenantIdentityRole>()
                    .AddDefaultTokenProviders()
                    //.AddDefaultUI(UIFramework.Bootstrap4)
                    .AddEntityFrameworkStores<JobContext>();

            //force lowercase urs
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<SiteContext>();
            context.Database.Migrate();
            //var manager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
            //var superAdminUser = Configuration.GetSection("SuperAdminUser").Get<SuperAdminUser>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //custom error page
            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseStaticFiles();
    
            app.UseRequestLocalization(BuildLocalizationOptions());
            //app.UseSession();
            //app.UseCookiePolicy();
            //app.UseAuthentication();
            //app.UseMultiTenant();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private RequestLocalizationOptions BuildLocalizationOptions()
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-GB")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-GB"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };


            return options;
        }

        //private void SetupDb()
        //{
        //    ti = new TenantInfo("megacorp", null, null, "Data Source=Data/MegaCorp_ToDoList.db", null);
        //    using (var db = new ToDoDbContext(ti))
        //    {
        //        db.Database.EnsureCreated();
        //        db.ToDoItems.Add(new ToDoItem { Title = "Send Invoices", Completed = true });
        //        db.ToDoItems.Add(new ToDoItem { Title = "Construct Additional Pylons", Completed = true });
        //        db.ToDoItems.Add(new ToDoItem { Title = "Call Insurance Company", Completed = false });
        //        db.SaveChanges();
        //    }
          
        //}
    }
}
