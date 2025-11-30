using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeds;

public static class AccountSeed
{
    public static void AccountData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountEntity>()
        .HasData(
            new AccountEntity
            {
                Id = 1,
                Name = "Admin",
                PasswordHash = "$2a$12$I7rA/YxPCKJPhZ5LBdJvV.smGCgMjYlHHnmtRHzcA1ZFvvD0YuWsm"
            }
        );
    }
}