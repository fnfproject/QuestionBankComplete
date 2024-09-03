
using QuestionBankApi.Trainer.Services;
using QuestionBankAPI.Trainer.Services;
using QuestionBankDll.Trainer.Models;
using QuestionBankDll.Trainer.Services;

namespace QuestionBankApi.Trainer
{
    public class Program
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuestionDbContext>();
            services.AddTransient<IQuestionDataService, QuestionDataService>();
            services.AddTransient<IResultDataService, ResultDataService>();
            services.AddTransient<IPracticePaperDataService, PracticePaperDataService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IResultService, ResultService>();
            services.AddTransient<IPracticePaperService, PracticePaperService>();
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services);


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("cors", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseCors("cors");


            app.MapControllers();

            app.Run();
        }
    }
}
