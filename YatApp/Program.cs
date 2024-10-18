
using Interface;
using Library.Data;
using Microsoft.EntityFrameworkCore;
using Repo;

namespace YatApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var con = builder.Configuration.GetConnectionString("con");
            // Add services to the container.
            builder.Services.AddDbContext<BookishAppDbContext>(options => options.UseSqlServer(con));
            //builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddTransient<IUnitofWork, UnitOfWork>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
