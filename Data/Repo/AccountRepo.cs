using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repo;

public class AccountRepo : BaseRepo<AccountEntity>, IAccountRepo
{
    public AccountRepo(MWMeDbContext context) : base(context) { }
}
