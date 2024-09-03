using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestionBankApi.Admin.Interfaces;
using QuestionBankApi.Admin.Services;
using QuestionBankApi.Trainer.Services;
using System.Text;
using AdminMVC.Models;
using AdminMVC.Service;
using UserService = AdminMVC.Service.UserService;

namespace AdminMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();

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
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register JwtService
            builder.Services.AddSingleton<JwtService>(sp =>
                new JwtService(builder.Configuration));

            // Register TwoFactorAuthService
            builder.Services.AddSingleton<TwoFactorAuthService>();

            builder.Services.AddScoped<IUserService, UserService>();



            // Add JWT authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
