using Application.Options;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Interface.Services;
using Repositories;

namespace WertyEnglish
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<IPairWordRepository, InMemoryPairWordRepository>();

            builder.Services.AddSingleton<IPairWordService, PairWordService>();
            builder.Services.AddSingleton<ITranslateService, TranslateService>();

            builder.Services.Configure<TranslateSetting>(
                builder.Configuration.GetSection("TranslateSetting"));

            var app = builder.Build();

            app.MapStaticAssets();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
