namespace Domain.Entities;

public class AccountEntity : BaseEntity
{
    public required string Name { get; set; }
    public required string PasswordHash { get; set; }
    public bool IsActived { get; set; }
    public bool IsOnline { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}
