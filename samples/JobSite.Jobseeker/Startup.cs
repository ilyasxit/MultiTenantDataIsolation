using Finbuckle.MultiTenant;
using JobSite.Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace JobSite.Jobseeker
{
    public class Startup
    {
        //public static string ConnectionString { get; private set; }
        public IConfiguration _configuration { get; }
        public IConfigurationRoot Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            _configuration = configuration;
        }
      
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMultiTenant().
                     WithInMemoryStore(Configuration.GetSection("Finbuckle:MultiTenant:InMemoryStore")).
                     WithHostStrategy();

            // Register the db context, but do not specify a provider/connection string since
            // these vary by tenant.

            services.AddDbContext<JobContext>();

            services.AddIdentity<MultiTenantIdentityUser, MultiTenantIdentityRole>()
                    .AddDefaultTokenProviders()
                    //.AddDefaultUI(UIFramework.Bootstrap4)
                    .AddEntityFrameworkStores<JobContext>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequiredLength = 5;
            //    options.SignIn.RequireConfirmedEmail = true;
            //});

            services.ConfigureApplicationCookie(options =>
            {
                //options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            //force lowercase urs
            services.AddRouting(options => options.LowercaseUrls = true);

            //services.AddMvc(config =>
            //{
            //    // Enforce required authentication 
            //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMultiTenant();
            app.UseMvc(routes => routes.MapRoute("Defaut", "{controller=Tenant}/{action=Index}"));
        }
    }

}
