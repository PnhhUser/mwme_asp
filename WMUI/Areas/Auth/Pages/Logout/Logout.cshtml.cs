using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WMUI.Areas.Auth.Pages.Logout
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        private readonly Application.Services.Interface.IAuthenticationService _authenticationService;

        public LogoutModel(Application.Services.Interface.IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public async Task<IActionResult> OnGet()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _authenticationService.Logout(id);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/login");
        }
    }
}
