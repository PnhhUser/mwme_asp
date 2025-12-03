using Domain.Entities;
using Domain.Exceptions;
using Domain.Rules;

namespace Application.Models;

public class AccountModel
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsActived { get; set; }
    public bool IsOnline { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
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
            IsOnline = entity.IsOnline,
            IsActived = entity.IsActived,
            ImageUrl = entity.ImageUrl,
            CreatedDate = entity.CreatedDate,
            UpdatedDate = entity.UpdatedDate
        };
    }

    public static AccountEntity ToEntity(AccountModel model, AccountEntity? acc)
    {
        if (model == null)
            throw new BadRequestException("Dữ liệu không hợp lệ.");

        var now = DateTime.UtcNow;

        if (acc == null)
        {
            return new AccountEntity
            {
                Name = model.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                CreatedDate = now,
                UpdatedDate = now,
                IsActived = model.IsActived,
                IsOnline = model.IsOnline,
                ImageUrl = model.ImageUrl
            };
        }

        acc.Name = model.Name;

        if (!string.IsNullOrWhiteSpace(model.Password))
        {
            acc.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
        }

        acc.UpdatedDate = now;
        acc.IsActived = model.IsActived;
        acc.IsOnline = model.IsOnline;
        acc.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl) ? acc.ImageUrl : model.ImageUrl;

        return acc;
    }


}