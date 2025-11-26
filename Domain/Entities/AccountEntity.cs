using System;

namespace Domain.Entities;

public class AccountEntity : BaseEntity
{
    public required string Name { get; set; }
    public required string PasswordHash { get; set; }
}
