using Microsoft.EntityFrameworkCore;
using TraineeBackendDll.Interfaces;
using TraineeBackendDll.Services;
using TraineeBackendDll.Data;
using Microsoft.Extensions.Configuration;

namespace ShufflingTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure CORS
            builder.Services.AddCors(setUp =>
            {
                setUp.AddPolicy("cors", setUp =>
                {
                    setUp.AllowAnyHeader();
                    setUp.AllowAnyMethod();
                    setUp.AllowAnyOrigin();
                });
            });

            // Register ApplicationDbContext with the dependency injection container
            builder.Services.AddDbContext<QuestionBankDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly("TraineeBackendDll")));

            // Register services
            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<ITestService, TestService>();
            builder.Services.AddScoped<IResultService, ResultService>();
            builder.Services.AddScoped<TestServices>();
            builder.Services.AddScoped<UpdateService>();

            // Register HttpClient
            builder.Services.AddHttpClient();

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
