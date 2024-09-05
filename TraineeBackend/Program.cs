using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TraineeBackendDll.Data;
using System.Collections.Generic;
using TraineeBackendDll.Dtos;

namespace TraineeBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddCors(setUp =>
            {
                setUp.AddPolicy("cors", setUp =>
                {
                    setUp.AllowAnyHeader();
                    setUp.AllowAnyMethod();
                    setUp.AllowAnyOrigin();
                });
            });

            builder.Services.AddDbContext<QuestionBankDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                             sqlOptions => sqlOptions.MigrationsAssembly("TrainerBackendDll")));

            // Add the dictionary as a singleton service
            builder.Services.AddSingleton<IDictionary<int, QuestionDto>>(provider => new Dictionary<int, QuestionDto>());

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

            app.UseCors("cors");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
