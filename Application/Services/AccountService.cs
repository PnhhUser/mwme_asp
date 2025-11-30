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
        BaseRules.ThrowIfStringIsNullOrEmpty(model.Name, nameof(model.Name));
        BaseRules.ThrowIfStringIsNullOrEmpty(model.Password, nameof(model.Password));

        var existName = await _accountRepo.FirstOrDefaultAsync(p => p.Name == model.Name);
        AccountRules.ThrowIfAccountExist(existName);

        var entity = AccountModel.ToEntity(model, null);

        await _accountRepo.AddAsync(entity);
        await _accountRepo.SaveChangesAsync();
    }

    public async Task Update(AccountModel model)
    {
        BaseRules.ThrowIfIdIsInvalid(model.Id ?? 0);
        BaseRules.ThrowIfStringIsNullOrEmpty(model.Name, nameof(model.Name));

        var existingEntity = await _accountRepo.GetByIdAsync(model.Id!.Value);
        AccountRules.ThrowIfAccountNotFound(existingEntity);

        var existName = await _accountRepo.FirstOrDefaultAsync(p => p.Name == model.Name && p.Id != model.Id);

        AccountRules.ThrowIfAccountExist(existName);


        var entity = AccountModel.ToEntity(model, existingEntity);

        _accountRepo.Update(entity);
        await _accountRepo.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        BaseRules.ThrowIfIdIsInvalid(id);

        var existingEntity = await _accountRepo.GetByIdAsync(id);

        AccountRules.ThrowIfAccountNotFound(existingEntity);

        await _accountRepo.DeleteAsync(existingEntity!.Id);
        await _accountRepo.SaveChangesAsync();
    }
}
