using Application.Services.Interface;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Rules;

namespace Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepo _accountRepo;

    public AccountService(IAccountRepo accountRepo)
    {
        _accountRepo = accountRepo;
    }

    public async Task<IEnumerable<AccountEntity>> FindAll()
    {
        return await _accountRepo.GetAllAsync();
    }

    public async Task<bool> ExistAsync(int id)
    {
        BaseRules.ThrowIfIdIsInvalid(id);

        return await _accountRepo.ExistsAsync(p => p.Id == id);
    }

    public async Task<AccountEntity?> FindByName(string name)
    {
        BaseRules.ThrowIfStringIsNullOrEmpty(name, nameof(name));

        return await _accountRepo.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<AccountEntity?> FindById(int id)
    {
        BaseRules.ThrowIfIdIsInvalid(id);

        return await _accountRepo.FirstOrDefaultAsync(p => p.Id == id);
    }
}