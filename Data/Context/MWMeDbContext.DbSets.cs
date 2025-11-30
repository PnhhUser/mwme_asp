using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public partial class MWMeDbContext
{
    public DbSet<AccountEntity> Accounts { get; set; } = null!;
}
