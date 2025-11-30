using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DotNetEnv;

namespace Data.Context;

public class MWMeDbContextFactory : IDesignTimeDbContextFactory<MWMeDbContext>
{
    public MWMeDbContext CreateDbContext(string[] args)
    {
        Env.Load();

        string server = Environment.GetEnvironmentVariable("DB_SERVER")!;
        string dbName = Environment.GetEnvironmentVariable("DB_NAME")!;
        string user = Environment.GetEnvironmentVariable("DB_USER")!;
        string password = Environment.GetEnvironmentVariable("DB_PASSWORD")!;

        if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(dbName) ||
    string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
        {
            throw new InvalidOperationException("Missing required environment variables. Check your .env file");
        }

        var connStr = $"Server={server};Database={dbName};User={user};Password={password};";
        var serverVersion = ServerVersion.AutoDetect(connStr);

        var optionsBuilder = new DbContextOptionsBuilder<MWMeDbContext>();
        optionsBuilder.UseMySql(connStr, serverVersion);

        return new MWMeDbContext(optionsBuilder.Options);
    }
}