using Application.Services.Interface;
using Domain.Entities;
using Domain.Interfaces;

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
        if (id <= 0)
        {
            throw new ArgumentException("ID phải lớn hơn 0", nameof(id));
        }

        try
        {
            var isExist = await _accountRepo.ExistsAsync(p => p.Id == id);

            return isExist;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Lỗi khi kiểm tra tồn tại Account với ID = {id}", e);
        }
    }

    public async Task<AccountEntity?> FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tên không được để trống", nameof(name));
        }

        try
        {
            var account = await _accountRepo.FirstOrDefaultAsync(p => p.Name == name);
            return account;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Lỗi khi tìm Account với tên = {name}", e);
        }
    }

    public async Task<AccountEntity?> FindById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID phải lớn hơn 0", nameof(id));
        }

        try
        {
            var account = await _accountRepo.FirstOrDefaultAsync(p => p.Id == id);
            return account;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Lỗi khi tìm Account với tên = {id}", e);
        }
    }
}