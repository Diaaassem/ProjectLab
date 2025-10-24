using Microsoft.EntityFrameworkCore;
using ProjectLab.Data;
using ProjectLab.Filters;
using ProjectLab.MiddleWares;
using ProjectLab.Repos;
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

            // Register AppDbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
