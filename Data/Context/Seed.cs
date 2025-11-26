using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public static class Seed
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        // Accounts
        modelBuilder.Entity<AccountEntity>()
        .HasData(
            new AccountEntity
            {
                Id = 1,
                Name = "Admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            }
        );
    }
}
