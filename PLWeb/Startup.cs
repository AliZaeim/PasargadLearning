using System;
using System.IO;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PLCore.Services;
using PLCore.Services.Interfaces;
using PLDataLayer.Context;
using WebMarkupMin.AspNetCore3;

namespace PLWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public IWebHostEnvironment WebHostEnvironment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebMarkupMin(options =>
            {
                options.AllowCompressionInDevelopmentEnvironment = true;
                options.AllowMinificationInDevelopmentEnvironment = true;
            })
            .AddHtmlMinification()
            .AddHttpCompression();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<CookiePolicyOptions>(options =>
            {

                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
            });

            services.AddElmah(options =>
            {
                // دسترسی پیدا کنید elamh مسیری که توسط آن میتوانید به  
                // می باشد ~/elmah این مسیر به صورت پیشفرض     
                options.Path = @"PLElmah";
                options.CheckPermissionAction = context => context.User.Identity.IsAuthenticated;
                // به گونه ای که ما آن را پیاده سازی می کنیم elmah محدود کردن دسترسی به 
                options.CheckPermissionAction = CheckPermissionAction;
            });
            
            #region Database Context
            services.AddDbContext<PLContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PLConnection"));
            }
            );
            #endregion Database Context
            //#region IoC
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITrainingService, TrainingService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<ISubEntityService, SubEntityService>();
            services.AddTransient<ISkyService, SkyService>();

            //#endregion
            #region Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Login/S";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);//Minute

            });
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(10);

            });
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.SlidingExpiration = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

            //});
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //    options.Cookie.HttpOnly = true;
            //});
            #endregion
            #region tempkey
            var keysFolder = Path.Combine(WebHostEnvironment.ContentRootPath, "wwwroot/temp-keys");
            services.AddDataProtection()
           .SetApplicationName("PLWeb")
           .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
           .SetDefaultKeyLifetime(TimeSpan.FromDays(180));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseCookiePolicy();
            app.UseElmah();
            //app.UseSession();
            app.UseWebMarkupMin();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "Admin",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!").ConfigureAwait(true);
                });

            });

        }
        private bool CheckPermissionAction(HttpContext httpContext)
        {
            // می باشد؟ elamh کاربری جاری سیستم دارای نقش ادمین برای دسترسی به 
            if (httpContext.User.Identity.IsAuthenticated)
            {
                int roleId = int.Parse(httpContext.User.FindFirst("RoleId").Value);
                return (httpContext.User.Identity.IsAuthenticated && roleId == 1);
            }
            return false;

            // در این قسمت ما تنها برای نمایش آزمایشی میگوییم که دسترسی دارند
            //return true;
        }
    }
}
