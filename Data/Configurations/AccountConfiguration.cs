using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
        .HasMaxLength(50);

        builder.Property(e => e.PasswordHash)
        .HasMaxLength(100);
    }
}
