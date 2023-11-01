using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTrello.Data.Contexts;

namespace MyTrello.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var driver = configuration?.GetValue("Database:Driver", "inmemory")?.ToLower();
        switch(driver)
        {
            case "inmemory":
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("MyTrello"));
                break;
            case "sqlite":
                services.AddDbContext<ApplicationDbContext, SQLiteContext>();
                break;
            default:
                throw new ArgumentException("Invalid database driver: {0}", driver);
        }

        return services;
    }
}
