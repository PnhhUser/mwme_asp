using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WMUI.Areas.Auth.Pages.Login
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly Application.Services.Interface.IAuthenticationService _authenticationService;

        public LoginModel(
            Application.Services.Interface.IAuthenticationService authenticationService
            )
        {
            _authenticationService = authenticationService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập tên người dùng")]
        [MinLength(3, ErrorMessage = "Tên người dùng ít nhất 3 ký tự")]
        public required string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên")]
        public required string Password { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return Redirect("/");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var acc = await _authenticationService.Login(Username, Password);

            if (acc != null)
            {
                if (acc.IsActived == false)
                {
                    ModelState.AddModelError("", "Tài khoản này đã bị khóa.");
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, acc.Id.ToString()!),
                    new Claim(ClaimTypes.Name, acc.Name),
                    new Claim("IsOnline", acc.IsOnline.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Redirect("/");
            }

            ModelState.AddModelError("", "Sai username hoặc password.");
            return Page();
        }
    }

}