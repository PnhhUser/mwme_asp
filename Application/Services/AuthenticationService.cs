using Application.Models;
using Application.Services.Interface;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Rules;

namespace Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAccountRepo _accountRepo;

    public AuthenticationService(IAccountRepo accountRepo)
    {
        _accountRepo = accountRepo;
    }

    public async Task<AccountModel?> Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        var acc = await _accountRepo.FirstOrDefaultAsync(p => p.Name == username);

        if (acc == null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(password, acc.PasswordHash))
        {
            return null;
        }

        acc.IsOnline = true;
        var model = AccountModel.ToModel(acc);

        _accountRepo.Update(acc);
        await _accountRepo.SaveChangesAsync();

        return model;
    }

    public async Task Logout(int id)
    {
        BaseRules.ThrowIfIdIsInvalid(id);

        var acc = await _accountRepo.FirstOrDefaultAsync(p => p.Id == id);

        AccountRules.ThrowIfAccountNotFound(acc);

        acc!.IsOnline = false;

        _accountRepo.Update(acc);
        await _accountRepo.SaveChangesAsync();
    }
}