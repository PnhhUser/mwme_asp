using Domain.Entities;
using Domain.Exceptions;
using Domain.Rules;

namespace Application.Models;

public class AccountModel
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public static AccountModel ToModel(AccountEntity entity)
    {
        AccountRules.ThrowIfAccountNotFound(entity);

        return new AccountModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Password = entity.PasswordHash,
            CreatedDate = entity.CreatedDate,
            UpdatedDate = entity.UpdatedDate
        };
    }

    public static AccountEntity ToEntity(AccountModel model, AccountEntity? acc)
    {
        if (model == null)
        {
            throw new BadRequestException("Dữ liệu không hợp lệ.");
        }

        var now = DateTime.UtcNow;

        return new AccountEntity
        {
            Id = model.Id ?? 0,
            Name = model.Name,
            PasswordHash = string.IsNullOrWhiteSpace(model.Password) && acc != null
                ? acc.PasswordHash
                : BCrypt.Net.BCrypt.HashPassword(model.Password),
            CreatedDate = acc != null ? acc.CreatedDate : now,
            UpdatedDate = now
        };
    }

}