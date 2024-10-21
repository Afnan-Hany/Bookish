using System.Configuration;
using ApiConsume;
using Interface;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repo;
using YatApp.UI_PresentaionLayer.ApiConsume;

namespace YatApp.UI_presentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            var con = builder.Configuration.GetConnectionString("con");

            // Add services to the container.
            builder.Services.AddDbContext<BookishAppDbContext>(options =>
                options.UseSqlServer(con, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure()));
            builder.Services.AddControllersWithViews();

            //builder.Services.AddTransient<IApiCall, ApiCallRestSharp>();
            builder.Services.AddTransient<IApiCall, ApiCall>();
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
