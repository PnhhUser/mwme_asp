using Application.Models;
using Domain.Entities;

namespace Application.Services.Interface;

public interface IAccountService
{
    Task<bool> ExistAsync(int id);
    Task<AccountEntity?> FindByName(string name);
    Task<AccountEntity?> FindById(int id);
    Task<IEnumerable<AccountEntity>> FindAll();
    Task Create(AccountModel model);
    Task Update(AccountModel model);
    Task Delete(int id);
}