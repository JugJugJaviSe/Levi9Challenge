
using Levi9Challenge.Repositories.Implementations;
using Levi9Challenge.Repositories.Interfaces;
using Levi9Challenge.Services.Implementations;
using Levi9Challenge.Services.Interfaces;

namespace Levi9Challenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
            builder.Services.AddSingleton<ICanteenRepository, CanteenRepository>();

            builder.Services.AddSingleton<ICanteenService, CanteenService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
