using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public partial class MWMeDbContext
{
    public required DbSet<AccountEntity> Accounts { get; set; }
}
