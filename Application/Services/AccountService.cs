using Application.Models;
using Application.Services.Interface;
using Domain.Entities;
using Domain.Exceptions;
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

    public async Task Create(AccountModel model)
    {
        var AccExist = await _accountRepo.FirstOrDefaultAsync(p => p.Name == model.Name);
        AccountRules.ThrowIfAccountExist(AccExist);

        await _accountRepo.AddAsync(AccountModel.ToEntity(model, null));
        await _accountRepo.SaveChangesAsync();
    }

    public async Task Update(int id, AccountModel model)
    {
        var acc = await _accountRepo.GetByIdAsync(id);

        AccountRules.ThrowIfAccountNotFound(acc);

        AccountModel.ToEntity(model, acc);

        await _accountRepo.SaveChangesAsync();
    }


    public async Task<bool> Delete(int id)
    {
        BaseRules.ThrowIfIdIsInvalid(id);

        var isDel = await _accountRepo.DeleteAsync(id);

        if (isDel)
        {
            await _accountRepo.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
