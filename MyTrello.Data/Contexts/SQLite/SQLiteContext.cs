using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace MyTrello.Data.Contexts;

public partial class SQLiteContext : ApplicationDbContext
{
    public readonly IConfiguration _configuration;

    public SQLiteContext(IConfiguration configuration) : base()
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("SQLite"));
        }
    }
}
