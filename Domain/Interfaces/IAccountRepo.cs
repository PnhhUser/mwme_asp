using System;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IAccountRepo
{
    Task<AccountEntity?> GetByName(string name);
}
