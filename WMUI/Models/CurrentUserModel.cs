using System.Security.Claims;

namespace WMUI.Models;

public class CurrentUserModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool Online { get; set; }

    public static CurrentUserModel FromClaims(ClaimsPrincipal user)
    {
        if (user == null) return new CurrentUserModel();

        return new CurrentUserModel
        {
            Id = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"),
            Name = user.Identity?.Name,
            Online = bool.Parse(user.FindFirst("online")!.Value)
        };
    }
}
