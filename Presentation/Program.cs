using Application.Options;
using Application.Repositories;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Interface.Services;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace WertyEnglish
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(o =>
            {
                o.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
            });

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IPairWordRepository, PairWordRepository>();
            builder.Services.AddScoped<IDictionaryRepository, DictionaryRepository>();

            builder.Services.AddScoped<IPairWordService, PairWordService>();
            builder.Services.AddScoped<ITranslateService, TranslateService>();
            builder.Services.AddScoped<IDictionaryService, DictionaryService>();

            builder.Services.AddHttpClient<ITranslateService, TranslateService>();

            builder.Services.Configure<TranslateSetting>(
                builder.Configuration.GetSection("TranslateSetting"));

            builder.Services.Configure<AudioSettings>(
                builder.Configuration.GetSection("AudioSettings"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();
            }

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
