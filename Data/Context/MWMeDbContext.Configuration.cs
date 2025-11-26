using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace Data.Context;

public partial class MWMeDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            Env.Load();

            string server = Environment.GetEnvironmentVariable("DB_SERVER")!;
            string dbName = Environment.GetEnvironmentVariable("DB_NAME")!;
            string user = Environment.GetEnvironmentVariable("DB_USER")!;
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD")!;

            var connStr = $"Server={server};Database={dbName};User={user};Password={password};";
            var serverVersion = ServerVersion.AutoDetect(connStr);

            optionsBuilder.UseMySql(connStr, serverVersion);
        }
    }
}
