using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectLab.Cookies;
using ProjectLab.Data;
using ProjectLab.Filters;
using ProjectLab.MiddleWares;
using ProjectLab.Repos;
using ProjectLab.Services;
using Serilog;

namespace ProjectLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog with writing logs to a file and console
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            // Add data protection (used to protect cookie payloads)
            builder.Services.AddDataProtection();

            // Provide IHttpContextAccessor so services can access HttpContext
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Register a cookie helper service
            builder.Services.AddScoped<ICookieService, CookieService>();

            // Register AppDbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=.\\SQLEXPRESS;Database=MvcLab;Trusted_Connection=True;TrustServerCertificate=True"));

            // Configure cookie policy defaults (HttpOnly, SameSite, Secure policy)
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
                options.Secure = CookieSecurePolicy.SameAsRequest;
            });

            // Add authentication services
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;
                });

            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddMemoryCache();

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseExceptionHandlingMiddleware();

            //app.UseLoggingMiddleware();

            app.UseStaticFiles();

            app.UseRouting();

            // enforce cookie policy before authentication
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "std",
                pattern: "/std",
                defaults: new { controller = "Student", action = "getAll" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
