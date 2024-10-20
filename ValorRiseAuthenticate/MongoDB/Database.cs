using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ValorRise;
using ValorRiseAuthenticate.MongoDB.Repositories;

namespace ValorRiseAuthenticate.MongoDB;

public class Database
{
    public static CharacterRepository CharacterRepository { get; set; }
    public static UserRepository UserRepository { get; set; }
    public static CharacterInfoRepository CharacterInfoRepository { get; set; }

    public static void Init()
    {
        var host = CreateHostBuilder().Build();

        CharacterRepository = host.Services.GetRequiredService<CharacterRepository>();
        UserRepository = host.Services.GetRequiredService<UserRepository>();
        CharacterInfoRepository = host.Services.GetRequiredService<CharacterInfoRepository>();

    }

    public static IHostBuilder CreateHostBuilder() =>
        Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<MongoDBSettings>(context.Configuration.GetSection("MongoDBSettings"));
                services.AddSingleton<MongoDBContext>();
                services.AddScoped<CharacterRepository>();
                services.AddScoped<UserRepository>();
                services.AddScoped<CharacterInfoRepository>();
            });
}
