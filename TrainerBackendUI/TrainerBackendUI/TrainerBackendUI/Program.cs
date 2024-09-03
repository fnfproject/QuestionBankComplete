using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using QuestionBankUI.Trainer.Services;
using Syncfusion.Blazor;
using TrainerBackendUI;

namespace QuestionBankUI.Trainer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped<QuestionUIService>();
            builder.Services.AddScoped<ResultUIService>();
            builder.Services.AddScoped<PracticePaperUIService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5012/api/Trainer/") });

            await builder.Build().RunAsync();
        }
    }
}
