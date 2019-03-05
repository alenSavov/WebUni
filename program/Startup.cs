using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using SchoolDiary.Data;
using System;
using AutoMapper;
using SchoolDiary.Web.Middleware.MiddlewareExtentions;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services;
using SchoolDiary.Web.Models.Account;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using SchoolDiary.ServicesViewModels.Courses;
using SchoolDiary.Services.Administration;
using SchoolDiary.Services.Administration.Models;
using SchoolDiary.Services.Articles.Models;
using SchoolDiary.Services.Events.Implementation;
using SchoolDiary.Services.Resourses.Implementation;
using SchoolDiary.Services.Comments.Implementation;
using SchoolDiary.Services.CloudinaryService;
using SchoolDiary.Services.Categories.Models;

namespace program
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
            AutoMapperConfig.RegisterMappings(
              typeof(RegisterInputViewModel).Assembly,
              typeof(CategoryViewModel).Assembly,
              typeof(CreateCourseInputViewModel).Assembly,
              typeof(AllUsersAdministrationServiceViewModel).Assembly,
              typeof(CreateArticleInputViewModel).Assembly
          );

            services.AddIdentity<User, IdentityRole>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<SchoolDbContext>();


            services.AddDbContext<SchoolDbContext>(options =>
                  options.UseSqlServer(
                      this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequiredLength = 4,
                    RequiredUniqueChars = 1,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };
            });


            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = "279043736149839";
                    facebookOptions.AppSecret = "a62e97af51facf7602f574111e6c74dd";
                });


            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = $"/Account/Login";
                option.LogoutPath = $"/Account/AccessDenided";
            });

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IArticleService, ArticlesService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<IResourcesService, ResourcesService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);            

            services.AddMvc(options =>
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseSeedDataMiddleware();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseSeedDataMiddleware();
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCookiePolicy();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "areas",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                     name: "default",
                     template: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
