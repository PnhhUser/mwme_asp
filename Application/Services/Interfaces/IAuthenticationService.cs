
using Application.Models;

namespace Application.Services.Interface;

public interface IAuthenticationService
{
    Task<AccountModel?> Login(string username, string password);
    Task Logout(int id);
}