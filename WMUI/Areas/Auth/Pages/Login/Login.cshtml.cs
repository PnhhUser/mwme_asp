using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WMUI.Areas.Auth.Pages.Login;

[AllowAnonymous]
public class LoginModel : PageModel
{

    public LoginModel(string? returnUrl = null)
    {

    }

    [BindProperty]
    public required string Username { get; set; }

    [BindProperty]
    public required string Password { get; set; }


    public string ReturnUrl { get; set; } = string.Empty;

    public IActionResult OnGet(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }

        ReturnUrl = returnUrl ?? Url.Content("~/");
        return Page();
    }


    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            return Page();
        }

        if (Username == "admin" && Password == "password")
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Username),
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Đăng nhập user
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = false, // Remember me?
                    ExpiresUtc = DateTime.UtcNow.AddHours(1)
                });

            // Redirect về trang gốc user muốn truy cập
            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }

        ReturnUrl = returnUrl ?? Url.Content("~/");
        return Page();
    }
}