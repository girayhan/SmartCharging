using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Configuration;
using SmartCharging.DataAccess.Database;
using System.Reflection;

namespace SmartCharging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = CreateWebApplication(args);

            app.Run();
        }

        public static WebApplication CreateWebApplication(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());
            builder.Services.AddDependencies();
            builder.Services.AddDbContext<SmartChargingDbContext>(options => options.UseInMemoryDatabase("items"));

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

            return app;
        }
    }
}