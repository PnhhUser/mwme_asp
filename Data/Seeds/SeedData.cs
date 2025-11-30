using Microsoft.EntityFrameworkCore;

namespace Data.Seeds;

public static class SeedData
{
    public static void ApplySeed(this ModelBuilder modelBuilder)
    {
        modelBuilder.AccountData();
    }
}
