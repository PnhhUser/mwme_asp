using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Application.Models;
using Application.Services.Interface;
using Domain.Rules;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WMUI.Pages.Accounts
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;
        public EditModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        [Display(Name = "Ảnh cá nhân")]
        public IFormFile? Avatar { get; set; }

        [BindProperty]
        [Required]
        [MinLength(3)]
        public required string Username { get; set; }

        [BindProperty]
        [MinLength(6)]
        public string? Password { get; set; }

        [BindProperty]
        public bool IsActived { get; set; }

        public string? CurrentImageUrl { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var acc = await _accountService.FindById(Id);
            if (acc == null)
            {
                return NotFound();
            }

            // Gán giá trị cho form
            Username = acc.Name;
            IsActived = acc.IsActived;
            CurrentImageUrl = acc.ImageUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var model = new AccountModel();

                if (!string.IsNullOrWhiteSpace(Password))
                {
                    model.Password = Password;
                }

                if (Avatar != null)
                {
                    model.ImageUrl = await Utils.UploadFile.File(Avatar, "Avatars");
                }

                model.Name = Username;
                model.IsActived = IsActived;

                await _accountService.Update(Id, model);

                return RedirectToPage("/Accounts/Index");

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Thông báo lỗi: {e.Message}");
                return Page();
            }
        }

    }
}
