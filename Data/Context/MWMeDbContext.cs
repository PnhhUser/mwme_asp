using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public partial class MWMeDbContext : DbContext
{
    public MWMeDbContext(DbContextOptions<MWMeDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MWMeDbContext).Assembly);

        modelBuilder.SeedData();
    }
}
